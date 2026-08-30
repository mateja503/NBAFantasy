using ApplicationDefaults.Exceptions;
using NBA.Data.Context;
using LeaguePlayerData = NBA.Data.Entities.Leagueplayer;

namespace NBA.Service.LeaguePlayer
{
    // Owns nba.leagueplayer: the per-league player pool that FreeAgencyService toggles. Named
    // *Service under rule 4 because every member here writes to Postgres - the pool it is seeded
    // from is resolved elsewhere (PlayerService.ResolvePlayerPoolIds) and arrives as a parameter,
    // so this type never touches Redis.
    public class LeaguePlayerService(NbaFantasyContext context)
    {
        private readonly NbaFantasyContext _context = context;

        // Seeds a newly created league with one row per player, all free agents.
        //
        // The caller owns the transaction: LeagueService.CreateAsync runs this inside the one that
        // already wraps its statsvalue and league inserts, because a committed league with no player
        // pool is a broken league - the fan-out and the league insert have to succeed or fail together.
        public async Task<List<LeaguePlayerData>> SeedLeaguePool(long leagueId, List<long>? playerIds)
        {
            // Guarded rather than assumed: this is a public entry point, and seeding a league with an
            // empty pool would silently produce the broken league the transaction exists to prevent.
            if (playerIds is null || playerIds.Count == 0)
                throw new NBAException($"No players supplied to seed the pool for league {leagueId}",
                    ErrorCodes.PlayerPoolEmpty);

            var leaguePlayers = playerIds
                .Select(id => new LeaguePlayerData
                {
                    Leagueid = leagueId,
                    Playerid = id,
                    Isfreeagent = true,
                })
                .ToList();

            return await _context.AddLeaguePlayersRange(leaguePlayers);
        }
    }
}
