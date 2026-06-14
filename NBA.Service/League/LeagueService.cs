using ApplicationDefaults.Exceptions;
using Microsoft.EntityFrameworkCore;
using NBA.Data.Context;
using NBA.Data.Entities;

namespace NBA.Service.League
{
    // Input contracts are owned by the service layer so it never depends on the API's
    // request types (dependencies point inward: Api -> Service -> Data).
    public record CreateLeagueInput(
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

    public record JoinLeagueResult(Team Team, NBA.Data.Entities.League League);

    public class LeagueService(NbaFantasyContext context)
    {
        private readonly NbaFantasyContext _context = context;

        public async Task<List<NBA.Data.Entities.League>> GetAllAsync()
        {
            return await _context.GetAllLeagues().AsNoTracking().ToListAsync();
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
            var newStatsValue = new Statsvalue
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
            };

            newStatsValue = await _context.AddStatsValue(newStatsValue);

            var year = DateTime.UtcNow.Year;
            var seasonYear = $"{year}/{year + 1}";

            var newLeague = new NBA.Data.Entities.League
            {
                Name = input.LeagueName,
                Commissioner = 1, // TODO: replace with the authenticated user id once auth lands.
                Seasonyear = seasonYear,
                Weeksforseason = input.WeeksForSeason,
                Transactionlimit = input.TransactionLimit,
                Autostart = input.Autostart,
                Typetransactionlimits = input.TypeTransactionLimits,
                Typeleague = input.LeagueType,
                Draftstyle = input.DraftStyle,
                Statsvalueid = newStatsValue.Statsvalueid
            };

            return await _context.AddLeague(newLeague);
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

            if (league.Teams.Any(u => u.Name.Equals(input.TeamName)))
                throw new NBAException($"Team with name {input.TeamName} already exists in league {league.Name}", ErrorCodes.TeamNameAlreadyInLeague);

            var team = await _context.AddTeam(new Team
            {
                Name = input.TeamName,
                Leagueid = league.Leagueid,
                Userid = input.UserId,
            });

            return new JoinLeagueResult(team, league);
        }
    }
}
