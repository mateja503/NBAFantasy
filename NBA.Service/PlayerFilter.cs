using ExternalClients.Response;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NBA.Service
{
    public static class PlayerFilter
    {
        public static List<PlayerInfoResponse> FilterNonActivePlayers(List<PlayerInfoResponse> playersInfo)
        {
            return playersInfo.Where(player => 
            {
                if (player.draft_year <= 2003)
                    return false;

                if(string.IsNullOrEmpty(player.position))
                    return false;

                return true;

            }).ToList();
        }
    }
}
