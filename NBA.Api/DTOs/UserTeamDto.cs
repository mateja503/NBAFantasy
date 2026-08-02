namespace NBA.Api.DTOs
{
    // A team owned by the authenticated user together with its roster. Unlike TeamDto it carries the
    // league label inline (Leagueid/Leaguename) rather than a nested LeagueDto, because the teams page
    // only needs to label/group by league — not the full league configuration.
    public class UserTeamDto
    {
        public long Teamid { get; set; }
        public string Name { get; set; } = null!;
        public int? Seed { get; set; } = null;
        public int? Waiverpriority { get; set; } = null;
        public double? Lastweekpoints { get; set; } = null;
        public double? Categoryleaguepoints { get; set; } = null;
        public bool? Islock { get; set; } = null;
        public long? Leagueid { get; set; } = null;
        public string? Leaguename { get; set; } = null;
        public List<PlayerDto> Players { get; set; } = [];
    }
}
