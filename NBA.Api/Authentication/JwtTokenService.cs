using System.Security.Claims;
using System.Text;
using ApplicationDefaults.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using NBA.Data.Entities;

namespace NBA.Api.Authentication
{
    // Issues short-lived HS256 access tokens. The symmetric signing key is injected from
    // configuration (user-secrets/env). For multiple independently-deployed validators,
    // switching to asymmetric RS256 (private key signs, public key validates) is the next step.
    public class JwtTokenService(IOptions<JwtOptions> options) : ITokenService
    {
        private readonly JwtOptions _options = options.Value;

        public AuthToken CreateToken(Applicationuser user)
        {
            if (string.IsNullOrWhiteSpace(_options.SigningKey))
                throw new InvalidOperationException("Jwt:SigningKey is not configured.");

            var expiresAt = DateTime.UtcNow.AddMinutes(_options.AccessTokenMinutes);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SigningKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Userid.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            });

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = expiresAt,
                Issuer = _options.Issuer,
                Audience = _options.Audience,
                SigningCredentials = credentials,
            };

            var token = new JsonWebTokenHandler().CreateToken(descriptor);
            return new AuthToken(token, expiresAt);
        }
    }
}
