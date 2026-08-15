using ApplicationDefaults.Exceptions;
using ApplicationDefaults.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using NBA.Api.DTOs;
using NBA.Api.Mappings;
using NBA.Api.SignalR.Clients;
using NBA.Data.Redis.Entities;
using NBA.Service.League.Draft;
using NBA.Service.League.Trade;
using NBA.Service.Player;

namespace NBA.Api.SignalR.Hubs
{
    [Authorize]
    public class TradeHub(TradeManager tradeManager, TradeService tradeService, DraftManager draftManager,
        PlayerManager playerManager, IHubContext<DraftHub, IDraftHubClient> draftHub,
        IOptions<ApplicationOptions> applicationOptions, ILogger<TradeHub> logger) : Hub<ITradeHubClient>
    {
        private readonly TradeManager _tradeManager = tradeManager;
        private readonly TradeService _tradeService = tradeService;
        private readonly DraftManager _draftManager = draftManager;
        private readonly PlayerManager _playerManager = playerManager;
        private readonly IHubContext<DraftHub, IDraftHubClient> _draftHub = draftHub;
        private readonly ApplicationOptions _applicationOptions = applicationOptions.Value;
        private readonly ILogger<TradeHub> _logger = logger;

        // The client opens the connection; here we subscribe it to the groups that trade
        // requests are routed to — the league group and the connecting team's group — and hand it any
        // offer that is already waiting for it.
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();

            // Both are parsed up front because the rehydrate below is keyed on the pair; the group
            // joins themselves still happen independently, as before.
            var hasLeagueId = long.TryParse(httpContext?.Request.Query["leagueId"], out long leagueId);
            var hasTeamId = long.TryParse(httpContext?.Request.Query["teamId"], out long teamId);

            if (hasLeagueId)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"league:trade:{leagueId}");
            }

            if (hasTeamId)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"team:trade:{teamId}");
            }

            if (hasLeagueId && hasTeamId)
            {
                await SendPendingProposals(leagueId, teamId);
            }

            await base.OnConnectedAsync();
        }

        // Proposals sent while this manager was offline only survive in Postgres — the Redis copies
        // expire after ProposedTradeTtlMinutes. So on connect: try the hot copies, fall back to the
        // durable rows, and hand the client its whole backlog in one message. A team can be holding
        // offers from several other teams at once, which is why this is a list.
        private async Task SendPendingProposals(long leagueId, long teamId)
        {
            try
            {
                var trades = await _tradeManager.GetProposedSeasonTrades(leagueId, teamId);

                if (trades.Count == 0)
                {
                    trades = await _tradeService.GetPendingProposals(leagueId, teamId);

                    // Warm the cache so the next connect skips Postgres. ProposeSeasonTrade is reused
                    // purely for its "write with the configured TTL" behaviour — nothing is newly
                    // proposed here. Note this re-arms the full TTL, so an actively reconnecting client
                    // keeps its cached copies alive indefinitely.
                    foreach (var trade in trades)
                        await _tradeManager.ProposeSeasonTrade(leagueId, trade);
                }

                if (trades.Count > 0)
                    await Clients.Caller.ReceiveTradeRequests(trades);
            }
            catch (Exception ex)
            {
                // Throwing out of OnConnectedAsync aborts the connection. Failing to rehydrate must
                // degrade to "connected but did not receive the pending offers", never to "could not
                // open the trade screen".
                _logger.LogError(ex, "Failed to deliver pending proposals for league {LeagueId}, team {TeamId}",
                    leagueId, teamId);
            }
        }

        // Action called by a team to propose a trade: persist it, then route the request to the
        // league group and the targeted team's group — the same keys OnConnectedAsync subscribes to.
        public async Task ProposeTrade(long leagueId, long fromTeam, long toTeam, List<long> playersIds)
        {
            var trade = new TradeBetweenTeams
            {
                TradeId = Guid.NewGuid(),
                FromTeam = fromTeam,
                ToTeam = toTeam,
                PlayersIds = playersIds,
            };

            await _tradeManager.IsTradeValid(leagueId, trade);

            await _tradeManager.ProposeDraftTrade(leagueId, trade);

            await Clients.Group($"team:trade:{toTeam}").ReceiveTradeRequest(trade);
        }

        // Action called to accept a proposed trade: execute the swap (which also records the
        // accepted trade in Redis), then notify the whole league once. Both teams involved are
        // members of the league group, so a single send reaches everyone exactly once.
        public async Task AcceptTrade(long leagueId, Guid tradeId)
        {
            var trade = await _tradeManager.AcceptDraftTrade(leagueId, tradeId);

            // Rebuild the same DraftState shape DraftHub broadcasts — repopulate the available board so we
            // don't blank it out on clients — and push it to the draft group so both teams' rosters refresh
            // live without a page refresh. This touches only draft:state, never the draft:timers set, so
            // the pick clock is untouched.
            var state = await _draftManager.GetDraftState(leagueId);
            state!.DraftPlayers = await _playerManager.GetPlayersOnDraftBoard(leagueId);
            await _draftHub.Clients.Group(leagueId.ToString()).UpdateDraftState(state);

            await Clients.Group($"league:trade:{leagueId}").ReceiveTradeAccepted(trade!);
        }

        // ---- In-season trades -------------------------------------------------------------------
        // Kept separate from ProposeTrade/AcceptTrade above, which are the draft-time flow: those
        // validate against the live DraftState in Redis and write the draft sorted set. During the
        // season that state is gone and nba.teamplayer holds the rosters instead, so the two cannot
        // share an implementation.

        // Validates against the Postgres rosters, records the proposal durably, caches it for the live
        // window, then pushes it to the recipient's group. Persist before notifying: Redis has no
        // persistence configured, so the row is the only copy that survives a restart, and a failed
        // send must never lose the offer.
        public async Task<TradeDto> ProposeSeasonTrade(long leagueId, long fromTeam, long toTeam, List<long> playersIds)
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

            var created = await _tradeService.AddProposedTrade(leagueId, proposal, DateTime.UtcNow.Add(ttl));

            await _tradeManager.ProposeSeasonTrade(leagueId, proposal);

            // No-ops when the recipient is not on the trade screen — their connection only exists
            // while that component is mounted, so an empty group *is* "they are not looking". They
            // pick the offer up from the backlog on their next connect.
            await Clients.Group($"team:trade:{toTeam}").ReceiveTradeRequest(proposal);

            return created.ToTradeDto();
        }

        // Re-validates against current rosters, swaps the teamplayer rows, marks the row accepted, and
        // clears the Redis copy so a settled trade cannot reappear in anyone's backlog.
        public async Task<TradeDto> AcceptSeasonTrade(long leagueId, Guid tradeId)
        {
            var accepted = await _tradeService.AcceptProposal(leagueId, tradeId);

            await _tradeManager.RemoveProposedSeasonTrade(leagueId, accepted.Toteamid, tradeId);

            var settled = new TradeBetweenTeams
            {
                TradeId = accepted.Tradeguid,
                FromTeam = accepted.Fromteamid,
                ToTeam = accepted.Toteamid,
                PlayersIds = accepted.Playerids ?? [],
            };

            // Both teams are in the league group, so one send reaches everyone exactly once.
            await Clients.Group($"league:trade:{leagueId}").ReceiveTradeAccepted(settled);

            return accepted.ToTradeDto();
        }
    }
}
