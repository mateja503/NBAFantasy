
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using NBA.Data.Entities;
using NBA.Service.Observer.Listeners.DeserializedObjects;

namespace NBA.Service.Observer.HubSignalR
{
    public class AuctionHub(EventManager eventManager) : Hub<IAuctionClient>
    {
        private readonly EventManager _eventManager = eventManager;
        public async Task JoinDraft(long leagueId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, leagueId.ToString());
        }

        public void PlaceBid(Auction bidData)
        {
            // Broadcast the bid to the rest of the system
            _eventManager.notify(EventType.Auction, bidData);
        }
    }
}
