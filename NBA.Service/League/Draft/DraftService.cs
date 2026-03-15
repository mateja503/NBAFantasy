using ApplicationDefaults.Exceptions;
using ApplicationDefaults.Options;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NBA.Data.Context;
using NBA.Data.Entities;
using NBA.Data.Enumerations;
using NBA.Service.Observer;
using NBA.Service.Observer.HubSignalR;
using NBA.Service.Observer.Listeners;
using PlayerData = NBA.Data.Entities.Player;

namespace NBA.Service.League.Draft
{
    public class DraftService(NbaFantasyContext context, IOptions<DraftOptions> draftOptions, 
        IOptions<ApplicationOptions> appOptions, AuctionListener _auctionDraftListener)
    {
        private readonly NbaFantasyContext _context = context;
        private readonly DraftOptions _draftOptions = draftOptions.Value;
        private readonly ApplicationOptions _appOptions = appOptions.Value;
        private readonly AuctionListener auctionDraftListener = _auctionDraftListener;

        public async Task<Dictionary<long,List<Team>>> DraftOrder(long leagueId) 
        {
            var leagueTeams = await _context.GetAllLeagueTeam().Where(u => u.Leagueid == leagueId)
                .Include(u => u.Team)
                .Include(u => u.League)
                .OrderBy(u => Guid.NewGuid())
                .Select(u => new { u.League.Draftstyle, u.Team })
                .ToListAsync();

            var teams = leagueTeams.Select(u => u.Team).ToList();
            var draftType = leagueTeams.Select(u => u.Draftstyle).FirstOrDefault() ?? (long)DraftType.Snake;

            Dictionary<long, List<Team>> draft = new Dictionary<long, List<Team>>();

            switch (draftType)
            {
                case (long)DraftType.Snake:

                    for (var i = 1; i <= _draftOptions.Rounds; i++) 
                    {
                        if (i % 2 == 0) draft.Add(i, teams.Reverse<Team>().ToList());
                        else draft.Add(i, teams);
                    }
                    return draft;

                case (long)DraftType.Auction:

                    return draft;
                case (long)DraftType.Linear:

                    for (var i = 1; i <= _draftOptions.Rounds; i++)
                        draft.Add(i, teams);

                    return draft;

                case (long)DraftType.RRR:
                    for (var i = 1; i <= _draftOptions.Rounds; i++)
                    {
                        if (i % 2 == 0 || i == 3) draft.Add(i, teams.Reverse<Team>().ToList());
                        else draft.Add(i, teams);
                    }
                    return draft;

                case (long)DraftType.Offline:
                    draft.Add(0, teams);
                    return draft;
                default:
                    throw new NBAException("Draft Type does not exist", ErrorCodes.EnumTypeDoesNotExist);
            }
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
