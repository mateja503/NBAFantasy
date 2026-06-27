using System.Security.Claims;
using System.Text.Encodings.Web;
using ApplicationDefaults.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NBA.Api.SignalR.Hubs;
using NBA.Data.Context;
using NBA.Data.Entities;
using NBA.Service.League.Draft;
using NBA.Service.League.Trade;
using StackExchange.Redis;
using Testcontainers.Redis;
using Xunit;

namespace NBA.Tests.Integration
{
    // Shared across the trade integration tests (one Redis container per run). Provides:
    //  - a throwaway Redis container + a NbaFantasyRedis bound to it (for direct Redis-write assertions), and
    //  - an in-memory TestServer hosting the TradeHub.
    // The accept path goes through DraftManager -> DraftSnapshotService, which needs a relational DB
    // (a league row). We stand that in with EF Core InMemory + seeded leagues so AcceptDraftTrade runs
    // for real without a Postgres container; the live state still lives in the real Redis container.
    public class TradeHubFixture : IAsyncLifetime
    {
        // League roster limits the hub validates against. Small CenterLimit lets the "invalid" test
        // push a team over the center limit with a single swap.
        public const int MaxPlayersPerTeam = 10;
        public const int CenterLimit = 1;

        // Leagues seeded into the InMemory DB; tests pick one each so they don't collide.
        public static readonly long[] SeededLeagueIds = { 1, 2, 3, 4, 5, 6, 7, 8 };

        private readonly RedisContainer _redisContainer = new RedisBuilder("redis:7.4").Build();

        private IConnectionMultiplexer _multiplexer = default!;
        private IHost _host = default!;

        private TestServer Server => _host.GetTestServer();

        public IDatabase Database => _multiplexer.GetDatabase();

        // A NbaFantasyRedis bound to the test container, for seeding/reading state directly in tests.
        public NbaFantasyRedis Redis => new(_multiplexer);

        public async Task InitializeAsync()
        {
            await _redisContainer.StartAsync();
            _multiplexer = await ConnectionMultiplexer.ConnectAsync(_redisContainer.GetConnectionString());

            _host = await new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseTestServer()
                        .ConfigureServices(services =>
                        {
                            services.AddRouting();
                            services.AddSignalR(o => o.EnableDetailedErrors = true);

                            services.AddSingleton(_multiplexer);
                            services.AddScoped<NbaFantasyRedis>();
                            services.Configure<ApplicationOptions>(o =>
                            {
                                o.MaxPlayersPerTeam = MaxPlayersPerTeam;
                                o.CenterLimit = CenterLimit;
                            });

                            // InMemory stand-in for Postgres so the accept-path draft/snapshot chain runs.
                            services.AddDbContext<NbaFantasyContext>(o => o.UseInMemoryDatabase("trade-tests"));
                            services.Configure<DraftOptions>(o =>
                            {
                                o.Rounds = 1;
                                o.DraftPickTime = 60;
                                o.ShowTeamDraftBoardCount = 1;
                            });
                            services.AddScoped<DraftSnapshotService>();

                            // DraftManager.GetDraftState / UpdaterDraftState (the only methods the accept
                            // path calls) use the snapshot service + Redis and never touch DraftService, so
                            // we pass null for it rather than wiring DraftService's whole graph.
                            services.AddScoped(sp => new DraftManager(
                                sp.GetRequiredService<NbaFantasyContext>(),
                                sp.GetRequiredService<IOptions<DraftOptions>>(),
                                sp.GetRequiredService<NbaFantasyRedis>(),
                                null!,
                                sp.GetRequiredService<DraftSnapshotService>()));

                            services.AddScoped<TradeManager>();

                            // [Authorize] on the hub needs an authenticated user; this scheme always succeeds.
                            services.AddAuthentication("Test")
                                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", _ => { });
                            services.AddAuthorization();
                        })
                        .Configure(app =>
                        {
                            app.UseRouting();
                            app.UseAuthentication();
                            app.UseAuthorization();
                            app.UseEndpoints(endpoints => endpoints.MapHub<TradeHub>("/tradeHub"));
                        });
                })
                .StartAsync();

            await SeedLeaguesAsync();
        }

        // DraftSnapshotService.EnsureRehydratedAsync requires a league row (and treats a completed draft
        // as a no-op), so each test league needs an in-progress League in the InMemory DB.
        private async Task SeedLeaguesAsync()
        {
            using var scope = _host.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<NbaFantasyContext>();

            foreach (var leagueId in SeededLeagueIds)
            {
                context.Leagues.Add(new League
                {
                    Leagueid = leagueId,
                    Name = $"Test League {leagueId}",
                    Commissioner = 1,
                    Seasonyear = "2026",
                    Draftcompleted = false,
                });
            }

            await context.SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            if (_host is not null)
            {
                await _host.StopAsync();
                _host.Dispose();
            }
            if (_multiplexer is not null) await _multiplexer.DisposeAsync();
            await _redisContainer.DisposeAsync();
        }

        // Builds a SignalR client wired to the in-memory TestServer. leagueId/teamId go on the query
        // string because TradeHub.OnConnectedAsync reads them from there to join the routing groups.
        public HubConnection BuildClient(long leagueId, long teamId)
        {
            var server = Server;
            return new HubConnectionBuilder()
                .WithUrl($"{server.BaseAddress}tradeHub?leagueId={leagueId}&teamId={teamId}", options =>
                {
                    options.HttpMessageHandlerFactory = _ => server.CreateHandler();
                    options.Transports = HttpTransportType.LongPolling;
                })
                .Build();
        }
    }

    // Lets both trade integration test classes share a single Redis container / host.
    [CollectionDefinition("Trade integration")]
    public class TradeIntegrationCollection : ICollectionFixture<TradeHubFixture> { }

    // Authenticates every request so the hub's [Authorize] passes. The hub reads leagueId/teamId from
    // the query string, not claims, so the principal only needs to be authenticated.
    public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder)
            : base(options, logger, encoder) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, "test-user") }, "Test");
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), "Test");
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
