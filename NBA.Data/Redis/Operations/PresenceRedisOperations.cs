using NBA.Data.Redis.Keys;
using StackExchange.Redis;

namespace NBA.Data.Redis.Operations
{
    // Tracks which SignalR connections a team currently has open on the trade screen. SignalR itself
    // exposes no "is this group non-empty" or "who is connected" API, and client results address a
    // single connection id, so the ids have to be recorded somewhere the API can read them back.
    //
    // The set is best-effort: a process killed without a clean disconnect leaves its id behind. That
    // is tolerable because a stale id only costs one failed invoke, and callers evict ids that throw
    // (see TradeManager.IsTeamOnTradeScreen). The key TTL is a backstop for the same problem.
    public class PresenceRedisOperations(IDatabase database)
    {
        private readonly IDatabase _redisDb = database;

        // Long enough to outlive any realistic session, short enough that an abandoned key does not
        // live forever. Refreshed on every connect.
        private static readonly TimeSpan PresenceTtl = TimeSpan.FromHours(12);

        public async Task AddTradeConnection(long teamId, string connectionId)
        {
            if (string.IsNullOrWhiteSpace(connectionId)) return;

            var key = RedisKeys.GetTeamTradeConnectionsKey(teamId);

            await _redisDb.SetAddAsync(key, connectionId);
            await _redisDb.KeyExpireAsync(key, PresenceTtl);
        }

        public Task<bool> RemoveTradeConnection(long teamId, string connectionId)
        {
            if (string.IsNullOrWhiteSpace(connectionId)) return Task.FromResult(false);

            return _redisDb.SetRemoveAsync(RedisKeys.GetTeamTradeConnectionsKey(teamId), connectionId);
        }

        public async Task<List<string>> GetTradeConnections(long teamId)
        {
            var members = await _redisDb.SetMembersAsync(RedisKeys.GetTeamTradeConnectionsKey(teamId));

            return members
                .Where(m => !m.IsNull)
                .Select(m => m.ToString())
                .Where(id => !string.IsNullOrWhiteSpace(id))
                .ToList();
        }
    }
}
