
using Microsoft.EntityFrameworkCore;
using NBA.Data.Context;
using NBA.Data.Entities;

namespace NBA.Service.League.Trade
{
    public class OneWayTradeStrategy : ITradeStrategy
    {
        private readonly NbaFantasyContext context;
        private long teamA;
        private long teamB;
        private List<long> playersA;
        private List<long> playersB;

        public OneWayTradeStrategy(NbaFantasyContext context, long teamA, long teamB, List<long> playersA, List<long> playersB)
        {
            this.context = context;
            this.teamA = teamA;
            this.teamB = teamB;
            this.playersA = playersA;
            this.playersB = playersB;
        }

        public async Task Trade()
        {
           
        }
    }
}
