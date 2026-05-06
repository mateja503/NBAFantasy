using ApplicationDefaults.Options;
using Hangfire;
using k8s.ClientSets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using NBA.Api.SignalR.Clients;
using NBA.Api.SignalR.Hubs;
using NBA.Data.Context;
using NBA.Data.Entities;
using NBA.Service.Draft;
using NBA.Service.League.Draft;
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
            await ResetTimerLoop(leagueId);
            await StartPick(leagueId);
        }
        public async Task ResetTimerLoop(long leagueId)
        {
            await _draftManager.ResetTimer(leagueId);

            var state = await _redis.GetCurrentDraftState(leagueId) ?? await _draftManager.CreateDraftState(leagueId);

            if (!state.IsDraftStarted)
            {
                state.IsDraftStarted = true;
                await _redis.SetDraftState(leagueId, state);
            }

            await _hubContext.Clients.Group(leagueId.ToString()).UpdateDraftState(state);

            var jobId = _backgroundJobClient.Schedule<DraftJobs>(job => job.ResetTimerLoop(leagueId), TimeSpan.FromSeconds(_draftOptions.DraftPickTime));
            await _redis.SetDraftTimerJobId(leagueId, jobId);

        }

        public async Task StartPick(long leagueId)
        {
            var draftTeams = await _redis.GetDraftTeams(leagueId);

            if (draftTeams is null)
            {
                await _draftService.DraftOrder(leagueId);
                draftTeams = await _redis.GetDraftTeams(leagueId);
            }
            var state = await _redis.GetCurrentDraftState(leagueId) ?? await _draftManager.CreateDraftState(leagueId);

            Team? teamToPick = null;
            while (teamToPick is null) 
            {
                if (draftTeams!.TryGetValue(state.Round ?? 1, out var teams))
                {
                    if (teams.Count != 0)
                    {
                        teamToPick = teams.Dequeue();
                        await _redis.SetDraftTeams(draftTeams, leagueId);
                    }
                    else
                    {
                        state.Round = (state.Round ?? 1) + 1;
                    }
                }
                else
                {
                    await _draftManager.EndDraft(leagueId); 
                    //send signalR message that draft has ended
                    return;
                }
            }

            state.TeamName = teamToPick!.Name;
            state.TeamId = teamToPick.Teamid;

            await _redis.SetDraftState(leagueId, state);

            await _hubContext.Clients.Group(leagueId.ToString()).UpdateDraftState(state);

            var jobId = _backgroundJobClient.Schedule<DraftJobs>(job => job.StartPick(leagueId), TimeSpan.FromSeconds(_draftOptions.DraftPickTime));
            await _redis.SetStartPickJobId(leagueId, jobId);
        }
    }
}
