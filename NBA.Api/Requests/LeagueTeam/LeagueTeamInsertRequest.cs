namespace NBA.Api.Requests.LeagueTeam
{
    public record LeagueTeamInsertRequest
    {
        public string? TeamName { get; init; } = null;
        public long? LeagueId { get; init; } = null;

        //this could be take from header in the future
        public long? UserId { get; init; } = null;
    }

}
