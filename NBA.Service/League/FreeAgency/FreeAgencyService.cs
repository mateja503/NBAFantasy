using Microsoft.EntityFrameworkCore;
using NBA.Data.Context;
using NBA.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using PlayerData = NBA.Data.Entities.Player;

namespace NBA.Service.League.FreeAgency
{
    public class FreeAgencyService(NbaFantasyContext context)
    {
        private readonly NbaFantasyContext _context = context;
        public async Task<List<PlayerData>> ToggleFreeAgencyStatus(long leagueId, List<long> playersToPickUp)
        {
            var leaguePlayers = await _context.GetAllLeaguePlayers()
                .Where(u=>u.Leagueid == leagueId && playersToPickUp.Contains(u.Playerid))
                .Include(u=>u.Player)
                .ToListAsync();

            List<Leagueplayer> updateEntity = [];
            foreach (var l in leaguePlayers) 
            {
                l.Isfreeagent = !l.Isfreeagent;
                updateEntity.Add(l);
            }
            _ = await _context.UpdatLeaguePlayersRange(updateEntity);

            return leaguePlayers.Select(u => u.Player).ToList();
        }
    }
}
