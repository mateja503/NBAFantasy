using ApplicationDefaults.Exceptions;
using NBA.Api.DTOs;
using NBA.Api.Requests.Team;
using NBA.Data.Context;
using NBA.Data.Entities;
using System.Reflection.Metadata.Ecma335;

namespace NBA.Api.Endpoints
{
    public static class TeamEndpoints
    {
        public static IEndpointRouteBuilder MapTeamEndpoints(this IEndpointRouteBuilder builder)
        {
            var team = builder.MapGroup("/team").WithTags("team");

            team.MapPost("/add", async (TeamRequest request, NbaFantasyContext context) =>
            {
                if (string.IsNullOrEmpty(request.teamName))
                    throw new NBAException($"{nameof(request.teamName)} is missing", ErrorCodes.MissingParametar);

                var team = await context.AddTeam(new Team { Name = request.teamName });

                var dto = new TeamDto
                {
                    Teamid = team.Teamid,
                    Name = team.Name,
                    Seed = team.Seed,
                    Waiverpriority = team.Waiverpriority,
                    Lastweekpoints = team.Lastweekpoints,
                    Categoryleaguepoints = team.Categoryleaguepoints,
                    Islock = team.Islock
                };
                return Results.Ok(dto);

            });

            return team;
        }
    }
}
