using ApplicationDefaults.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NBA.Api.DTOs;
using NBA.Api.Requests.League;
using NBA.Api.Requests.LeagueTeam;
using NBA.Data.Context;
using NBA.Data.Entities;
using NBA.Service;
using System.Data;

namespace NBA.Api.Endpoints
{
    public static class LeagueEndpoints
    {
        public static IEndpointRouteBuilder MapLeaguEndpoints(this IEndpointRouteBuilder builder)
        {
            var league = builder.MapGroup("/league").WithTags("league");

            league.MapGet("", async (NbaFantasyContext context) =>
            {
                return await context.GetAllLeagues().AsNoTracking()
                .Select(u=> new LeagueDto 
                {
                    Leagueid = u.Leagueid,
                    Name = u.Name,
                    Commissioner = u.Commissioner,
                    Seasonyear = u.Seasonyear,
                    Weeksforseason = u.Weeksforseason,
                    Transactionlimit = u.Transactionlimit,
                    Autostart = u.Autostart,
                    Typetransactionlimits = u.Typetransactionlimits,
                    Typeleague = u.Typeleague,
                    Draftstyle = u.Draftstyle,
                    Statsvalueid = u.Statsvalueid,
                }).ToListAsync();
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

                var dto = new LeagueDto
                {
                    Leagueid = newLeague.Leagueid,
                    Name = newLeague.Name,
                    Commissioner = newLeague.Commissioner,
                    Seasonyear = newLeague.Seasonyear,
                    Weeksforseason = newLeague.Weeksforseason,
                    Transactionlimit = newLeague.Transactionlimit,
                    Autostart = newLeague.Autostart,
                    Typetransactionlimits = newLeague.Typetransactionlimits,
                    Typeleague = newLeague.Typeleague,
                    Draftstyle = newLeague.Draftstyle,
                    Statsvalueid = newLeague.Statsvalueid,
                };

                return Results.Ok(dto);

            });

            league.MapPost("/join", async ([FromBody] LeagueTeamInsertRequest request, NbaFantasyContext context) =>
            {
                if (!request.LeagueId.HasValue)
                    throw new NBAException($"LeagueId is required", ErrorCodes.MissingValue);

                if (string.IsNullOrEmpty(request.TeamName))
                    throw new NBAException($"TeamName is required", ErrorCodes.MissingValue);

                if (!request.UserId.HasValue)
                    throw new NBAException($"UserId is required", ErrorCodes.MissingValue);

                var league = await context.GetAllLeagues().Where(u => u.Leagueid == request.LeagueId.Value)
                    .Include(u => u.Teams)
                    .SingleOrDefaultAsync();

                if (league == null)
                    throw new NBAException($"League with id {request.LeagueId.Value} not found", ErrorCodes.DataBaseRecordNotFound);

                if (league.Teams.Count != 0)
                {
                    if (league.Teams.Any(u => u.Name.Equals(request.TeamName)))
                    {
                        throw new NBAException($"Team with name {request.TeamName} already exists in league {league.Name}", ErrorCodes.TeamNameAlreadyInLeague);
                    }
                }

                var team = await context.AddTeam(new Team
                {
                    Name = request.TeamName,
                    Leagueid = league.Leagueid,
                    Userid = request.UserId,
                });


                var dto = new TeamDto
                {
                    Teamid = team.Teamid,
                    Name = team.Name,
                    Seed = team.Seed,
                    Waiverpriority = team.Waiverpriority,
                    Lastweekpoints = team.Lastweekpoints,
                    Categoryleaguepoints = team.Categoryleaguepoints,
                    Islock = team.Islock,
                    Competesinleague = new LeagueDto
                    {
                        Leagueid = league.Leagueid,
                        Name = league.Name,
                        Commissioner = league.Commissioner,
                        Seasonyear = league.Seasonyear,
                        Weeksforseason = league.Weeksforseason,
                        Transactionlimit = league.Transactionlimit,
                        Autostart = league.Autostart,
                        Typetransactionlimits = league.Typetransactionlimits,
                        Typeleague = league.Typeleague,
                        Draftstyle = league.Draftstyle,
                        Statsvalueid = league.Statsvalueid,
                    }
                };

                return Results.Ok(dto);
            });

            return league;
        }
    }
}
