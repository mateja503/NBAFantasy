using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NBA.Data.Constants;
using NBA.Data.Context;
using NBA.Data.Entities;
using NBA.Data.Redis.Entities;
using NBA.Data.Redis.Keys;
using Xunit;
using TradeData = NBA.Data.Entities.Trade;

namespace NBA.Tests.Integration
{
    // End-to-end coverage of the IN-SEASON trade flow over the real SignalR pipeline, a real Redis
    // container and the InMemory relational store. The season path is the one that survives: draft-night
    // trading was removed, so these cases carry the coverage the draft tests used to provide —
    // roster limits at propose and at accept, double-accept, unknown ids, and unowned players.
    //
    // Each test takes its own league (and therefore its own teams) so the shared container and DB need
    // no per-test reset.
    [Collection("Trade integration")]
    public class SeasonTradeHubTests
    {
        private readonly TradeHubFixture _fixture;

        public SeasonTradeHubTests(TradeHubFixture fixture) => _fixture = fixture;

        private static async Task<T> WaitFor<T>(TaskCompletionSource<T> tcs, int timeoutMs = 2000)
        {
            var completed = await Task.WhenAny(tcs.Task, Task.Delay(timeoutMs));
            Assert.True(completed == tcs.Task, "Expected a SignalR message but none arrived in time.");
            return await tcs.Task;
        }

        // The season path's durable record is the nba.trades row, so most assertions read it back rather
        // than inspecting Redis. A fresh scope per read: the hub wrote through its own scoped context.
        private async Task<TradeData?> ReadTradeRow(Guid tradeId)
        {
            using var scope = _fixture.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<NbaFantasyContext>();
            return await context.Trades.AsNoTracking().FirstOrDefaultAsync(t => t.Tradeid == tradeId);
        }

        private async Task<List<long>> ReadRoster(long teamId)
        {
            using var scope = _fixture.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<NbaFantasyContext>();
            return await context.Teamplayers.AsNoTracking()
                .Where(tp => tp.Teamid == teamId)
                .Select(tp => tp.Playerid)
                .OrderBy(id => id)
                .ToListAsync();
        }

        // Ported from ProposeTrade_valid_notifies_target_team_and_stores_proposed_trade.
        // The one behavioural difference is deliberate and matches the hub: a draft-time proposal was
        // sent to the recipient's group alone, a season proposal goes to the whole league, because the
        // season trade board shows every open offer.
        [Fact]
        public async Task ProposeSeasonTrade_valid_broadcasts_to_league_and_persists_a_pending_row()
        {
            const long leagueId = 21;
            long fromTeam = TradeHubFixture.SeasonTeamA(leagueId), toTeam = TradeHubFixture.SeasonTeamB(leagueId);

            await using var proposer = _fixture.BuildClient(leagueId, fromTeam);
            await using var target = _fixture.BuildClient(leagueId, toTeam);

            var targetReceived = new TaskCompletionSource<TradeBetweenTeams>(TaskCreationOptions.RunContinuationsAsynchronously);
            target.On<TradeBetweenTeams>("ReceiveTradeRequest", t => targetReceived.TrySetResult(t));

            await proposer.StartAsync();
            await target.StartAsync();

            var playersIds = new List<long> { TradeHubFixture.PlayerGuardA, TradeHubFixture.PlayerForwardB };
            var created = await proposer.InvokeAsync<TradeDtoShape>(
                "ProposeSeasonTrade", leagueId, fromTeam, toTeam, playersIds);

            var pushed = await WaitFor(targetReceived);
            Assert.Equal(fromTeam, pushed.FromTeam);
            Assert.Equal(toTeam, pushed.ToTeam);
            Assert.Equal(playersIds, pushed.PlayersIds);

            // The durable row is the copy that outlives the Redis TTL.
            var row = await ReadTradeRow(pushed.TradeId);
            Assert.NotNull(row);
            Assert.Equal(TradeStatuses.Pending, row!.Status);
            Assert.Equal(leagueId, row.Leagueid);
            Assert.Equal(pushed.TradeId, created.Tradeid);

            // The hot copy drives the connect-time backlog, keyed on the recipient.
            Assert.Equal(1, await _fixture.Database.SortedSetLengthAsync(
                RedisKeys.GetProposedTradeKey(leagueId, toTeam)));
        }

