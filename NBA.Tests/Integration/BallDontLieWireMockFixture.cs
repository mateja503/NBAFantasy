using ApplicationDefaults.Options;
using ExternalClients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Options;
using Polly;
using System.Threading.RateLimiting;
using WireMock.Server;
using Xunit;

namespace NBA.Tests.Integration
{
    // Shared by the balldontlie integration tests. Unlike BallDontLieClientTests (which pins a
    // StubHttpMessageHandler straight onto an HttpClient), this fixture stands up a real HTTP server
    // (WireMock.Net, on loopback) and resolves IBallDontLieClient out of a container wired the same way
    // Program.cs wires it: BallDontLieClientOptions -> AddHttpClient<IBallDontLieClient, BallDontLieClient>
    // with the base address / Accept / Authorization defaults, plus the named external-api-shield pipeline.
    // So requests travel over a socket through the real SocketsHttpHandler and the real resilience
    // pipeline - which is what makes this an integration test of the client's wiring, not just its logic.
    // No Docker, no Aspire stack, no network egress needed.
    public class BallDontLieWireMockFixture : IAsyncLifetime
    {
        public const string ApiKey = "integration-test-api-key";
        public const int PerPage = 100;

        // Production retry count (NBA.Api/Extentions.cs) so retry-attempt assertions stay honest.
        public const int MaxRetryAttempts = 3;

        private WireMockServer _server = default!;
        private ServiceProvider _provider = default!;

        /// <summary>The stand-in balldontlie API. Tests stub mappings on it and read its request log.</summary>
        public WireMockServer Server => _server;

        /// <summary>The typed client, resolved from DI exactly as the API resolves it.</summary>
        public IBallDontLieClient Client => _provider.GetRequiredService<IBallDontLieClient>();

        public Task InitializeAsync()
        {
            _server = WireMockServer.Start();

            var services = new ServiceCollection();

            services.Configure<BallDontLieClientOptions>(options =>
            {
                options.BaseUrl = _server.Url!;
                options.ApiKey = ApiKey;
                options.Per_Page = PerPage;
            });

            // Same strategies as Extentions.CreateResiliencePipeline, but with a zero, un-jittered delay:
            // the production 2s exponential backoff would put ~14s of sleeping into every 5xx test while
            // proving nothing extra. Attempt counts are what the tests assert, and those are unchanged.
            services.AddResiliencePipeline<string, HttpResponseMessage>("external-api-shield", pipelineBuilder =>
            {
                pipelineBuilder
                    .AddRetry(new HttpRetryStrategyOptions
                    {
                        BackoffType = DelayBackoffType.Constant,
                        MaxRetryAttempts = MaxRetryAttempts,
                        UseJitter = false,
                        Delay = TimeSpan.Zero
                    })
                    .AddRateLimiter(new HttpRateLimiterStrategyOptions
                    {
                        DefaultRateLimiterOptions = new ConcurrencyLimiterOptions
                        {
                            PermitLimit = 1
                        }
                    });
            });

            // Mirrors the registration in NBA.Api/Program.cs (#region HttpClients).
            services.AddHttpClient<IBallDontLieClient, BallDontLieClient>((serviceProvider, client) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<BallDontLieClientOptions>>().Value;

                client.BaseAddress = new Uri(options.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", options.ApiKey);
            });

            _provider = services.BuildServiceProvider();

            return Task.CompletedTask;
        }

        /// <summary>Drops stubs, scenario state and the request log so each test starts from a clean server.</summary>
        public void Reset()
        {
            _server.Reset();
            _server.ResetScenarios();
        }

        public async Task DisposeAsync()
        {
            await _provider.DisposeAsync();
            _server.Stop();
            _server.Dispose();
        }
    }
}
