namespace NBA.Api.Requests.FreeAgency
{
    public record FreeAgencyPickUpRequest
    {
        public long? leagueId { get; init; } = null;

        public List<long>? playerIds { get; init; } = null;
    }
}
