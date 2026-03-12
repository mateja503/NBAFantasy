using NBA.Data.Context;
using PlayerData = NBA.Data.Entities.Player;
namespace NBA.Service.League
{
    public class TradeService(NbaFantasyContext context)
    {
        private readonly NbaFantasyContext _context = context;
        public async Task TradePlayers(Dictionary<(long TeamIdA,long TeamIdB),Dictionary<long,long>> trade) 
        {
            foreach (var t in trade) 
            {
                
            
            
            }
        }
    }
}
