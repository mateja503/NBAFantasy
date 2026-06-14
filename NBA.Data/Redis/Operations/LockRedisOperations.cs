using StackExchange.Redis;

namespace NBA.Data.Redis.Operations
{
    // Reusable distributed lock for cross-replica coordination (e.g. "only one instance should run
    // startup seeding"). Backed by Redis SET NX + a random token so a lock is only released by its
    // owner. Returns a token on success, null if the lock is already held.
    public class LockRedisOperations(IDatabase redis)
    {
        private readonly IDatabase _redisDb = redis;

        public async Task<string?> TryAcquire(string key, TimeSpan expiry)
        {
            var token = Guid.NewGuid().ToString();
            var acquired = await _redisDb.LockTakeAsync(key, token, expiry);
            return acquired ? token : null;
        }

        // LockRelease only deletes the key if the token matches, so we never release a lock that
        // has since expired and been re-acquired by another instance.
        public Task Release(string key, string token) => _redisDb.LockReleaseAsync(key, token);
    }
}
