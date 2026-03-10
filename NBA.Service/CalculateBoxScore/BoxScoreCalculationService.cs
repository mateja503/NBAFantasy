
using ExternalClients.Response;
using NBA.Data.Context;
using NBA.Service.Builder;
using PlayerData = NBA.Data.Entities.Player;
namespace NBA.Service.CalculateBoxScore
{
    public class BoxScoreCalculationService(NbaFantasyContext context)
    {
        private readonly NbaFantasyContext _context = context;  
        public async Task PerformCalculations(List<PlayerStatsResponse> playersStats, Dictionary<long,PlayerData> players)
        {
           List<PlayerData?> result = playersStats
                .Select(stats => 
                    {
                        players.TryGetValue(stats.player_id, out var player);

                        if (player == null)
                            return null;

                        return new BoxScoreCalculationBuilder(player)
                            .CalculatePoints(stats.pts)
                            .CalculateAssists(stats.ast)
                            .CalculateRebounds(stats.reb)
                            .CalculateBlocks(stats.blk)
                            .CalculateSteals(stats.stl)
                            .CalculateThreePointers(stats.fg3m, stats.fg3a)
                            .CalculateFieldGoals(stats.fgm, stats.fga)
                            .CalculateFreeThrows(stats.ftm, stats.fta)
                            .Calculate();
                    } 
                )
                .Where(player => player != null)
                .ToList(); ;

             await _context.UpdatePlayersRange(result!);
        }
    }
}
