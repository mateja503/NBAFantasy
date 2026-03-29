namespace NBA.Api.Requests.LeagueTeam
{
    public record LeagueTeamRequest
    {
        public string? TeamName { get; init; } = string.Empty;
        public long? LeagueId { get; init; }
    }
}
