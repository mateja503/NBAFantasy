using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using NBA.Data.Redis.Entities;
using NBA.Data.Redis.Keys;
using Xunit;

namespace NBA.Tests.Integration
{
    // End-to-end coverage of the trade flow over the real SignalR pipeline and a real Redis container:
    // every public TradeManager method runs for real (propose/validate via the hub's ProposeTrade,
    // accept via AcceptTrade), with assertions against the actual Redis state.
    // Each test uses a distinct seeded leagueId so the shared container/DB need no per-test reset.
    [Collection("Trade integration")]
    public class TradeHubTests
    {
        private readonly TradeHubFixture _fixture;

        public TradeHubTests(TradeHubFixture fixture) => _fixture = fixture;

        // fromTeam roster: C(1), G(2). toTeam roster: C(3), F(4). Swapping [2,4] is valid (no team ends
        // with >1 center); swapping in toTeam's center (3) pushes fromTeam to two centers (invalid).
        private static DraftState BuildDraftState(long fromTeam, long toTeam) => new()
        {
            LeagueName = "Test League",
            DraftedPlayersPerTeam = new()
            {
                [fromTeam] = new()
                {
                    new PlayerShort { PlayerId = 1, Position = "C", FullName = "From Center" },
                    new PlayerShort { PlayerId = 2, Position = "G", FullName = "From Guard" },
                },
                [toTeam] = new()
                {
                    new PlayerShort { PlayerId = 3, Position = "C", FullName = "To Center" },
                    new PlayerShort { PlayerId = 4, Position = "F", FullName = "To Forward" },
                },
            },
        };

        private static async Task<T> WaitFor<T>(TaskCompletionSource<T> tcs, int timeoutMs = 2000)
        {
            var completed = await Task.WhenAny(tcs.Task, Task.Delay(timeoutMs));
            Assert.True(completed == tcs.Task, "Expected a SignalR message but none arrived in time.");
            return await tcs.Task;
        }

        [Fact]
        public async Task ProposeTrade_valid_notifies_target_team_and_stores_proposed_trade()
        {
            const long leagueId = 1, fromTeam = 100, toTeam = 200;
            await _fixture.Redis.Draft.SetDraftState(leagueId, BuildDraftState(fromTeam, toTeam));

            await using var proposer = _fixture.BuildClient(leagueId, fromTeam);
            await using var target = _fixture.BuildClient(leagueId, toTeam);

            var targetReceived = new TaskCompletionSource<TradeBetweenTeams>(TaskCreationOptions.RunContinuationsAsynchronously);
            var proposerReceived = new TaskCompletionSource<TradeBetweenTeams>(TaskCreationOptions.RunContinuationsAsynchronously);
            target.On<TradeBetweenTeams>("ReceiveTradeRequest", t => targetReceived.TrySetResult(t));
            proposer.On<TradeBetweenTeams>("ReceiveTradeRequest", t => proposerReceived.TrySetResult(t));

            await proposer.StartAsync();
            await target.StartAsync();

            await proposer.InvokeAsync("ProposeTrade", leagueId, fromTeam, toTeam, new List<long> { 2, 4 });

            var trade = await WaitFor(targetReceived);
            Assert.Equal(fromTeam, trade.FromTeam);
            Assert.Equal(toTeam, trade.ToTeam);
            Assert.Equal(new List<long> { 2, 4 }, trade.PlayersIds);

            // Persisted as one entry in the proposed sorted set, and the proposer (only in its own team
            // group, not the target team group) is not notified.
            Assert.Equal(1, await _fixture.Database.SortedSetLengthAsync(RedisKeys.GetProposedDraftTradesKey(leagueId)));
            Assert.False(proposerReceived.Task.IsCompleted);
        }

