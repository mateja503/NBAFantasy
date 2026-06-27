using NBA.Data.Redis.Entities;
using NBA.Data.Redis.Keys;
using Xunit;

namespace NBA.Tests.Integration
{
    // Direct Redis-write coverage for the trade operations, against the real Redis container. Focused on
    // RemoveProposedTrade, whose sorted-set member is the full trade JSON (not the id) -- the bug the fix
    // addresses. Uses leagueIds outside the seeded set since these hit the Redis layer directly.
    [Collection("Trade integration")]
    public class TradeRedisWriteTests
    {
        private readonly TradeHubFixture _fixture;

        public TradeRedisWriteTests(TradeHubFixture fixture) => _fixture = fixture;

        [Fact]
        public async Task RemoveProposedTrade_removes_only_the_matching_trade_and_returns_it()
        {
            const long leagueId = 11;
            var trade = new TradeBetweenTeams { FromTeam = 1, ToTeam = 2, PlayersIds = new() { 7 } };
            var other = new TradeBetweenTeams { FromTeam = 3, ToTeam = 4, PlayersIds = new() { 8 } };

            await _fixture.Redis.Trade.SetProposedTrade(leagueId, trade);
            await _fixture.Redis.Trade.SetProposedTrade(leagueId, other);

            var removed = await _fixture.Redis.Trade.RemoveProposedTrade(leagueId, trade.TradeId);

            Assert.NotNull(removed);
            Assert.Equal(trade.TradeId, removed!.TradeId);
            // Only the matching member is gone; the unrelated proposal remains.
            Assert.Equal(1, await _fixture.Database.SortedSetLengthAsync(RedisKeys.GetProposedDraftTradesKey(leagueId)));
        }

        [Fact]
        public async Task RemoveProposedTrade_returns_null_for_unknown_id()
        {
            const long leagueId = 12;
            await _fixture.Redis.Trade.SetProposedTrade(leagueId,
                new TradeBetweenTeams { FromTeam = 1, ToTeam = 2, PlayersIds = new() { 9 } });

            var removed = await _fixture.Redis.Trade.RemoveProposedTrade(leagueId, Guid.NewGuid());

            Assert.Null(removed);
            Assert.Equal(1, await _fixture.Database.SortedSetLengthAsync(RedisKeys.GetProposedDraftTradesKey(leagueId)));
        }

        [Fact]
        public async Task SetAcceptedDraftTrade_is_returned_by_GetAcceptedDraftTrades()
        {
            const long leagueId = 13;
            var trade = new TradeBetweenTeams { FromTeam = 5, ToTeam = 6, PlayersIds = new() { 10, 11 } };

            await _fixture.Redis.Trade.SetAcceptedDraftTrade(leagueId, trade);

            var accepted = await _fixture.Redis.Trade.GetAcceptedDraftTrades(leagueId);

            var stored = Assert.Single(accepted);
            Assert.Equal(trade.TradeId, stored.TradeId);
            Assert.Equal(new List<long> { 10, 11 }, stored.PlayersIds);
        }
    }
}
