using ApplicationDefaults.LogDefaults;
using ApplicationDefaults.Options;
using ExternalClients.Poco;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NBA.Data.Context;
using NBA.Data.Redis.Keys;
using NBA.Service.Player;

namespace NBA.Api.HostedService
{
    public class ApplicationHostedService(ILogger<ApplicationHostedService> logger, IServiceProvider serviceProvider, IOptions<BallDontLieClientOptions> options, NbaFantasyRedis redis) : IHostedService
    {
        private static readonly TimeSpan SeedLockExpiry = TimeSpan.FromMinutes(10);

        private readonly ILogger<ApplicationHostedService> _logger = logger;
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly BallDontLieClientOptions _options = options.Value;
        private readonly NbaFantasyRedis _redis = redis;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // With more than one API replica every instance boots and runs this. The distributed
            // lock ensures exactly one performs the DB back-fill / Redis load; the others skip,
            // since Postgres and Redis are shared. (Single instance always wins the lock.)
            var lockKey = RedisKeys.GetStartupSeedLockKey();
            var lockToken = await _redis.Lock.TryAcquire(lockKey, SeedLockExpiry);
            if (lockToken is null)
            {
                _logger.LogInformation("{Log}", new Log("Player initialization skipped: another instance holds the seed lock.").ToJson());
                return;
            }

            try
            {
                await InitializePlayersAsync(cancellationToken);
            }
            finally
            {
                await _redis.Lock.Release(lockKey, lockToken);
            }
        }

        private async Task InitializePlayersAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<NbaFantasyContext>();
            var playerService = scope.ServiceProvider.GetRequiredService<PlayerService>();
            var playerManager = scope.ServiceProvider.GetRequiredService<PlayerManager>();

            //this is done because balldontlie api isn't payed and getActivePlayers is much better api to use rather than get all
            if (!await context.GetAllPlayers().AnyAsync(cancellationToken))
            {
                _logger.LogInformation("{Log}", new Log("Start populating db with active players.......").ToJson());
                long? next_cursor = 0;
                int per_page = _options.Per_Page;
                while (!cancellationToken.IsCancellationRequested)
                {
                    var meta = new MetaData { Per_page = per_page, Next_cursor = next_cursor };

                    var res = await playerService.AddPlayersToDb(meta, cancellationToken);

                    if (!res.data.Any())
                        break;

                    await playerManager.AddPlayersToRedis(res.data);

                    _logger.LogInformation("{Log}", new Log("Start populating db with active players.......", meta, res.data).ToJson());

                    next_cursor = res.meta.Next_cursor;
                }
            }
            else
            {
                var players = await context.GetAllPlayers().ToListAsync(cancellationToken);
                await playerManager.AddPlayerToRedisFromDB(players);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    }
}
