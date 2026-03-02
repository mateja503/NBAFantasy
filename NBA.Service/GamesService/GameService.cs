using ExternalClients;
using ExternalClients.Response;

namespace NBA.Service.GamesService
{
    public class GameService(BallDontLieClient ballDontLieClient)
    {
        private readonly BallDontLieClient _ballDontLieClient = ballDontLieClient;
        public async Task<List<Game>> TodaysGames(CancellationToken cancellationToken)
        {
            var res = await _ballDontLieClient.GetTodaysGames(cancellationToken);
            return res.data;
        }
    }
}
