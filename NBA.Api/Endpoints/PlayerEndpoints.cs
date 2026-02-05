using Microsoft.EntityFrameworkCore;
using NBA.Data.Context;
using NBA.Data.Entities;

namespace NBA.Api.Endpoints
{
    public static class PlayerEndpoints
    {
        public static IEndpointRouteBuilder MapPlayerEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var player = endpoints.MapGroup("/player")
                .WithTags("players");

            player.MapGet("/", async (NbaFantasyContext context) =>
            {
                var players = await context.GetAllPlayers().AsNoTracking().ToListAsync();
                return Results.Ok(players);
            })
            .WithName("GetAllPlayers")
            .WithSummary("Retrieves a list of all NBA players.")
            .WithDescription("This endpoint returns a comprehensive list of all NBA players in the database.")
            .Produces<List<Player>>(StatusCodes.Status200OK);

            //group.MapGet("/{id:int}", async (int id, IPlayerService playerService) =>
            //{
            //    var player = await playerService.GetPlayerByIdAsync(id);
            //    return player is not null ? Results.Ok(player) : Results.NotFound();
            //})
            //.WithName("GetPlayerById")
            //.WithSummary("Retrieves a specific NBA player by their ID.")
            //.WithDescription("This endpoint returns the details of an NBA player based on the provided player ID.")
            //.Produces<Player>(StatusCodes.Status200OK)
            //.Produces(StatusCodes.Status404NotFound);
            return player;
        }
    }
}
