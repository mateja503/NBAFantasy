namespace NBA.Api.Requests.StatValue
{
    public record StatsValueRequest
    {
        public double? Points { get; init; }
        public double? Assists { get; init; }
        public double? Rebounds { get; init; }
        public double? Blocks { get; init; }
        public double? Steals { get; init; }
        public double? FGMade { get; init; }
        public double? FGMissed { get; init; }
        public double? FTMade { get; init; }
        public double? FTMissded { get; init; }
        public double? ThreePointersMade { get; init; }
        public double? ThreePointersMissed { get; init; }
    }
}
