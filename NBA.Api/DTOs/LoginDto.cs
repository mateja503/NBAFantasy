using System.ComponentModel;

namespace NBA.Api.DTOs
{
    public class LoginDto
    {
        public string Username { get; set; }
        public List<TeamDto>? Teams { get; set; } = null;
        public List<LeagueDto>? Leagues { get; set; } = null;
    }
}
