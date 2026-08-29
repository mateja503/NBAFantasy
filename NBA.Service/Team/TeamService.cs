using ApplicationDefaults.Exceptions;
using Microsoft.EntityFrameworkCore;
using NBA.Data.Context;
using NBA.Data.Entities;
using PlayerData = NBA.Data.Entities.Player;
using TeamData = NBA.Data.Entities.Team;

namespace NBA.Service.Team
{
    public class TeamService(NbaFantasyContext context)
    {
        private readonly NbaFantasyContext _context = context;

        public async Task<TeamData> AddAsync(string? teamName)
        {
            if (string.IsNullOrEmpty(teamName))
                throw new NBAException($"{nameof(teamName)} is missing", ErrorCodes.MissingParametar);

            return await _context.AddTeam(new TeamData { Name = teamName });
        }

        public async Task<List<TeamData>> GetLeagueTeamsAsync(long leagueId)
        {
            return await _context.GetAllTeams()
                .Where(t => t.Leagueid == leagueId)
                .AsNoTracking()
                .ToListAsync();
        }

        // Every team the user owns, keyed by the team itself so the caller gets the roster without a
        // second lookup. A team with nobody drafted yet maps to an empty list, never a missing key.
        public async Task<Dictionary<TeamData, List<PlayerData>>> GetUserTeamsWithPlayersAsync(long userId)
        {
            if (userId <= 0)
                throw new NBAException($"{nameof(userId)} is missing", ErrorCodes.MissingParametar);

            var teams = await _context.GetAllTeams()
                .Where(t => t.Userid == userId)
                .Include(t => t.League)
                .Include(t => t.Teamplayers)
                    .ThenInclude(tp => tp.Player)
                .AsNoTracking()
                .ToListAsync();

            return teams.ToDictionary(
                t => t,
                t => t.Teamplayers.Select(tp => tp.Player).ToList());
        }

        // One team's roster. GetUserTeamsWithPlayersAsync only ever returns teams the caller owns, which
        // is no use to the trade board: to offer a swap you have to see the other side's players too.
        //
        // The guard is league membership rather than ownership — you may read any roster in a league you
        // play in, and none outside it.
        public async Task<List<PlayerData>> GetTeamPlayersAsync(long teamId, long userId)
        {
            if (teamId <= 0)
                throw new NBAException($"{nameof(teamId)} is missing", ErrorCodes.MissingParametar);

            // Leagueid is nullable, so a null here means either "no such team" or "team not in a
            // league". Both are the same answer to the caller: there is no roster it may read.
            var leagueId = await _context.GetAllTeams()
                .Where(t => t.Teamid == teamId)
                .Select(t => t.Leagueid)
                .FirstOrDefaultAsync()
                ?? throw new NBAException($"Team {teamId} was not found in a league.", ErrorCodes.DataBaseRecordNotFound);

            var isMember = await _context.GetAllTeams()
                .AnyAsync(t => t.Leagueid == leagueId && t.Userid == userId);

            if (!isMember)
                throw new NBAException($"User does not manage a team in league {leagueId}.", ErrorCodes.UserNotInLeague);

            return await _context.GetAllTeamPlayer()
                .Where(tp => tp.Teamid == teamId)
                .Select(tp => tp.Player)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
