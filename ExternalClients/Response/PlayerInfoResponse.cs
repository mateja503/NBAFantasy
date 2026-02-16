

namespace ExternalClients.Response
{

    public record GetAllPlayersResponse 
    {
        public List<PlayerInfoResponse> data { get; init; }
        public object meta { get; init; }
    }

    public record PlayerInfoResponse
    {
        public long id { get; init; }
        public string fist_name { get; init; }
        public string last_name { get; init; }
        public string position { get; init; }
        public string height { get; init; }
        public string weight { get; init; }
        public string jersey_number { get; init; }
        public string college { get; init; }
        public string country { get; init; }
        public int? draft_year { get; init; }
        public int? draft_round { get; init; }
        public int? draft_number { get; init; }

    }

    public record TeamInforResponse 
    {
        public long id { get; init; }
        public string conference { get; init; }
        public string division { get; init; }
        public string city { get; init; }
        public string name { get; init; }
        public string full_name { get; init; }
        public string abbreviation { get; init; }
    }
}
