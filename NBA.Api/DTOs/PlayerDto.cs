namespace NBA.Api.DTOs
{
    public class PlayerDto
    {
        public long Playerid { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Irlteamname { get; set; } = null;
        // Playerposition is stored as the numeric PlayerPositionEnum code; the API hands out the
        // readable label ("G", "GF", ...) so clients don't have to know the enum.
        public string? Position { get; set; } = null;
        public decimal? Points { get; set; } = null;
        public decimal? Rebounds { get; set; } = null;
        public decimal? Assists { get; set; } = null;
        public decimal? Steals { get; set; } = null;
        public decimal? Blocks { get; set; } = null;
        public decimal? Threepointers { get; set; } = null;
        public decimal? Turnovers { get; set; } = null;
        public decimal? Fieldgoal { get; set; } = null;
        public decimal? Freethrow { get; set; } = null;
        public long? Irlteamid { get; set; } = null;
        public bool? Allowdrop { get; set; } = null;
        public bool? Islock { get; set; } = null;
        public int? Rosterrole { get; set; } = null;
        public int? Gameready { get; set; } = null;
        public long? Playermemontoid { get; set; } = null;
        public DateTime? Tsupdated { get; set; } = null;
        // Name of the fantasy team rostering this player in the requested league. Null when no
        // leagueId was supplied, or when nobody in that league has drafted them.
        public string? Team { get; set; } = null;
        // Tscreated is deliberately not exposed: it is an ingest bookkeeping timestamp with no
        // meaning to a client. Every other nba.player column is here.
    }
}
