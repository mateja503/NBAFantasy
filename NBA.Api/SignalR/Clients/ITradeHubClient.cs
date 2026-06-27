using NBA.Data.Redis.Entities;

namespace NBA.Api.SignalR.Clients
{
    public interface ITradeHubClient
    {
        Task ReceiveTradeRequest(TradeBetweenTeams trade);
        Task ReceiveTradeAccepted(TradeBetweenTeams trade);
    }
}
