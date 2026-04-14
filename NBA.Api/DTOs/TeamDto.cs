namespace NBA.Api.DTOs
{
    public class TeamDto
    {
        public long Teamid { get; set; }

        public string Name { get; set; } = null!;

        public int? Seed { get; set; }

        public int? Waiverpriority { get; set; }

        public double? Lastweekpoints { get; set; }

        public double? Categoryleaguepoints { get; set; }

        public bool? Islock { get; set; }
    }
}
