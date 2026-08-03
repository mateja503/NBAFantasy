namespace NBA.Api.Requests.Player
{
    // Bound with [AsParameters] from the query string, so every member is optional: a request with
    // no parameters at all means "give me every player", one page at a time. Covers every column of
    // nba.player except the primary key. Numeric columns are ranges (min/max) rather than exact
    // matches because the stats are decimals and equality on them is useless for searching.
    public record PlayersFilterSearch
    {
        // Matched with a case-insensitive "contains", so a partial name is enough.
        public string? name { get; init; } = null;
        public string? surname { get; init; } = null;
        public string? irlteamname { get; init; } = null;

        public long? irlteamid { get; init; } = null;

        // Readable position labels ("G", "GF", ...) rather than the stored PlayerPositionEnum codes,
        // matching what PlayerDto hands out. Repeat the parameter to select several.
        public string[]? playerposition { get; init; } = null;

        public decimal? minPoints { get; init; } = null;
        public decimal? maxPoints { get; init; } = null;
        public decimal? minAssists { get; init; } = null;
        public decimal? maxAssists { get; init; } = null;
        public decimal? minRebounds { get; init; } = null;
        public decimal? maxRebounds { get; init; } = null;
        public decimal? minBlocks { get; init; } = null;
        public decimal? maxBlocks { get; init; } = null;
        public decimal? minSteals { get; init; } = null;
        public decimal? maxSteals { get; init; } = null;
        public decimal? minThreepointers { get; init; } = null;
        public decimal? maxThreepointers { get; init; } = null;
        public decimal? minTurnovers { get; init; } = null;
        public decimal? maxTurnovers { get; init; } = null;
        public decimal? minFreethrow { get; init; } = null;
        public decimal? maxFreethrow { get; init; } = null;
        public decimal? minFieldgoal { get; init; } = null;
        public decimal? maxFieldgoal { get; init; } = null;

        public bool? allowdrop { get; init; } = null;
        public bool? islock { get; init; } = null;

        public int? rosterrole { get; init; } = null;
        public int? gameready { get; init; } = null;
        public long? playermemontoid { get; init; } = null;

        public DateTime? tscreatedFrom { get; init; } = null;
        public DateTime? tscreatedTo { get; init; } = null;
        public DateTime? tsupdatedFrom { get; init; } = null;
        public DateTime? tsupdatedTo { get; init; } = null;

        // Not a filter: it does not narrow the result set. It scopes which fantasy team name comes
        // back on each player, since a player can be rostered in several leagues at once. Without
        // it every player's Team is null.
        public long? leagueId { get; init; } = null;

        public int? page { get; init; } = null;
        public int? pageSize { get; init; } = null;
    }
}
