using ApplicationDefaults.Exceptions;
using Microsoft.EntityFrameworkCore;
using NBA.Data.Context;

namespace NBA.Service.Player
{
    // Player logic that needs both stores at once. Rule 4 splits the folder by store - PlayerService
    // owns Postgres, PlayerManager owns Redis - and neither suffix can honestly describe a member that
    // reads or reconciles across the two, so those land here instead of forcing a lie into one of them.
    // Keeping them out of PlayerService is what lets PlayerService stay Postgres-only.
    public class PlayerCoordinator(NbaFantasyRedis redis, NbaFantasyContext nbaContext)
    {
        private readonly NbaFantasyRedis _redis = redis;
        private readonly NbaFantasyContext _nbaContext = nbaContext;

        // The full player pool, as ids. Callers that seed a per-league pool (LeagueService.CreateAsync)
        // need the membership of the master set, not the player payloads.
        //
        // Reading Redis first and Postgres second is not a fallback for convenience: nba:master:players
        // is only a boot-time cache, seeded under GetStartupSeedLockKey(). If Redis was flushed, failing
        // here would block league creation for a reason the user cannot act on - so the durable copy in
        // nba.player answers instead. Only an empty pool in both places is an error.
        public async Task<List<long>> ResolvePlayerPoolIds()
        {
            var playerIds = await _redis.Player.GetAllPlayerIds();

            if (playerIds.Count == 0)
                playerIds = await _nbaContext.GetAllPlayers().Select(p => p.Playerid).ToListAsync();

            if (playerIds.Count == 0)
                throw new NBAException("Player pool is empty in Redis and in the database; cannot seed league players",
                    ErrorCodes.PlayerPoolEmpty);

            return playerIds;
        }
    }
}
