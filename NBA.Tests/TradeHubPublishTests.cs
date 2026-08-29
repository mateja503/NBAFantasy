using Microsoft.Extensions.Logging.Abstractions;
using NBA.Api.SignalR.Hubs;
using NBA.Data.Redis.Entities;
using NBA.Service.Trade;
using NBA.Tests.Fakes;
using Xunit;
using TradeData = NBA.Data.Entities.Trade;

namespace NBA.Tests
{
    // Pure unit tests for TradeHub's routing: which group each trade event is sent to, and in what
    // order. No Docker, no Redis, no database — which is the point. Before TradeHub depended on
    // ITradeOrchestrator it named four concrete collaborators that each transitively required both
    // stores, so this file could not have existed: the same assertions needed a Testcontainers run.
    //
    // The integration tests in Integration/SeasonTradeHubTests still cover the rules and the real wire;
    // these cover the fan-out the orchestrator hands back, which is cheap to get wrong and slow to
    // catch there.
    public class TradeHubPublishTests
    {
        private const long LeagueId = 42;
        private static string LeagueGroup => $"group:league:trade:{LeagueId}";

        private static TradeBetweenTeams Trade() => new() { FromTeam = 1, ToTeam = 2, PlayersIds = [7] };

        private static (TradeHub Hub, RecordingTradeHubClients Clients) BuildHub(
            params TradeEvent[] events)
        {
            var clients = new RecordingTradeHubClients();
            var hub = new TradeHub(new FakeTradeOrchestrator(events), NullLogger<TradeHub>.Instance)
            {
                Clients = clients,
            };
            return (hub, clients);
        }

        [Fact]
        public async Task ProposeSeasonTrade_sends_the_offer_to_the_league_group()
        {
            var trade = Trade();
            var (hub, clients) = BuildHub(new TradeEvent.OfferedToLeague(trade));

            await hub.ProposeSeasonTrade(LeagueId, 1, 2, [7]);

            var send = Assert.Single(clients.Sends);
            Assert.Equal(LeagueGroup, send.Target);
            Assert.Equal("ReceiveTradeRequest", send.Method);
            Assert.Equal(trade.TradeId, send.Trade!.TradeId);
        }

        // The ordering guarantee TradeOutcome documents: a client processing the sends in arrival order
        // must see the displaced offer retired before the one that displaced it, or its board shows the
        // dead offer as the live one.
        [Fact]
        public async Task ProposeSeasonTrade_sends_every_supersede_before_the_new_offer()
        {
            var displaced = Trade();
            var offered = Trade();
            var (hub, clients) = BuildHub(
                new TradeEvent.Superseded(displaced),
                new TradeEvent.OfferedToLeague(offered));

            await hub.ProposeSeasonTrade(LeagueId, 1, 2, [7]);

            Assert.Equal(
                new[] { "ReceiveTradeSuperseded", "ReceiveTradeRequest" },
                clients.Sends.Select(s => s.Method));
            Assert.Equal(displaced.TradeId, clients.Sends[0].Trade!.TradeId);
            Assert.Equal(offered.TradeId, clients.Sends[1].Trade!.TradeId);
        }

        [Fact]
        public async Task AcceptSeasonTrade_sends_the_accepted_trade_to_the_league_group()
        {
            var trade = Trade();
            var (hub, clients) = BuildHub(new TradeEvent.Accepted(trade));

            await hub.AcceptSeasonTrade(LeagueId, trade.TradeId);

            var send = Assert.Single(clients.Sends);
            Assert.Equal(LeagueGroup, send.Target);
            Assert.Equal("ReceiveTradeAccepted", send.Method);
        }

        [Fact]
        public async Task RejectSeasonTrade_sends_the_rejected_trade_to_the_league_group()
        {
            var trade = Trade();
            var (hub, clients) = BuildHub(new TradeEvent.Rejected(trade));

            await hub.RejectSeasonTrade(LeagueId, trade.TradeId);

            var send = Assert.Single(clients.Sends);
            Assert.Equal(LeagueGroup, send.Target);
            Assert.Equal("ReceiveTradeRejected", send.Method);
        }

        // Every event is addressed to the league, never to the individual teams: the trade board shows
        // the whole league's offers, and both teams are already in that group, so a per-team send would
        // deliver the same event twice.
        [Fact]
        public async Task Every_event_goes_to_the_league_group_exactly_once()
        {
            var (hub, clients) = BuildHub(
                new TradeEvent.Superseded(Trade()),
                new TradeEvent.Superseded(Trade()),
                new TradeEvent.OfferedToLeague(Trade()));

            await hub.ProposeSeasonTrade(LeagueId, 1, 2, [7]);

            Assert.Equal(3, clients.Sends.Count);
            Assert.All(clients.Sends, s => Assert.Equal(LeagueGroup, s.Target));
        }

        // Hands back a scripted event list, so a test states the fan-out it wants without standing up the
        // rules that would produce it.
        private sealed class FakeTradeOrchestrator(TradeEvent[] events) : ITradeOrchestrator
        {
            private TradeOutcome<TradeData> Outcome() => new(new TradeData
            {
                Tradeid = Guid.NewGuid(),
                Leagueid = LeagueId,
                Playerids = [],
                Status = "pending",
            }, events);

            public Task<TradeOutcome<TradeData>> ProposeAsync(
                long leagueId, long fromTeam, long toTeam, List<long> playersIds) => Task.FromResult(Outcome());

            public Task<TradeOutcome<TradeData>> AcceptAsync(long leagueId, Guid tradeId) => Task.FromResult(Outcome());

            public Task<TradeOutcome<TradeData>> RejectAsync(long leagueId, Guid tradeId) => Task.FromResult(Outcome());

            public Task<List<TradeBetweenTeams>> GetBacklogAsync(long leagueId, long teamId) =>
                Task.FromResult(new List<TradeBetweenTeams>());
        }
    }
}
