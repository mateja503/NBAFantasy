using System.Security.Claims;
using NBA.Api.Authentication;
using NBA.Api.Mappings;
using NBA.Service.Trade;

namespace NBA.Api.Endpoints
{
    public static class TradeEndpoints
    {
        public static IEndpointRouteBuilder MapTradeEndpoints(this IEndpointRouteBuilder builder)
        {
            var trades = builder.MapGroup("/trades").WithTags("trades").RequireAuthorization();

            // The trade board's initial load. TradeHub only ever pushes a client the offers aimed at
            // its own team (and only while it is connected), so without this read a manager opening
            // /trade would see nothing but whatever arrived during this session.
            //
            // `status` is optional: omit it for the whole history, pass "pending" for the open offers.
            trades.MapGet("", async (long leagueId, string? status, ClaimsPrincipal user, TradeService tradeService) =>
            {
                var rows = await tradeService.GetLeagueTrades(leagueId, user.GetUserId(), status);

                return Results.Ok(rows.Select(t => t.ToTradeDto()));
            });

            return trades;
        }
    }
}
