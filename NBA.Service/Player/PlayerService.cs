using ApplicationDefaults.Exceptions;
using ExternalClients;
using ExternalClients.Poco;
using ExternalClients.Response;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using NBA.Data.Context;
using NBA.Data.Entities;
using NBA.Service.CalculateBoxScore;
using PlayerData = NBA.Data.Entities.Player;

namespace NBA.Service.Player
{
    //use hangfire so this service to be executaed at a spefici time 
    public class PlayerService(BallDontLieClient ballClient, NbaFantasyContext nbaContext, BoxScoreCalculationService boxScoreCalculationService)
    {
        private readonly BallDontLieClient _ballDontLieClient = ballClient;    
        private readonly NbaFantasyContext _nbaContext = nbaContext;
        private readonly BoxScoreCalculationService _boxScoreCalculationService = boxScoreCalculationService;
        public async Task<GetAllPlayersResponse> AddPlayersToDb(MetaData metadata, CancellationToken cancellationToken) 
        {
            var externalPlayers = await _ballDontLieClient.GetAllPlayers(metadata, cancellationToken);
            var filteredPlayers = PlayerFilter.FilterNonActivePlayers(externalPlayers.data);
            var players = Addapter.ToPlayer(filteredPlayers);
            await _nbaContext.AddPlayers(players, cancellationToken);
            return externalPlayers;
        }

        public async Task<PlayerData?> GetSpecificPlayerById(long playerId) 
        {
            return await _nbaContext.GetAllPlayers().FirstOrDefaultAsync(u => u.Playerid == playerId) ?? throw new NBAException($"For playerId {playerId}",ErrorCodes.DataBaseRecordNotFound);
        }

        public async Task<List<PlayerData>> GetPlayersForTeams(List<long> teamIds) 
        {
            return await _nbaContext.GetAllPlayers()
                .Where(u => teamIds.Contains(u.Irlteamid ?? 0))
                .ToListAsync();
        }

        [JobDisplayName("Stats Check: {4} vs {5} (ID: {0})")]
        public async Task<List<PlayerStatsResponse>> GetPlayersGameStats(long gameId, long hometeamId, long awayteamId, string homeTeam, string awayTeam, CancellationToken cancellationToken)
        {
            //TODO implment observer where the calculated points are stored in database and the fantasy teams are updated with the new points

            var players = await GetPlayersForTeams([hometeamId, awayteamId]);

            var playerIds = players.Select(p => p.Playerid).ToList();

            var playersStats = await _ballDontLieClient.GetPlayerStats(playerIds, gameId, cancellationToken);

            await _boxScoreCalculationService.PerformCalculations(playersStats);

            return playersStats;
        }
    }
}
