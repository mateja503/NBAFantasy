using ExternalClients;
using Microsoft.EntityFrameworkCore;
using NBA.Data.Context;
using NBA.Service.League.Draft;

namespace NBA.Api.Endpoints
{
    public static class TestingEndpoints
    {
        public static IEndpointRouteBuilder TestEndpoints(this IEndpointRouteBuilder builder) 
        {
            //builder.MapGet("/activePlayers", async (NbaFantasyContext context, BallDontLieClient client) => 
            //{
            //    return await context.GetAllPlayers().AsNoTracking().ToListAsync();
            //    //return await client.GetAllPlayers();

            //}).WithTags("misc");

            builder.MapGet("/players", async (NbaFantasyContext context, BallDontLieClient client) =>
            {
                return Results.Ok();

            }).WithTags("misc");
            return builder;

        }
    }
}
