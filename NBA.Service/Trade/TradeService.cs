using ApplicationDefaults.Exceptions;
using ApplicationDefaults.LogDefaults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NBA.Data.Constants;
using NBA.Data.Context;
using NBA.Data.Entities;
using NBA.Data.Enumerations;
using NBA.Data.Redis.Entities;
using NBA.Service.Roster;
using PlayerData = NBA.Data.Entities.Player;
// NBA.Service.Trade is a namespace, so the entity needs an alias to be reachable here — the
// same problem PlayerData above solves for NBA.Service.Player.
using TradeData = NBA.Data.Entities.Trade;
namespace NBA.Service.Trade
{
    public class TradeService(NbaFantasyContext context, ILogger<TradeService> logger, RosterValidator rosterValidator)
    {
        private readonly NbaFantasyContext _context = context;
        private readonly ILogger<TradeService> _logger = logger;
        private readonly RosterValidator _rosterValidator = rosterValidator;

        // Validates a proposal against the rosters in nba.teamplayer. This is now the only trade
        // validation: the draft-time counterpart, which checked the live DraftState in Redis, went with
        // draft-night trading. Roster limits come from the shared RosterValidator rule.
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

        // Every offer of record aimed at a team, newest first — a recipient can hold proposals from
        // several teams at once, and only the proposer's own previous offer is ever retired.
        //
        // Deliberately ignores Tsexpires: that timestamp ends the real-time push window, not the offer
        // itself, so a manager who was away when one arrived is still shown it. Returns the
        // Redis/SignalR shape rather than the entity so callers can push these straight to a client.
        public async Task<List<TradeBetweenTeams>> GetPendingProposals(long leagueId, long toTeamId)
        {
            var rows = await _context.GetAllTrades()
                .Where(t => t.Leagueid == leagueId
                            && t.Toteamid == toTeamId
                            && t.Status == TradeStatuses.Pending)
                .OrderByDescending(t => t.Tscreated)
                .ToListAsync();

            return rows.Select(row => new TradeBetweenTeams
            {
                TradeId = row.Tradeid,
                FromTeam = row.Fromteamid,
                ToTeam = row.Toteamid,
                PlayersIds = row.Playerids ?? [],
                // SpecifyKind matters: a DateTime whose Kind is Unspecified (what the EF InMemory
                // provider hands back) would otherwise be read as local time and shift the timestamp
                // by the machine's UTC offset.
                TradeDate = new DateTimeOffset(DateTime.SpecifyKind(row.Tscreated, DateTimeKind.Utc)),
            }).ToList();
        }

        // Every trade in a league, newest first — the read behind the trade board, where a manager
        // browses offers between *any* two teams, not just the ones aimed at them.
        //
        // Deliberately not filtered to the caller's own team: seeing that two rivals are negotiating is
        // part of the game. The caller still has to manage a team in the league, otherwise anyone with a
        // token could enumerate another league's negotiations.
        public async Task<List<TradeData>> GetLeagueTrades(long leagueId, long userId, string? status = null)
        {
            if (leagueId <= 0)
                throw new NBAException($"{nameof(leagueId)} is missing", ErrorCodes.MissingParametar);

            await EnsureLeagueMember(leagueId, userId);

            var query = _context.GetAllTrades().Where(t => t.Leagueid == leagueId);

            if (!string.IsNullOrWhiteSpace(status))
            {
                // Compared lowercased because the column stores the TradeStatuses constants verbatim;
                // an unknown value is an error rather than an empty list, so a typo in the client is
                // visible instead of looking like "this league has no trades".
                var normalized = status.Trim().ToLowerInvariant();

                if (!TradeStatuses.All.Contains(normalized))
                    throw new NBAException($"Unknown trade status '{status}'.", ErrorCodes.InvalidFilterValue);

                query = query.Where(t => t.Status == normalized);
            }

            return await query
                .OrderByDescending(t => t.Tscreated)
                .AsNoTracking()
                .ToListAsync();
        }