        // Ported from ProposeTrade_over_center_limit_is_rejected_and_stores_nothing. Same rule
        // (RosterValidator), different source of truth: nba.teamplayer instead of the Redis draft state.
        [Fact]
        public async Task ProposeSeasonTrade_over_center_limit_is_rejected_and_stores_nothing()
        {
            const long leagueId = 22;
            long fromTeam = TradeHubFixture.SeasonTeamA(leagueId), toTeam = TradeHubFixture.SeasonTeamB(leagueId);

            await using var proposer = _fixture.BuildClient(leagueId, fromTeam);
            await proposer.StartAsync();

            // Taking toTeam's center leaves fromTeam holding two centers -> over CenterLimit.
            await Assert.ThrowsAsync<HubException>(() => proposer.InvokeAsync<TradeDtoShape>(
                "ProposeSeasonTrade", leagueId, fromTeam, toTeam, new List<long> { TradeHubFixture.PlayerCenterB }));

            Assert.Equal(0, await _fixture.Database.SortedSetLengthAsync(
                RedisKeys.GetProposedTradeKey(leagueId, toTeam)));

            using var scope = _fixture.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<NbaFantasyContext>();
            Assert.False(await context.Trades.AnyAsync(t => t.Leagueid == leagueId));
        }

        // Ported from AcceptTrade_swaps_rosters_broadcasts_to_league_and_removes_proposed. The swap now
        // lands in nba.teamplayer rather than the Redis draft state, and the Redis copy being cleared is
        // what stops a settled trade reappearing in the recipient's backlog.
        [Fact]
        public async Task AcceptSeasonTrade_swaps_rosters_broadcasts_to_league_and_clears_the_hot_copy()
        {
            const long leagueId = 23;
            long fromTeam = TradeHubFixture.SeasonTeamA(leagueId), toTeam = TradeHubFixture.SeasonTeamB(leagueId);

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

            await proposer.InvokeAsync<TradeDtoShape>("ProposeSeasonTrade", leagueId, fromTeam, toTeam,
                new List<long> { TradeHubFixture.PlayerGuardA, TradeHubFixture.PlayerForwardB });
            var proposed = await WaitFor(requestReceived);

            await proposer.InvokeAsync<TradeDtoShape>("AcceptSeasonTrade", leagueId, proposed.TradeId);

            // Both league members are notified.
            Assert.Equal(proposed.TradeId, (await WaitFor(proposerAccepted)).TradeId);
            Assert.Equal(proposed.TradeId, (await WaitFor(targetAccepted)).TradeId);

            // Rosters reflect the swap: A keeps C(1), gains F(4); B keeps C(3), gains G(2).
            Assert.Equal(new List<long> { TradeHubFixture.PlayerCenterA, TradeHubFixture.PlayerForwardB },
                await ReadRoster(fromTeam));
            Assert.Equal(new List<long> { TradeHubFixture.PlayerGuardA, TradeHubFixture.PlayerCenterB },
                await ReadRoster(toTeam));

            var row = await ReadTradeRow(proposed.TradeId);
            Assert.Equal(TradeStatuses.Accepted, row!.Status);

            Assert.Equal(0, await _fixture.Database.SortedSetLengthAsync(
                RedisKeys.GetProposedTradeKey(leagueId, toTeam)));
        }

        // Ported from AcceptTrade_unknown_trade_id_throws.
        [Fact]
        public async Task AcceptSeasonTrade_unknown_trade_id_throws()
        {
            const long leagueId = 24;
            long fromTeam = TradeHubFixture.SeasonTeamA(leagueId);

            await using var client = _fixture.BuildClient(leagueId, fromTeam);
            await client.StartAsync();

            await Assert.ThrowsAsync<HubException>(() =>
                client.InvokeAsync<TradeDtoShape>("AcceptSeasonTrade", leagueId, Guid.NewGuid()));
        }

        // Ported from AcceptTrade_cannot_be_accepted_twice. The draft path relied on the proposal being
        // consumed from Redis; the season path guards on the row's status instead, which is the stronger
        // check — it holds even after the hot copy has lapsed.
        [Fact]
        public async Task AcceptSeasonTrade_cannot_be_accepted_twice()
        {
            const long leagueId = 25;
            long fromTeam = TradeHubFixture.SeasonTeamA(leagueId), toTeam = TradeHubFixture.SeasonTeamB(leagueId);

            await using var proposer = _fixture.BuildClient(leagueId, fromTeam);
            await using var target = _fixture.BuildClient(leagueId, toTeam);

            var requestReceived = new TaskCompletionSource<TradeBetweenTeams>(TaskCreationOptions.RunContinuationsAsynchronously);
            target.On<TradeBetweenTeams>("ReceiveTradeRequest", t => requestReceived.TrySetResult(t));

            await proposer.StartAsync();
            await target.StartAsync();

            await proposer.InvokeAsync<TradeDtoShape>("ProposeSeasonTrade", leagueId, fromTeam, toTeam,
                new List<long> { TradeHubFixture.PlayerGuardA, TradeHubFixture.PlayerForwardB });
            var proposed = await WaitFor(requestReceived);

            await proposer.InvokeAsync<TradeDtoShape>("AcceptSeasonTrade", leagueId, proposed.TradeId);

            await Assert.ThrowsAsync<HubException>(() =>
                proposer.InvokeAsync<TradeDtoShape>("AcceptSeasonTrade", leagueId, proposed.TradeId));

            // Still exactly one accepted row, and the rosters did not move a second time.
            var row = await ReadTradeRow(proposed.TradeId);
            Assert.Equal(TradeStatuses.Accepted, row!.Status);
            Assert.Equal(new List<long> { TradeHubFixture.PlayerCenterA, TradeHubFixture.PlayerForwardB },
                await ReadRoster(fromTeam));
        }

