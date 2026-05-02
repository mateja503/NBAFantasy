using Hangfire;
using k8s.ClientSets;
using Microsoft.AspNetCore.SignalR;
using NBA.Api.SignalR.Clients;
using NBA.Api.SignalR.Hubs;
using NBA.Service.Draft;
using NBA.Service.Redis;
using StackExchange.Redis;

namespace NBA.Api.HangFire
{
    public class DraftJobs(IHubContext<DraftHub, IDraftHubClient> hubContext, DraftManager draftManager, IBackgroundJobClient backgroundJobClient, IConnectionMultiplexer redis)
    {
        private readonly IHubContext<DraftHub, IDraftHubClient> _hubContext = hubContext;
        private readonly DraftManager _draftManager = draftManager;
        private readonly IBackgroundJobClient _backgroundJobClient = backgroundJobClient;
        private readonly IDatabase _redisDb = redis.GetDatabase();
        public async Task StartDraft(long leagueId) 
        {
            await ResetTimerLoop(leagueId);
        }
        public async Task ResetTimerLoop(long leagueId) 
        {
            await _draftManager.ResetTimer(leagueId);

            var state = await _draftManager.GetCurrentDraftState(leagueId) ?? await _draftManager.CreateDraftState(leagueId);

            await _hubContext.Clients.Group(leagueId.ToString()).UpdateDraftState(state);

            var jobId =_backgroundJobClient.Schedule<DraftJobs>(job => job.ResetTimerLoop(leagueId), TimeSpan.FromSeconds(60));

            var redisKey = RedisKeys.GetStartDraftTimerJobIdKey(leagueId);
            await _redisDb.StringSetAsync(redisKey, jobId);
        }
    }
}
