using ApplicationDefaults.Options;
using Microsoft.Extensions.Options;
using NBA.Data.Context;
using NBA.Data.Redis.Entities;

namespace NBA.Service.Trade
{
    // Redis-only side of trading (rule 4): the live copies that drive the real-time push. The durable
    // record is the nba.trades row TradeService writes.
    //
    // Trading during the draft was removed, so there is no longer a second set of methods here reading
    // and writing the Redis draft state — with it went this type's dependencies on DraftManager (for
    // the draft state) and RosterValidator (the roster limits are now only checked against Postgres,
    // in TradeService).
    public class TradeManager(NbaFantasyRedis redis, IOptions<ApplicationOptions> applicationOptions)
    {
        private readonly NbaFantasyRedis _redis = redis;
        private readonly ApplicationOptions _applicationOptions = applicationOptions.Value;

        // In-season proposal, expiring after ProposedTradeTtlMinutes. This is only the hot copy that
        // drives the live push — the durable record is the nba.trades row TradeService writes, which
        // outlives it. A team replaces its own standing offer; offers from other teams are untouched.
        public Task ProposeSeasonTrade(long leagueId, TradeBetweenTeams trade) =>
            _redis.League(leagueId).Trades.SetProposedSeason(
                trade, TimeSpan.FromMinutes(_applicationOptions.ProposedTradeTtlMinutes));

        // Every live offer aimed at this team, newest first — a recipient can hold proposals from
        // several teams at once.
        public Task<List<TradeBetweenTeams>> GetProposedSeasonTrades(long leagueId, long toTeamId) =>
            _redis.League(leagueId).Trades.GetProposedSeason(toTeamId);

        // Clears a settled proposal from the recipient's live set. Without this an accepted trade keeps
        // turning up in the connect-time backlog until its score happens to lapse.
        public Task<bool> RemoveProposedSeasonTrade(long leagueId, long toTeamId, Guid tradeId) =>
            _redis.League(leagueId).Trades.RemoveProposedSeason(toTeamId, tradeId);
    }
}
