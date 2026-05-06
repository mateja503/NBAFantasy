using Hangfire;
using Microsoft.AspNetCore.SignalR;
using NBA.Api.SignalR.Clients;
using NBA.Data.Context;
using NBA.Data.Redis.Entities;
using NBA.Service.Draft;
using NBA.Service.League.Draft;
using StackExchange.Redis;

namespace NBA.Api.SignalR.Hubs
{
    public class DraftHub(DraftManager draftManager, NbaFantasyRedis redis, IBackgroundJobClient backgroundJobClient, DraftService draftService) : Hub<IDraftHubClient>
    {
        private readonly DraftManager _draftManager = draftManager;
        private readonly NbaFantasyRedis _redis = redis;
        private readonly IBackgroundJobClient _backgroundJobClient= backgroundJobClient;
        private readonly DraftService _draftService = draftService;
        // 1. Send state to a user the moment they connect/refresh
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var leagueIdString = long.TryParse(httpContext?.Request.Query["leagueId"],out long leagueId);

            if (leagueIdString)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, leagueId.ToString());
            }

            var state = await _redis.Draft.GetCurrentDraftState(leagueId)
                     ?? await _draftManager.CreateDraftState(leagueId);

            await _draftService.DraftOrder(leagueId);
            await Clients.Caller.UpdateDraftState(state!);
            await base.OnConnectedAsync();
        }
        // 2. Action called by the Commissioner refresh the timer for user that requested it
        public async Task<DraftState> ResetTimer(long leagueId) 
        {
            await _draftManager.ResetTimer(leagueId);

            var state = await _redis.Draft.GetCurrentDraftState(leagueId) ?? await _draftManager.CreateDraftState(leagueId);

            await Clients.Group(leagueId.ToString()).UpdateDraftState(state);

            var redisKey = RedisKeys.GetStartDraftTimerJobIdKey(leagueId);

            var jobId = await _redis.Draft.GetDeleteDraftTimerJobId(leagueId);

            if (!string.IsNullOrEmpty(jobId))
                _backgroundJobClient.Delete(jobId);


            return state;
        }


    }
}
