namespace NBA.Api.DTOs
{
    public class TeamDto
    {
        public long Teamid { get; set; }

        public string Name { get; set; } = null!;

        public int? Seed { get; set; } = null;

        public int? Waiverpriority { get; set; } = null;

        public double? Lastweekpoints { get; set; } = null;

        public double? Categoryleaguepoints { get; set; } = null;

        public bool? Islock { get; set; } = null;
    }
}
