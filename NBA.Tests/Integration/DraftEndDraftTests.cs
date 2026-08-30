using ApplicationDefaults.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using NBA.Data.Context;
using NBA.Data.Entities;
using NBA.Data.Enumerations;
using NBA.Data.Redis.Dtos;
using NBA.Data.Redis.Entities;
using NBA.Data.Redis.Keys;
using NBA.Service.Draft;
using Xunit;

namespace NBA.Tests.Integration
{
    // Verifies the draft-end flush to Postgres: DraftLifecycleService.EndDraft reads each team's drafted
    // players from the (real) Redis container and bulk-inserts them into the (InMemory) Postgres stand-in,
    // then marks the league completed. Reuses the shared Redis container from TradeHubFixture; the EF
    // context is built per test against an isolated InMemory store so EndDraft's transaction runs in
    // isolation. EndDraft also tears down the league's draft-time Redis keys and its snapshot row; both
    // tests use their own leagueId, so that clean-up cannot leak across them.
    [Collection("Trade integration")]
    public class DraftEndDraftTests
    {
        private readonly TradeHubFixture _fixture;

        public DraftEndDraftTests(TradeHubFixture fixture) => _fixture = fixture;

        private static NbaFantasyContext NewContext() =>
            new(new DbContextOptionsBuilder<NbaFantasyContext>()
                .UseInMemoryDatabase($"enddraft-{Guid.NewGuid()}")
                // InMemory has no transactions; EndDraft opens one, so silence the would-be-error warning.
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options);

        // DraftLifecycleService owns EndDraft outright now — DraftService no longer wraps it — so the
        // test builds it directly. It needs only the context, the Redis facade and the snapshot service.
        private DraftLifecycleService BuildService(NbaFantasyContext context)
        {
            var draftOptions = Options.Create(new DraftOptions { Rounds = 1, DraftPickTime = 60, ShowTeamDraftBoardCount = 1 });
            // EndDraft deletes the league's snapshot row as its last step, so this dependency is
            // exercised here rather than merely satisfied.
            var snapshot = new DraftSnapshotService(context, _fixture.Redis, draftOptions);

            return new DraftLifecycleService(context, draftOptions, _fixture.Redis, snapshot);
        }

        private static void SeedLeague(NbaFantasyContext context, long leagueId, string name, bool draftCompleted, params long[] teamIds)
        {
            context.Leagues.Add(new League
            {
                Leagueid = leagueId,
                Name = name,
                Commissioner = 1,
                Seasonyear = "2026",
                Draftcompleted = draftCompleted,
            });

            // GetLeagueTeamIds reads these rows to scope the per-team Redis clean-up, so a purge test
            // that seeded no teams would assert nothing about the per-team roster keys.
            foreach (var teamId in teamIds)
                context.Teams.Add(new Team { Teamid = teamId, Leagueid = leagueId, Name = $"Team {teamId}", Approved = true });
        }

        // The pool LeagueService.CreateAsync seeds: one row per player, every one a free agent.
        // Leagueplayerid is assigned explicitly because the InMemory store shares its key generator
        // across the whole context and two leagues seeding the same player ids would otherwise collide.
        private static void SeedLeaguePool(NbaFantasyContext context, long leagueId, params long[] playerIds)
        {
            foreach (var playerId in playerIds)
                context.Leagueplayers.Add(new Leagueplayer
                {
                    Leagueplayerid = leagueId * 1000 + playerId,
                    Leagueid = leagueId,
                    Playerid = playerId,
                    Isfreeagent = true,
                });
        }

        private static async Task<bool> IsFreeAgentAsync(NbaFantasyContext context, long leagueId, long playerId) =>
            (await context.Leagueplayers.SingleAsync(lp => lp.Leagueid == leagueId && lp.Playerid == playerId)).Isfreeagent;

