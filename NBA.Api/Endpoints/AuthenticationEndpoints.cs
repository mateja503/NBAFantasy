using ApplicationDefaults.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NBA.Api.Requests.Authentication;
using NBA.Data.Context;
using System.Net.Security;

namespace NBA.Api.Endpoints
{
    public static class AuthenticationEndpoints
    {
        public static IEndpointRouteBuilder MapAuthenticationEndpoints(this IEndpointRouteBuilder builder)
        {
            var auth = builder.MapGroup("auth");

            auth.MapPost("login", async ([FromBody] LoginRequestNBA request, [FromServices]NbaFantasyContext context) => 
            {
                var user = await context.GetApplicationuser(request.Username, request.Password);

                if(user is null)
                    throw new NBAException($"Failed To login", ErrorCodes.LoginFailed);

                return Results.Ok(user);
            });


            //auth.MapPost("sign-up", async ([FromBody] SignUpRequest request) => 
            //{
            //    var user = await context.GetApplicationuser(request.Username, request.Password);

            //});

            return auth;
        
        }
    }
}
