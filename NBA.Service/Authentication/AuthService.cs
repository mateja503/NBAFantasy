using ApplicationDefaults.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NBA.Data.Context;
using NBA.Data.Entities;

namespace NBA.Service.Authentication
{
    public record LoginResult(
        Applicationuser User,
        List<NBA.Data.Entities.League> CommissionerLeagues,
        List<Team> OtherTeams);

    public class AuthService(NbaFantasyContext context, IPasswordHasher<Applicationuser> passwordHasher)
    {
        private readonly NbaFantasyContext _context = context;
        private readonly IPasswordHasher<Applicationuser> _passwordHasher = passwordHasher;

        public async Task<Applicationuser> RegisterAsync(string username, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new NBAException("Username is required", ErrorCodes.MissingValue);
            if (string.IsNullOrWhiteSpace(password))
                throw new NBAException("Password is required", ErrorCodes.MissingValue);

            var existing = await _context.GetApplicationuserByUsername(username);
            if (existing is not null)
                throw new NBAException($"Username '{username}' is already taken", ErrorCodes.UsernameAlreadyExists);

            var user = new Applicationuser
            {
                Username = username,
                Email = email,
            };
            // Never store the raw password; PasswordHasher applies a salted PBKDF2 hash.
            user.Password = _passwordHasher.HashPassword(user, password);

            return await _context.AddApplicationuser(user);
        }

        public async Task<LoginResult> LoginAsync(string username, string password)
        {
            var user = await _context.GetApplicationuserByUsername(username)
                ?? throw new NBAException("Failed To login", ErrorCodes.LoginFailed);

            await VerifyPasswordAsync(user, password);

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

        // Verifies the password against the stored hash. Existing seed users still have plaintext
        // passwords, so we transparently migrate them: if the hash check fails but the legacy
        // plaintext matches, we re-store a proper hash and let the user in. This avoids a hard
        // cutover while ensuring every successful login leaves a hashed credential behind.
        private async Task VerifyPasswordAsync(Applicationuser user, string password)
        {
            var stored = user.Password ?? string.Empty;

            PasswordVerificationResult result;
            try
            {
                result = _passwordHasher.VerifyHashedPassword(user, stored, password);
            }
            catch (Exception)
            {
                // Any failure parsing/verifying the stored value (legacy plaintext, malformed hash,
                // or a different IPasswordHasher implementation) is treated as a failed verification
                // so the migrate-on-login fallback below can take over.
                result = PasswordVerificationResult.Failed;
            }

            if (result == PasswordVerificationResult.Success)
                return;

            if (result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.Password = _passwordHasher.HashPassword(user, password);
                await _context.UpdateApplicationuser(user);
                return;
            }

            // Legacy plaintext fallback + migrate-on-login.
            if (stored == password)
            {
                user.Password = _passwordHasher.HashPassword(user, password);
                await _context.UpdateApplicationuser(user);
                return;
            }

            throw new NBAException("Failed To login", ErrorCodes.LoginFailed);
        }
    }
}