        // Puts a league into the state a live draft leaves behind: draft state (carrying the rosters
        // EndDraft flushes), the remaining pick order, the available pool, the league's pick-ordered
        // drafted set, one roster set per team, an armed pick deadline and a durable snapshot row.
        private async Task SeedLiveDraftAsync(NbaFantasyContext context, long leagueId, string leagueName, IReadOnlyList<long> teamIds)
        {
            var league = _fixture.Redis.League(leagueId);

            await league.Draft.SetState(new DraftState
            {
                LeagueName = leagueName,
                DraftedPlayersPerTeam = teamIds
                    .Select((teamId, i) => (teamId, playerId: (long)(900 + i)))
                    .ToDictionary(t => t.teamId, t => new List<PlayerShortDto>
                    {
                        new() { PlayerId = t.playerId, Position = nameof(PlayerPositionEnum.G) },
                    }),
            });

            await league.Draft.SetTeams(new Dictionary<long, Queue<TeamDraftBoard>>
            {
                [1] = new(teamIds.Select(id => new TeamDraftBoard { TeamId = id, TeamName = $"Team {id}", Pick = 1 })),
            });

            await league.Players.AddAvailableDraftPlayers(
                [new PlayerShort { PlayerId = 999, FullName = "Undrafted Guy", Position = (int)PlayerPositionEnum.F }]);

            for (var i = 0; i < teamIds.Count; i++)
            {
                await league.Players.AddDraftedPlayer(900 + i, i + 1);
                await _fixture.Redis.Player.AddTeamsDrafterPlayer(teamIds[i], 900 + i);
            }

            await league.Draft.ScheduleTimer(DateTimeOffset.UtcNow.AddSeconds(60));

            await context.UpsertDraftSnapshot(new Draftsnapshot
            {
                Leagueid = leagueId,
                Draftstate = "{}",
                Draftteams = "{}",
                Tsupdated = DateTime.UtcNow,
            });
        }

        // Every draft-time key for the league, plus the armed deadline and the snapshot row, is gone.
        private async Task AssertDraftDataPurgedAsync(NbaFantasyContext context, long leagueId, IReadOnlyList<long> teamIds)
        {
            var league = _fixture.Redis.League(leagueId);

            Assert.False(await league.Draft.StateExists());
            Assert.False(await league.Draft.TeamsExist());
            Assert.False(await league.Draft.IsTimerScheduled());

            // The available pool comes back null (not empty) once its key is gone.
            Assert.Null(await league.Players.GetAvailableDraftPlayers());
            Assert.Empty(await league.Players.GetDraftedPlayers() ?? []);

            // Asserted on the raw key: GetTeamsDraftedPlayers projects through the player cache, so an
            // empty list from it would not prove the set itself was deleted.
            foreach (var teamId in teamIds)
                Assert.False(await _fixture.Database.KeyExistsAsync(RedisKeys.GetTeamsDraftedPlayersKey(teamId)));

            Assert.Null(await context.GetDraftSnapshot(leagueId));
        }

        [Fact]
        public async Task EndDraft_inserts_every_teams_redis_players_into_the_db_and_completes_the_league()
        {
            const long leagueId = 101;
            using var context = NewContext();

            context.Leagues.Add(new League
            {
                Leagueid = leagueId,
                Name = "End Draft League",
                Commissioner = 1,
                Seasonyear = "2026",
                Draftcompleted = false,
            });
            await context.SaveChangesAsync();

            // Seed the live draft rosters in Redis: 2 players for team 10, 1 for team 20.
            await _fixture.Redis.Draft.SetDraftState(leagueId, new DraftState
            {
                LeagueName = "End Draft League",
                DraftedPlayersPerTeam = new Dictionary<long, List<PlayerShortDto>>
                {
                    [10] = new() { new PlayerShortDto { PlayerId = 100, Position = nameof(PlayerPositionEnum.G) }, new PlayerShortDto { PlayerId = 101, Position = nameof(PlayerPositionEnum.C) } },
                    [20] = new() { new PlayerShortDto { PlayerId = 200, Position = nameof(PlayerPositionEnum.F) } },
                },
            });

            var service = BuildService(context);

            await service.EndDraft(leagueId);

            // Every Redis roster entry became a Teamplayer row, keyed by the team it belonged to.
            var rows = await context.Teamplayers.ToListAsync();
            Assert.Equal(3, rows.Count);
            Assert.Contains(rows, r => r.Teamid == 10 && r.Playerid == 100);
            Assert.Contains(rows, r => r.Teamid == 10 && r.Playerid == 101);
            Assert.Contains(rows, r => r.Teamid == 20 && r.Playerid == 200);

            var league = await context.Leagues.SingleAsync(l => l.Leagueid == leagueId);
            Assert.True(league.Draftcompleted);
        }

