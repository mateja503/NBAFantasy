using NBA.Api.Mappings;
using NBA.Service.Game;

namespace NBA.Api.Endpoints
{
    public static class GameEndpoints
    {
        public static IEndpointRouteBuilder MapGameEndpoints(this IEndpointRouteBuilder builder)
        {
            // Anonymous by design, for the same reason as /players (see rule 3 in CLAUDE.md): the NBA
            // schedule is public data with nothing user-specific in it, and the web dashboard shows
            // /games to signed-out visitors.
            var games = builder.MapGroup("/games").WithTags("games").AllowAnonymous();

            games.MapGet("", async (GameService gameService, CancellationToken cancellationToken) =>
            {
                var scheduled = await gameService.GetScheduledGamesAsync(cancellationToken);

                return Results.Ok(scheduled.ToScheduledGamesDto());
            });

            return games;
        }
    }
}
