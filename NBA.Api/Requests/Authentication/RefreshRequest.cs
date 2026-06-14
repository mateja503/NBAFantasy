namespace NBA.Api.Requests.Authentication
{
    public record RefreshRequest
    {
        public required string RefreshToken { get; init; }
    }
}
