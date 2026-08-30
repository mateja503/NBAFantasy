using ApplicationDefaults.Exceptions;
using Microsoft.EntityFrameworkCore;
using NBA.Data.Context;
using NBA.Data.Entities;
using TeamData = NBA.Data.Entities.Team;

namespace NBA.Service.League
{
    // Input contracts are owned by the service layer so it never depends on the API's
    // request types (dependencies point inward: Api -> Service -> Data).
    public record CreateLeagueInput(
        long CommissionerUserId,
        string? LeagueName,
        int? LeagueType,
        int? DraftStyle,
        int? WeeksForSeason,
        int? TransactionLimit,
        int? TypeTransactionLimits,
        bool? Autostart,
        StatsValueInput? StatsValue);

    public record StatsValueInput(
        double? Points, double? Assists, double? Rebounds, double? Blocks,
        double? ThreePointersMade, double? ThreePointersMissed,
        double? FGMade, double? FGMissed,
        double? FTMade, double? FTMissed, double? Turnovers);

    public record JoinLeagueInput(long? LeagueId, string? TeamName, long? UserId);

    public record JoinLeagueResult(TeamData Team, NBA.Data.Entities.League League);

    public class LeagueService(NbaFantasyContext context)
    {
        private readonly NbaFantasyContext _context = context;

        private const int MaxPageSize = 100;

        // Paged so the list endpoint never loads the whole table. Ordering by the primary key gives
        // a stable sort, which is required for correct skip/take paging.
        public async Task<PagedResult<NBA.Data.Entities.League>> GetPagedAsync(int page, int pageSize)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize < 1 ? 20 : (pageSize > MaxPageSize ? MaxPageSize : pageSize);

            var query = _context.GetAllLeagues().AsNoTracking().OrderBy(l => l.Leagueid);

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<NBA.Data.Entities.League>(items, page, pageSize, totalCount);
        }

