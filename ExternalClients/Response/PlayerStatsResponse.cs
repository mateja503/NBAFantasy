
namespace ExternalClients.Response
{
    public record PlayerStatsResponse
    {
        public required long player_id { get; init; }
        public required int fgm { get; init; }
        public required int fga { get; init; }
        public required int fg3m { get; init; }
        public required int fg3a { get; init; }
        public required int ftm { get; init; }
        public required int fta { get; init; }
        public required int reb { get; init; }
        public required int ast { get; init; }
        public required int stl { get; init; }
        public required int blk { get; init; }
        public required int turnover { get; init; }
        public required int pts { get; init; }
        
    }
}
