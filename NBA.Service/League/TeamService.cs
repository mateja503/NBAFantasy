using ApplicationDefaults.Exceptions;
using Microsoft.EntityFrameworkCore;
using NBA.Data.Context;
using NBA.Data.Entities;
using PlayerData = NBA.Data.Entities.Player;

namespace NBA.Service.League
{
    public class TeamService(NbaFantasyContext context)
    {
        private readonly NbaFantasyContext _context = context;

        public async Task<Team> AddAsync(string? teamName)
        {
            if (string.IsNullOrEmpty(teamName))
                throw new NBAException($"{nameof(teamName)} is missing", ErrorCodes.MissingParametar);

            return await _context.AddTeam(new Team { Name = teamName });
        }

        public async Task<List<Team>> GetLeagueTeamsAsync(long leagueId)
        {
            return await _context.GetAllTeams()
                .Where(t => t.Leagueid == leagueId)
                .AsNoTracking()
                .ToListAsync();
        }

        // Every team the user owns, keyed by the team itself so the caller gets the roster without a
        // second lookup. A team with nobody drafted yet maps to an empty list, never a missing key.
        public async Task<Dictionary<Team, List<PlayerData>>> GetUserTeamsWithPlayersAsync(long userId)
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
    }
}
