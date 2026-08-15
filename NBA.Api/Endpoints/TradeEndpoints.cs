using ApplicationDefaults.Exceptions;
using ApplicationDefaults.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using NBA.Api.Mappings;
using NBA.Api.Requests.Trade;
using NBA.Api.SignalR;
using NBA.Api.SignalR.Clients;
using NBA.Api.SignalR.Hubs;
using NBA.Data.Redis.Entities;
using NBA.Service.League.Trade;

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

            trade.MapPost("/propose-trade", async (
                [FromBody] ProposeTradeRequest request,
                TradeService tradeService,
                TradeManager tradeManager,
                TradePresenceProbe presenceProbe,
                IHubContext<TradeHub, ITradeHubClient> tradeHub,
                IOptions<ApplicationOptions> applicationOptions,
                CancellationToken cancellationToken) =>
            {
                if (!request.leagueId.HasValue)
                    throw new NBAException("Missing value for leagueId", ErrorCodes.MissingValue);

                if (!request.fromTeam.HasValue)
                    throw new NBAException("Missing value for fromTeam", ErrorCodes.MissingValue);

                if (!request.toTeam.HasValue)
                    throw new NBAException("Missing value for toTeam", ErrorCodes.MissingValue);

                if (request.playersIds is null || request.playersIds.Count == 0)
                    throw new NBAException("Missing value for playersIds", ErrorCodes.MissingValue);

                var proposal = new TradeBetweenTeams
                {
                    TradeId = Guid.NewGuid(),
                    FromTeam = request.fromTeam.Value,
                    ToTeam = request.toTeam.Value,
                    PlayersIds = request.playersIds,
                };

                // Rosters come from nba.teamplayer, not the draft's Redis state — by the season that
                // state is gone. Runs before anything is written so an illegal offer is never stored.
                await tradeService.ValidateSeasonTrade(request.leagueId.Value, proposal);

                var ttl = TimeSpan.FromMinutes(applicationOptions.Value.ProposedTradeTtlMinutes);

                // Persist first, notify second: Redis has no persistence configured, so the row is the
                // only copy that survives a restart, and a SignalR failure must never lose the offer.
                var created = await tradeService.AddProposedTrade(
                    request.leagueId.Value, proposal, DateTime.UtcNow.Add(ttl));

                await tradeManager.ProposeSeasonTrade(request.leagueId.Value, proposal);

                // Bounded deliberately: an unresponsive browser must not hold the request open. On
                // timeout the probe reports false and the recipient picks the offer up on their next read.
                using var probeTimeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                probeTimeout.CancelAfter(TimeSpan.FromSeconds(2));

                var deliveredLive = await presenceProbe.IsTeamOnTradeScreen(proposal.ToTeam, probeTimeout.Token);

                if (deliveredLive)
                    await tradeHub.Clients.Group($"team:trade:{proposal.ToTeam}").ReceiveTradeRequest(proposal);

                return Results.Ok(created.ToTradeDto(deliveredLive));
            });

            trade.MapPost("/accept-trade", ([FromBody] AcceptTradeRequest request) =>
            {
                return Results.StatusCode(StatusCodes.Status501NotImplemented);
            });

            // leagueId scopes the Redis key (RedisKeys.GetProposedTradeKey); teamId is optional
            // so a manager can ask for only the proposals aimed at them.
            trade.MapGet("/get-proposed-trades", (long leagueId, long? teamId) =>
            {
                return Results.StatusCode(StatusCodes.Status501NotImplemented);
            });

            return trade;
        }
    }
}
