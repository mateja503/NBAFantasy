using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NBA.Api.Requests.League;
using NBA.Data.Context;
using NBA.Data.Entities;
using System;

namespace NBA.Api.Endpoints
{
    public static class LeagueEndpoints
    {
        public static IEndpointRouteBuilder MapLeagueEnpoints(this IEndpointRouteBuilder builder) 
        {
            var league = builder.MapGroup("/league");

            league.MapGet("/all", async (NbaFantasyContext context) =>
            {
                var leagues = await context.GetAllLeagues().AsNoTracking().ToListAsync();
                return Results.Ok(leagues);
            }).WithTags("league");

            league.MapPost("/add", async (LeagueInsertRequest request,NbaFantasyContext context, [FromHeader] string userId) =>
            {
                League entity = new League
                {
                    Name = request.Name ?? "UNKOWN",
                    Usercreated = userId,
                    Tscreated = DateTime.UtcNow
                };

                entity = await context.AddLeague(entity);

                return Results.Ok(entity);
            }).WithTags("league");

            league.MapPut("/update/{leagueid:required}", async (long leagueid,LeagueUpdateRequest request, NbaFantasyContext context, [FromHeader] string userId) =>
            {
                var league = await context.GetAllLeagues().FirstOrDefaultAsync(l => l.Leagueid == leagueid);
                if (league == null)
                {
                    return Results.NotFound();
                }
                league.Name = request.Name ?? league.Name;
                league.Tsupdated = DateTime.UtcNow;
                league.Userupdated = userId;
                await context.UpdateLeage(league);
                return Results.Ok(league);
            }).WithTags("league");



            return league;
        }
    }
}
