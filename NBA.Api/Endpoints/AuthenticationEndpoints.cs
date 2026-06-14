using Microsoft.AspNetCore.Mvc;
using NBA.Api.Authentication;
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
            // Anonymous by default: these endpoints are how a caller obtains a token. They are not
            // covered by the RequireAuthorization() applied to the other groups.
            var auth = builder.MapGroup("auth").WithTags("auth").AllowAnonymous();

            auth.MapPost("register", async ([FromBody] SignUpRequest request, [FromServices] AuthService authService, [FromServices] AuthTokenIssuer tokenIssuer) =>
            {
                var user = await authService.RegisterAsync(request.Username, request.Email, request.Password);
                var tokens = await tokenIssuer.IssueAsync(user);

                return Results.Ok(new LoginDto
                {
                    Userid = user.Userid,
                    Username = user.Username!,
                    Token = tokens.AccessToken,
                    RefreshToken = tokens.RefreshToken,
                    Teams = [],
                    Leagues = []
                });
            });

            auth.MapPost("login", async ([FromBody] LoginRequestNBA request, [FromServices] AuthService authService, [FromServices] AuthTokenIssuer tokenIssuer) =>
            {
                var result = await authService.LoginAsync(request.Username, request.Password);
                var tokens = await tokenIssuer.IssueAsync(result.User);

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

                return Results.Ok(new LoginDto
                {
                    Userid = result.User.Userid,
                    Username = result.User.Username!,
                    Token = tokens.AccessToken,
                    RefreshToken = tokens.RefreshToken,
                    Teams = teams,
                    Leagues = leagues
                });
            });

            // Exchanges a valid refresh token for a new access+refresh pair. The presented token is
            // consumed (single-use rotation), so a stolen-then-reused token is rejected as invalid.
            auth.MapPost("refresh", async ([FromBody] RefreshRequest request, [FromServices] AuthTokenIssuer tokenIssuer) =>
            {
                var tokens = await tokenIssuer.RefreshAsync(request.RefreshToken);
                return Results.Ok(new LoginDto
                {
                    Token = tokens.AccessToken,
                    RefreshToken = tokens.RefreshToken
                });
            });

            // Logout revokes the refresh token so it can't be used again. The access token remains
            // valid until it expires (stateless JWTs can't be revoked server-side without a denylist).
            auth.MapPost("logout", async ([FromBody] RefreshRequest request, [FromServices] AuthTokenIssuer tokenIssuer) =>
            {
                await tokenIssuer.RevokeAsync(request.RefreshToken);
                return Results.NoContent();
            });

            return auth;
        }
    }
}
