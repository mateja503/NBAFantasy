using ApplicationDefaults.Exceptions;
using ExternalClients.Response;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using NBA.Api.DTOs;
using NBA.Api.HangFire;
using NBA.Api.Requests.Draft;
using NBA.Api.SignalR.Clients;
using NBA.Api.SignalR.Hubs;
using NBA.Data.Context;
using NBA.Data.Entities;
using NBA.Data.Redis.Entities;
using NBA.Service.Draft;
using NBA.Service.League.Draft;
using StackExchange.Redis;
using StreamJsonRpc;
using System.Text.Json;
namespace NBA.Api.Endpoints
{
    public static class DraftEndpoints
    {
        public static IEndpointRouteBuilder MapDraftEndpoints(this IEndpointRouteBuilder builder)
        {
            var draft = builder.MapGroup("draft").WithTags("draft");

            draft.MapPost("start-draft", async ([FromBody] DraftRequest request,
                IBackgroundJobClient backgroundJobs, IHubContext<DraftHub,
                IDraftHubClient> draftHub, DraftJobs draftJob, NbaFantasyRedis redis) =>
            {
                if (!request.LeagueId.HasValue)
                    throw new NBAException($"Missing value for leagueId", ErrorCodes.MissingValue);

                var jobId = await redis.GetStartDraftTimerJobId(request.LeagueId.Value);

                if (string.IsNullOrEmpty(jobId))
                {
                    backgroundJobs.Enqueue<DraftJobs>(job => job.StartDraft(request.LeagueId.Value));
                }
                else
                {
                    throw new NBAException($"Draft has already started with leagueId = {request.LeagueId.Value}", ErrorCodes.DraftAlreadyStarted);
                }
            });

            draft.MapPost("end-draft", async ([FromBody] DraftRequest request, IBackgroundJobClient backgroundJobClient, 
                IHubContext<DraftHub,IDraftHubClient> draftHub, IOptions<JsonOptions> jsonOptions, NbaFantasyRedis redis) => 
            {
                if (!request.LeagueId.HasValue)
                    throw new NBAException($"Missing value for leagueId", ErrorCodes.MissingValue);


                var jobId = await redis.GetDeleteDraftTimerJobId(request.LeagueId.Value);
                if (!string.IsNullOrEmpty(jobId)) 
                {
                    backgroundJobClient.Delete(jobId);
                }

                var state = await redis.GetCurrentDraftState(request.LeagueId.Value);
                if (state is not null) 
                {
                    var draftState = JsonSerializer.Deserialize<DraftState>(state.ToString(), jsonOptions.Value.JsonSerializerOptions);
                    draftState!.PickEndTime = DateTime.UtcNow;
                    draftState!.IsPaused = false;
                    draftState!.IsDraftStarted = false;
                    await draftHub.Clients.Group(request.LeagueId.ToString()!).UpdateDraftState(draftState!);
                }

                return Results.Ok();
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
