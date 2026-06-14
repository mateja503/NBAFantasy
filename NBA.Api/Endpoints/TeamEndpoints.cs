using NBA.Api.Mappings;
using NBA.Api.Requests.Team;
using NBA.Service.League;

namespace NBA.Api.Endpoints
{
    public static class TeamEndpoints
    {
        public static IEndpointRouteBuilder MapTeamEndpoints(this IEndpointRouteBuilder builder)
        {
            var team = builder.MapGroup("/team").WithTags("team");

            team.MapPost("/add", async (TeamRequest request, TeamService teamService) =>
            {
                var created = await teamService.AddAsync(request.teamName);
                return Results.Ok(created.ToTeamDto());
            });

            return team;
        }
    }
}
