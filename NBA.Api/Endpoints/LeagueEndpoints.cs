using ApplicationDefaults.Exceptions;
using Microsoft.EntityFrameworkCore;
using NBA.Api.Requests.League;
using NBA.Data.Context;
using NBA.Data.Entities;

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

                if (string.IsNullOrEmpty(request.Name))
                    throw new NBAException($"Missing parametar {nameof(request.Name)} for league", ErrorCodes.MissingValue);

                if (!request.Typeleague.HasValue)
                    throw new NBAException($"Missing parametar {nameof(request.Typeleague)} for league", ErrorCodes.MissingValue);

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

                if (!request.UseDefaultScorigValues.HasValue)
                    throw new NBAException($"Missing parametar {nameof(request.UseDefaultScorigValues)} for league", ErrorCodes.MissingValue);

                if (request.StatsValue == null)
                    throw new NBAException($"Missing parametar {nameof(request.StatsValue)} for league", ErrorCodes.MissingValue);


                //TODO decide if to use the default values for ther StatsValue or custom 

                var league = new League
                {
                    Name = request.Name,
                    Commissioner = 1,//user-created id
                    Seasonyear = "2026/2027",
                    Weeksforseason = request.WeeksForSeason,
                    Transactionlimit = request.TransactionLimit,
                    Autostart = request.Autostart,
                    Typetransactionlimits = request.TypeTransactionLimits,
                    Typeleague = request.Typeleague,
                    Draftstyle = request.DraftStyle,
                    Statsvalueid = 1
                };

                league = await context.AddLeague(league);

                return Results.Ok(league);

            });

            return league;
        }
    }
}