        // Closes a standing offer without executing it. Separate from letting it lapse: Tsexpires only
        // ends the real-time push window, so an untouched offer stays 'pending' forever and would keep
        // reappearing on the board. This is what a counter-offer uses to retire the offer it answers.
        public async Task<TradeData> RejectProposal(long leagueId, Guid tradeId)
        {
            var row = await _context.GetAllTrades()
                .FirstOrDefaultAsync(t => t.Leagueid == leagueId && t.Tradeid == tradeId)
                ?? throw new NBAException("Trade not found.", ErrorCodes.TradeCantBeExecuted);

            // Same guard as AcceptProposal: a settled trade must not flip status, or an accepted swap
            // would look reversed on the board while the roster rows stay moved.
            if (row.Status != TradeStatuses.Pending)
                throw new NBAException($"Trade is {row.Status} and can no longer be rejected.", ErrorCodes.TradeCantBeExecuted);

            row.Status = TradeStatuses.Rejected;
            _ = await _context.UpdateTradeRange([row]);

            return row;
        }

        // Team membership is the league's access boundary — the same rule the trade validation uses
        // when it insists both teams belong to the league.
        private async Task EnsureLeagueMember(long leagueId, long userId)
        {
            var isMember = await _context.GetAllTeams()
                .AnyAsync(t => t.Leagueid == leagueId && t.Userid == userId);

            if (!isMember)
                throw new NBAException($"User does not manage a team in league {leagueId}.", ErrorCodes.UserNotInLeague);
        }

