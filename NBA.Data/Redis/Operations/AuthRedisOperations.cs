using NBA.Data.Redis.Keys;
using StackExchange.Redis;

namespace NBA.Data.Redis.Operations
{
    // Refresh tokens live in Redis keyed by their hash, with a TTL equal to the token lifetime.
    // Storing only the hash means a Redis dump never exposes a usable token.
    public class AuthRedisOperations
    {
        private readonly IDatabase _redisDb;

        public AuthRedisOperations(IDatabase redis)
        {
            _redisDb = redis;
        }

        public async Task StoreRefreshToken(string tokenHash, long userId, TimeSpan ttl)
        {
            var key = RedisKeys.GetRefreshTokenKey(tokenHash);
            await _redisDb.StringSetAsync(key, userId, expiry: ttl);
        }

        // Atomically reads and deletes the token (GETDEL): a refresh token is single-use, so
        // presenting it both rotates it and prevents replay. Returns null if it doesn't exist
        // (already used, expired, or forged).
        public async Task<long?> ConsumeRefreshToken(string tokenHash)
        {
            var key = RedisKeys.GetRefreshTokenKey(tokenHash);
            var value = await _redisDb.StringGetDeleteAsync(key);
            return value.HasValue && long.TryParse(value.ToString(), out var userId) ? userId : null;
        }
    }
}
