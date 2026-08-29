using ApplicationDefaults.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using NBA.Data.Context;
using NBA.Data.Entities;
using NBA.Data.Enumerations;
using NBA.Data.Redis.Dtos;
using NBA.Data.Redis.Entities;
using NBA.Service.Draft;
using NBA.Service.Roster;
using Xunit;

namespace NBA.Tests.Integration
{
    // Verifies the draft-end flush to Postgres: DraftService.EndDraft reads each team's drafted players
    // from the (real) Redis container and bulk-inserts them into the (InMemory) Postgres stand-in, then
    // marks the league completed. Reuses the shared Redis container from TradeHubFixture; the EF context
    // is built per test against an isolated InMemory store so EndDraft's transaction runs in isolation.
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

        // EndDraft only touches the context + Redis; the other deps are required by the ctor but unused here.
        private DraftService BuildService(NbaFantasyContext context)
        {
            var draftOptions = Options.Create(new DraftOptions { Rounds = 1, DraftPickTime = 60, ShowTeamDraftBoardCount = 1 });
            var appOptions = Options.Create(new ApplicationOptions { MaxPlayersPerTeam = 13, CenterLimit = 4 });
            var jsonOptions = Options.Create(new JsonOptions());
            var snapshot = new DraftSnapshotService(context, _fixture.Redis, draftOptions);
            var rosterValidator = new RosterValidator(appOptions);
            // EndDraft now lives on DraftLifecycleService; DraftService delegates to it, so this test
            // still exercises the same write path through the same entry point.
            var lifecycle = new DraftLifecycleService(context, draftOptions, _fixture.Redis);
            // DraftService reaches the draft-order Redis key through DraftOrderManager now; EndDraft
            // does not use it, but the ctor requires it.
            var draftOrder = new DraftOrderManager(_fixture.Redis);

            return new DraftService(context, draftOptions, appOptions, jsonOptions, draftOrder, snapshot, rosterValidator, lifecycle);
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

            // Early-out on Draftcompleted == true means nothing is inserted.
            Assert.Empty(await context.Teamplayers.ToListAsync());
        }
    }
}
