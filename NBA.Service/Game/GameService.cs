using ExternalClients;
using ExternalClients.Response;
using Hangfire;
using NBA.Service.Player;

namespace NBA.Service.Game
{
    public class GameService(IBallDontLieClient ballDontLieClient, IBackgroundJobClient jobClient)
    {
        private readonly IBallDontLieClient _ballDontLieClient = ballDontLieClient;

        public IBackgroundJobClient _jobClient = jobClient;

        public async Task<List<GameInfoResponse>> TodaysGames(CancellationToken cancellationToken)
        {
            var games = await _ballDontLieClient.GetTodaysGames(cancellationToken);

            foreach (var game in games.data) 
            {
                DateTimeOffset gameFinishes = new DateTimeOffset(game.datetime).AddHours(4);//for each game, schedule a job to fetch the stats after the game is finished (4 hours after the game starts)

                _jobClient.Schedule<PlayerService>(
                    playerService => playerService.GetPlayersGameStats(game.id,game.home_team.id,
                    game.visitor_team.id, game.home_team.full_name,game.visitor_team.full_name, CancellationToken.None),
                    gameFinishes);
            }
            return games.data;
        }

    }
}
