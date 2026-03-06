using ExternalClients;
using ExternalClients.Response;

namespace NBA.Service.GamesService
{
    public class GameService(BallDontLieClient ballDontLieClient)
    {
        private readonly BallDontLieClient _ballDontLieClient = ballDontLieClient;
        public async Task<List<GameInfoResponse>> TodaysGames(CancellationToken cancellationToken)
        {
            var res = await _ballDontLieClient.GetTodaysGames(cancellationToken);

            //TODO create jobs for when the game finishes to get the box score and the stats for that game

            return res.data;
        }





        
    }
}