        [Fact]
        public async Task ProposeTrade_over_center_limit_is_rejected_and_stores_nothing()
        {
            const long leagueId = 2, fromTeam = 100, toTeam = 200;
            await _fixture.Redis.Draft.SetDraftState(leagueId, BuildDraftState(fromTeam, toTeam));

            await using var proposer = _fixture.BuildClient(leagueId, fromTeam);
            await proposer.StartAsync();

            // Trading for toTeam's center (3) leaves fromTeam with two centers -> exceeds CenterLimit.
            await Assert.ThrowsAsync<HubException>(() =>
                proposer.InvokeAsync("ProposeTrade", leagueId, fromTeam, toTeam, new List<long> { 3 }));

            Assert.Equal(0, await _fixture.Database.SortedSetLengthAsync(RedisKeys.GetProposedDraftTradesKey(leagueId)));
        }

        [Fact]
        public async Task AcceptTrade_swaps_rosters_broadcasts_to_league_and_removes_proposed()
        {
            const long leagueId = 3, fromTeam = 100, toTeam = 200;
            await _fixture.Redis.Draft.SetDraftState(leagueId, BuildDraftState(fromTeam, toTeam));

            await using var proposer = _fixture.BuildClient(leagueId, fromTeam);
            await using var target = _fixture.BuildClient(leagueId, toTeam);

            var requestReceived = new TaskCompletionSource<TradeBetweenTeams>(TaskCreationOptions.RunContinuationsAsynchronously);
            var proposerAccepted = new TaskCompletionSource<TradeBetweenTeams>(TaskCreationOptions.RunContinuationsAsynchronously);
            var targetAccepted = new TaskCompletionSource<TradeBetweenTeams>(TaskCreationOptions.RunContinuationsAsynchronously);
            target.On<TradeBetweenTeams>("ReceiveTradeRequest", t => requestReceived.TrySetResult(t));
            proposer.On<TradeBetweenTeams>("ReceiveTradeAccepted", t => proposerAccepted.TrySetResult(t));
            target.On<TradeBetweenTeams>("ReceiveTradeAccepted", t => targetAccepted.TrySetResult(t));

            await proposer.StartAsync();
            await target.StartAsync();

            await proposer.InvokeAsync("ProposeTrade", leagueId, fromTeam, toTeam, new List<long> { 2, 4 });
            var proposed = await WaitFor(requestReceived);

            await proposer.InvokeAsync("AcceptTrade", leagueId, proposed.TradeId);

            // Both league members are notified.
            var acceptedByProposer = await WaitFor(proposerAccepted);
            var acceptedByTarget = await WaitFor(targetAccepted);
            Assert.Equal(proposed.TradeId, acceptedByProposer.TradeId);
            Assert.Equal(proposed.TradeId, acceptedByTarget.TradeId);

            // Draft state reflects the swap: fromTeam keeps C(1), gains F(4); toTeam keeps C(3), gains G(2).
            var state = await _fixture.Redis.Draft.GetCurrentDraftState(leagueId);
            Assert.NotNull(state);
            Assert.Equal(new[] { 1L, 4L }, state!.DraftedPlayersPerTeam[fromTeam].Select(p => p.PlayerId!.Value).OrderBy(x => x));
            Assert.Equal(new[] { 2L, 3L }, state.DraftedPlayersPerTeam[toTeam].Select(p => p.PlayerId!.Value).OrderBy(x => x));

            // Proposed trade is removed (RemoveProposedTrade fix) and recorded as accepted.
            Assert.Equal(0, await _fixture.Database.SortedSetLengthAsync(RedisKeys.GetProposedDraftTradesKey(leagueId)));
            Assert.Equal(1, await _fixture.Database.SortedSetLengthAsync(RedisKeys.GetAcceptedDraftTradeKey(leagueId)));
        }

