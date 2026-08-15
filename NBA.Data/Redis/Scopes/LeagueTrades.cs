using NBA.Data.Redis.Entities;
using NBA.Data.Redis.Operations;

namespace NBA.Data.Redis.Scopes
{
    // League-bound view over TradeRedisOperations. Every trade key is per-league, so the whole class
    // maps onto the scope. "DraftTrade" collapses to the proposed/accepted distinction that actually
    // varies between the calls.
    public readonly struct LeagueTrades(TradeRedisOperations operations, long leagueId)
    {
        public long LeagueId => leagueId;

        public Task SetProposed(TradeBetweenTeams trade) => operations.SetProposedTrade(leagueId, trade);

        public Task SetAccepted(TradeBetweenTeams trade) => operations.SetAcceptedDraftTrade(leagueId, trade);

        public Task<List<TradeBetweenTeams>> GetAccepted() => operations.GetAcceptedDraftTrades(leagueId);

        public Task<TradeBetweenTeams?> GetProposed(Guid tradeId) => operations.GetProposedTrade(leagueId, tradeId);

        public Task<TradeBetweenTeams?> RemoveProposed(Guid tradeId) => operations.RemoveProposedTrade(leagueId, tradeId);

        // In-season proposals: a sorted set per recipient holding every live offer aimed at that team,
        // separate from the draft-time sorted set the members above use.
        public Task SetProposedSeason(TradeBetweenTeams trade, TimeSpan ttl) =>
            operations.SetProposedSeasonTrade(leagueId, trade, ttl);

        public Task<List<TradeBetweenTeams>> GetProposedSeason(long toTeamId) =>
            operations.GetProposedSeasonTrades(leagueId, toTeamId);

        public Task<bool> RemoveProposedSeason(long toTeamId, Guid tradeId) =>
            operations.RemoveProposedSeasonTrade(leagueId, toTeamId, tradeId);
    }
}
