
namespace NBA.Data.Redis.Entities
{
    public record TradeBetweenTeams
    {
        public Guid TradeId { get; init; } = Guid.NewGuid();
        public long FromTeam { get; init; }
        public long ToTeam { get; init; }
        public List<long> PlayersIds { get; init; } = [];
        public DateTimeOffset TradeDate { get; init; } = DateTimeOffset.UtcNow;

    }
}
