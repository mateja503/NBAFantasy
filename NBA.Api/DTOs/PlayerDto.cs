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
    }
}
