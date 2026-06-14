using System.Security.Claims;
using ApplicationDefaults.Exceptions;
using Microsoft.IdentityModel.JsonWebTokens;

namespace NBA.Api.Authentication
{
    public static class ClaimsPrincipalExtensions
    {
        // Reads the authenticated user id from the token's "sub" claim (with a NameIdentifier
        // fallback in case inbound claim mapping is left on). Throws if the principal is not a
        // valid authenticated user, so callers never silently fall back to a default identity.
        public static long GetUserId(this ClaimsPrincipal principal)
        {
            var value = principal.FindFirstValue(JwtRegisteredClaimNames.Sub)
                ?? principal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (long.TryParse(value, out var userId))
                return userId;

            throw new NBAException("Authenticated user id is missing or invalid", ErrorCodes.LoginFailed);
        }
    }
}
