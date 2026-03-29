namespace NBA.Api.Requests.Team
{
    public record TeamRequest
    {
        public string? teamName { get; init; } = string.Empty;
    }
}
