using System.Security.Cryptography;
using System.Text;

namespace NBA.Api.Authentication
{
    // Pure, dependency-free token helpers, separated from AuthTokenIssuer so they can be unit tested
    // without Redis or a database.
    public static class RefreshTokenGenerator
    {
        // 256 bits of CSPRNG entropy, URL-safe so it survives query strings and headers.
        public static string Generate()
        {
            var bytes = RandomNumberGenerator.GetBytes(32);
            return Convert.ToBase64String(bytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }

        // Deterministic hash used as the Redis key, so clear-text tokens are never persisted.
        public static string Hash(string token)
        {
            var hash = SHA256.HashData(Encoding.UTF8.GetBytes(token));
            return Convert.ToHexString(hash);
        }
    }
}
