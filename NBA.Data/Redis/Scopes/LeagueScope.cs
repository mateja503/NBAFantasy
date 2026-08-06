using NBA.Data.Context;

namespace NBA.Data.Redis.Scopes
{
    // Everything in Redis that belongs to one league, reached through a single bound entry point:
    //
    //     var league = _redis.League(leagueId);
    //     await league.Draft.CancelTimer();
    //     await league.Players.DeleteDraftPlayers(teamIds);
    //
    // This extends the NbaFantasyRedis facade (rule 8) rather than replacing it — the *Operations
    // classes still own every logical Redis operation and RedisKeys still owns every key string.
    // A readonly struct so binding a league costs nothing at runtime.
    public readonly struct LeagueScope(NbaFantasyRedis redis, long leagueId)
    {
        public long LeagueId => leagueId;

        public LeagueDraft Draft => new(redis.Draft, leagueId);

        public LeaguePlayers Players => new(redis.Player, leagueId);

        public LeagueTrades Trades => new(redis.Trade, leagueId);
    }
}
