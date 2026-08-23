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

        // A standing offer was closed without executing — declined outright, or retired by the
        // counter-offer that answers it. Sent to the league so every board drops it, not just the two
        // teams involved.
        Task ReceiveTradeRejected(TradeBetweenTeams trade);

        // The proposer replaced this offer with a newer one to the same team. Distinct from
        // ReceiveTradeRejected because nobody declined it: the offer is gone, but the negotiation is
        // still live, and a board that said "declined" would misread what happened.
        Task ReceiveTradeSuperseded(TradeBetweenTeams trade);
    }
}
