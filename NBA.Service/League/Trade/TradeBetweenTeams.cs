using System;
using System.Collections.Generic;
using System.Text;

namespace NBA.Service.League.Trade
{
    public record TradeBetweenTeams
    {
        public long FromTeam { get; init; }
        public long ToTeam { get; init; }
        public List<long> PlayersIds { get; init; } = [];
    }
}
