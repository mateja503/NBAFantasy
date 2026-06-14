using System.ComponentModel;

namespace NBA.Api.DTOs
{
    public class LoginDto
    {
        public long? Userid { get; set; } = null;
        public string Username { get; set; }
        public string? Token { get; set; } = null;
        public string? RefreshToken { get; set; } = null;
        public List<TeamDto>? Teams { get; set; } = null;
        public List<LeagueDto>? Leagues { get; set; } = null;
    }
}
