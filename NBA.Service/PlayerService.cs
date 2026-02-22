using ExternalClients;
using NBA.Data.Context;

namespace NBA.Service
{
    //use hangfire so this service to be executaed at a spefici time 
    public class PlayerService(BallDontLieClient ballClient, NbaFantasyContext nbaContext)
    {
        private readonly BallDontLieClient _ballClient = ballClient;    
        private readonly NbaFantasyContext _nbaContext = nbaContext;    
        public async Task AddPlayersToDb() 
        {
            var externalPlayers = await _ballClient.GetAllPlayers();
            var filteredPlayers = PlayerFilter.FilterNonActivePlayers(externalPlayers);
            var players = Addapter.ToPlayer(filteredPlayers);
            await _nbaContext.AddPlayers(players);
        }
    }
}