        [Fact]
        public async Task EndDraft_is_a_no_op_when_the_league_draft_is_already_completed()
        {
            const long leagueId = 102;
            using var context = NewContext();

            context.Leagues.Add(new League
            {
                Leagueid = leagueId,
                Name = "Already Done League",
                Commissioner = 1,
                Seasonyear = "2026",
                Draftcompleted = true,
            });
            await context.SaveChangesAsync();

            await _fixture.Redis.Draft.SetDraftState(leagueId, new DraftState
            {
                LeagueName = "Already Done League",
                DraftedPlayersPerTeam = new Dictionary<long, List<PlayerShortDto>>
                {
                    [30] = new() { new PlayerShortDto { PlayerId = 300, Position = nameof(PlayerPositionEnum.G) } },
                },
            });

            var service = BuildService(context);

            await service.EndDraft(leagueId);

            // The Draftcompleted == true guard skips the flush, so nothing is inserted — the Redis and
            // snapshot clean-up that follows it writes nothing to Postgres either.
            Assert.Empty(await context.Teamplayers.ToListAsync());
        }

        [Fact]
        public async Task EndDraft_purges_every_draft_time_redis_key_and_the_snapshot()
        {
            const long leagueId = 103;
            long[] teamIds = [1030, 1031];
            using var context = NewContext();

            SeedLeague(context, leagueId, "Purge League", draftCompleted: false, teamIds);
            await context.SaveChangesAsync();

            await SeedLiveDraftAsync(context, leagueId, "Purge League", teamIds);

            await BuildService(context).EndDraft(leagueId);

            // The flush ran first - the rosters are safely in Postgres before anything is deleted.
            Assert.Equal(2, (await context.Teamplayers.ToListAsync()).Count);
            Assert.True((await context.Leagues.SingleAsync(l => l.Leagueid == leagueId)).Draftcompleted);

            await AssertDraftDataPurgedAsync(context, leagueId, teamIds);
        }

        [Fact]
        public async Task EndDraft_still_purges_redis_when_the_league_draft_is_already_completed()
        {
            const long leagueId = 104;
            long[] teamIds = [1040, 1041];
            using var context = NewContext();

            SeedLeague(context, leagueId, "Already Done Purge League", draftCompleted: true, teamIds);
            await context.SaveChangesAsync();

            await SeedLiveDraftAsync(context, leagueId, "Already Done Purge League", teamIds);

            await BuildService(context).EndDraft(leagueId);

            // Draftcompleted == true means the rosters were flushed by an earlier call, so re-inserting
            // them would duplicate the Teamplayer rows - nba.teamplayer is a many-to-many join with no
            // unique constraint to reject them, which is exactly why the guard exists.
            Assert.Empty(await context.Teamplayers.ToListAsync());

            // ...but the clean-up is deliberately NOT behind that guard. This is the crash-recovery
            // path: if an earlier run committed the flush and died before the deletes, re-running
            // end-draft is the only way to clear these keys, and an early return would strand them.
            await AssertDraftDataPurgedAsync(context, leagueId, teamIds);
        }

        [Fact]
        public async Task EndDraft_called_twice_does_not_duplicate_the_teamplayer_rows()
        {
            const long leagueId = 105;
            long[] teamIds = [1050, 1051];
            using var context = NewContext();

            SeedLeague(context, leagueId, "Twice League", draftCompleted: false, teamIds);
            await context.SaveChangesAsync();

            await SeedLiveDraftAsync(context, leagueId, "Twice League", teamIds);

            var service = BuildService(context);

            await service.EndDraft(leagueId);
            // The second call finds Draftcompleted == true and must not flush again. Nothing else in
            // the codebase writes that flag, so it is the only thing standing between a repeated
            // end-draft request and a doubled roster.
            await service.EndDraft(leagueId);

            var rows = await context.Teamplayers.ToListAsync();
            Assert.Equal(2, rows.Count);
            Assert.Equal(2, rows.Select(r => (r.Teamid, r.Playerid)).Distinct().Count());

            await AssertDraftDataPurgedAsync(context, leagueId, teamIds);
        }

