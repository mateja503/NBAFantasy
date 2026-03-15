using Microsoft.AspNetCore.SignalR;
using NBA.Service.Observer.HubSignalR;
using NBA.Service.Observer.Listeners.DeserializedObjects;
using System.Text.Json;

namespace NBA.Service.Observer.Listeners
{
    public class AuctionListener(IHubContext<AuctionHub, IAuctionClient> hubContext) : ISubscriber
    {
        private readonly IHubContext<AuctionHub, IAuctionClient> _hubContext  = hubContext;
        public async void HandleMessage(object message)
        {
            var auction = JsonSerializer.Deserialize<Auction>(message.ToString()!)
                ?? throw new Exception("Failed to deserialize auction message.");

            await _hubContext.Clients.Group(auction.LeagueId.ToString()).AuctionSendBid(auction);
        }
    }
}
