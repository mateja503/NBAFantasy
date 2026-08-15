using Microsoft.AspNetCore.Mvc;
using NBA.Api.Requests.Trade;

namespace NBA.Api.Endpoints
{
    // HTTP surface for trades that happen during the regular season. The draft-time path stays on
    // TradeHub: there, every manager already holds a socket and picks run on a clock. In-season a
    // proposal has to survive until the other manager next logs in, so it is stored and read back
    // over these routes rather than pushed.
    public static class TradeEndpoints
    {
        public static IEndpointRouteBuilder MapTradeEndpoints(this IEndpointRouteBuilder builder)
        {
            var trade = builder.MapGroup("/trade").WithTags("trade").RequireAuthorization();

            // In-season trade proposal. Unlike TradeHub.ProposeTrade this must not depend on a live
            // DraftState — by now the draft is over and its Redis state may be gone.
            trade.MapPost("/propose-trade", ([FromBody] ProposeTradeRequest request) =>
            {
                return Results.StatusCode(StatusCodes.Status501NotImplemented);
            });

            trade.MapPost("/accept-trade", ([FromBody] AcceptTradeRequest request) =>
            {
                return Results.StatusCode(StatusCodes.Status501NotImplemented);
            });

            // leagueId scopes the Redis key (RedisKeys.GetProposedDraftTradesKey); teamId is optional
            // so a manager can ask for only the proposals aimed at them.
            trade.MapGet("/get-proposed-trades", (long leagueId, long? teamId) =>
            {
                return Results.StatusCode(StatusCodes.Status501NotImplemented);
            });

            return trade;
        }
    }
}
