namespace NBA.Data.Constants
{
    // The vocabulary of nba.trades.status. Kept as constants rather than an enum because the column is
    // a varchar the SQL scripts default to 'pending' — the strings have to match exactly.
    public static class TradeStatuses
    {
        // Live offer, awaiting a response. Survives the Redis TTL: that window only governs the
        // real-time push, not whether the offer is still open.
        public const string Pending = "pending";

        // Replaced by a newer offer to the same team. Kept rather than deleted so the displaced team
        // can be told why its proposal disappeared.
        public const string Superseded = "superseded";

        public const string Accepted = "accepted";

        public const string Rejected = "rejected";

        // The whole vocabulary, for validating a caller-supplied status filter. Kept next to the
        // constants so adding a status here can't leave the filter silently rejecting it.
        public static readonly string[] All = [Pending, Superseded, Accepted, Rejected];
    }
}
