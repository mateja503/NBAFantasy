namespace NBA.Api.Endpoints
{
    public static class TeamEndpoints
    {
        public static IEndpointRouteBuilder MapTeamEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var team = endpoints.MapGroup("/team")
                .WithTags("team");
            team.MapGet("/", () =>
            {
                return Results.Ok("This is the team endpoint.");
            })
            .WithName("GetTeam")
            .WithSummary("Retrieves information about NBA teams.")
            .WithDescription("This endpoint returns information about NBA teams.");

            team.MapGet("/{id:int}", (int id) =>
            {
                return Results.Ok($"This is the team endpoint for team with ID: {id}");
            });

            team.MapPost("/", () =>
            {
                return Results.Ok("This is the POST endpoint for teams.");
            });
            team.MapDelete("/{id:int}", (int id) =>
            {
                return Results.Ok($"This is the DELETE endpoint for team with ID: {id}");
            });
            return team;
        }
    }
}
