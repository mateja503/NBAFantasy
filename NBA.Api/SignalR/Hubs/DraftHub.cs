using ApplicationDefaults.Options;
using Hangfire;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using NBA.Api.HangFire;
using NBA.Api.SignalR.Clients;
using NBA.Data.Context;
using NBA.Data.Entities;
using NBA.Data.Redis.Entities;
using NBA.Service.League.Draft;
using NBA.Service.Player;
using Pipelines.Sockets.Unofficial;
using StackExchange.Redis;
using StreamJsonRpc;

namespace NBA.Api.SignalR.Hubs
{
    public class DraftHub(DraftManager draftManager, NbaFantasyRedis redis,
        IBackgroundJobClient backgroundJobClient, PlayerManager playerManager,
        DraftService draftService, IOptions<DraftOptions> draftOptions) : Hub<IDraftHubClient>
    {
        private readonly DraftManager _draftManager = draftManager;
        private readonly NbaFantasyRedis _redis = redis;
        private readonly IBackgroundJobClient _backgroundJobClient = backgroundJobClient;
        private readonly DraftService _draftService = draftService;
        private readonly PlayerManager _playerManager = playerManager;
        private readonly DraftOptions _draftOptions = draftOptions.Value;
        // 1. Send state to a user the moment they connect/refresh
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var leagueIdString = long.TryParse(httpContext?.Request.Query["leagueId"], out long leagueId);

            await _draftService.CheckDraftCompleted(leagueId);

            if (leagueIdString)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, leagueId.ToString());
            }

            var state = await _draftManager.GetDraftState(leagueId) ?? await _draftManager.CreateDraftState(leagueId);

            var draft = await _draftService.DraftOrder(leagueId);
            state.DraftBoardTeams = _draftService.PrepareDraftBoard(draft);



            await _draftManager.UpdaterDraftState(leagueId, state);

            await Clients.Caller.UpdateDraftState(state!);
            await base.OnConnectedAsync();
        }
        // 2. Action called by the Commissioner refresh the timer for user that requested it
        public async Task<DraftState> ResetTimer(long leagueId)
        {
            await _draftManager.ResetTimer(leagueId);

            var state = await _redis.Draft.GetCurrentDraftState(leagueId) ?? await _draftManager.CreateDraftState(leagueId);

            await Clients.Group(leagueId.ToString()).UpdateDraftState(state);

            var jobId = await _redis.Draft.GetDeleteDraftTimerJobId(leagueId);

            if (!string.IsNullOrEmpty(jobId))
                _backgroundJobClient.Delete(jobId);

            return state;
        }


        public async Task DraftPlayer(long leagueid, long playerid, int pick) 
        {
            await _playerManager.AddDraftedPlayers(leagueid, playerid, pick);

            var state = await _draftManager.ResetTimer(leagueid);

            await _draftManager.NextPick(state, leagueid);

            await Clients.Group(leagueid.ToString()).UpdateDraftState(state);

            var jobId = await _redis.Draft.GetDeleteDraftTimerJobId(leagueid);

            if (!string.IsNullOrEmpty(jobId))
                _backgroundJobClient.Delete(jobId);

            jobId = _backgroundJobClient.Schedule<DraftJobs>(job => job.StartDraft(leagueid), TimeSpan.FromSeconds(_draftOptions.DraftPickTime));
            await _redis.Draft.SetDraftTimerJobId(leagueid, jobId);
        }


    }
}
