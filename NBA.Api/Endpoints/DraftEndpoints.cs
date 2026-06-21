using ApplicationDefaults.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using NBA.Api.Draft;
using NBA.Api.Requests.Draft;
using NBA.Api.SignalR.Clients;
using NBA.Api.SignalR.Hubs;
using NBA.Data.Context;
using NBA.Data.Redis.Entities;
using NBA.Data.Redis.Enumerations;
using NBA.Service.League.Draft;
using StackExchange.Redis;
using System.Text.Json;
namespace NBA.Api.Endpoints
{
    public static class DraftEndpoints
    {
        public static IEndpointRouteBuilder MapDraftEndpoints(this IEndpointRouteBuilder builder)
        {
            var draft = builder.MapGroup("draft").WithTags("draft").RequireAuthorization();

            draft.MapPost("start-draft", async ([FromBody] DraftRequest request,
                NbaFantasyRedis redis, DraftTimerProcessor timerProcessor) =>
            {
                if (!request.LeagueId.HasValue)
                    throw new NBAException($"Missing value for leagueId", ErrorCodes.MissingValue);

                // A scheduled timer means the draft is already running.
                if (await redis.Draft.IsDraftTimerScheduled(request.LeagueId.Value))
                    throw new NBAException($"Draft has already started with leagueId = {request.LeagueId.Value}", ErrorCodes.DraftAlreadyStarted);

                await timerProcessor.StartDraftAsync(request.LeagueId.Value);
            });

            draft.MapPost("end-draft", async ([FromBody] DraftRequest request, DraftManager draftManager, DraftService draftService, IHubContext<DraftHub,IDraftHubClient> draftHub) => 
            {
                if (!request.LeagueId.HasValue)
                    throw new NBAException($"Missing value for leagueId", ErrorCodes.MissingValue);

                await draftManager.EndDraft(request.LeagueId.Value);

                var state = new DraftState { DraftStatus = (int)DraftStatus.DraftEnded};

                await draftHub.Clients.Group(request.LeagueId.Value.ToString()).UpdateDraftState(state);

                return Results.Ok(state);
            });


            draft.MapGet("get-draft-teams", async (long leagueId,DraftService draftService, IConnectionMultiplexer redis, IOptions<JsonOptions> jsonOptions) => 
            {
                //var redisDb = redis.GetDatabase();
                //var redisKey = RedisKeys.GetDraftTeamsKey(leagueId);

                //var val = await redisDb.StringGetAsync(redisKey);
                //if (val.IsNull) 
                //{
                //    var dict = await draftService.DraftOrder(leagueId);

                //    var res = dict.Select(k => new DraftOrderDto
                //    {
                //        Round = k.Key,
                //        Teams = k.Value.Select(u => new TeamDto
                //        {
                //            Teamid = u.Teamid,
                //            Name = u.Name,
                //        }).ToList()
                //    }).ToList();

                //    await redisDb.StringSetAsync(redisKey, JsonSerializer.Serialize(res, jsonOptions.Value.JsonSerializerOptions));

                //    return Results.Ok(res);
                //}

                //var cachedData = JsonSerializer.Deserialize<List<DraftOrderDto>>(val.ToString(), jsonOptions.Value.JsonSerializerOptions);
                //return Results.Ok(cachedData);
            });

            return draft;
        }
    }
}
