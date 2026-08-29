using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NBA.Api.DTOs;
using NBA.Api.Mappings;
using NBA.Api.SignalR.Clients;
using NBA.Service.Trade;

namespace NBA.Api.SignalR.Hubs
{
    // Transport only. Every trade rule lives in ITradeOrchestrator; what remains here is the part that
    // is genuinely about SignalR — parsing the connection's query string, joining the routing groups,
    // and turning the orchestrator's events into sends on those groups.
    //
    // The split matters because it is what makes the rules testable without a hub: this class is now
    // the only thing in the trade path that a test needs a live connection to exercise.
    [Authorize]
    public class TradeHub(ITradeOrchestrator trades, ILogger<TradeHub> logger) : Hub<ITradeHubClient>
    {
        private readonly ITradeOrchestrator _trades = trades;
        private readonly ILogger<TradeHub> _logger = logger;

        // The client opens the connection; here we subscribe it to the groups that trade requests are
        // routed to — the league group and the connecting team's group — and hand it any offer that is
        // already waiting for it.
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();

            // Both are parsed up front because the rehydrate below is keyed on the pair; the group joins
            // themselves still happen independently, as before.
            var hasLeagueId = long.TryParse(httpContext?.Request.Query["leagueId"], out long leagueId);
            var hasTeamId = long.TryParse(httpContext?.Request.Query["teamId"], out long teamId);

            if (hasLeagueId)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, LeagueGroup(leagueId));
            }

            if (hasTeamId)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, TeamGroup(teamId));
            }

            if (hasLeagueId && hasTeamId)
            {
                await SendPendingProposals(leagueId, teamId);
            }

            await base.OnConnectedAsync();
        }

        // A team can be holding offers from several other teams at once, which is why this is a list.
        private async Task SendPendingProposals(long leagueId, long teamId)
        {
            try
            {
                //this are the porposed trades
                var trades = await _trades.GetBacklogAsync(leagueId, teamId);

                if (trades.Count > 0)
                    await Clients.Caller.ReceiveTradeRequests(trades);
            }
            catch (Exception ex)
            {
                // Throwing out of OnConnectedAsync aborts the connection. Failing to rehydrate must
                // degrade to "connected but did not receive the pending offers", never to "could not
                // open the trade screen". This stays in the hub rather than moving into the
                // orchestrator: it is a statement about the connection, not about trading.
                _logger.LogError(ex, "Failed to deliver pending proposals for league {LeagueId}, team {TeamId}",
                    leagueId, teamId);
            }
        }

        public async Task<TradeDto> ProposeSeasonTrade(long leagueId, long fromTeam, long toTeam, List<long> playersIds)
        {
            var outcome = await _trades.ProposeAsync(leagueId, fromTeam, toTeam, playersIds);

            await Publish(leagueId, outcome.Events);

            return outcome.Result.ToTradeDto();
        }

        public async Task<TradeDto> AcceptSeasonTrade(long leagueId, Guid tradeId)
        {
            var outcome = await _trades.AcceptAsync(leagueId, tradeId);

            await Publish(leagueId, outcome.Events);

            return outcome.Result.ToTradeDto();
        }

        public async Task<TradeDto> RejectSeasonTrade(long leagueId, Guid tradeId)
        {
            var outcome = await _trades.RejectAsync(leagueId, tradeId);

            await Publish(leagueId, outcome.Events);

            return outcome.Result.ToTradeDto();
        }

        // The only place that knows which group a trade event is routed to.
        //
        // Sequential rather than concurrent on purpose: the events arrive in the order the client has to
        // process them (a supersede before the proposal that displaced it), and fanning them out in
        // parallel would surrender that order.
        private async Task Publish(long leagueId, IReadOnlyList<TradeEvent> events)
        {
            // Everything currently goes league-wide: the trade board shows every open offer in the
            // league, so a trade between two other teams still has to land on everyone's screen. Both
            // teams involved are in this group, so it is one send, not two — nobody sees it twice.
            //
            // No-op for anyone not on the trade screen: their connection only exists while that
            // component is mounted. They pick the offer up from GET /v1/trades on their next visit.
            var league = Clients.Group(LeagueGroup(leagueId));

            foreach (var e in events)
            {
                switch (e)
                {
                    case TradeEvent.OfferedToLeague x:
                        await league.ReceiveTradeRequest(x.Trade);
                        break;
                    case TradeEvent.Accepted x:
                        await league.ReceiveTradeAccepted(x.Trade);
                        break;
                    case TradeEvent.Rejected x:
                        await league.ReceiveTradeRejected(x.Trade);
                        break;
                    case TradeEvent.Superseded x:
                        await league.ReceiveTradeSuperseded(x.Trade);
                        break;
                    // A case with no send wired up for it — a fifth TradeEvent added without a line here,
                    // or a null slipped into the list (a type pattern never matches null, so that lands
                    // here too). Logged rather than thrown: by the time Publish runs the trade is already
                    // committed to Postgres and Redis, so throwing would turn a settled trade into a
                    // client-visible failure and drop every event queued behind it. Continuing delivers
                    // the rest of the fan-out; the log is what tells us the new event type is unrouted.
                    default:
                        _logger.LogError(
                            "Unrouted trade event {EventType} for league {LeagueId}: no client send is wired up for it",
                            e?.GetType().Name ?? "null", leagueId);
                        break;
                }
            }
        }

        // The group names OnConnectedAsync subscribes to. Built here rather than inline so a rename
        // cannot leave a send addressing a group nobody joined.
        private static string LeagueGroup(long leagueId) => $"league:trade:{leagueId}";

        private static string TeamGroup(long teamId) => $"team:trade:{teamId}";
    }
}
