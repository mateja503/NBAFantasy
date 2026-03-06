using Hangfire;
using NBA.Service.GamesService;

namespace NBA.Api.HostedService
{
    public class HangFireJobSchedulerHostedService(IRecurringJobManager recurringJobManager) : IHostedService
    {
        private readonly IRecurringJobManager _recurringJobManager = recurringJobManager;
        public Task StartAsync(CancellationToken cancellationToken)
        {
           _recurringJobManager.AddOrUpdate<GameService>("get-todays-games", gamesService => gamesService.TodaysGames(CancellationToken.None), Cron.Daily);
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    }
}
