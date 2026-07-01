using NBA.Api.Mappings;
using NBA.Api.Requests.Team;
using NBA.Service.League;

namespace NBA.Api.Endpoints
{
    public static class TeamEndpoints
    {
        public static IEndpointRouteBuilder MapTeamEndpoints(this IEndpointRouteBuilder builder)
        {
            var team = builder.MapGroup("/team").WithTags("team").RequireAuthorization();

            team.MapPost("/add", async (TeamRequest request, TeamService teamService) =>
            {
                var created = await teamService.AddAsync(request.teamName);
                return Results.Ok(created.ToTeamDto());
            });

            team.MapGet("/get-leagues-teams/{leagueId}", async (long leagueId, TeamService teamService) =>
            {
                var teams = await teamService.GetLeagueTeamsAsync(leagueId);
                return Results.Ok(teams.Select(t => t.ToTeamDto()));
            });

            return team;
        }
    }
}
