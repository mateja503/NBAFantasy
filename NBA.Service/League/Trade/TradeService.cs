using ApplicationDefaults.Exceptions;
using ApplicationDefaults.LogDefaults;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NBA.Data.Constants;
using NBA.Data.Context;
using NBA.Data.Entities;
using NBA.Data.Enumerations;
using NBA.Data.Redis.Entities;
using NBA.Service.League.Roster;
using PlayerData = NBA.Data.Entities.Player;
// NBA.Service.League.Trade is a namespace, so the entity needs an alias to be reachable here — the
// same problem PlayerData above solves for NBA.Service.Player.
using TradeData = NBA.Data.Entities.Trade;
namespace NBA.Service.League.Trade
{
    public class TradeService(NbaFantasyContext context, ILogger<TradeService> logger, RosterValidator rosterValidator)
    {
        private readonly NbaFantasyContext _context = context;
        private readonly ILogger<TradeService> _logger = logger;
        private readonly RosterValidator _rosterValidator = rosterValidator;

        // Validates an in-season proposal. Deliberately separate from TradeManager.IsTradeValid, which
        // reads the live DraftState out of Redis: by the season that state is gone, so the rosters have
        // to come from nba.teamplayer instead. The roster limits themselves are the same shared rule.
        public async Task ValidateSeasonTrade(long leagueId, TradeBetweenTeams trade)
        {
            if (trade.FromTeam == trade.ToTeam)
                throw new NBAException("A team cannot trade with itself.", ErrorCodes.TradeIsNotValid);

            if (trade.PlayersIds is null || trade.PlayersIds.Count == 0)
                throw new NBAException("A trade must include at least one player.", ErrorCodes.TradeIsNotValid);

            var teams = await _context.GetAllTeams()
                .Where(t => t.Teamid == trade.FromTeam || t.Teamid == trade.ToTeam)
                .ToListAsync();

            if (teams.Count != 2)
                throw new NBAException("Both teams in the trade must exist.", ErrorCodes.TradeIsNotValid);

            // Without this a caller could trade across leagues, which would corrupt both rosters.
            if (teams.Any(t => t.Leagueid != leagueId))
                throw new NBAException($"Both teams must belong to league {leagueId}.", ErrorCodes.TradeIsNotValid);

            var rosters = await _context.GetAllTeamPlayer()
                .Where(tp => tp.Teamid == trade.FromTeam || tp.Teamid == trade.ToTeam)
                .Include(tp => tp.Player)
                .ToListAsync();

            var tradedIds = trade.PlayersIds.ToHashSet();

            // Every traded id must sit on one of the two rosters. Without this check the swap below
            // silently drops unknown ids and the trade "succeeds" without moving those players.
            var unowned = tradedIds.Except(rosters.Select(tp => tp.Playerid)).ToList();

            if (unowned.Count > 0)
                throw new NBAException(
                    $"Players not on either team's roster: {string.Join(", ", unowned)}", ErrorCodes.TradeIsNotValid);

            var fromRoster = rosters.Where(tp => tp.Teamid == trade.FromTeam).ToList();
            var toRoster = rosters.Where(tp => tp.Teamid == trade.ToTeam).ToList();

            // Same swap shape as the draft-time path (TradeManager.ComputeSwappedRosters): each team
            // keeps what it is not trading away and gains the other team's traded players.
            var newFromPlayers = fromRoster.Where(tp => !tradedIds.Contains(tp.Playerid))
                .Concat(toRoster.Where(tp => tradedIds.Contains(tp.Playerid)))
                .ToList();

            var newToPlayers = toRoster.Where(tp => !tradedIds.Contains(tp.Playerid))
                .Concat(fromRoster.Where(tp => tradedIds.Contains(tp.Playerid)))
                .ToList();

            ValidateRoster(newFromPlayers);
            ValidateRoster(newToPlayers);
        }

        // Records the proposal durably. Redis holds a copy for the live push, but it has no persistence
        // configured, so this row is the only version that survives a restart.
        public async Task<TradeData> AddProposedTrade(long leagueId, TradeBetweenTeams trade, DateTime expiresAt)
        {
            // Only the newest offer to a team is live — the Redis key is per recipient and overwrites —
            // so any still-pending row for the same recipient is retired rather than left competing.
            var superseded = await _context.GetAllTrades()
                .Where(t => t.Leagueid == leagueId
                            && t.Toteamid == trade.ToTeam
                            && t.Status == TradeStatuses.Pending)
                .ToListAsync();

            var row = new TradeData
            {
                Tradeguid = trade.TradeId,
                Leagueid = leagueId,
                Fromteamid = trade.FromTeam,
                Toteamid = trade.ToTeam,
                Playerids = [.. trade.PlayersIds],
                Status = TradeStatuses.Pending,
                Tscreated = trade.TradeDate.UtcDateTime,
                Tsexpires = expiresAt,
            };

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (superseded.Count > 0)
                {
                    superseded.ForEach(t => t.Status = TradeStatuses.Superseded);
                    _ = await _context.UpdateTradeRange(superseded);
                }

                var created = await _context.AddTrade(row);

                await transaction.CommitAsync();
                return created;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError("{Log}", new Log("Proposing trade failed", trade, ex.Message).ToJson());
                throw new NBAException("Proposing the trade failed", ErrorCodes.TradeCantBeExecuted);
            }
        }

        private void ValidateRoster(List<Teamplayer> roster) =>
            _rosterValidator.Validate(
                roster.Count,
                roster.Count(tp => tp.Player?.Playerposition == (int)PlayerPositionEnum.C));
        public async Task Trade(List<TradeBetweenTeams> tradeBetweenTeams)
        {
            var teamIds = tradeBetweenTeams.SelectMany(u => new[] { u.FromTeam, u.ToTeam })
                .Distinct()
                .ToList();

            var playerdIds = tradeBetweenTeams.SelectMany(u=>u.PlayersIds).Distinct().ToList();

            var oldEntires = await _context.GetAllTeamPlayer()
                .Where(u=> teamIds.Contains(u.Teamid) && playerdIds.Contains(u.Playerid))
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
