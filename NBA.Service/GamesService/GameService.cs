using ExternalClients;
using ExternalClients.Response;
using Hangfire;
using NBA.Service.Player;

namespace NBA.Service.GamesService
{
    public class GameService(BallDontLieClient ballDontLieClient, PlayerService playerService)
    {
        private readonly BallDontLieClient _ballDontLieClient = ballDontLieClient;
        public async Task<List<GameInfoResponse>> TodaysGames(CancellationToken cancellationToken)
        {
            var games = await _ballDontLieClient.GetTodaysGames(cancellationToken);

            foreach (var game in games.data) 
            {
                DateTimeOffset gameFinishes = new DateTimeOffset(game.datetime).AddMinutes(4);
                BackgroundJob.Schedule<PlayerService>(
                    PlayerService => PlayerService.GetPlayersGameStats(game.id,game.home_team.id,
                    game.visitor_team.id, game.home_team.full_name,game.visitor_team.full_name, CancellationToken.None),
                    gameFinishes);
            }
            return games.data;
        }

        







    }
}
