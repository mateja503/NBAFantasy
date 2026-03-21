using NBA.Api.Requests.StatValue;

namespace NBA.Api.Requests.League
{
    public record LeagueRequest
    {
        public string? Name { get; init; }
        public int? Typeleague { get; init; }
        public int? DraftStyle { get; init; }
        public int? WeeksForSeason { get; init; }
        public int? TransactionLimit { get; init; }
        public int? TypeTransactionLimits { get; init; }
        public bool? Autostart { get; init; }
        public bool? UseDefaultScorigValues { get; init; }
        public StatsValueRequest? StatsValue { get; init; }
    }
}
