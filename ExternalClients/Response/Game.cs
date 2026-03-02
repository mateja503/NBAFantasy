

using ExternalClients.Poco;

namespace ExternalClients.Response
{
    public record GetTodaysGamesResponse 
    {
        public List<Game> data { get; init; }
        public MetaData meta { get; init; }
    }
    public record Game
    {
        public long id { get; init; }
        public string date { get; init; } 
        public string status { get; init; }
        public string time { get; init;  }
        public bool postseason { get; init; }
        public bool postponed { get; init; }
        public long homeTeamId { get; init; }
        public string homeTeamName { get; init; }
        public long awayTeamId { get; init; }   
        public string awayTeamName { get; init; }
    }
}
