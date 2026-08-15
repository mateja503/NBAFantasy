using Microsoft.AspNetCore.SignalR;
using NBA.Api.SignalR.Clients;
using NBA.Api.SignalR.Hubs;
using NBA.Service.League.Trade;

namespace NBA.Api.SignalR
{
    // Answers "is this team's manager looking at the trade screen right now?".
    //
    // The server cannot open a SignalR connection to a browser — connections are always client
    // initiated — so this only reports on connections the client already established. It uses a
    // SignalR *client result*: the server invokes IsOnTradeScreen on a specific me; there is no "asconnection and awaits
    // the return value. That addresses one connection at a tik whoever is in this
    // group" primitive, which is why TradeManager tracks connection ids in Redis.
    //
    // Lives in NBA.Api rather than NBA.Service because ITradeHubClient is an API-layer contract.
    public class TradePresenceProbe(TradeManager tradeManager, IHubContext<TradeHub, ITradeHubClient> tradeHub)
    {
        private readonly TradeManager _tradeManager = tradeManager;
        private readonly IHubContext<TradeHub, ITradeHubClient> _tradeHub = tradeHub;

        // Never throws. By the time this runs the proposal is already persisted, so a failed probe
        // costs a live notification and nothing else — the recipient still sees the offer on their
        // next read. Returning false on error is therefore the correct degradation, not a swallowed bug.
        public async Task<bool> IsTeamOnTradeScreen(long teamId, CancellationToken cancellationToken)
        {
            var connectionIds = await _tradeManager.GetTradeConnectionIds(teamId);

            foreach (var connectionId in connectionIds)
            {
                try
                {
                    if (await _tradeHub.Clients.Client(connectionId).IsOnTradeScreen(cancellationToken))
                        return true;
                }
                catch (Exception)
                {
                    // A connection that no longer exists throws IOException; one that never answers
                    // surfaces as a cancellation. Either way the id is dead weight — drop it, so the
                    // set does not accumulate ghosts from clients that died without disconnecting.
                    await _tradeManager.DropTradeConnection(teamId, connectionId);
                }
            }

            return false;
        }
    }
}
