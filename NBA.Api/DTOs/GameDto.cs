namespace NBA.Api.DTOs
{
    // Mirrors the GameShort / GameTeamShort casing it maps from (rule 5).
    public class GameDto
    {
        public long GameId { get; set; }

        // yyyy-MM-dd in the NBA timezone (America/New_York), not the caller's timezone.
        public string? Date { get; set; } = null;

        // balldontlie's own label: a tip-off time ("7:00 pm ET") before the game, a clock or
        // "Final" once it is underway. Handed through as-is rather than parsed.
        public string? Status { get; set; } = null;
        public string? Time { get; set; } = null;
        public DateTime? StartTime { get; set; } = null;
        public bool Postseason { get; set; }
        public bool Postponed { get; set; }
        public GameTeamDto? HomeTeam { get; set; } = null;
        public GameTeamDto? VisitorTeam { get; set; } = null;
    }

    public class GameTeamDto
    {
        public long TeamId { get; set; }
        public string? FullName { get; set; } = null;
        public string? Abbreviation { get; set; } = null;
        public string? City { get; set; } = null;
        public int Score { get; set; }
    }

    public class ScheduledGamesDto
    {
        public List<GameDto> Today { get; set; } = [];
        public List<GameDto> Tomorrow { get; set; } = [];

        // Day after tomorrow through the Sunday that closes the current week; never overlaps the
        // two lists above.
        public List<GameDto> RestOfWeek { get; set; } = [];
    }
}
