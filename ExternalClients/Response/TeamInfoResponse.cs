using ExternalClients.Poco;

namespace ExternalClients.Response
{
    public record GetAllTeamsResponse
    {
        public required List<TeamInfoResponse> data { get; init; }
        public required MetaData meta { get; init; }
    }
    public record TeamInfoResponse
    {
        public required long id { get; init; }
        public required string  conference { get; init; }
        public required string division { get; init; }
        public required string city { get; init; }
        public required string name { get; init; }
        public required string full_name { get; init; }
        public required string abbreviation { get; init; }
    }
}
