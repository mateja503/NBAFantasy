using Microsoft.AspNetCore.SignalR;
using NBA.Api.SignalR.Clients;
using NBA.Data.Redis.Entities;

namespace NBA.Tests.Fakes
{
    /// <summary>
    /// Test double for a hub's client proxies. It is the seam that makes <c>TradeHub</c> unit testable:
    /// every send is recorded with the group it was addressed to, so a test can assert the routing and
    /// the order of a broadcast without a SignalR connection, a Redis container, or a database.
    /// </summary>
    public sealed class RecordingTradeHubClients : IHubCallerClients<ITradeHubClient>
    {
        /// <summary>Every send, in order: the group addressed and the client method invoked.</summary>
        public List<(string Target, string Method, TradeBetweenTeams? Trade)> Sends { get; } = new();

        public ITradeHubClient Group(string groupName) => new Recorder(this, $"group:{groupName}");

        public ITradeHubClient Caller => new Recorder(this, "caller");

        public ITradeHubClient All => new Recorder(this, "all");

        public ITradeHubClient Others => new Recorder(this, "others");

        public ITradeHubClient Client(string connectionId) => new Recorder(this, $"client:{connectionId}");

        public ITradeHubClient Clients(IReadOnlyList<string> connectionIds) => new Recorder(this, "clients");

        public ITradeHubClient Groups(IReadOnlyList<string> groupNames) => new Recorder(this, "groups");

        public ITradeHubClient User(string userId) => new Recorder(this, $"user:{userId}");

        public ITradeHubClient Users(IReadOnlyList<string> userIds) => new Recorder(this, "users");

        public ITradeHubClient AllExcept(IReadOnlyList<string> excludedConnectionIds) => new Recorder(this, "allExcept");

        public ITradeHubClient GroupExcept(string groupName, IReadOnlyList<string> excludedConnectionIds) =>
            new Recorder(this, $"groupExcept:{groupName}");

        public ITradeHubClient OthersInGroup(string groupName) => new Recorder(this, $"othersInGroup:{groupName}");

        // One proxy per addressed target; it writes straight through to the parent's log so the order
        // across different targets is preserved in a single list.
        private sealed class Recorder(RecordingTradeHubClients parent, string target) : ITradeHubClient
        {
            private Task Record(string method, TradeBetweenTeams? trade)
            {
                parent.Sends.Add((target, method, trade));
                return Task.CompletedTask;
            }

            public Task ReceiveTradeRequest(TradeBetweenTeams trade) => Record(nameof(ReceiveTradeRequest), trade);

            public Task ReceiveTradeRequests(List<TradeBetweenTeams> trades)
            {
                // The backlog is a list, so it is logged once per trade — a test asserting "the caller was
                // handed these three offers" reads the same way as one asserting a single send.
                foreach (var trade in trades) Record(nameof(ReceiveTradeRequests), trade);
                return Task.CompletedTask;
            }

            public Task ReceiveTradeAccepted(TradeBetweenTeams trade) => Record(nameof(ReceiveTradeAccepted), trade);

            public Task ReceiveTradeRejected(TradeBetweenTeams trade) => Record(nameof(ReceiveTradeRejected), trade);

            public Task ReceiveTradeSuperseded(TradeBetweenTeams trade) => Record(nameof(ReceiveTradeSuperseded), trade);
        }
    }
}
