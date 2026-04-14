namespace NBA.Api.DTOs
{
    public class LeagueTeamDto
    {
        public long Leagueteamid { get; set; }

        public long Teamid { get; set; }

        public long Leagueid { get; set; }

        public bool Approved { get; set; }
    }
}
