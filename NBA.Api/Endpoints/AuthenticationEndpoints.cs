using ApplicationDefaults.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NBA.Api.DTOs;
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

                //TODO get leagues and teams in a different way
                var leagues = await context.GetAllLeagues().Where(l => l.Commissioner == user.Userid
                && l.Teams.Any(lt => lt.Userid == user.Userid))
               .Select(u => new LeagueDto
               {
                   Leagueid = u.Leagueid,
                   Name = u.Name,
                   Commissioner = u.Commissioner,
                   Seasonyear = u.Seasonyear,
                   Weeksforseason = u.Weeksforseason,
                   Transactionlimit = u.Transactionlimit,
                   Autostart = u.Autostart,
                   Typetransactionlimits = u.Typetransactionlimits,
                   Typeleague = u.Typeleague,
                   Draftstyle = u.Draftstyle,
                   Statsvalueid = u.Statsvalueid,
                   CommissionersTeam = u.Teams.Where(lt => lt.Userid == user.Userid).Select(t => new TeamDto
                   {
                       Teamid = t.Teamid,
                       Name = t.Name,
                       Seed = t.Seed,
                       Waiverpriority = t.Waiverpriority,
                       Lastweekpoints = t.Lastweekpoints,
                       Categoryleaguepoints = t.Categoryleaguepoints,
                       Islock = t.Islock
                   }).FirstOrDefault()
               })
               .ToListAsync();
               

                var commissinersTeams = leagues.Select(u => u.CommissionersTeam!.Teamid).ToList();

                var teams = await context.GetAllTeams().Where(t => t.Userid == user.Userid && !commissinersTeams.Contains(t.Teamid))
                .Select(t => new TeamDto
                {
                    Teamid = t.Teamid,
                    Name = t.Name,
                    Seed = t.Seed,
                    Waiverpriority = t.Waiverpriority,
                    Lastweekpoints = t.Lastweekpoints,
                    Categoryleaguepoints = t.Categoryleaguepoints,
                    Islock = t.Islock,
                    Competesinleague = new LeagueDto 
                    {
                        Leagueid = t.League!.Leagueid,
                        Name = t.League!.Name,
                        Commissioner = t.League.Commissioner,
                        Seasonyear = t.League.Seasonyear,
                        Weeksforseason = t.League.Weeksforseason,
                        Transactionlimit = t.League.Transactionlimit,
                        Autostart = t.League.Autostart,
                        Typetransactionlimits = t.League.Typetransactionlimits,
                        Typeleague = t.League.Typeleague,
                        Draftstyle = t.League.Draftstyle,
                        Statsvalueid = t.League.Statsvalueid,
                    }
                })
                .ToListAsync();

                var res = new LoginDto
                {
                    Username = user.Username!,
                    Teams = teams,
                    Leagues = leagues
                };

                return Results.Ok(res);
            });


            //auth.MapPost("sign-up", async ([FromBody] SignUpRequest request) => 
            //{
            //    var user = await context.GetApplicationuser(request.Username, request.Password);

            //});

            return auth;
        
        }
    }
}