        // Records the proposal durably. Redis holds a copy for the live push, but it has no persistence
        // configured, so this row is the only version that survives a restart.
        //
        // The rows this proposal displaced come back with it: the caller has to clear their Redis
        // copies and tell the league, otherwise every trade board keeps showing an offer the database
        // no longer considers open.
        public async Task<(TradeData Created, List<TradeData> Superseded)> AddProposedTrade(
            long leagueId, TradeBetweenTeams trade, DateTime expiresAt)
        {
            var row = new TradeData
            {
                Tradeid = trade.TradeId,
                Leagueid = leagueId,
                Fromteamid = trade.FromTeam,
                Toteamid = trade.ToTeam,
                Playerids = [.. trade.PlayersIds],
                Status = TradeStatuses.Pending,
                Tscreated = trade.TradeDate.UtcDateTime,
                Tsexpires = expiresAt,
            };

            // Aspire's AddNpgsqlDbContext enables EnableRetryOnFailure, and a retrying execution
            // strategy refuses a user-initiated transaction unless the whole unit runs through it:
            // it can replay one operation, but not the others that a hand-rolled transaction had
            // already grouped with it, so replaying blindly could commit half a trade. Without this
            // wrapper the first SaveChanges inside the transaction throws and the catch below buries
            // the reason under "Proposing the trade failed". Same shape as DraftService.EndDraft.
            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                // Read inside the delegate: a retry replays this whole block, and a list captured
                // before the first attempt would be stale — with its entities already mutated.
                //
                // A team replaces its own standing offer rather than queuing another one, so the
                // supersede is scoped to the (fromTeam, toTeam) pair. Offers to the same recipient
                // from *other* teams stay pending and compete alongside it.
                var superseded = await _context.GetAllTrades()
                    .Where(t => t.Leagueid == leagueId
                                && t.Fromteamid == trade.FromTeam
                                && t.Toteamid == trade.ToTeam
                                && t.Status == TradeStatuses.Pending)
                    .ToListAsync();

                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    if (superseded.Count > 0)
                    {
                        superseded.ForEach(t => t.Status = TradeStatuses.Superseded);
                        _ = await _context.UpdateTradeRange(superseded);
                    }

                    var created = await _context.AddTrade(row);

                    await transaction.CommitAsync();
                    return (created, superseded);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    // The exception object, not just its message: the cause is replaced by a generic
                    // NBAException below, so this log is the only place it survives.
                    _logger.LogError(ex, "{Log}", new Log("Proposing trade failed", trade, ex.Message).ToJson());
                    throw new NBAException("Proposing the trade failed", ErrorCodes.TradeCantBeExecuted);
                }
            });
        }

        private void ValidateRoster(List<Teamplayer> roster) =>
            _rosterValidator.Validate(
                roster.Count,
                roster.Count(tp => tp.Player?.Playerposition == (int)PlayerPositionEnum.C));
        // Moves the players between the two rosters. Private and single-trade: AcceptProposal is the only
        // caller and only ever settles one proposal, so the list parameter this used to take was a
        // generality nothing asked for — and one that made the "which trade does this row belong to"
        // lookup below necessary in the first place.
        private async Task ApplyTrade(TradeBetweenTeams trade)
        {
            List<TradeBetweenTeams> tradeBetweenTeams = [trade];

            var teamIds = tradeBetweenTeams.SelectMany(u => new[] { u.FromTeam, u.ToTeam })
                .Distinct()
                .ToList();

            var playerdIds = tradeBetweenTeams.SelectMany(u=>u.PlayersIds).Distinct().ToList();

            var oldEntires = await _context.GetAllTeamPlayer()
                .Where(u=> teamIds.Contains(u.Teamid) && playerdIds.Contains(u.Playerid))
                .ToListAsync();

            var newEntries = oldEntires.Select(u =>
            {
                // A trade moves players BOTH ways, so a row is matched on either side of the pair and
                // sent to the opposite team. Matching only on FromTeam (as this did) meant every row
                // belonging to the toTeam found no trade and threw "Trade not specified for team".
                TradeBetweenTeams temp = tradeBetweenTeams!
                    .FirstOrDefault(t => (u.Teamid == t.FromTeam || u.Teamid == t.ToTeam)
                                         && t.PlayersIds.Contains(u.Playerid))
                    ?? throw new NBAException($"Trade not specified for team with id: {u.Teamid}", ErrorCodes.TradeCantBeExecuted);

                return new Teamplayer
                {
                    Teamid = u.Teamid == temp.FromTeam ? temp.ToTeam : temp.FromTeam,
                    Playerid = u.Playerid
                };
            }).ToList();

            // Same reason as AddProposedTrade: the retrying execution strategy has to own the
            // transaction, or the first SaveChanges inside it throws before a single row moves.
            var strategy = _context.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    _ = await _context.DeleteTeamPlayerRange(oldEntires);
                    _ = await _context.AddTeamPlayerRange(newEntries);

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "{Log}", new Log("Trade Failed",tradeBetweenTeams,ex.Message).ToJson());
                    throw new NBAException($"Trader failed", ErrorCodes.TradeCantBeExecuted);
                }
            });


        }

        // Executes an accepted proposal: re-validates against current rosters, swaps the teamplayer
        // rows, and marks the row accepted — all before the caller clears the Redis copy.
        //
        // Re-validation is not redundant with the check at propose time: rosters drift between the two
        // (the other team may have picked up free agents), and this is the point where the data
        // actually changes, so it is the check that protects it.
        public async Task<TradeData> AcceptProposal(long leagueId, Guid tradeId)
        {
            var row = await _context.GetAllTrades()
                .FirstOrDefaultAsync(t => t.Leagueid == leagueId && t.Tradeid == tradeId)
                ?? throw new NBAException("Trade not found.", ErrorCodes.TradeCantBeExecuted);

            if (row.Status != TradeStatuses.Pending)
                throw new NBAException($"Trade is {row.Status} and can no longer be accepted.", ErrorCodes.TradeCantBeExecuted);

            var proposal = new TradeBetweenTeams
            {
                TradeId = row.Tradeid,
                FromTeam = row.Fromteamid,
                ToTeam = row.Toteamid,
                PlayersIds = row.Playerids ?? [],
                TradeDate = new DateTimeOffset(DateTime.SpecifyKind(row.Tscreated, DateTimeKind.Utc)),
            };

            await ValidateSeasonTrade(leagueId, proposal);

            await ApplyTrade(proposal);

            row.Status = TradeStatuses.Accepted;
            _ = await _context.UpdateTradeRange([row]);

            return row;
        }
    }
}
