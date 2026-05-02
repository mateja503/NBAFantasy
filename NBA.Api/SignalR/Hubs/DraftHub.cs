using Hangfire;
using Microsoft.AspNetCore.SignalR;
using NBA.Api.SignalR.Clients;
using NBA.Data.Context;
using NBA.Service.Draft;
using NBA.Service.Redis;
using StackExchange.Redis;

namespace NBA.Api.SignalR.Hubs
{
    public class DraftHub(DraftManager draftManager, IConnectionMultiplexer redis, IBackgroundJobClient backgroundJobClient) : Hub<IDraftHubClient>
    {
        private readonly DraftManager _draftManager = draftManager;
        private readonly IDatabase _redisDb = redis.GetDatabase();
        private readonly IBackgroundJobClient _backgroundJobClient= backgroundJobClient;

        // 1. Send state to a user the moment they connect/refresh
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var leagueIdString = long.TryParse(httpContext?.Request.Query["leagueId"],out long leagueId);

            if (leagueIdString)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, leagueId.ToString());
            }

            var state = await _draftManager.GetCurrentDraftState(leagueId)
                     ?? await _draftManager.CreateDraftState(leagueId);

            await Clients.Caller.UpdateDraftState(state!);
            await base.OnConnectedAsync();
        }
        // 2. Action called by the Commissioner refresh the timer for user that requested it
        public async Task<DraftState> ResetTimer(long leagueId) 
        {
            await _draftManager.ResetTimer(leagueId);

            var state = await _draftManager.GetCurrentDraftState(leagueId) ?? await _draftManager.CreateDraftState(leagueId);

            await Clients.Group(leagueId.ToString()).UpdateDraftState(state);

            var redisKey = RedisKeys.GetDraftTimerJobIdKey(leagueId);

            var jobId = await _redisDb.StringGetDeleteAsync(redisKey);

            if (!jobId.IsNull)
                _backgroundJobClient.Delete(jobId.ToString());
            
            return state;
        }


    }
}
