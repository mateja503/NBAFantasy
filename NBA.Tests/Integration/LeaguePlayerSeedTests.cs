using ApplicationDefaults.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NBA.Data.Context;
using NBA.Data.Entities;
using NBA.Data.Redis.Keys;
using NBA.Service.CalculateBoxScore;
using NBA.Service.League;
using NBA.Service.LeaguePlayer;
using NBA.Service.Player;
using Xunit;

namespace NBA.Tests.Integration
{
    // Verifies that creating a league fans nba.leagueplayer out over the whole player pool. The pool
    // comes from the real Redis container (the nba:master:players set), the league write goes to the
    // InMemory Postgres stand-in, and the fallback path is exercised by emptying Redis and leaving the
    // rows only in the relational store.
    //
    // The sequence under test lives in LeagueService.CreateAsync, which resolves the pool and seeds
    // it in the same call (and, against Postgres, the same transaction) as the league insert. The
    // tests drive that service directly, so they cover the three services and their ordering but not
    // the DI wiring or the POST /v1/league/add route itself.
    //
    // nba:master:players is a single global key, not a league-scoped one, so each test clears it first
    // and writes exactly the membership it wants. That is safe because everything in this collection
    // runs sequentially against the one shared container.
    [Collection("Trade integration")]
    public class LeaguePlayerSeedTests
    {
        private readonly TradeHubFixture _fixture;

        public LeaguePlayerSeedTests(TradeHubFixture fixture) => _fixture = fixture;

        // A fresh InMemory store per test: CreateAsync lets EF generate Leagueid, so isolating the
        // store is what keeps one test's league out of another's assertions.
        private static NbaFantasyContext NewContext() =>
            new(new DbContextOptionsBuilder<NbaFantasyContext>()
                .UseInMemoryDatabase($"leagueplayer-seed-{Guid.NewGuid()}")
                // InMemory has no transactions; CreateAsync and DeleteAsync both open one, so silence
                // the would-be-error warning.
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options);

        private static CreateLeagueInput NewLeagueInput(string name) => new(
            CommissionerUserId: 1,
            LeagueName: name,
            LeagueType: 1,
            DraftStyle: 1,
            WeeksForSeason: 20,
            TransactionLimit: 5,
            TypeTransactionLimits: 1,
            Autostart: false,
            StatsValue: null);

        private async Task SetMasterPlayerPoolAsync(params long[] playerIds)
        {
            var masterKey = RedisKeys.GetMasterPlayerKey();
            await _fixture.Database.KeyDeleteAsync(masterKey);

            if (playerIds.Length > 0)
                await _fixture.Database.SetAddAsync(masterKey, Array.ConvertAll(playerIds, id => (StackExchange.Redis.RedisValue)id));
        }

        private static void SeedPlayers(NbaFantasyContext context, params long[] playerIds)
        {
            foreach (var playerId in playerIds)
                context.Players.Add(new Player { Playerid = playerId, Name = $"Player {playerId}", Surname = "Test" });
        }

        // IBallDontLieClient is passed as null: ResolvePlayerPoolIds is the only PlayerService member
        // exercised here and it touches neither the external client nor the box-score calculator.
        private PlayerService BuildPlayerService(NbaFantasyContext context) =>
            new(null!, context, new BoxScoreCalculationService(context), _fixture.Redis);

        private LeagueService BuildLeagueService(NbaFantasyContext context) =>
            new(context, BuildPlayerService(context), new LeaguePlayerService(context));

        private Task<League> CreateLeagueWithPoolAsync(NbaFantasyContext context, string leagueName) =>
            BuildLeagueService(context).CreateAsync(NewLeagueInput(leagueName));

        [Fact]
        public async Task Creating_a_league_seeds_one_free_agent_row_per_id_in_the_master_set()
        {
            long[] playerIds = [7001, 7002, 7003];
            using var context = NewContext();

            await SetMasterPlayerPoolAsync(playerIds);

            var created = await CreateLeagueWithPoolAsync(context, "Seeded From Redis");

            var rows = await context.Leagueplayers.ToListAsync();

            Assert.Equal(playerIds.Length, rows.Count);
            Assert.Equal(playerIds.Order(), rows.Select(r => r.Playerid).Order());
            Assert.All(rows, r => Assert.Equal(created.Leagueid, r.Leagueid));
            Assert.All(rows, r => Assert.True(r.Isfreeagent));
        }

        [Fact]
        public async Task Creating_a_league_falls_back_to_the_player_table_when_the_master_set_is_empty()
        {
            long[] playerIds = [7101, 7102];
            using var context = NewContext();

            // The master set is only a boot-time cache; a flushed Redis must not block league creation.
            await SetMasterPlayerPoolAsync();
            SeedPlayers(context, playerIds);
            await context.SaveChangesAsync();

            var created = await CreateLeagueWithPoolAsync(context, "Seeded From Postgres");

            var rows = await context.Leagueplayers.ToListAsync();

            Assert.Equal(playerIds.Length, rows.Count);
            Assert.Equal(playerIds.Order(), rows.Select(r => r.Playerid).Order());
            Assert.All(rows, r => Assert.Equal(created.Leagueid, r.Leagueid));
            Assert.All(rows, r => Assert.True(r.Isfreeagent));
        }

        [Fact]
        public async Task Creating_a_league_throws_before_writing_anything_when_both_sources_are_empty()
        {
            using var context = NewContext();

            await SetMasterPlayerPoolAsync();

            var ex = await Assert.ThrowsAsync<NBAException>(
                () => CreateLeagueWithPoolAsync(context, "No Players League"));

            Assert.Equal(ErrorCodes.PlayerPoolEmpty, ex.ErrorCode);

            // CreateAsync resolves the pool before its first write, so an empty pool throws with no
            // league ever inserted - nothing to compensate for. Without that ordering this would be a
            // league with no free agents, which ToggleFreeAgencyStatus can never repair.
            Assert.Empty(await context.Leagues.ToListAsync());
            Assert.Empty(await context.Statsvalues.ToListAsync());
            Assert.Empty(await context.Leagueplayers.ToListAsync());
        }

        [Fact]
        public async Task Undoing_a_league_also_removes_the_rows_a_seed_already_wrote()
        {
            using var context = NewContext();

            await SetMasterPlayerPoolAsync(7201, 7202);

            var created = await CreateLeagueWithPoolAsync(context, "Partially Seeded League");
            Assert.Equal(2, (await context.Leagueplayers.ToListAsync()).Count);

            // DeleteAsync has to clear the pool before the league: leagueplayer rows point at it, so
            // leaving them would orphan rows against a league id that no longer exists.
            await BuildLeagueService(context).DeleteAsync(created.Leagueid);

            Assert.Empty(await context.Leagues.ToListAsync());
            Assert.Empty(await context.Statsvalues.ToListAsync());
            Assert.Empty(await context.Leagueplayers.ToListAsync());
        }
    }
}
