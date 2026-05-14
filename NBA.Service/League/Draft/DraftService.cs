using ApplicationDefaults.Exceptions;
using ApplicationDefaults.Options;
using Hangfire.States;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NBA.Data.Context;
using NBA.Data.Entities;
using NBA.Data.Enumerations;
using NBA.Data.Redis.Entities;
using NBA.Service.Draft;
using Polly.CircuitBreaker;
using StackExchange.Redis;
using System.Text.Json;
using PlayerData = NBA.Data.Entities.Player;

namespace NBA.Service.League.Draft
{
    public class DraftService(NbaFantasyContext context, IOptions<DraftOptions> draftOptions, 
        IOptions<ApplicationOptions> appOptions,IOptions<JsonOptions> jsonOptions,
        NbaFantasyRedis redis)
    {
        private readonly NbaFantasyContext _context = context;
        private readonly DraftOptions _draftOptions = draftOptions.Value;
        private readonly ApplicationOptions _appOptions = appOptions.Value;
        private readonly JsonSerializerOptions _jsonOptions = jsonOptions.Value.JsonSerializerOptions;
        private readonly NbaFantasyRedis _redis = redis;
        //private readonly AuctionListener auctionDraftListener = _auctionDraftListener;

        public async Task<Dictionary<long, Queue<Team>>> DraftOrder(long leagueId) 
        {
            var leagueTeams = await _context.GetAllLeagueTeam().Where(u => u.Leagueid == leagueId)
                .Include(u => u.Team)
                .Include(u => u.League)
                .OrderBy(u => Guid.NewGuid())
                .Select(u => new { u.League.Draftstyle, u.Team })
                .ToListAsync();

            var teams = leagueTeams.Select(u => u.Team).ToList();
            var draftType = leagueTeams.Select(u => u.Draftstyle).FirstOrDefault() ?? (long)DraftType.Snake;

            Dictionary<long, Queue<Team>> draft = new Dictionary<long, Queue<Team>>();

            //var redisKey = RedisKeys.GetDraftTeamsKey(leagueId);

            switch (draftType)
            {
                case (long)DraftType.Snake:

                    for (var i = 1; i <= _draftOptions.Rounds; i++) 
                    {
                        if (i % 2 == 0) draft.Add(i, new Queue<Team>(teams.AsEnumerable().Reverse()));
                        else draft.Add(i, new Queue<Team>(teams));
                    }
                    await _redis.Draft.SetDraftTeams(draft, leagueId);
                    return draft;

                case (long)DraftType.Auction:

                    draft.Add(1, new Queue<Team>(teams));
                    await _redis.Draft.SetDraftTeams(draft, leagueId);
                    return draft;
                case (long)DraftType.Linear:

                    for (var i = 1; i <= _draftOptions.Rounds; i++)
                        draft.Add(i, new Queue<Team>(teams));

                    await _redis.Draft.SetDraftTeams(draft, leagueId);
                    return draft;

                case (long)DraftType.RRR:
                    for (var i = 1; i <= _draftOptions.Rounds; i++)
                    {
                        if (i % 2 == 0 || i == 3) draft.Add(i, new Queue<Team>(teams.AsEnumerable().Reverse()));
                        else draft.Add(i, new Queue<Team>(teams));
                    }
                    await _redis.Draft.SetDraftTeams(draft, leagueId);
                    return draft;

                case (long)DraftType.Offline:
                    draft.Add(0, new Queue<Team>(teams));
                    await _redis.Draft.SetDraftTeams(draft, leagueId);
                    return draft;
                default:
                    throw new NBAException("Draft Type does not exist", ErrorCodes.EnumTypeDoesNotExist);
            }
        }

        public DraftBoardTeams? PrepareDraftBoard(Dictionary<long, Queue<Team>> teams) 
        {
            var currentRound = teams.Keys.FirstOrDefault();
            if (currentRound == 0) return null;

            var onTheClockTeam = teams[currentRound].Select(t=> new TeamDraftBoard { TeamId = t.Teamid , TeamName = t.Name}).FirstOrDefault();
            var onTheClockTeams = teams[currentRound].Select(t => new TeamDraftBoard { TeamId = t.Teamid, TeamName = t.Name }).Skip(1).Take(3).ToList();

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
                .Include(u=>u.Player)
                .ToListAsync();

            if(team.Count + 1 > _appOptions.MaxPlayersPerTeam) 
                throw new NBAException("Team has reached maximum number of players", ErrorCodes.TeamMaxPlayersReached);

            var player = await _context.GetAllPlayers().FirstOrDefaultAsync(u => u.Playerid == playerId);

            if (player is { Playerposition: (int)PlayerPositionEnum.C }) 
            {
                var countPlayerCenters = team.Count(u=>u.Player.Playerposition == (int)PlayerPositionEnum.C);

                if(countPlayerCenters + 1 > _appOptions.CenterLimit) 
                    throw new NBAException("Team has reached maximum number of centers", ErrorCodes.MaxCenterLimitReached);

            }else throw new NBAException($"Player with id {playerId} does not exist",ErrorCodes.DataBaseRecordNotFound);

            _ = await _context.AddTeamPlayer(new Teamplayer { Playerid = playerId, Teamid = teamId });
            return player;
        }

    }
}