        // Replaces AcceptTrade_throws_when_a_team_is_missing_from_draft_state. There is no draft state to
        // go missing here, so the equivalent "the trade names something that isn't there" case is a team
        // that does not belong to this league — the check that stops a cross-league trade corrupting
        // both rosters.
        [Fact]
        public async Task ProposeSeasonTrade_rejects_a_team_from_another_league()
        {
            const long leagueId = 26, otherLeagueId = 27;
            long fromTeam = TradeHubFixture.SeasonTeamA(leagueId);
            long foreignTeam = TradeHubFixture.SeasonTeamB(otherLeagueId);

            await using var proposer = _fixture.BuildClient(leagueId, fromTeam);
            await proposer.StartAsync();

            await Assert.ThrowsAsync<HubException>(() => proposer.InvokeAsync<TradeDtoShape>(
                "ProposeSeasonTrade", leagueId, fromTeam, foreignTeam,
                new List<long> { TradeHubFixture.PlayerGuardA }));

            using var scope = _fixture.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<NbaFantasyContext>();
            Assert.False(await context.Trades.AnyAsync(t => t.Leagueid == leagueId));
        }

        // Replaces AcceptTrade_throws_when_draft_state_is_missing. The season analogue of "the rosters
        // aren't there" is a traded player that sits on neither roster — without that check the swap
        // silently drops the id and the trade "succeeds" having moved nobody.
        [Fact]
        public async Task ProposeSeasonTrade_rejects_a_player_on_neither_roster()
        {
            const long leagueId = 28;
            long fromTeam = TradeHubFixture.SeasonTeamA(leagueId), toTeam = TradeHubFixture.SeasonTeamB(leagueId);

            await using var proposer = _fixture.BuildClient(leagueId, fromTeam);
            await proposer.StartAsync();

            await Assert.ThrowsAsync<HubException>(() => proposer.InvokeAsync<TradeDtoShape>(
                "ProposeSeasonTrade", leagueId, fromTeam, toTeam, new List<long> { 9999 }));

            Assert.Equal(0, await _fixture.Database.SortedSetLengthAsync(
                RedisKeys.GetProposedTradeKey(leagueId, toTeam)));
        }

        // Ported from AcceptTrade_revalidates_roster_limits_against_current_state. Rosters drift between
        // propose and accept, so the accept-time re-validation is the check that actually protects the
        // data. Here the drift is another team's player arriving on fromTeam's roster.
        [Fact]
        public async Task AcceptSeasonTrade_revalidates_roster_limits_against_current_rosters()
        {
            const long leagueId = 29;
            long fromTeam = TradeHubFixture.SeasonTeamA(leagueId), toTeam = TradeHubFixture.SeasonTeamB(leagueId);

            await using var proposer = _fixture.BuildClient(leagueId, fromTeam);
            await using var target = _fixture.BuildClient(leagueId, toTeam);

            var requestReceived = new TaskCompletionSource<TradeBetweenTeams>(TaskCreationOptions.RunContinuationsAsynchronously);
            target.On<TradeBetweenTeams>("ReceiveTradeRequest", t => requestReceived.TrySetResult(t));

            await proposer.StartAsync();
            await target.StartAsync();

            // Valid against the rosters as they stand: G(2) for F(4).
            await proposer.InvokeAsync<TradeDtoShape>("ProposeSeasonTrade", leagueId, fromTeam, toTeam,
                new List<long> { TradeHubFixture.PlayerGuardA, TradeHubFixture.PlayerForwardB });
            var proposed = await WaitFor(requestReceived);

            // State drifts: fromTeam picks up a second center, so the same swap would now leave it over
            // CenterLimit once F(4) arrives.
            using (var scope = _fixture.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<NbaFantasyContext>();
                context.Teamplayers.Add(new Teamplayer
                {
                    Teamplayerid = leagueId * 1000 + 90,
                    Teamid = fromTeam,
                    Playerid = TradeHubFixture.PlayerCenterB,
                });
                await context.SaveChangesAsync();
            }

            await Assert.ThrowsAsync<HubException>(() =>
                proposer.InvokeAsync<TradeDtoShape>("AcceptSeasonTrade", leagueId, proposed.TradeId));

            // Rejected at accept time: the row stays pending and nothing moved between the rosters.
            var row = await ReadTradeRow(proposed.TradeId);
            Assert.Equal(TradeStatuses.Pending, row!.Status);
            Assert.Contains(TradeHubFixture.PlayerGuardA, await ReadRoster(fromTeam));
        }

