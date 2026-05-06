using NBA.Data.Entities;
using NBA.Data.Redis.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace NBA.Data.Redis.Operations
{
    public class DraftRedisOperations
    {
        private readonly IDatabase _redisDb;
        private readonly JsonSerializerOptions _jsonOptions;
        public DraftRedisOperations(IDatabase redis, JsonSerializerOptions jsonOptions)
        {
            _redisDb = redis;
            _jsonOptions = jsonOptions;
        }

        public async Task<DraftState?> GetCurrentDraftState(long leagueId)
        {
            var redisKey = RedisKeys.GetDraftStateKey(leagueId);
            var ds = await _redisDb.StringGetAsync(redisKey);
            DraftState? state = ds.HasValue ? JsonSerializer.Deserialize<DraftState>(ds.ToString(), _jsonOptions) : null;
            return state;
        }

        public async Task SetDraftState(long leagueId, DraftState state)
        {
            var redisKey = RedisKeys.GetDraftStateKey(leagueId);
            await _redisDb.StringSetAsync(redisKey, JsonSerializer.Serialize(state, _jsonOptions));
        }

        public async Task<DraftState?> DeleteStringDraftState(long leagueId)
        {
            var redisKey = RedisKeys.GetDraftStateKey(leagueId);
            var value = await _redisDb.StringGetDeleteAsync(redisKey);
            DraftState? state = value.HasValue ? JsonSerializer.Deserialize<DraftState>(value.ToString(), _jsonOptions) : null;
            return state;
        }

        public async Task<string?> GetStartDraftTimerJobId(long leagueId)
        {
            var redisKey = RedisKeys.GetStartDraftTimerJobIdKey(leagueId);
            var jobId = await _redisDb.StringGetAsync(redisKey);
            return jobId.HasValue ? jobId.ToString() : null;
        }

        public async Task<string?> GetDeleteDraftTimerJobId(long leagueId)
        {
            var redisKey = RedisKeys.GetStartDraftTimerJobIdKey(leagueId);
            var jobId = await _redisDb.StringGetDeleteAsync(redisKey);
            return jobId.HasValue ? jobId.ToString() : null;
        }

        public async Task SetDraftTimerJobId(long leagueId, string jobId)
        {
            var redisKey = RedisKeys.GetStartDraftTimerJobIdKey(leagueId);
            await _redisDb.StringSetAsync(redisKey, jobId);
        }

        public async Task SetDraftTeams(Dictionary<long, Queue<Team>> draft, long leagueId)
        {
            var redisKey = RedisKeys.GetDraftTeamsKey(leagueId);
            await _redisDb.StringSetAsync(redisKey, JsonSerializer.Serialize(draft, _jsonOptions));

        }

        public async Task DeleteDraftTeams(long leagueId)
        {
            var redisKey = RedisKeys.GetDraftTeamsKey(leagueId);
            _ = await _redisDb.StringGetDeleteAsync(redisKey);
        }

        public async Task<Dictionary<long, Queue<Team>>?> GetDraftTeams(long leagueId)
        {
            var redisKey = RedisKeys.GetDraftTeamsKey(leagueId);
            var value = await _redisDb.StringGetAsync(redisKey);
            return value.HasValue ? JsonSerializer.Deserialize<Dictionary<long, Queue<Team>>>(value.ToString(), _jsonOptions) : null;
        }

        public async Task SetStartPickJobId(long leagueId, string jobId)
        {
            var redisKey = RedisKeys.GetStartPickJobIdKey(leagueId);
            await _redisDb.StringSetAsync(redisKey, jobId);
        }

        public async Task<string?> GetDeleteStartPickJobId(long leagueId)
        {
            var redisKey = RedisKeys.GetStartPickJobIdKey(leagueId);
            return await _redisDb.StringGetDeleteAsync(redisKey);
        }
    }
}
