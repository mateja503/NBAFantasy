
using ExternalClients.Response;
using NBA.Data.Context;
using NBA.Service.Builder;
using PlayerData = NBA.Data.Entities.Player;
namespace NBA.Service.CalculateBoxScore
{
    public class BoxScoreCalculationService(NbaFantasyContext context)
    {
        private readonly NbaFantasyContext _context = context;  
        public async Task PerformCalculations(List<PlayerStatsResponse> playersStats)
        {
           List<PlayerData> players = playersStats
                .Select(player => new BoxScoreCalculationBuilder()
                    .CalculatePoints(player.pts)
                    .CalculateAssists(player.ast)
                    .CalculateRebounds(player.reb)
                    .CalculateBlocks(player.blk)
                    .CalculateSteals(player.stl)
                    .CalculateThreePointers(player.fg3m,player.fg3a)
                    .CalculateFieldGoals(player.fgm,player.fga)
                    .CalculateFreeThrows(player.ftm,player.fta)
                    .Calculate(player.player_id)
                ).ToList();

             await _context.UpdatePlayersRange(players);
        }
    }
}
