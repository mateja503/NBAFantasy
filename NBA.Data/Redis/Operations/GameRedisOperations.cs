using NBA.Data.Redis.Entities;
using NBA.Data.Redis.Keys;
using StackExchange.Redis;
using System.Text.Json;

namespace NBA.Data.Redis.Operations
{
    // Caches the public NBA schedule. This exists to shield the balldontlie quota: GET /v1/games is
    // anonymous, so every signed-out visitor landing on the dashboard would otherwise trigger an
    // outbound call through a pipeline that permits one concurrent request.
    public class GameRedisOperations(IDatabase database, JsonSerializerOptions jsonOptions)
    {
        private readonly IDatabase _redisDb = database;
        private readonly JsonSerializerOptions _jsonOptions = jsonOptions;

        public async Task SetScheduledGames(string nbaDate, ScheduledGames games, TimeSpan ttl)
        {
            var redisKey = RedisKeys.GetScheduledGamesKey(nbaDate);

            await _redisDb.StringSetAsync(redisKey, JsonSerializer.Serialize(games, _jsonOptions), expiry: ttl);
        }

        public async Task<ScheduledGames?> GetScheduledGames(string nbaDate)
        {
            var redisKey = RedisKeys.GetScheduledGamesKey(nbaDate);

            var cached = await _redisDb.StringGetAsync(redisKey);
            if (!cached.HasValue)
            {
                return null;
            }

            try
            {
                return JsonSerializer.Deserialize<ScheduledGames>(cached.ToString(), _jsonOptions);
            }
            catch (JsonException)
            {
                // A snapshot written by an older shape must not break the page — treat it as a miss
                // and let the caller refetch, exactly like TradeRedisOperations does for its members.
                return null;
            }
        }
    }
}
