using NBA.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using PlayerData = NBA.Data.Entities.Player;

namespace NBA.Service.League.FreeAgency
{
    public class FreeAgencyService(NbaFantasyContext context)
    {
        private readonly NbaFantasyContext _context = context;

        //public async Task<List<PlayerData>> PickUpPlayers(List<long> playersToPickUp) 
        //{
            
        //}
    }
}
