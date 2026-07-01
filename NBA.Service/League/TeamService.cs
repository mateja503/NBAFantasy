using ApplicationDefaults.Exceptions;
using Microsoft.EntityFrameworkCore;
using NBA.Data.Context;
using NBA.Data.Entities;

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
    }
}
