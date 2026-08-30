using Microsoft.EntityFrameworkCore;
using NBA.Data.Context;
using PlayerData = NBA.Data.Entities.Player;

namespace NBA.Service.FreeAgency
{
    public class FreeAgencyService(NbaFantasyContext context)
    {
        private readonly NbaFantasyContext _context = context;
        // The free-agent pool for one league. Projects straight to the Player entity because that is
        // the only part a client can use - a Leagueplayer row is nothing but the three ids plus the
        // flag we just filtered on, so returning it would force every caller into a second lookup.
        //
        // AsNoTracking: read-only path, nothing here is written back (unlike ToggleFreeAgencyStatus,
        // which needs the tracked entities).
        public async Task<List<PlayerData>> GetFreeAgents(long leagueId)
        {
            return await _context.GetAllLeaguePlayers()
                .AsNoTracking()
                .Where(lp => lp.Leagueid == leagueId && lp.Isfreeagent)
                .Select(lp => lp.Player)
                .ToListAsync();
        }

        public async Task<List<PlayerData>> ToggleFreeAgencyStatus(long leagueId, List<long> playersToPickUp)
        {
            var leaguePlayers = await _context.GetAllLeaguePlayers()
                .Where(u=>u.Leagueid == leagueId && playersToPickUp.Contains(u.Playerid))
                .Include(u=>u.Player)
                .ToListAsync();

            foreach (var l in leaguePlayers) 
                l.Isfreeagent = !l.Isfreeagent;

            _ = await _context.UpdatLeaguePlayersRange(leaguePlayers);

            return leaguePlayers.Select(u => u.Player).ToList();
        }
    }
}
