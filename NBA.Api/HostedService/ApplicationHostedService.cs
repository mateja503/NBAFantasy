using ApplicationDefaults.LogDefaults;
using ExternalClients.Options;
using ExternalClients.Poco;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using NBA.Data.Context;
using NBA.Service.Player;

namespace NBA.Api.HostedService
{
    public class ApplicationHostedService(ILogger<ApplicationHostedService> logger, IServiceProvider serviceProvider, IOptions<BallDontLieClientOptions> options) : IHostedService
    {
        private readonly ILogger<ApplicationHostedService> _logger = logger;
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly BallDontLieClientOptions _options = options.Value;
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var score = _serviceProvider.CreateScope();
            var context = score.ServiceProvider.GetRequiredService<NbaFantasyContext>();
            var playerService = score.ServiceProvider.GetRequiredService<PlayerService>();

            if (!await context.GetAllPlayers().AnyAsync()) 
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

                    _logger.LogInformation("{Log}", new Log("Start populating db with active players.......", meta, res.data).ToJson());

                    next_cursor = res.meta.Next_cursor;
                }

            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        
    }
}
