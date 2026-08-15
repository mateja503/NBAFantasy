using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationDefaults.Options
{
    public class ApplicationOptions
    {
        public int CenterLimit { get; set; }
        public int MaxPlayersPerTeam { get; set; }

        // How long an in-season proposal stays in Redis. This is the live/hot window that drives the
        // real-time push only — the nba.trades row outlives it, so an offline manager can still act
        // on the offer. Defaulted so a missing config value cannot silently mean "expire instantly".
        public int ProposedTradeTtlMinutes { get; set; } = 3;
    }
}
