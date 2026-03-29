using ApplicationDefaults.Exceptions;
using Microsoft.EntityFrameworkCore;
using NBA.Api.Requests.LeagueTeam;
using NBA.Data.Context;
using NBA.Data.Entities;

namespace NBA.Api.Endpoints
{
    public static class LeagueTeamEndpoints
    {
        public static IEndpointRouteBuilder MapLeagueTeamEndpoints(this IEndpointRouteBuilder builder)
        {
            var leagueTeam = builder.MapGroup("/league-team").WithTags("league-team");

            leagueTeam.MapPost("/join", async (LeagueTeamRequest request, NbaFantasyContext context) =>
            {
                if (!request.LeagueId.HasValue)
                    throw new NBAException($"{nameof(request.LeagueId)} is missing", ErrorCodes.MissingValue);

                if (string.IsNullOrEmpty(request.TeamName))
                    throw new NBAException($"{nameof(request.TeamName)} is missing", ErrorCodes.MissingValue);

                var teamsInLeague = await context.GetAllLeagueTeam().Where(u => u.Leagueid == request.LeagueId.Value)
                .Include(u => u.Team)
                .Select(u => u.Team.Name)
                .ToListAsync();

                if (teamsInLeague.Contains(request.TeamName))
                    throw new NBAException($"Team with name {request.TeamName} already exists in league with id {request.LeagueId.Value}", ErrorCodes.TeamNameAlreadyInLeague);

                var newTeam = await context.AddTeam(new Team { Name = request.TeamName });

                var res = await context.AddLeagueTeam(new Leagueteam
                {
                    Leagueid = request.LeagueId.Value,
                    Teamid = newTeam.Teamid
                });

                return Results.Ok(res);
            });
           
            return leagueTeam;
        }
    }
}
