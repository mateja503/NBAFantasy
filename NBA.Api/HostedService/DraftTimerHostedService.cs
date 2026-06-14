using NBA.Api.Draft;
using NBA.Data.Context;

namespace NBA.Api.HostedService
{
    // Polls the Redis sorted set for expired pick deadlines and advances those drafts. One cheap
    // O(log n) ZSET scan per second covers every concurrent draft, versus Hangfire polling Postgres
    // for one delayed job per pick. Claiming is atomic (Lua GETDEL-style), so running multiple API
    // replicas is safe — each due timer is handled exactly once.
    public class DraftTimerHostedService(
        IServiceProvider serviceProvider,
        NbaFantasyRedis redis,
        ILogger<DraftTimerHostedService> logger) : BackgroundService
    {
        private static readonly TimeSpan PollInterval = TimeSpan.FromSeconds(1);

        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly NbaFantasyRedis _redis = redis;
        private readonly ILogger<DraftTimerHostedService> _logger = logger;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Drain every timer that is due this tick.
                    long? leagueId;
                    while ((leagueId = await _redis.Draft.ClaimDueDraftTimer(DateTimeOffset.UtcNow)) is not null)
                    {
                        await AdvanceOneAsync(leagueId.Value, stoppingToken);
                    }
                }
                catch (Exception ex) when (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogError(ex, "Draft timer poll failed");
                }

                try
                {
                    await Task.Delay(PollInterval, stoppingToken);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
            }
        }

        private async Task AdvanceOneAsync(long leagueId, CancellationToken stoppingToken)
        {
            // A scope per league gives each advance its own scoped DbContext.
            using var scope = _serviceProvider.CreateScope();
            var processor = scope.ServiceProvider.GetRequiredService<DraftTimerProcessor>();

            try
            {
                await processor.AdvanceAsync(leagueId, nextPick: true);
            }
            catch (Exception ex) when (!stoppingToken.IsCancellationRequested)
            {
                // One failing league must not stall the others or the loop.
                _logger.LogError(ex, "Failed to advance draft for league {LeagueId}", leagueId);
            }
        }
    }
}