        // Season-only, and not something the draft path ever had: a team holds one standing offer to any
        // given team, so a second proposal retires the first. Without the supersede the recipient keeps
        // a dead offer on its board and answering it fails.
        [Fact]
        public async Task ProposeSeasonTrade_supersedes_the_proposers_previous_offer_to_the_same_team()
        {
            const long leagueId = 30;
            long fromTeam = TradeHubFixture.SeasonTeamA(leagueId), toTeam = TradeHubFixture.SeasonTeamB(leagueId);

            await using var proposer = _fixture.BuildClient(leagueId, fromTeam);
            await using var target = _fixture.BuildClient(leagueId, toTeam);

            var firstReceived = new TaskCompletionSource<TradeBetweenTeams>(TaskCreationOptions.RunContinuationsAsynchronously);
            var supersededReceived = new TaskCompletionSource<TradeBetweenTeams>(TaskCreationOptions.RunContinuationsAsynchronously);
            target.On<TradeBetweenTeams>("ReceiveTradeRequest", t => firstReceived.TrySetResult(t));
            target.On<TradeBetweenTeams>("ReceiveTradeSuperseded", t => supersededReceived.TrySetResult(t));

            await proposer.StartAsync();
            await target.StartAsync();

            await proposer.InvokeAsync<TradeDtoShape>("ProposeSeasonTrade", leagueId, fromTeam, toTeam,
                new List<long> { TradeHubFixture.PlayerGuardA });
            var first = await WaitFor(firstReceived);

            await proposer.InvokeAsync<TradeDtoShape>("ProposeSeasonTrade", leagueId, fromTeam, toTeam,
                new List<long> { TradeHubFixture.PlayerGuardA, TradeHubFixture.PlayerForwardB });

            var superseded = await WaitFor(supersededReceived);
            Assert.Equal(first.TradeId, superseded.TradeId);

            var firstRow = await ReadTradeRow(first.TradeId);
            Assert.Equal(TradeStatuses.Superseded, firstRow!.Status);
        }

        // Season-only: declining closes the offer without moving anyone, and clears the hot copy so the
        // offer does not come back in the recipient's connect-time backlog.
        [Fact]
        public async Task RejectSeasonTrade_marks_the_row_rejected_and_clears_the_hot_copy()
        {
            const long leagueId = 31;
            long fromTeam = TradeHubFixture.SeasonTeamA(leagueId), toTeam = TradeHubFixture.SeasonTeamB(leagueId);

            await using var proposer = _fixture.BuildClient(leagueId, fromTeam);
            await using var target = _fixture.BuildClient(leagueId, toTeam);

            var requestReceived = new TaskCompletionSource<TradeBetweenTeams>(TaskCreationOptions.RunContinuationsAsynchronously);
            var rejectedReceived = new TaskCompletionSource<TradeBetweenTeams>(TaskCreationOptions.RunContinuationsAsynchronously);
            target.On<TradeBetweenTeams>("ReceiveTradeRequest", t => requestReceived.TrySetResult(t));
            target.On<TradeBetweenTeams>("ReceiveTradeRejected", t => rejectedReceived.TrySetResult(t));

            await proposer.StartAsync();
            await target.StartAsync();

            // A one-way offer of the guard: it has to be a non-center, or handing it over would put the
            // recipient on two centers and the proposal would be refused before it could be declined.
            await proposer.InvokeAsync<TradeDtoShape>("ProposeSeasonTrade", leagueId, fromTeam, toTeam,
                new List<long> { TradeHubFixture.PlayerGuardA });
            var proposed = await WaitFor(requestReceived);

            await target.InvokeAsync<TradeDtoShape>("RejectSeasonTrade", leagueId, proposed.TradeId);

            Assert.Equal(proposed.TradeId, (await WaitFor(rejectedReceived)).TradeId);

            var row = await ReadTradeRow(proposed.TradeId);
            Assert.Equal(TradeStatuses.Rejected, row!.Status);

            Assert.Equal(0, await _fixture.Database.SortedSetLengthAsync(
                RedisKeys.GetProposedTradeKey(leagueId, toTeam)));
        }
    }

    // The three season hub methods return a TradeDto. Declared here rather than referenced from
    // NBA.Api.DTOs so the test project keeps asserting on the wire shape a client actually sees — a
    // rename on the DTO should fail this deserialisation, not silently pass.
    public class TradeDtoShape
    {
        public Guid Tradeid { get; set; }
        public long Leagueid { get; set; }
        public long Fromteamid { get; set; }
        public long Toteamid { get; set; }
        public List<long> Playerids { get; set; } = [];
        public string Status { get; set; } = string.Empty;
    }
}
