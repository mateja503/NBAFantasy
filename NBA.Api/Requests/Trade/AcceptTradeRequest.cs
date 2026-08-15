namespace NBA.Api.Requests.Trade
{
    public record AcceptTradeRequest
    {
        public long? leagueId { get; init; }
        public Guid? tradeId { get; init; }
    }
}