        public async Task<NBA.Data.Entities.League> CreateAsync(CreateLeagueInput input)
        {
            if (input is null)
                throw new NBAException("Body is empty", ErrorCodes.MissingBody);

            if (string.IsNullOrEmpty(input.LeagueName))
                throw new NBAException($"Missing parametar {nameof(input.LeagueName)} for league", ErrorCodes.MissingValue);
            if (!input.LeagueType.HasValue)
                throw new NBAException($"Missing parametar {nameof(input.LeagueType)} for league", ErrorCodes.MissingValue);
            if (!input.DraftStyle.HasValue)
                throw new NBAException($"Missing parametar {nameof(input.DraftStyle)} for league", ErrorCodes.MissingValue);
            if (!input.WeeksForSeason.HasValue)
                throw new NBAException($"Missing parametar {nameof(input.WeeksForSeason)} for league", ErrorCodes.MissingValue);
            if (!input.TransactionLimit.HasValue)
                throw new NBAException($"Missing parametar {nameof(input.TransactionLimit)} for league", ErrorCodes.MissingValue);
            if (!input.TypeTransactionLimits.HasValue)
                throw new NBAException($"Missing parametar {nameof(input.TypeTransactionLimits)} for league", ErrorCodes.MissingValue);
            if (!input.Autostart.HasValue)
                throw new NBAException($"Missing parametar {nameof(input.Autostart)} for league", ErrorCodes.MissingValue);

            var sv = input.StatsValue;

            var year = DateTime.UtcNow.Year;
            var seasonYear = $"{year}/{year + 1}";

            // The statsvalue and league are two separate SaveChanges calls; wrap them so a failed
            // league insert can't leave an orphaned statsvalue row behind.
            //
            // Aspire's AddNpgsqlDbContext enables EnableRetryOnFailure, and a retrying execution
            // strategy refuses a user-initiated transaction unless the whole unit runs through it:
            // it can replay one operation, but not the others a hand-rolled transaction had already
            // grouped with it. Without this wrapper BeginTransactionAsync throws
            // InvalidOperationException ("...does not support user-initiated transactions"). Same
            // shape as TradeService.ProposeAsync and DraftLifecycleService.EndDraft.
            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // Both entities are built inside the delegate: a retry replays this whole block,
                    // and instances the context already tracks from the failed attempt would be
                    // re-added on the second pass.
                    var newStatsValue = await _context.AddStatsValue(new Statsvalue
                    {
                        Pointsvalue = sv?.Points ?? (double)BoxScoreEvaluation.Points,
                        Assistsvalue = sv?.Assists ?? (double)BoxScoreEvaluation.Assists,
                        Reboundsvalue = sv?.Rebounds ?? (double)BoxScoreEvaluation.Rebounds,
                        Blocksvalue = sv?.Blocks ?? (double)BoxScoreEvaluation.Blocks,
                        Threepointsvaluemade = sv?.ThreePointersMade ?? (double)BoxScoreEvaluation.ThreePointsMade,
                        Threepointsvaluemissed = sv?.ThreePointersMissed ?? (double)BoxScoreEvaluation.ThreePointsMissed,
                        Fieldgoalvaluemade = sv?.FGMade ?? (double)BoxScoreEvaluation.FieldGoalMade,
                        Fieldgoalvaluemissed = sv?.FGMissed ?? (double)BoxScoreEvaluation.FieldGoalMissed,
                        Freethrowvaluemade = sv?.FTMade ?? (double)BoxScoreEvaluation.FreeThrowMade,
                        Freethrowvaluemissed = sv?.FTMissed ?? (double)BoxScoreEvaluation.FreeThrowMissed,
                        Turnoversvalue = sv?.Turnovers ?? (double)BoxScoreEvaluation.Turnovers,
                    });

                    var created = await _context.AddLeague(new NBA.Data.Entities.League
                    {
                        Name = input.LeagueName,
                        Commissioner = input.CommissionerUserId,
                        Seasonyear = seasonYear,
                        Weeksforseason = input.WeeksForSeason,
                        Transactionlimit = input.TransactionLimit,
                        Autostart = input.Autostart,
                        Typetransactionlimits = input.TypeTransactionLimits,
                        Typeleague = input.LeagueType,
                        Draftstyle = input.DraftStyle,
                        Statsvalueid = newStatsValue.Statsvalueid
                    });

                    await transaction.CommitAsync();
                    return created;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

        // Compensating delete for a league whose creation only half-succeeded. CreateAsync commits
        // the league before its player pool is seeded (the seed is sequenced by the caller), so a
        // failed seed would otherwise leave a league no one can use - this is what undoes it.
        //
        // Order matters: leagueplayer rows reference the league and the league references the
        // statsvalue, so the children go first or the foreign keys reject the delete. Wrapped in a
        // transaction because it is three SaveChanges calls and a partial undo is worse than none.
        public async Task DeleteAsync(long leagueId)
        {
            // Same reason as CreateAsync: the retrying Npgsql strategy rejects a hand-rolled
            // transaction unless the whole unit is handed to it.
            var strategy = _context.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                // Read inside the delegate: a retry replays this whole block, and rows fetched
                // before the first attempt would be stale - or already removed by it.
                var league = await _context.GetAllLeagues()
                    .Where(l => l.Leagueid == leagueId)
                    .SingleOrDefaultAsync();

                // Already gone - nothing to undo, and the caller is on an error path either way.
                if (league is null)
                    return;

                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // Whatever the failed seed managed to insert before it threw.
                    var leaguePlayers = await _context.GetAllLeaguePlayers()
                        .Where(lp => lp.Leagueid == leagueId)
                        .ToListAsync();

                    if (leaguePlayers.Count > 0)
                        _ = await _context.DeleteLeaguePlayersRange(leaguePlayers);

                    var statsValueId = league.Statsvalueid;

                    _ = await _context.DeleteLeague(league);

                    // The statsvalue is owned by the league (one-to-one, created alongside it), so it
                    // would be orphaned if left behind.
                    if (statsValueId.HasValue)
                    {
                        var statsValue = await _context.GetAllStatsValues()
                            .Where(sv => sv.Statsvalueid == statsValueId.Value)
                            .SingleOrDefaultAsync();

                        if (statsValue is not null)
                            _ = await _context.DeleteStatsValue(statsValue);
                    }

                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

        public async Task<JoinLeagueResult> JoinAsync(JoinLeagueInput input)
        {
            if (!input.LeagueId.HasValue)
                throw new NBAException("LeagueId is required", ErrorCodes.MissingValue);
            if (string.IsNullOrEmpty(input.TeamName))
                throw new NBAException("TeamName is required", ErrorCodes.MissingValue);
            if (!input.UserId.HasValue)
                throw new NBAException("UserId is required", ErrorCodes.MissingValue);




            var league = await _context.GetAllLeagues()
                .Where(u => u.Leagueid == input.LeagueId.Value)
                .Include(u => u.Teams)
                .SingleOrDefaultAsync();

            if (league is null)
                throw new NBAException($"League with id {input.LeagueId.Value} not found", ErrorCodes.DataBaseRecordNotFound);

            if (league.Teams.Any(u => u.Name.Equals(input.TeamName,StringComparison.OrdinalIgnoreCase)))
                throw new NBAException($"Team with name {input.TeamName} already exists in league {league.Name}", ErrorCodes.TeamNameAlreadyInLeague);

            if(league.Teams.Any(u => u.Userid!.Value == input.UserId))
                throw new NBAException($"User with id {input.UserId} already has a team in league {league.Name}", ErrorCodes.UserAlreadyHasTeamInLeague);

            var team = await _context.AddTeam(new TeamData
            {
                Name = input.TeamName,
                Leagueid = league.Leagueid,
                Userid = input.UserId,
            });

            return new JoinLeagueResult(team, league);
        }
    }
}