        [Fact]
        public async Task AcceptTrade_unknown_trade_id_throws()
        {
            const long leagueId = 4, fromTeam = 100, toTeam = 200;
            await _fixture.Redis.Draft.SetDraftState(leagueId, BuildDraftState(fromTeam, toTeam));

            await using var client = _fixture.BuildClient(leagueId, fromTeam);
            await client.StartAsync();

            await Assert.ThrowsAsync<HubException>(() =>
                client.InvokeAsync("AcceptTrade", leagueId, Guid.NewGuid()));
        }

        [Fact]
        public async Task AcceptTrade_cannot_be_accepted_twice()
        {
            const long leagueId = 5, fromTeam = 100, toTeam = 200;
            await _fixture.Redis.Draft.SetDraftState(leagueId, BuildDraftState(fromTeam, toTeam));

            await using var proposer = _fixture.BuildClient(leagueId, fromTeam);
            await using var target = _fixture.BuildClient(leagueId, toTeam);

            var requestReceived = new TaskCompletionSource<TradeBetweenTeams>(TaskCreationOptions.RunContinuationsAsynchronously);
            target.On<TradeBetweenTeams>("ReceiveTradeRequest", t => requestReceived.TrySetResult(t));

            await proposer.StartAsync();
            await target.StartAsync();

            await proposer.InvokeAsync("ProposeTrade", leagueId, fromTeam, toTeam, new List<long> { 2, 4 });
            var proposed = await WaitFor(requestReceived);

            await proposer.InvokeAsync("AcceptTrade", leagueId, proposed.TradeId);

            // The proposal was removed on the first accept, so re-accepting the same id is rejected and
            // does not produce a second accepted record.
            await Assert.ThrowsAsync<HubException>(() =>
                proposer.InvokeAsync("AcceptTrade", leagueId, proposed.TradeId));

            Assert.Equal(0, await _fixture.Database.SortedSetLengthAsync(RedisKeys.GetProposedDraftTradesKey(leagueId)));
            Assert.Equal(1, await _fixture.Database.SortedSetLengthAsync(RedisKeys.GetAcceptedDraftTradeKey(leagueId)));
        }

        [Fact]
        public async Task AcceptTrade_throws_when_a_team_is_missing_from_draft_state()
        {
            const long leagueId = 6, fromTeam = 100, toTeam = 200;
            await _fixture.Redis.Draft.SetDraftState(leagueId, BuildDraftState(fromTeam, toTeam));

            await using var proposer = _fixture.BuildClient(leagueId, fromTeam);
            await using var target = _fixture.BuildClient(leagueId, toTeam);

            var requestReceived = new TaskCompletionSource<TradeBetweenTeams>(TaskCreationOptions.RunContinuationsAsynchronously);
            target.On<TradeBetweenTeams>("ReceiveTradeRequest", t => requestReceived.TrySetResult(t));

            await proposer.StartAsync();
            await target.StartAsync();

            await proposer.InvokeAsync("ProposeTrade", leagueId, fromTeam, toTeam, new List<long> { 2, 4 });
            var proposed = await WaitFor(requestReceived);

            // The toTeam disappears from the draft state before the trade is accepted.
            await _fixture.Redis.Draft.SetDraftState(leagueId, new DraftState
            {
                LeagueName = "Test League",
                DraftedPlayersPerTeam = new()
                {
                    [fromTeam] = new() { new PlayerShort { PlayerId = 1, Position = "C" } },
                },
            });

            await Assert.ThrowsAsync<HubException>(() =>
                proposer.InvokeAsync("AcceptTrade", leagueId, proposed.TradeId));

            // A failed accept leaves the proposal intact (it is only consumed after a successful swap)
            // and records nothing as accepted.
            Assert.Equal(1, await _fixture.Database.SortedSetLengthAsync(RedisKeys.GetProposedDraftTradesKey(leagueId)));
            Assert.Equal(0, await _fixture.Database.SortedSetLengthAsync(RedisKeys.GetAcceptedDraftTradeKey(leagueId)));
        }

