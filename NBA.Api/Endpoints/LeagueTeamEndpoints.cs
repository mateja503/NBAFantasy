using ApplicationDefaults.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NBA.Api.DTOs;
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

            leagueTeam.MapPost("/join", async ([FromBody] LeagueTeamInsertRequest request, NbaFantasyContext context) =>
            {
                if (!request.LeagueId.HasValue)
                    throw new NBAException($"LeagueId is required", ErrorCodes.MissingValue);

                if (string.IsNullOrEmpty(request.TeamName))
                    throw new NBAException($"TeamName is required", ErrorCodes.MissingValue);

                if(!request.UserId.HasValue)
                    throw new NBAException($"UserId is required", ErrorCodes.MissingValue);

                var league = await context.GetAllLeagues().Where(u => u.Leagueid == request.LeagueId.Value)
                    .Include(u => u.Teams)
                    .SingleOrDefaultAsync();

                if (league == null)
                    throw new NBAException($"League with id {request.LeagueId.Value} not found", ErrorCodes.DataBaseRecordNotFound);

                if (league.Teams.Count != 0) 
                {
                    if (league.Teams.Any(u => u.Name.Equals(request.TeamName)))
                    {
                        throw new NBAException($"Team with name {request.TeamName} already exists in league {league.Name}", ErrorCodes.TeamNameAlreadyInLeague);
                    }
                }

                var team = await context.AddTeam(new Team
                {
                    Name = request.TeamName,
                    Leagueid = league.Leagueid,
                    Userid = request.UserId,
                });

                league.Teams.Add(team);

                var dtos = league.Teams.Select(u => new LeagueTeamDto
                {
                    Teamid = u.Teamid,
                    Leagueid = league.Leagueid,
                    Approved = u.Approved,
                }).ToList();

                return Results.Ok(dtos);
            });

            return leagueTeam;
        }
    }
}
