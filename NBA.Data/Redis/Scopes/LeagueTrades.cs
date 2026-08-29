using NBA.Data.Redis.Entities;
using NBA.Data.Redis.Operations;

namespace NBA.Data.Redis.Scopes
{
    // League-bound view over TradeRedisOperations. Every trade key is per-league, so the whole class
    // maps onto the scope.
    //
    // Trading during the draft was removed, so the draft-time members (a per-league sorted set of
    // proposed and accepted trades) are gone with it — what remains is the in-season set below.
    public readonly struct LeagueTrades(TradeRedisOperations operations, long leagueId)
    {
        public long LeagueId => leagueId;

        // In-season proposals: a sorted set per recipient holding every live offer aimed at that team.
        public Task SetProposedSeason(TradeBetweenTeams trade, TimeSpan ttl) =>
            operations.SetProposedSeasonTrade(leagueId, trade, ttl);

        public Task<List<TradeBetweenTeams>> GetProposedSeason(long toTeamId) =>
            operations.GetProposedSeasonTrades(leagueId, toTeamId);

        public Task<bool> RemoveProposedSeason(long toTeamId, Guid tradeId) =>
            operations.RemoveProposedSeasonTrade(leagueId, toTeamId, tradeId);
    }
}
