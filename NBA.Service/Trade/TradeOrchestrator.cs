using ApplicationDefaults.Exceptions;
using ApplicationDefaults.Options;
using Microsoft.Extensions.Options;
using NBA.Data.Redis.Entities;
using TradeData = NBA.Data.Entities.Trade;

namespace NBA.Service.Trade
{
    // The trade transaction, in one place: validate, persist the durable row, cache the hot copy, and
    // say what the league should be told. Previously this lived in TradeHub method bodies, which made
    // the rules reachable only through a SignalR connection — and therefore only testable with a live
    // hub, a Redis container and a database.
    //
    // The split with its two collaborators follows rule 4: TradeService owns the Postgres rows,
    // TradeManager owns the Redis copies, and coordinating the two is this type's whole job.
    public class TradeOrchestrator(
        TradeService tradeService,
        TradeManager tradeManager,
        IOptions<ApplicationOptions> applicationOptions) : ITradeOrchestrator
    {
        private readonly TradeService _tradeService = tradeService;
        private readonly TradeManager _tradeManager = tradeManager;
        private readonly ApplicationOptions _applicationOptions = applicationOptions.Value;

        // Validates against the rosters, records the proposal durably, caches it for the live window.
        // Persist before caching: Redis has no persistence configured, so the row is the only copy that
        // survives a restart, and a failed cache write must never lose the offer.
        public async Task<TradeOutcome<TradeData>> ProposeAsync(
            long leagueId, long fromTeam, long toTeam, List<long> playersIds)
        {
            if (playersIds is null || playersIds.Count == 0)
                throw new NBAException("Missing value for playersIds", ErrorCodes.MissingValue);

            var proposal = new TradeBetweenTeams
            {
                TradeId = Guid.NewGuid(),
                FromTeam = fromTeam,
                ToTeam = toTeam,
                PlayersIds = playersIds,
            };

            await _tradeService.ValidateSeasonTrade(leagueId, proposal);

            var ttl = TimeSpan.FromMinutes(_applicationOptions.ProposedTradeTtlMinutes);

            var (created, superseded) = await _tradeService.AddProposedTrade(
                leagueId, proposal, DateTime.UtcNow.Add(ttl));

            await _tradeManager.ProposeSeasonTrade(leagueId, proposal);

            var events = new List<TradeEvent>();

            // A team holds only one standing offer to any given team, so this proposal displaced its own
            // predecessor. Clear the hot copies and announce it, or the recipient keeps the dead offer in
            // its backlog and every board still shows it as open.
            //
            // Announced BEFORE the new proposal: see the ordering note on TradeOutcome.
            foreach (var row in superseded)
            {
                await _tradeManager.RemoveProposedSeasonTrade(leagueId, row.Toteamid, row.Tradeid);
                events.Add(new TradeEvent.Superseded(ToSettled(row)));
            }

            events.Add(new TradeEvent.OfferedToLeague(proposal));

            return new TradeOutcome<TradeData>(created, events);
        }

        // Re-validates against current rosters, swaps the teamplayer rows, marks the row accepted, then
        // clears the Redis copy so a settled trade cannot reappear in anyone's backlog.
        public async Task<TradeOutcome<TradeData>> AcceptAsync(long leagueId, Guid tradeId)
        {
            var accepted = await _tradeService.AcceptProposal(leagueId, tradeId);

            await _tradeManager.RemoveProposedSeasonTrade(leagueId, accepted.Toteamid, tradeId);

            return new TradeOutcome<TradeData>(accepted, [new TradeEvent.Accepted(ToSettled(accepted))]);
        }

        // Closes a standing offer without executing it. Two callers: a manager declining outright, and
        // the counter-offer flow, which proposes its own trade first and then retires the offer it is
        // answering — in that order, so a validation failure on the counter leaves the original open
        // rather than killing an offer and putting nothing in its place.
        public async Task<TradeOutcome<TradeData>> RejectAsync(long leagueId, Guid tradeId)
        {
            var rejected = await _tradeService.RejectProposal(leagueId, tradeId);

            // Clear the hot copy as well, or the offer would be handed back to the recipient as part of
            // its backlog on the next connect even though the row is settled.
            await _tradeManager.RemoveProposedSeasonTrade(leagueId, rejected.Toteamid, tradeId);

            return new TradeOutcome<TradeData>(rejected, [new TradeEvent.Rejected(ToSettled(rejected))]);
        }

        // Everything already waiting for a team when it connects. Proposals sent while the manager was
        // offline only survive in Postgres — the Redis copies expire after ProposedTradeTtlMinutes — so
        // this tries the hot copies first and falls back to the durable rows.
        public async Task<List<TradeBetweenTeams>> GetBacklogAsync(long leagueId, long teamId)
        {
            var trades = await _tradeManager.GetProposedSeasonTrades(leagueId, teamId);

            if (trades.Count > 0) return trades;

            trades = await _tradeService.GetPendingProposals(leagueId, teamId);

            // Warm the cache so the next connect skips Postgres. ProposeSeasonTrade is reused purely for
            // its "write with the configured TTL" behaviour — nothing is newly proposed here. Note this
            // re-arms the full TTL, so an actively reconnecting client keeps its cached copies alive
            // indefinitely.
            foreach (var trade in trades)
                await _tradeManager.ProposeSeasonTrade(leagueId, trade);

            return trades;
        }

        // The settled-trade payload. Clients key every event on TradeId, so what the other fields carry
        // is context for rendering, not identity.
        //
        // Lives here rather than in NBA.Api/Mappings: rule 5's mappers turn entities into response DTOs,
        // and this turns an entity into the Redis/SignalR shape, which is the same layer the events
        // themselves belong to.
        private static TradeBetweenTeams ToSettled(TradeData row) => new()
        {
            TradeId = row.Tradeid,
            FromTeam = row.Fromteamid,
            ToTeam = row.Toteamid,
            PlayersIds = row.Playerids ?? [],
        };
    }
}
