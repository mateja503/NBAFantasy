using ApplicationDefaults.Exceptions;
using Microsoft.EntityFrameworkCore;
using NBA.Api.Requests.League;
using NBA.Data.Context;
using NBA.Data.Entities;
using NBA.Service;

namespace NBA.Api.Endpoints
{
    public static class LeagueEndpoints
    {
        public static IEndpointRouteBuilder MapLeaguEndpoints(this IEndpointRouteBuilder builder)
        {
            var league = builder.MapGroup("/league").WithTags("league");


            league.MapGet("", async (NbaFantasyContext context) =>
            {
                return await context.GetAllLeagues().AsNoTracking().ToListAsync();
            });

            league.MapPost("/add", async (LeagueRequest? request, NbaFantasyContext context) =>
            {
                if (request == null) throw new NBAException("Body is emtpy", ErrorCodes.MissingBody);

                if (string.IsNullOrEmpty(request.LeagueName))
                    throw new NBAException($"Missing parametar {nameof(request.LeagueName)} for league", ErrorCodes.MissingValue);

                if (!request.LeagueType.HasValue)
                    throw new NBAException($"Missing parametar {nameof(request.LeagueType)} for league", ErrorCodes.MissingValue);

                if (!request.DraftStyle.HasValue)
                    throw new NBAException($"Missing parametar {nameof(request.DraftStyle)} for league", ErrorCodes.MissingValue);

                if (!request.WeeksForSeason.HasValue)
                    throw new NBAException($"Missing parametar {nameof(request.WeeksForSeason)} for league", ErrorCodes.MissingValue);

                if (!request.TransactionLimit.HasValue)
                    throw new NBAException($"Missing parametar {nameof(request.TransactionLimit)} for league", ErrorCodes.MissingValue);

                if (!request.TypeTransactionLimits.HasValue)
                    throw new NBAException($"Missing parametar {nameof(request.TypeTransactionLimits)} for league", ErrorCodes.MissingValue);

                if (!request.Autostart.HasValue)
                    throw new NBAException($"Missing parametar {nameof(request.Autostart)} for league", ErrorCodes.MissingValue);

                //if (!request.ScoringSystem.HasValue)
                //    throw new NBAException($"Missing parametar {nameof(request.ScoringSystem)} for league", ErrorCodes.MissingValue);

                var newStatsValue = new Statsvalue()
                {
                    Pointsvalue = request.StatsValue?.Points ?? (double)BoxScoreEvaluation.Points,
                    Assistsvalue = request.StatsValue?.Assists ?? (double)BoxScoreEvaluation.Assists,
                    Reboundsvalue = request.StatsValue?.Rebounds ?? (double)BoxScoreEvaluation.Rebounds,
                    Blocksvalue = request.StatsValue?.Blocks ?? (double)BoxScoreEvaluation.Blocks,
                    Threepointsvaluemade = request.StatsValue?.ThreePointersMade ?? (double)BoxScoreEvaluation.ThreePointsMade,
                    Threepointsvaluemissed = request.StatsValue?.ThreePointersMissed ?? (double)BoxScoreEvaluation.ThreePointsMissed,
                    Fieldgoalvaluemade = request.StatsValue?.FGMade ?? (double)BoxScoreEvaluation.FieldGoalMade,
                    Fieldgoalvaluemissed = request.StatsValue?.FGMissed ?? (double)BoxScoreEvaluation.FieldGoalMissed,
                    Freethrowvaluemade = request.StatsValue?.FTMade ?? (double)BoxScoreEvaluation.FreeThrowMade,
                    Freethrowvaluemissed = request.StatsValue?.FTMissed ?? (double)BoxScoreEvaluation.FreeThrowMissed,
                    Turnoversvalue = request.StatsValue?.Turnovers ?? (double)BoxScoreEvaluation.Turnovers,
                };

                newStatsValue = await context.AddStatsValue(newStatsValue);
                var year = DateTime.UtcNow.Year;
                var nextYear = year + 1;
                
                var newLeague = new League
                {
                    Name = request.LeagueName,
                    Commissioner = 1,//user-created id
                    Seasonyear = $"{year}/{nextYear}",
                    Weeksforseason = request.WeeksForSeason,
                    Transactionlimit = request.TransactionLimit,
                    Autostart = request.Autostart,
                    Typetransactionlimits = request.TypeTransactionLimits,
                    Typeleague = request.LeagueType,
                    Draftstyle = request.DraftStyle,
                    Statsvalueid = newStatsValue.Statsvalueid
                };

                newLeague = await context.AddLeague(newLeague);

                return Results.Ok(newLeague);

            });

            return league;
        }
    }
}
