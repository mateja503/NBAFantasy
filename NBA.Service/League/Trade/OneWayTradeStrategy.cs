
using Microsoft.EntityFrameworkCore;
using NBA.Data.Context;
using NBA.Data.Entities;

namespace NBA.Service.League.Trade
{
    public class OneWayTradeStrategy : ITradeStrategy
    {
        private readonly NbaFantasyContext context;
        private List<long> teamPlayers;

        public OneWayTradeStrategy(List<long> _teamPlayers, NbaFantasyContext _context)
        {
            context = _context;
            teamPlayers = _teamPlayers;
        }

        public async Task Trade()
        {
            List<Teamplayer> temp = await context.GetAllTeamPlayer().Where(u => teamPlayers.Contains(u.Teamplayerid)).ToListAsync();

            Dictionary<long, List<long>> completeTrade = temp.GroupBy(u => u.Teamid).Select(u => new
            {
                Key = u.Key,
                Value = u.Select(u => u.Playerid).ToList()
            }).ToDictionary(x=>x.Key,x=>x.Value);

            await context.DeleteTeamPlayerRange(temp);

            //foreach (var team in completeTrade) 
            //{
            //    foreach (var playerId in team.Value) 
            //    {
            //        await context.AddTeamPlayer(new Teamplayer { Teamid = team.Key, Playerid = playerId });
            //    }
            
            //}
        }
    }
}
