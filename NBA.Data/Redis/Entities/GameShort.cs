namespace NBA.Data.Redis.Entities
{
    /// <summary>
    /// The trimmed schedule shape that gets cached, mirroring <c>PlayerShort</c>: NBA.Data does not
    /// reference ExternalClients, so <c>Adapter</c> in NBA.Service translates the balldontlie payload
    /// into this before it ever reaches Redis.
    /// </summary>
    public class GameShort
    {
        public long GameId { get; set; }

        // yyyy-MM-dd in the NBA timezone — this is what the day buckets are cut on.
        public string? Date { get; set; } = null;
        public string? Status { get; set; } = null;
        public string? Time { get; set; } = null;
        public DateTime? StartTime { get; set; } = null;
        public bool Postseason { get; set; }
        public bool Postponed { get; set; }
        public GameTeamShort? HomeTeam { get; set; } = null;
        public GameTeamShort? VisitorTeam { get; set; } = null;
    }

    public class GameTeamShort
    {
        public long TeamId { get; set; }
        public string? FullName { get; set; } = null;
        public string? Abbreviation { get; set; } = null;
        public string? City { get; set; } = null;

        // 0 for a game that has not been played yet, which is the normal case on a schedule page.
        public int Score { get; set; }
    }

    /// <summary>
    /// The three schedule buckets served by GET /v1/games. <see cref="RestOfWeek"/> deliberately
    /// excludes today and tomorrow so the three lists never overlap.
    /// </summary>
    public class ScheduledGames
    {
        public List<GameShort> Today { get; set; } = [];
        public List<GameShort> Tomorrow { get; set; } = [];
        public List<GameShort> RestOfWeek { get; set; } = [];
    }
}