        [Fact]
        public async Task AcceptTrade_throws_when_draft_state_is_missing()
        {
            const long leagueId = 7, fromTeam = 100, toTeam = 200;
            await _fixture.Redis.Draft.SetDraftState(leagueId, BuildDraftState(fromTeam, toTeam));

            await using var proposer = _fixture.BuildClient(leagueId, fromTeam);
            await using var target = _fixture.BuildClient(leagueId, toTeam);

            var requestReceived = new TaskCompletionSource<TradeBetweenTeams>(TaskCreationOptions.RunContinuationsAsynchronously);
            target.On<TradeBetweenTeams>("ReceiveTradeRequest", t => requestReceived.TrySetResult(t));

            await proposer.StartAsync();
            await target.StartAsync();

            await proposer.InvokeAsync("ProposeTrade", leagueId, fromTeam, toTeam, new List<long> { 2, 4 });
            var proposed = await WaitFor(requestReceived);

            // The draft state evaporates (e.g. Redis eviction with no snapshot) before the accept.
            await _fixture.Database.KeyDeleteAsync(RedisKeys.GetDraftStateKey(leagueId));

            await Assert.ThrowsAsync<HubException>(() =>
                proposer.InvokeAsync("AcceptTrade", leagueId, proposed.TradeId));

            // The proposal is preserved since the accept failed before applying anything.
            Assert.Equal(1, await _fixture.Database.SortedSetLengthAsync(RedisKeys.GetProposedDraftTradesKey(leagueId)));
            Assert.Equal(0, await _fixture.Database.SortedSetLengthAsync(RedisKeys.GetAcceptedDraftTradeKey(leagueId)));
        }

        [Fact]
        public async Task AcceptTrade_revalidates_roster_limits_against_current_state()
        {
            const long leagueId = 8, fromTeam = 100, toTeam = 200;
            await _fixture.Redis.Draft.SetDraftState(leagueId, BuildDraftState(fromTeam, toTeam));

            await using var proposer = _fixture.BuildClient(leagueId, fromTeam);
            await using var target = _fixture.BuildClient(leagueId, toTeam);

            var requestReceived = new TaskCompletionSource<TradeBetweenTeams>(TaskCreationOptions.RunContinuationsAsynchronously);
            target.On<TradeBetweenTeams>("ReceiveTradeRequest", t => requestReceived.TrySetResult(t));

            await proposer.StartAsync();
            await target.StartAsync();

            // Propose a swap that is valid against the current rosters (G2 <-> F4).
            await proposer.InvokeAsync("ProposeTrade", leagueId, fromTeam, toTeam, new List<long> { 2, 4 });
            var proposed = await WaitFor(requestReceived);

            // State drifts: fromTeam picks up a second center, so applying the same swap would now leave
            // fromTeam with two centers (C1 + C5 + the incoming F4) -> over CenterLimit.
            await _fixture.Redis.Draft.SetDraftState(leagueId, new DraftState
            {
                LeagueName = "Test League",
                DraftedPlayersPerTeam = new()
                {
                    [fromTeam] = new()
                    {
                        new PlayerShort { PlayerId = 1, Position = "C" },
                        new PlayerShort { PlayerId = 5, Position = "C" },
                        new PlayerShort { PlayerId = 2, Position = "G" },
                    },
                    [toTeam] = new()
                    {
                        new PlayerShort { PlayerId = 3, Position = "C" },
                        new PlayerShort { PlayerId = 4, Position = "F" },
                    },
                },
            });

            await Assert.ThrowsAsync<HubException>(() =>
                proposer.InvokeAsync("AcceptTrade", leagueId, proposed.TradeId));

            // Rejected at accept time; proposal preserved, nothing recorded as accepted.
            Assert.Equal(1, await _fixture.Database.SortedSetLengthAsync(RedisKeys.GetProposedDraftTradesKey(leagueId)));
            Assert.Equal(0, await _fixture.Database.SortedSetLengthAsync(RedisKeys.GetAcceptedDraftTradeKey(leagueId)));
        }
    }
}
