using TradeData = NBA.Data.Entities.Trade;

namespace NBA.Api.DTOs
{
    // Mirrors the nba.trades entity casing (rule 5). Tradeid is the UUID clients quote back on accept
    // and reject — the same id the Redis copy and the SignalR payloads carry, so there is one
    // identifier for a trade across all three stores.
    public class TradeDto
    {
        public Guid Tradeid { get; set; }
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
