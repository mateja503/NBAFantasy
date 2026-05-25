using ApplicationDefaults.Options;
using Hangfire;
using Hangfire.States;
using k8s.ClientSets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using NBA.Api.SignalR.Clients;
using NBA.Api.SignalR.Hubs;
using NBA.Data.Context;
using NBA.Data.Entities;
using NBA.Data.Redis.Entities;
using NBA.Service.Draft;
using NBA.Service.League.Draft;
using StreamJsonRpc;
using System.Text.Json;

namespace NBA.Api.HangFire
{
    public class DraftJobs(IHubContext<DraftHub, IDraftHubClient> hubContext, DraftManager draftManager,
        IBackgroundJobClient backgroundJobClient,  DraftService draftService, NbaFantasyRedis redis,
        IOptions<JsonOptions> jsonOptions, IOptions<DraftOptions> draftOptions)
    {
        private readonly IHubContext<DraftHub, IDraftHubClient> _hubContext = hubContext;
        private readonly DraftManager _draftManager = draftManager;
        private readonly IBackgroundJobClient _backgroundJobClient = backgroundJobClient;
        private readonly JsonSerializerOptions _jsonOptions = jsonOptions.Value.JsonSerializerOptions;
        private readonly DraftService _draftService = draftService;
        private readonly DraftOptions _draftOptions = draftOptions.Value;
        private readonly NbaFantasyRedis _redis = redis;
        public async Task StartDraft(long leagueId)
        {
            await draftService.CheckDraftCompleted(leagueId);
            await DraftCycle(leagueId);
        }
        public async Task DraftCycle(long leagueId)
        {
            var state = await _draftManager.ResetTimer(leagueId);

            await _hubContext.Clients.Group(leagueId.ToString()).UpdateDraftState(state!);

            state = await _draftManager.NextPick(state, leagueId);

            if (state!.DraftBoardTeams == null) 
            {
                await _draftService.EndDraft(leagueId);
                return;
            }

            var jobId = _backgroundJobClient.Schedule<DraftJobs>(job => job.DraftCycle(leagueId), TimeSpan.FromSeconds(_draftOptions.DraftPickTime));
            await _redis.Draft.SetDraftTimerJobId(leagueId, jobId);

        }
    }
}
