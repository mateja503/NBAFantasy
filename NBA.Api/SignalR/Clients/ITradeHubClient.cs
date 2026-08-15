using NBA.Data.Redis.Entities;

namespace NBA.Api.SignalR.Clients
{
    public interface ITradeHubClient
    {
        Task ReceiveTradeRequest(TradeBetweenTeams trade);
        Task ReceiveTradeAccepted(TradeBetweenTeams trade);

        // A SignalR *client result*: the server invokes this on the browser and awaits its answer,
        // rather than pushing one way. The JS handler must RETURN a value
        // (connection.on("IsOnTradeScreen", () => true)) — a handler that only receives will hang
        // until the caller's CancellationToken fires.
        Task<bool> IsOnTradeScreen(CancellationToken cancellationToken);
    }
}
