

using ExternalClients.Poco;

namespace ExternalClients.Response
{

    public record GetAllPlayersResponse 
    {
        // Defaults keep a body-less or partial payload from producing nulls the callers don't guard for.
        public List<PlayerInfoResponse> data { get; init; } = [];
        public MetaData meta { get; init; } = new();
    }

    public record PlayerInfoResponse
    {
        public long id { get; init; }
        public string first_name { get; init; } = string.Empty;
        public string last_name { get; init; } = string.Empty;
        public string position { get; init; } = string.Empty;
        public string height { get; init; } = string.Empty;
        public string weight { get; init; } = string.Empty;
        public string jersey_number { get; init; } = string.Empty;
        public string college { get; init; } = string.Empty;
        public string country { get; init; } = string.Empty;
        public int? draft_year { get; init; }
        public int? draft_round { get; init; }
        public int? draft_number { get; init; }
        public TeamInfoResponse? team { get; init; }   

    }



    
}
