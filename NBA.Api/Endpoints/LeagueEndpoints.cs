using Microsoft.AspNetCore.Mvc;
using NBA.Api.Mappings;
using NBA.Api.Requests.League;
using NBA.Api.Requests.LeagueTeam;
using NBA.Service.League;

namespace NBA.Api.Endpoints
{
    public static class LeagueEndpoints
    {
        public static IEndpointRouteBuilder MapLeaguEndpoints(this IEndpointRouteBuilder builder)
        {
            var league = builder.MapGroup("/league").WithTags("league");

            league.MapGet("", async (LeagueService leagueService) =>
            {
                var leagues = await leagueService.GetAllAsync();
                return Results.Ok(leagues.Select(l => l.ToLeagueDto()).ToList());
            });

            league.MapPost("/add", async (LeagueRequest? request, LeagueService leagueService) =>
            {
                var input = new CreateLeagueInput(
                    request?.LeagueName,
                    request?.LeagueType,
                    request?.DraftStyle,
                    request?.WeeksForSeason,
                    request?.TransactionLimit,
                    request?.TypeTransactionLimits,
                    request?.Autostart,
                    request?.StatsValue is null ? null : new StatsValueInput(
                        request.StatsValue.Points,
                        request.StatsValue.Assists,
                        request.StatsValue.Rebounds,
                        request.StatsValue.Blocks,
                        request.StatsValue.ThreePointersMade,
                        request.StatsValue.ThreePointersMissed,
                        request.StatsValue.FGMade,
                        request.StatsValue.FGMissed,
                        request.StatsValue.FTMade,
                        request.StatsValue.FTMissed,
                        request.StatsValue.Turnovers));

                var created = await leagueService.CreateAsync(input);
                return Results.Ok(created.ToLeagueDto());
            });

            league.MapPost("/join", async ([FromBody] LeagueTeamInsertRequest request, LeagueService leagueService) =>
            {
                var result = await leagueService.JoinAsync(
                    new JoinLeagueInput(request.LeagueId, request.TeamName, request.UserId));

                var dto = result.Team.ToTeamDto();
                dto.Competesinleague = result.League.ToLeagueDto();

                return Results.Ok(dto);
            });

            return league;
        }
    }
}
