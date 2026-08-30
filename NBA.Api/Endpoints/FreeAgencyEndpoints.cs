using ApplicationDefaults.Exceptions;
using Microsoft.AspNetCore.Mvc;
using NBA.Api.Mappings;
using NBA.Api.Requests.FreeAgency;
using NBA.Service.FreeAgency;

namespace NBA.Api.Endpoints
{
    public static class FreeAgencyEndpoints
    {
        public static IEndpointRouteBuilder MapFreeAgencyEndpoints(this IEndpointRouteBuilder builder)
        {
            var freeAgency = builder.MapGroup("free-agency").WithTags("free-agency").RequireAuthorization();

            // Every unowned player in one league's pool. leagueId is a query parameter rather than a
            // route segment so the path stays /free-agency/all-players; a missing binding arrives here
            // as null, which is rejected instead of silently querying league 0.
            freeAgency.MapGet("all-players", async (long? leagueId, FreeAgencyService freeAgencyService) =>
            {
                if (!leagueId.HasValue || leagueId.Value <= 0)
                    throw new NBAException("Missing value for leagueId", ErrorCodes.MissingValue);

                var players = await freeAgencyService.GetFreeAgents(leagueId.Value);

                // No leagueId passed to the mapper on purpose: a free agent has no fantasy team, so
                // resolving the Team column would be a guaranteed-null join.
                return Results.Ok(players.Select(p => p.ToPlayerDto()));
            });

            // Flips Isfreeagent on the league's rows for the supplied players. PUT rather than POST
            // because the call sets the state of rows that already exist - the pool is seeded at league
            // creation, so nothing is created here.
            freeAgency.MapPut("pick-up-player", async ([FromBody] FreeAgencyPickUpRequest request,
                FreeAgencyService freeAgencyService) =>
            {
                if (!request.leagueId.HasValue || request.leagueId.Value <= 0)
                    throw new NBAException("Missing value for leagueId", ErrorCodes.MissingValue);

                // Null and empty are both rejected: an empty list would toggle nothing and return an
                // empty 200, which reads as success to the caller.
                if (request.playerIds is null || request.playerIds.Count == 0)
                    throw new NBAException("Missing value for playerIds", ErrorCodes.MissingValue);

                var players = await freeAgencyService.ToggleFreeAgencyStatus(request.leagueId.Value, request.playerIds);

                // Only the rows that actually exist in this league's pool come back, so the caller can
                // compare against what it sent to see which ids were ignored.
                return Results.Ok(players.Select(p => p.ToPlayerDto()));
            });

            return freeAgency;
        }
    }
}
