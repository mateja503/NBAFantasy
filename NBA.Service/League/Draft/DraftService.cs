using NBA.Data.Context;
using NBA.Data.Entities;
using PlayerData = NBA.Data.Entities.Player;

namespace NBA.Service.League.Draft
{
    //maybe more work needed for these service not sure what more, leave as it is for now 
    public class DraftService(NbaFantasyContext context)
    {
        private readonly NbaFantasyContext _context = context;
        public async Task<PlayerData> DraftPlayer(long playerId, long teamId) 
        {
            var teamplayer = await _context.AddTeamPlayer(new Teamplayer { Playerid = playerId, Teamid = teamId });
            return teamplayer.Player;
        }

    }
}
