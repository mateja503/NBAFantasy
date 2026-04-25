using Microsoft.AspNetCore.SignalR;
using NBA.Api.SignalR.Clients;
using NBA.Service.Draft;

namespace NBA.Api.SignalR.Hubs
{
    public class DraftHub(DraftManager draftManager) : Hub<IDraftHubClient>
    {
        private readonly DraftManager _draftManager = draftManager;

        // 1. Send state to a user the moment they connect/refresh
        public override async Task OnConnectedAsync()
        {
            var state = _draftManager.CurrentState;
            await Clients.Caller.UpdateDraftState(state);
            await base.OnConnectedAsync();
        }
        // 2. Action called by the Commissioner to start a pick
        public async Task<DraftState> ResetTimer() 
        {
            _draftManager.ResetTimer(60);// Reset timer to 60s
            var state = _draftManager.CurrentState;

            await Clients.All.UpdateDraftState(state);

            return state;
        }


    }
}
