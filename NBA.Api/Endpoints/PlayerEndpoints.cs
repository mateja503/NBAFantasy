using NBA.Api.DTOs;
using NBA.Api.Mappings;
using NBA.Api.Requests.Player;
using NBA.Data.Entities;
using NBA.Service;
using NBA.Service.Player;

namespace NBA.Api.Endpoints
{
    public static class PlayerEndpoints
    {
        public static IEndpointRouteBuilder MapPlayerEndpoints(this IEndpointRouteBuilder builder)
        {
            // Anonymous by design: the web dashboard shows the player pool to signed-out visitors
            // (see rule 3 in CLAUDE.md). Nothing here is user-specific.
            var players = builder.MapGroup("/players").WithTags("players").AllowAnonymous();

            players.MapGet("", async ([AsParameters] PlayersFilterSearch filter, PlayerService playerService) =>
            {
                var paged = await playerService.GetPagedAsync(filter.ToPlayerSearchInput());

                // Mapped after the query has been materialised: ToPlayerDto resolves the position
                // label with an in-memory switch that EF cannot translate to SQL.
                var result = new PagedResult<PlayerDto>(
                    paged.Items.Select(p => p.ToPlayerDto(filter.leagueId)).ToList(),
                    paged.Page,
                    paged.PageSize,
                    paged.TotalCount);

                return Results.Ok(result);
            });

            return players;
        }

        private static PlayerSearchInput ToPlayerSearchInput(this PlayersFilterSearch filter) => new()
        {
            Name = filter.name,
            Surname = filter.surname,
            Irlteamname = filter.irlteamname,
            Irlteamid = filter.irlteamid,
            Positions = filter.playerposition,

            MinPoints = filter.minPoints,
            MaxPoints = filter.maxPoints,
            MinAssists = filter.minAssists,
            MaxAssists = filter.maxAssists,
            MinRebounds = filter.minRebounds,
            MaxRebounds = filter.maxRebounds,
            MinBlocks = filter.minBlocks,
            MaxBlocks = filter.maxBlocks,
            MinSteals = filter.minSteals,
            MaxSteals = filter.maxSteals,
            MinThreepointers = filter.minThreepointers,
            MaxThreepointers = filter.maxThreepointers,
            MinTurnovers = filter.minTurnovers,
            MaxTurnovers = filter.maxTurnovers,
            MinFreethrow = filter.minFreethrow,
            MaxFreethrow = filter.maxFreethrow,
            MinFieldgoal = filter.minFieldgoal,
            MaxFieldgoal = filter.maxFieldgoal,

            Allowdrop = filter.allowdrop,
            Islock = filter.islock,

            Rosterrole = filter.rosterrole,
            Gameready = filter.gameready,
            Playermemontoid = filter.playermemontoid,

            TscreatedFrom = filter.tscreatedFrom,
            TscreatedTo = filter.tscreatedTo,
            TsupdatedFrom = filter.tsupdatedFrom,
            TsupdatedTo = filter.tsupdatedTo,

            LeagueId = filter.leagueId,

            Page = filter.page,
            PageSize = filter.pageSize,
        };
    }
}
