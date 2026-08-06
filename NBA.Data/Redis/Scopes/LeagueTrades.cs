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
    }
}
