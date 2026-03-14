using ApplicationDefaults.Exceptions;
using ApplicationDefaults.LogDefaults;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NBA.Data.Context;
using NBA.Data.Entities;
using PlayerData = NBA.Data.Entities.Player;
namespace NBA.Service.League.Trade
{
    public class TradeService(NbaFantasyContext context, ILogger<TradeService> logger)
    {
        private readonly NbaFantasyContext _context = context;
        private readonly ILogger<TradeService> _logger = logger;
        public async Task Trade(List<TradeBetweenTeams> tradeBetweenTeams)
        {
            var teamIds = tradeBetweenTeams.SelectMany(u => new[] { u.FromTeam, u.ToTeam })
                .Distinct()
                .ToList();

            var playerdIds = tradeBetweenTeams.SelectMany(u=>u.PlayersIds).Distinct().ToList();

            var oldEntires = await _context.GetAllTeamPlayer()
                .Where(u=> teamIds.Any(t => t == u.Teamid) && playerdIds.Any(p=>p == u.Playerid))
                .ToListAsync();

            var newEntries = oldEntires.Select(u =>
            {
                TradeBetweenTeams temp = tradeBetweenTeams!.FirstOrDefault(t => u.Teamid == t.FromTeam)
                ?? throw new NBAException($"Trade not specified for team with id: {u.Teamid}", ErrorCodes.TradeCantBeExecuted);

                return new Teamplayer
                {
                    Teamid = temp.ToTeam,
                    Playerid = u.Playerid 
                };
            }).ToList();

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _ = await _context.DeleteTeamPlayerRange(oldEntires);
                    _ = await _context.AddTeamPlayerRange(newEntries);

                    await transaction.CommitAsync();
                }
                catch (Exception ex) 
                {
                    await transaction.RollbackAsync();
                    _logger.LogError("{Log}", new Log("Trade Failed",tradeBetweenTeams,ex.Message).ToJson());
                    throw new NBAException($"Trader failed", ErrorCodes.TradeCantBeExecuted);
                }       
            }            

         
        }
    }
}
