using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NBA.Api.SignalR.Clients;
using NBA.Data.Redis.Entities;
using NBA.Service.League.Trade;

namespace NBA.Api.SignalR.Hubs
{
    [Authorize]
    public class TradeHub(TradeManager tradeManager) : Hub<ITradeHubClient>
    {
        private readonly TradeManager _tradeManager = tradeManager;

        // The client opens the connection; here we subscribe it to the groups that trade
        // requests are routed to — the league group and the connecting team's group.
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();

            if (long.TryParse(httpContext?.Request.Query["leagueId"], out long leagueId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"league:trade:{leagueId}");
            }

            if (long.TryParse(httpContext?.Request.Query["teamId"], out long teamId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"team:trade:{teamId}");
            }

            await base.OnConnectedAsync();
        }

        // Action called by a team to propose a trade: persist it, then route the request to the
        // league group and the targeted team's group — the same keys OnConnectedAsync subscribes to.
        public async Task ProposeTrade(long leagueId, long fromTeam, long toTeam, List<long> playersIds)
        {
            var trade = new TradeBetweenTeams
            {
                FromTeam = fromTeam,
                ToTeam = toTeam,
                PlayersIds = playersIds,
            };

            await _tradeManager.IsTradeValid(leagueId, trade);

            await _tradeManager.ProposeDraftTrade(leagueId, trade);

            await Clients.Group($"team:trade:{toTeam}").ReceiveTradeRequest(trade);
        }

        // Action called to accept a proposed trade: execute the swap (which also records the
        // accepted trade in Redis), then notify the whole league once. Both teams involved are
        // members of the league group, so a single send reaches everyone exactly once.
        public async Task AcceptTrade(long leagueId, Guid tradeId)
        {
            var trade = await _tradeManager.AcceptDraftTrade(leagueId, tradeId);

            await Clients.Group($"league:trade:{leagueId}").ReceiveTradeAccepted(trade!);
        }
    }
}
