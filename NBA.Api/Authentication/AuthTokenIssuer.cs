using ApplicationDefaults.Exceptions;
using ApplicationDefaults.Options;
using Microsoft.Extensions.Options;
using NBA.Data.Context;
using NBA.Data.Entities;

namespace NBA.Api.Authentication
{
    public record TokenPair(string AccessToken, string RefreshToken, DateTime AccessTokenExpiresAtUtc);

    // Orchestrates the access-token + refresh-token pair. Lives in the API layer because it needs
    // the JWT signer (ITokenService) together with the Redis-backed refresh store. Refresh tokens
    // are opaque random strings, hashed before storage and single-use (rotated on every refresh).
    public class AuthTokenIssuer(ITokenService tokenService, NbaFantasyRedis redis,
        NbaFantasyContext context, IOptions<JwtOptions> options)
    {
        private readonly ITokenService _tokenService = tokenService;
        private readonly NbaFantasyRedis _redis = redis;
        private readonly NbaFantasyContext _context = context;
        private readonly JwtOptions _options = options.Value;

        public async Task<TokenPair> IssueAsync(Applicationuser user)
        {
            var access = _tokenService.CreateToken(user);

            var refreshToken = RefreshTokenGenerator.Generate();
            var ttl = TimeSpan.FromDays(_options.RefreshTokenDays);
            await _redis.Auth.StoreRefreshToken(RefreshTokenGenerator.Hash(refreshToken), user.Userid, ttl);

            return new TokenPair(access.AccessToken, refreshToken, access.ExpiresAtUtc);
        }

        public async Task<TokenPair> RefreshAsync(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new NBAException("Refresh token is required", ErrorCodes.MissingValue);

            // GETDEL: consuming the old token here is what enforces single-use rotation.
            var userId = await _redis.Auth.ConsumeRefreshToken(RefreshTokenGenerator.Hash(refreshToken))
                ?? throw new NBAException("Invalid or expired refresh token", ErrorCodes.LoginFailed);

            var user = await _context.GetApplicationuserById(userId)
                ?? throw new NBAException("Invalid or expired refresh token", ErrorCodes.LoginFailed);

            return await IssueAsync(user);
        }

        public Task RevokeAsync(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                return Task.CompletedTask;

            // Deleting (consuming) the token is the logout — no new pair is issued.
            return _redis.Auth.ConsumeRefreshToken(RefreshTokenGenerator.Hash(refreshToken));
        }
    }
}
