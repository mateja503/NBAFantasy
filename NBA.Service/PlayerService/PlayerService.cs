using ExternalClients;
using ExternalClients.Poco;
using ExternalClients.Response;
using NBA.Data.Context;

namespace NBA.Service.PlayerService
{
    //use hangfire so this service to be executaed at a spefici time 
    public class PlayerService(BallDontLieClient ballClient, NbaFantasyContext nbaContext)
    {
        private readonly BallDontLieClient _ballClient = ballClient;    
        private readonly NbaFantasyContext _nbaContext = nbaContext;    
        public async Task<GetAllPlayersResponse> AddPlayersToDb(MetaData metadata, CancellationToken cancellationToken) 
        {
            var externalPlayers = await _ballClient.GetAllPlayers(metadata, cancellationToken);
            var filteredPlayers = PlayerFilter.FilterNonActivePlayers(externalPlayers.data);
            var players = Addapter.ToPlayer(filteredPlayers);
            await _nbaContext.AddPlayers(players, cancellationToken);
            return externalPlayers;
        }
      
    }
}
