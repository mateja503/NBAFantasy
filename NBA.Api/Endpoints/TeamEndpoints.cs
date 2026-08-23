using System.Security.Claims;
using ApplicationDefaults.Exceptions;
using NBA.Api.Authentication;
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

            team.MapGet("/get-user-teams/{userId}", async (long userId, ClaimsPrincipal user, TeamService teamService) =>
            {
                // The client sends the id it holds in local storage, which is trivially editable, so it
                // is only honoured when it matches the id the token was issued for.
                if (user.GetUserId() != userId)
                    throw new NBAException("Requested userId does not match the authenticated user", ErrorCodes.UserIdMismatch);

                var teams = await teamService.GetUserTeamsWithPlayersAsync(userId);
                return Results.Ok(teams.Select(kvp => kvp.Key.ToUserTeamDto(kvp.Value)));
            });

            // Any roster in a league you play in — what the trade board needs to show both sides of an
            // offer. The service enforces the league-membership check; the id in the route is not
            // trusted on its own.
            team.MapGet("/get-team-players/{teamId}", async (long teamId, ClaimsPrincipal user, TeamService teamService) =>
            {
                var players = await teamService.GetTeamPlayersAsync(teamId, user.GetUserId());

                // No leagueId passed: the fantasy-team column is meaningless here, the roster already
                // says which team these players belong to.
                return Results.Ok(players.Select(p => p.ToPlayerDto()));
            });

            return team;
        }
    }
}