        [Fact]
        public async Task EndDraft_clears_the_free_agent_flag_for_drafted_players_only()
        {
            const long leagueId = 106;
            long[] teamIds = [1060, 1061];
            using var context = NewContext();

            SeedLeague(context, leagueId, "Free Agency League", draftCompleted: false, teamIds);
            // SeedLiveDraftAsync hands out 900 and 901 (900 + team index); 999 is the pool entry
            // nobody picks, so it is the control row.
            SeedLeaguePool(context, leagueId, 900, 901, 999);
            await context.SaveChangesAsync();

            await SeedLiveDraftAsync(context, leagueId, "Free Agency League", teamIds);

            await BuildService(context).EndDraft(leagueId);

            Assert.False(await IsFreeAgentAsync(context, leagueId, 900));
            Assert.False(await IsFreeAgentAsync(context, leagueId, 901));
            Assert.True(await IsFreeAgentAsync(context, leagueId, 999));
        }

        [Fact]
        public async Task EndDraft_does_not_touch_leagueplayer_rows_of_another_league()
        {
            const long leagueId = 107;
            const long otherLeagueId = 110;
            long[] teamIds = [1070, 1071];
            using var context = NewContext();

            SeedLeague(context, leagueId, "Drafting League", draftCompleted: false, teamIds);
            SeedLeaguePool(context, leagueId, 900, 901);

            // Same player ids, different league: the update is scoped by Leagueid, so this pool has to
            // come out untouched.
            SeedLeague(context, otherLeagueId, "Bystander League", draftCompleted: false);
            SeedLeaguePool(context, otherLeagueId, 900, 901);
            await context.SaveChangesAsync();

            await SeedLiveDraftAsync(context, leagueId, "Drafting League", teamIds);

            await BuildService(context).EndDraft(leagueId);

            Assert.False(await IsFreeAgentAsync(context, leagueId, 900));
            Assert.True(await IsFreeAgentAsync(context, otherLeagueId, 900));
            Assert.True(await IsFreeAgentAsync(context, otherLeagueId, 901));
        }

        [Fact]
        public async Task EndDraft_leaves_leagueplayer_untouched_when_the_league_draft_is_already_completed()
        {
            const long leagueId = 108;
            long[] teamIds = [1080, 1081];
            using var context = NewContext();

            SeedLeague(context, leagueId, "Already Done Free Agency League", draftCompleted: true, teamIds);
            SeedLeaguePool(context, leagueId, 900, 901, 999);
            await context.SaveChangesAsync();

            await SeedLiveDraftAsync(context, leagueId, "Already Done Free Agency League", teamIds);

            await BuildService(context).EndDraft(leagueId);

            // The update sits inside the Draftcompleted guard alongside the roster flush, so a re-run
            // must not become a second code path that writes half the state.
            Assert.All(await context.Leagueplayers.Where(lp => lp.Leagueid == leagueId).ToListAsync(),
                lp => Assert.True(lp.Isfreeagent));

            await AssertDraftDataPurgedAsync(context, leagueId, teamIds);
        }

        [Fact]
        public async Task EndDraft_with_no_drafted_players_leaves_every_leagueplayer_row_a_free_agent()
        {
            const long leagueId = 109;
            long[] teamIds = [1090, 1091];
            using var context = NewContext();

            SeedLeague(context, leagueId, "Empty Draft League", draftCompleted: false, teamIds);
            SeedLeaguePool(context, leagueId, 900, 901, 999);
            await context.SaveChangesAsync();

            await SeedLiveDraftAsync(context, leagueId, "Empty Draft League", teamIds);

            // Overwrite the rosters the helper seeded: a draft that ended with nobody picked.
            await _fixture.Redis.League(leagueId).Draft.SetState(new DraftState
            {
                LeagueName = "Empty Draft League",
                DraftedPlayersPerTeam = new Dictionary<long, List<PlayerShortDto>>(),
            });

            await BuildService(context).EndDraft(leagueId);

            Assert.Empty(await context.Teamplayers.ToListAsync());
            Assert.All(await context.Leagueplayers.Where(lp => lp.Leagueid == leagueId).ToListAsync(),
                lp => Assert.True(lp.Isfreeagent));

            // The rest of the teardown still runs: the league is completed and the draft-time keys go.
            Assert.True((await context.Leagues.SingleAsync(l => l.Leagueid == leagueId)).Draftcompleted);
            await AssertDraftDataPurgedAsync(context, leagueId, teamIds);
        }
    }
}
