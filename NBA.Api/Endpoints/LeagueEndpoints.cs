using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using NBA.Api.Authentication;
using NBA.Api.DTOs;
using NBA.Api.Mappings;
using NBA.Api.Requests.League;
using NBA.Api.Requests.LeagueTeam;
using NBA.Service;
using NBA.Service.League;
using NBA.Service.LeaguePlayer;
using NBA.Service.Player;

namespace NBA.Api.Endpoints
{
    public static class LeagueEndpoints
    {
        public static IEndpointRouteBuilder MapLeaguEndpoints(this IEndpointRouteBuilder builder)
        {
            var league = builder.MapGroup("/league").WithTags("league").RequireAuthorization();

            league.MapGet("", async (int? page, int? pageSize, LeagueService leagueService) =>
            {
                var paged = await leagueService.GetPagedAsync(page ?? 1, pageSize ?? 20);

                var result = new PagedResult<LeagueDto>(
                    paged.Items.Select(l => l.ToLeagueDto()).ToList(),
                    paged.Page,
                    paged.PageSize,
                    paged.TotalCount);

                return Results.Ok(result);
            });

            // The league and its player pool are two writes, not one: CreateAsync commits the league
            // (with its statsvalue) before the pool can be seeded, because the seed needs the
            // generated Leagueid. The pool is resolved by PlayerService and written by
            // LeaguePlayerService, and the try/catch is what keeps the pair all-or-nothing - a league
            // with no leagueplayer rows is unusable, so a failed seed undoes the league rather than
            // leaving one behind.
            league.MapPost("/add", async (LeagueRequest? request, ClaimsPrincipal user,
                LeagueService leagueService, PlayerService playerService, LeaguePlayerService leaguePlayerService) =>
            {
                var input = new CreateLeagueInput(
                    user.GetUserId(),
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

                // Declared out here so the catch can undo it; null until CreateAsync has committed,
                // which is also how the catch tells a failed create (nothing to undo) from a failed
                // seed (a committed league to remove).
                NBA.Data.Entities.League? created = null;
                try
                {
                    created = await leagueService.CreateAsync(input);

                    // Every league starts with the whole player pool available as free agents.
                    var playerIds = await playerService.ResolvePlayerPoolIds();
                    _ = await leaguePlayerService.SeedLeaguePool(created.Leagueid, playerIds);

                    return Results.Ok(created.ToLeagueDto());
                }
                catch
                {
                    if (created is not null)
                        await leagueService.DeleteAsync(created.Leagueid);

                    // Rethrown so GlobalExceptionHandler still turns the original NBAException into
                    // the response - the delete is compensation, not error handling.
                    throw;
                }
            });

            league.MapPost("/join", async ([FromBody] LeagueTeamInsertRequest request, ClaimsPrincipal user, LeagueService leagueService) =>
            {
                // User id comes from the token, not the request body.
                var result = await leagueService.JoinAsync(
                    new JoinLeagueInput(request.LeagueId, request.TeamName, user.GetUserId()));

                var dto = result.Team.ToTeamDto();
                dto.Competesinleague = result.League.ToLeagueDto();

                return Results.Ok(dto);
            });

            return league;
        }
    }
}
