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

            leagueTeam.MapPost("/join", async (List<LeagueTeamInsertRequest> request, NbaFantasyContext context) =>
            {
                if (!request.Any())
                    throw new NBAException($"{nameof(request)} object is missing", ErrorCodes.MissingValue);

                var getAllLeagues = request.Select(u => u.LeagueId).ToList();
                var getAllTeamNames = request.DistinctBy(u => u.TeamName).Select(u => u.TeamName?.ToLower()).ToList();

                var teamsInLeague = await context.GetAllLeagueTeam().Where(u => getAllLeagues.Contains(u.Leagueid) 
                && getAllTeamNames.Contains(u.Team.Name.ToLower()))
                .ToListAsync();

                if (teamsInLeague is null)
                {
                    var newTeams = request.DistinctBy(u => u.TeamName).Select(u => new Team { Name = u.TeamName ?? "FantasyTeam1" }).ToList();
                    newTeams = await context.AddTeamRange(newTeams);

                    var leagueTeams = request.Select(u => 
                    new Leagueteam { 
                        Leagueid = u.LeagueId ?? 0,
                        Teamid = newTeams.Select(u=>u.Teamid).FirstOrDefault()//this is becuse there will be only one teamName
                    }).ToList();

                    leagueTeams = await context.AddLeagueTeamRange(leagueTeams);

                    return Results.Ok(leagueTeams);
                }
                var existingNames = string.Join(", ", teamsInLeague.Select(t => $"{t.League.Name} - {t.Team.Name}"));
                throw new NBAException($"Team with names for leagues already exist {existingNames}", ErrorCodes.TeamNameAlreadyInLeague);
             
            });
           
            return leagueTeam;
        }
    }
}
