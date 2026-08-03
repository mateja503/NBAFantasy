namespace NBA.Api.Requests.LeagueTeam
{
    public record LeagueTeamInsertRequest
    {
        public string? TeamName { get; init; } = null;
        public long? LeagueId { get; init; } = null;
    }

}
