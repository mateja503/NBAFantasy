using ApplicationDefaults.Options;
using Hangfire;
using k8s.ClientSets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using NBA.Api.SignalR.Clients;
using NBA.Api.SignalR.Hubs;
using NBA.Data.Entities;
using NBA.Service.Draft;
using NBA.Service.League.Draft;
using NBA.Service.Redis;
using StackExchange.Redis;
using System.Text.Json;

namespace NBA.Api.HangFire
{
    public class DraftJobs(IHubContext<DraftHub, IDraftHubClient> hubContext, DraftManager draftManager,
        IBackgroundJobClient backgroundJobClient, IConnectionMultiplexer redis, DraftService draftService,
        IOptions<JsonOptions> jsonOptions, IOptions<DraftOptions> draftOptions)
    {
        private readonly IHubContext<DraftHub, IDraftHubClient> _hubContext = hubContext;
        private readonly DraftManager _draftManager = draftManager;
        private readonly IBackgroundJobClient _backgroundJobClient = backgroundJobClient;
        private readonly IDatabase _redisDb = redis.GetDatabase();
        private readonly JsonSerializerOptions _jsonOptions = jsonOptions.Value.JsonSerializerOptions;
        private readonly DraftService _draftService = draftService;
        private readonly DraftOptions _draftOptions = draftOptions.Value;
        public async Task StartDraft(long leagueId)
        {
            await ResetTimerLoop(leagueId);
            await StartPick(leagueId);
        }
        public async Task ResetTimerLoop(long leagueId)
        {
            await _draftManager.ResetTimer(leagueId);

            var state = await _draftManager.GetCurrentDraftState(leagueId) ?? await _draftManager.CreateDraftState(leagueId);

            if (!state.IsDraftStarted)
            {
                state.IsDraftStarted = true;
                var redisStateKey = RedisKeys.GetDraftStateKey(leagueId);
                await _redisDb.StringSetAsync(redisStateKey, JsonSerializer.Serialize(state, _jsonOptions));
            }

            await _hubContext.Clients.Group(leagueId.ToString()).UpdateDraftState(state);


            var jobId = _backgroundJobClient.Schedule<DraftJobs>(job => job.ResetTimerLoop(leagueId), TimeSpan.FromSeconds(_draftOptions.DraftPickTime));

            var redisKey = RedisKeys.GetStartDraftTimerJobIdKey(leagueId);
            await _redisDb.StringSetAsync(redisKey, jobId);
        }

        public async Task StartPick(long leagueId)
        {
            var redisKey = RedisKeys.GetDraftTeamsKey(leagueId);
            var value = await _redisDb.StringGetAsync(redisKey);

            if (value.IsNull)
            {
                await _draftService.DraftOrder(leagueId);
                value = await _redisDb.StringGetAsync(redisKey);
            }
            var state = await _draftManager.GetCurrentDraftState(leagueId) ?? await _draftManager.CreateDraftState(leagueId);
            var deserializedTeams = JsonSerializer.Deserialize<Dictionary<long, Queue<Team>>>(value.ToString(), _jsonOptions);

            //TODO when to chenge the value of the ROUND in the draftState?
            Team? teamToPick = null;
            if (deserializedTeams!.TryGetValue(state.Round ?? 1, out var teams)) 
            {
                teamToPick = teams.Dequeue();
                string updatedJson = JsonSerializer.Serialize(deserializedTeams, _jsonOptions);
                await _redisDb.StringSetAsync(redisKey, updatedJson);
            }

            state.TeamName = teamToPick!.Name;
            state.TeamId = teamToPick.Teamid;

            var redisStateKey = RedisKeys.GetDraftStateKey(leagueId);
            await _redisDb.StringSetAsync(redisStateKey, JsonSerializer.Serialize(state, _jsonOptions));

            await _hubContext.Clients.Group(leagueId.ToString()).UpdateDraftState(state);

            var jobId = _backgroundJobClient.Schedule<DraftJobs>(job => job.StartPick(leagueId), TimeSpan.FromSeconds(_draftOptions.DraftPickTime));

        }
    }
}
