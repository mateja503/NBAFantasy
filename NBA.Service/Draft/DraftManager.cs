using ApplicationDefaults.Options;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NBA.Data.Context;
using NBA.Data.Entities;
using NBA.Data.Redis.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace NBA.Service.Draft
{
    public class DraftManager(IServiceScopeFactory scopeFactory, 
        IOptions<JsonOptions> jsonOptions, IOptions<DraftOptions> draftOptions, NbaFantasyRedis redis)
    {
        private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
        private readonly JsonSerializerOptions _jsonOptions = jsonOptions.Value.SerializerOptions;
        private readonly DraftOptions _draftOptions = draftOptions.Value;
        private readonly NbaFantasyRedis _redis = redis;
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
            await _redis.SetDraftState(leagueId, currentState);

            return currentState;
        }
      
        public async Task ResetTimer(long leagueId, int seconds = 60) 
        {
            var state = await _redis.GetCurrentDraftState(leagueId);
            seconds = _draftOptions.DraftPickTime;
            state?.PickEndTime = DateTime.UtcNow.AddSeconds(seconds);
            state?.IsPaused = false;
            await _redis.SetDraftState(leagueId, state!);
        }

        public async Task SendUpdateDraftState() 
        {
        
        }

        void PauseDraft() => currentState.IsPaused = true;
    }
}
