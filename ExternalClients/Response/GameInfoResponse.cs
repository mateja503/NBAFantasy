

using ExternalClients.Poco;

namespace ExternalClients.Response
{
    public record GetTodaysGamesResponse 
    {
        public required List<GameInfoResponse> data { get; init; }
        public required MetaData meta { get; init; }
    }
    public record GameInfoResponse
    {
        public required long id { get; init; }
        public required string date { get; init; } 
        public required string status { get; init; }
        public DateTime datetime { get; init; }
        public required string time { get; init;  }
        public required bool postseason { get; init; }
        public required bool postponed { get; init; }
        public required Team home_team { get; init; }
        public required Team visitor_team { get; init; }
    }

    public record Team 
    {
        public required long id { get; init; }
        public required string full_name { get; init; }

    }
}
