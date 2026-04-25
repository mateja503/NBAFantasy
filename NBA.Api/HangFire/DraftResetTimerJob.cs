using Hangfire;
using Microsoft.AspNetCore.SignalR;
using NBA.Api.SignalR.Clients;
using NBA.Api.SignalR.Hubs;
using NBA.Service.Draft;

namespace NBA.Api.HangFire
{
    public class DraftResetTimerJob(IHubContext<DraftHub, IDraftHubClient> draftHubContext, DraftManager draftManager, IBackgroundJobClient backgroundJobs)
    {
        private readonly IHubContext<DraftHub, IDraftHubClient> _draftHubContext = draftHubContext;
        private readonly DraftManager _draftManager = draftManager;

        public async Task ExecuteReset() 
        {
            _draftManager.ResetTimer(60);
            var state = _draftManager.CurrentState;

            await _draftHubContext.Clients.All.UpdateDraftState(state);

            backgroundJobs.Schedule<DraftResetTimerJob>(job => job.ExecuteReset(), TimeSpan.FromSeconds(60));

        }
    }
}
