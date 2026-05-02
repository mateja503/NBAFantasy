using ApplicationDefaults.Options;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NBA.Data.Context;
using NBA.Data.Entities;
using NBA.Service.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace NBA.Service.Draft
{
    public class DraftManager(IServiceScopeFactory scopeFactory, IConnectionMultiplexer redis, 
        IOptions<JsonOptions> jsonOptions, IOptions<DraftOptions> draftOptions)
    {
        private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
        private readonly IDatabase _redisDb = redis.GetDatabase();
        private readonly JsonSerializerOptions _jsonOptions = jsonOptions.Value.SerializerOptions;
        private readonly DraftOptions _draftOptions = draftOptions.Value;

        public DraftState currentState { get; private set; }
        public async Task<DraftState> CreateDraftState(long leagueId) 
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<NbaFantasyContext>();

            var leagueName = await _context.GetAllLeagues().Where(u => u.Leagueid == leagueId).Select(u => u.Name).SingleOrDefaultAsync();

            currentState = new DraftState
            {
                LeagueName = leagueName ?? "NO LEAGUE",
                IsPaused = false,
                PickEndTime = DateTime.UtcNow,
                IsDraftStarted = false,
                Round = 1,
            };

            var redisKey = RedisKeys.GetDraftStateKey(leagueId);
            await _redisDb.StringSetAsync(redisKey, JsonSerializer.Serialize(currentState, _jsonOptions));

            return currentState;
        }

        public async Task<DraftState?> GetCurrentDraftState(long leagueId) 
        {
            var redisKey = RedisKeys.GetDraftStateKey(leagueId);
            var ds = await _redisDb.StringGetAsync(redisKey);

            DraftState? state = ds.HasValue ? JsonSerializer.Deserialize<DraftState>(ds.ToString(), _jsonOptions) : null;

            return state;
        }

        public async Task ResetTimer(long leagueId, int seconds = 60) 
        {
            seconds = _draftOptions.DraftPickTime;
            var redisKey = RedisKeys.GetDraftStateKey(leagueId);
            var ds = await _redisDb.StringGetAsync(redisKey);

            DraftState? state = ds.HasValue ? JsonSerializer.Deserialize<DraftState>(ds.ToString(), _jsonOptions) : null;

            state?.PickEndTime = DateTime.UtcNow.AddSeconds(seconds);
            state?.IsPaused = false;

            await _redisDb.StringSetAsync(redisKey, JsonSerializer.Serialize(state, _jsonOptions));

        }

        public async Task SendUpdateDraftState() 
        {
        
        }

        void PauseDraft() => currentState.IsPaused = true;
    }
}
