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
using NBA.Service.Roster;
using Polly.CircuitBreaker;
using StackExchange.Redis;
using System.Text.Json;
using PlayerData = NBA.Data.Entities.Player;

namespace NBA.Service.Draft
{
    public class DraftService(NbaFantasyContext context, IOptions<DraftOptions> draftOptions,
        IOptions<ApplicationOptions> appOptions, IOptions<JsonOptions> jsonOptions,
        DraftOrderManager draftOrder, DraftSnapshotService snapshot, RosterValidator rosterValidator)
    {
        private readonly NbaFantasyContext _context = context;
        private readonly DraftOptions _draftOptions = draftOptions.Value;
        private readonly ApplicationOptions _appOptions = appOptions.Value;
        private readonly JsonSerializerOptions _jsonOptions = jsonOptions.Value.JsonSerializerOptions;
        private readonly DraftOrderManager _draftOrder = draftOrder;
        private readonly DraftSnapshotService _snapshot = snapshot;
        private readonly RosterValidator _rosterValidator = rosterValidator;
        //private readonly AuctionListener auctionDraftListener = _auctionDraftListener;

        public async Task<Dictionary<long, Queue<TeamDraftBoard>>> DraftOrder(long leagueId)
        {
            // Recover the existing order from the durable snapshot before deciding to regenerate it.
            // Without this, a Redis flush mid-draft would fall through and reshuffle the draft order.
            await _snapshot.EnsureRehydratedAsync(leagueId);

            var draftTeams = await _draftOrder.GetTeams(leagueId);

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
                    await _draftOrder.SetTeams(leagueId, draft);
                    return draft;

                case (long)DraftType.Auction:

                    draft.Add(1, new Queue<TeamDraftBoard>(teams));
                    await _draftOrder.SetTeams(leagueId, draft);
                    return draft;
                case (long)DraftType.Linear:

                    for (var i = 1; i <= _draftOptions.Rounds; i++)
                        draft.Add(i, new Queue<TeamDraftBoard>(teams.Select(u => new TeamDraftBoard { TeamId = u.TeamId, TeamName = u.TeamName, Pick = pick++ })));

                    await _draftOrder.SetTeams(leagueId, draft);
                    return draft;

                case (long)DraftType.RRR:
                    for (var i = 1; i <= _draftOptions.Rounds; i++)
                    {
                        if (i % 2 == 0 || i == 3) draft.Add(i, new Queue<TeamDraftBoard>(teams.AsEnumerable()
                            .Select(u => new TeamDraftBoard { TeamId = u.TeamId, TeamName = u.TeamName, Pick = pick++ }).Reverse()));
                        else draft.Add(i, new Queue<TeamDraftBoard>(teams.Select(u => new TeamDraftBoard { TeamId = u.TeamId, TeamName = u.TeamName, Pick = pick++ })));
                    }
                    await _draftOrder.SetTeams(leagueId, draft);
                    return draft;

                case (long)DraftType.Offline:
                    draft.Add(0, new Queue<TeamDraftBoard>(teams));
                    await _draftOrder.SetTeams(leagueId, draft);
                    return draft;
                default:
                    throw new NBAException("Draft Type does not exist", ErrorCodes.EnumTypeDoesNotExist);
            }
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


        public async Task<bool> CheckDraftCompleted(long leagueId)
        {
            var league = await _context.GetAllLeagues().SingleOrDefaultAsync(l => leagueId == l.Leagueid);

            if (league is null)
                throw new NBAException($"Missing league with leagueId {leagueId}", ErrorCodes.DataBaseRecordNotFound);

            return league.Draftcompleted ?? false;
        }

    }
}
