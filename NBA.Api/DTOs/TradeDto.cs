using TradeData = NBA.Data.Entities.Trade;

namespace NBA.Api.DTOs
{
    // Mirrors the nba.trades entity casing (rule 5). Tradeguid rather than Tradeid is what clients
    // quote back on accept: it is the id that also travels in the Redis copy and over SignalR, while
    // Tradeid is a database surrogate.
    public class TradeDto
    {
        public long Tradeid { get; set; }
        public Guid Tradeguid { get; set; }
        public long Leagueid { get; set; }
        public long Fromteamid { get; set; }
        public long Toteamid { get; set; }
        public List<long> Playerids { get; set; } = [];
        public string Status { get; set; } = string.Empty;
        public DateTime Tscreated { get; set; }

        // When the Redis hot copy lapses. The offer itself stays open past this — it only ends the
        // real-time push window.
        public DateTime Tsexpires { get; set; }
    }
}
