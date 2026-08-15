using NBA.Data.Redis.Entities;

namespace NBA.Api.SignalR.Clients
{
    public interface ITradeHubClient
    {
        Task ReceiveTradeRequest(TradeBetweenTeams trade);

        // Initial sync on connect: every offer already waiting for this team, newest first. Separate
        // from ReceiveTradeRequest so the client can tell "here is your backlog" from "a new offer
        // just arrived" — the list can legitimately be several trades from different teams.
        Task ReceiveTradeRequests(List<TradeBetweenTeams> trades);

        Task ReceiveTradeAccepted(TradeBetweenTeams trade);
    }
}
