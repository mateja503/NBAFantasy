using ApplicationDefaults.Exceptions;
using Microsoft.EntityFrameworkCore;
using NBA.Data.Context;
using NBA.Data.Entities;

namespace NBA.Service.Authentication
{
    public record LoginResult(
        Applicationuser User,
        List<NBA.Data.Entities.League> CommissionerLeagues,
        List<Team> OtherTeams);

    public class AuthService(NbaFantasyContext context)
    {
        private readonly NbaFantasyContext _context = context;

        public async Task<LoginResult> LoginAsync(string username, string password)
        {
            // SECURITY TODO (follow-up): passwords are still compared in plaintext here, matching
            // the current seed data. The planned JWT + PasswordHasher work replaces this with a
            // hashed verification and token issuance. See REFACTOR_NOTES.md.
            var user = await _context.GetApplicationuser(username, password)
                ?? throw new NBAException("Failed To login", ErrorCodes.LoginFailed);

            // Leagues the user commissions and also fields a team in. Filtered Include loads only
            // the user's own team so the caller can surface it as the commissioner's team.
            var commissionerLeagues = await _context.GetAllLeagues()
                .Where(l => l.Commissioner == user.Userid && l.Teams.Any(t => t.Userid == user.Userid))
                .Include(l => l.Teams.Where(t => t.Userid == user.Userid))
                .AsNoTracking()
                .ToListAsync();

            var commissionerTeamIds = commissionerLeagues
                .SelectMany(l => l.Teams)
                .Select(t => t.Teamid)
                .ToList();

            var otherTeams = await _context.GetAllTeams()
                .Where(t => t.Userid == user.Userid && !commissionerTeamIds.Contains(t.Teamid))
                .Include(t => t.League)
                .AsNoTracking()
                .ToListAsync();

            return new LoginResult(user, commissionerLeagues, otherTeams);
        }
    }
}
