using Microsoft.AspNetCore.Mvc;
using NBA.Api.DTOs;
using NBA.Api.Mappings;
using NBA.Api.Requests.Authentication;
using NBA.Service.Authentication;

namespace NBA.Api.Endpoints
{
    public static class AuthenticationEndpoints
    {
        public static IEndpointRouteBuilder MapAuthenticationEndpoints(this IEndpointRouteBuilder builder)
        {
            var auth = builder.MapGroup("auth");

            auth.MapPost("login", async ([FromBody] LoginRequestNBA request, [FromServices] AuthService authService) =>
            {
                var result = await authService.LoginAsync(request.Username, request.Password);

                var leagues = result.CommissionerLeagues.Select(l =>
                {
                    var dto = l.ToLeagueDto();
                    dto.CommissionersTeam = l.Teams.FirstOrDefault()?.ToTeamDto();
                    return dto;
                }).ToList();

                var teams = result.OtherTeams.Select(t =>
                {
                    var dto = t.ToTeamDto();
                    dto.Competesinleague = t.League?.ToLeagueDto();
                    return dto;
                }).ToList();

                var res = new LoginDto
                {
                    Userid = result.User.Userid,
                    Username = result.User.Username!,
                    Teams = teams,
                    Leagues = leagues
                };

                return Results.Ok(res);
            });

            return auth;
        }
    }
}
