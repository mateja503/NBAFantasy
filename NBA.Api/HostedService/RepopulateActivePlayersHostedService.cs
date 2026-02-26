using ApplicationDefaults.LogDefaults;
using NBA.Data.Context;

namespace NBA.Api.HostedService
{
    public class RepopulateActivePlayersHostedService(ILogger<RepopulateActivePlayersHostedService> logger,  IServiceProvider serviceProvider) : BackgroundService
    {
        private readonly ILogger<RepopulateActivePlayersHostedService> _logger = logger;
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _logger.LogInformation("{Log}", new Log("Start populating db with active players.......").ToJson());
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<NbaFantasyContext>();

            while (!stoppingToken.IsCancellationRequested) 
            {
               
            }

            throw new NotImplementedException();
        }
    }
}
