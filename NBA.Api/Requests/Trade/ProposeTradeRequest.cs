namespace NBA.Api.Requests.Trade
{
    public record ProposeTradeRequest
    {
        public long? leagueId { get; init; }
        public long? fromTeam { get; init; }
        public long? toTeam { get; init; }
        public List<long>? playersIds { get; init; }
    }
}
