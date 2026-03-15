using NBA.Service.Observer.Listeners.DeserializedObjects;


namespace NBA.Service.Observer.HubSignalR
{
    public interface IAuctionClient
    {
        Task AuctionSendBid(Auction auction);
        Task UpdateClock(int secondsRemaining);
    }
}
