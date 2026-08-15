using ApplicationDefaults.Exceptions;
using ApplicationDefaults.Options;
using Hangfire.States;
using MessagePack.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NBA.Data.Context;
using NBA.Data.Entities;
using NBA.Data.Enumerations;
using NBA.Data.Redis.Entities;
using NBA.Service.League.Draft;
using NBA.Service.League.Roster;
using Polly.CircuitBreaker;
using StackExchange.Redis;
using System.Text.Json;
using PlayerData = NBA.Data.Entities.Player;

namespace NBA.Service.League.Draft
{
    public class DraftService(NbaFantasyContext context, IOptions<DraftOptions> draftOptions,
        IOptions<ApplicationOptions> appOptions, IOptions<JsonOptions> jsonOptions,
        NbaFantasyRedis redis, DraftSnapshotService snapshot, RosterValidator rosterValidator)
    {
        private readonly NbaFantasyContext _context = context;
        private readonly DraftOptions _draftOptions = draftOptions.Value;
        private readonly ApplicationOptions _appOptions = appOptions.Value;
        private readonly JsonSerializerOptions _jsonOptions = jsonOptions.Value.JsonSerializerOptions;
        private readonly NbaFantasyRedis _redis = redis;
        private readonly DraftSnapshotService _snapshot = snapshot;
        private readonly RosterValidator _rosterValidator = rosterValidator;
        //private readonly AuctionListener auctionDraftListener = _auctionDraftListener;

        public async Task<Dictionary<long, Queue<TeamDraftBoard>>> DraftOrder(long leagueId)
        {
            // Recover the existing order from the durable snapshot before deciding to regenerate it.
            // Without this, a Redis flush mid-draft would fall through and reshuffle the draft order.
            await _snapshot.EnsureRehydratedAsync(leagueId);

            // Named draftRedis rather than draft — the generated order below already owns that name.
            var draftRedis = _redis.League(leagueId).Draft;

            var draftTeams = await draftRedis.GetTeams();

            if (draftTeams is not null)
                return draftTeams;


            var league = await _context.GetAllLeagues().Where(u => u.Leagueid == leagueId)
                .Include(u => u.Teams)
                .SingleOrDefaultAsync();

            if (league is null)
            {
                throw new NBAException($"Location record for id: {leagueId} not found", ErrorCodes.DataBaseRecordNotFound);
            }


            var teams = league.Teams.OrderBy(t => Guid.NewGuid())
              .Select(u => new TeamDraftBoard { TeamId = u.Teamid, TeamName = u.Name })
              .ToList();
            var draftType = league.Draftstyle ?? (long)DraftType.Snake;

            Dictionary<long, Queue<TeamDraftBoard>> draft = new Dictionary<long, Queue<TeamDraftBoard>>();
            int pick = 1;
            switch (draftType)
            {
                case (long)DraftType.Snake:

                    for (var i = 1; i <= _draftOptions.Rounds; i++)
                    {
                        if (i % 2 == 0) draft.Add(i, new Queue<TeamDraftBoard>(teams.AsEnumerable()
                            .Select(u => new TeamDraftBoard { TeamId = u.TeamId, TeamName = u.TeamName, Pick = pick++ }).Reverse()));
                        else draft.Add(i, new Queue<TeamDraftBoard>(teams.Select(u => new TeamDraftBoard { TeamId = u.TeamId, TeamName = u.TeamName, Pick = pick++ })));
                    }
                    await draftRedis.SetTeams(draft);
                    return draft;

                case (long)DraftType.Auction:

                    draft.Add(1, new Queue<TeamDraftBoard>(teams));
                    await draftRedis.SetTeams(draft);
                    return draft;
                case (long)DraftType.Linear:

                    for (var i = 1; i <= _draftOptions.Rounds; i++)
                        draft.Add(i, new Queue<TeamDraftBoard>(teams.Select(u => new TeamDraftBoard { TeamId = u.TeamId, TeamName = u.TeamName, Pick = pick++ })));

                    await draftRedis.SetTeams(draft);
                    return draft;

                case (long)DraftType.RRR:
                    for (var i = 1; i <= _draftOptions.Rounds; i++)
                    {
                        if (i % 2 == 0 || i == 3) draft.Add(i, new Queue<TeamDraftBoard>(teams.AsEnumerable()
                            .Select(u => new TeamDraftBoard { TeamId = u.TeamId, TeamName = u.TeamName, Pick = pick++ }).Reverse()));
                        else draft.Add(i, new Queue<TeamDraftBoard>(teams.Select(u => new TeamDraftBoard { TeamId = u.TeamId, TeamName = u.TeamName, Pick = pick++ })));
                    }
                    await draftRedis.SetTeams(draft);
                    return draft;

                case (long)DraftType.Offline:
                    draft.Add(0, new Queue<TeamDraftBoard>(teams));
                    await draftRedis.SetTeams(draft);
                    return draft;
                default:
                    throw new NBAException("Draft Type does not exist", ErrorCodes.EnumTypeDoesNotExist);
            }
        }

        public DraftBoardTeams? PrepareDraftBoard(Dictionary<long, Queue<TeamDraftBoard>> teams)
        {
            var currentRound = teams.Keys.FirstOrDefault();
            if (currentRound == 0) return null;

            var onTheClockTeam = teams[currentRound].Select(t => new TeamDraftBoard { TeamId = t.TeamId, TeamName = t.TeamName!, Pick = t.Pick }).FirstOrDefault();
            var onTheClockTeams = teams[currentRound].Select(t => new TeamDraftBoard { TeamId = t.TeamId, TeamName = t.TeamName!, Pick = t.Pick }).Skip(1).Take(_draftOptions.ShowTeamDraftBoardCount).ToList();

            return new DraftBoardTeams
            {
                CurrentRound = currentRound,
                onTheClockTeam = onTheClockTeam,
                DraftOrder = onTheClockTeams
            };
        }

        public async Task<PlayerData> DraftPlayer(long teamId, long playerId)
        {
            var team = await _context.GetAllTeamPlayer()
                .Where(u => u.Teamid == teamId)
                .Include(u => u.Player)
                .ToListAsync();

            var player = await _context.GetAllPlayers().FirstOrDefaultAsync(u => u.Playerid == playerId);

            // Previously this was an `else` on the position pattern below, so drafting any non-center
            // reported "does not exist". Existence is its own check.
            if (player is null)
                throw new NBAException($"Player with id {playerId} does not exist", ErrorCodes.DataBaseRecordNotFound);

            // Both league limits now live in RosterValidator, shared with the trade and free-agency
            // paths. Counts describe the roster as it would look with this pick applied.
            var centerCount = team.Count(u => u.Player.Playerposition == (int)PlayerPositionEnum.C);

            if (player.Playerposition == (int)PlayerPositionEnum.C)
                centerCount++;

            _rosterValidator.Validate(team.Count + 1, centerCount);

            _ = await _context.AddTeamPlayer(new Teamplayer { Playerid = playerId, Teamid = teamId });
            return player;
        }


        public async Task EndDraft(long leagueId)
        {
            var league = await _context.GetAllLeagues().SingleOrDefaultAsync(l => leagueId == l.Leagueid)
                    ?? throw new NBAException($"Missing league with leagueId {leagueId}", ErrorCodes.DataBaseRecordNotFound);

            if (league.Draftcompleted == true) return;

            var draftedPerTeam = await _redis.League(leagueId).Draft.GetAllTeamsDraftedPlayers();
            var teamPlayers = draftedPerTeam
                .SelectMany(kvp => kvp.Value.Select(p => new Teamplayer { Teamid = kvp.Key, Playerid = p.PlayerId ?? 0 }))
                .ToList();


            var strategy = _context.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using var tx = await _context.Database.BeginTransactionAsync();
                try
                {
                    if (teamPlayers.Count > 0)
                        await _context.AddTeamPlayerRange(teamPlayers);

                    league.Draftcompleted = true;
                    await _context.UpdateLeague(league);

                    await tx.CommitAsync();
                }
                catch
                {
                    await tx.RollbackAsync();
                    throw;
                }
            });
        }


        public async Task<bool> CheckDraftCompleted(long leagueId)
        {
            var league = await _context.GetAllLeagues().SingleOrDefaultAsync(l => leagueId == l.Leagueid);

            if (league is null)
                throw new NBAException($"Missing league with leagueId {leagueId}", ErrorCodes.DataBaseRecordNotFound);

            return league.Draftcompleted ?? false;
        }

    }
}
