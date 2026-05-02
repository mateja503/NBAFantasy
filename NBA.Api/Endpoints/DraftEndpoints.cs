using ApplicationDefaults.Exceptions;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using NBA.Api.HangFire;
using NBA.Api.Requests.Draft;
using NBA.Api.SignalR.Clients;
using NBA.Api.SignalR.Hubs;
using NBA.Data.Context;
using NBA.Data.Entities;
using NBA.Service.Draft;
using NBA.Service.Redis;
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
                IBackgroundJobClient backgroundJobs, IHubContext<DraftHub, IDraftHubClient> draftHub, DraftJobs draftJob,
                IConnectionMultiplexer redis) =>
            {
                if (!request.LeagueId.HasValue)
                    throw new NBAException($"Missing value for leagueId", ErrorCodes.MissingValue);

                var redisKey = RedisKeys.GetStartDraftTimerJobIdKey(request.LeagueId.Value);
                var redisDb = redis.GetDatabase();

                var jobId = await redisDb.StringGetAsync(redisKey);

                if (jobId.IsNull)
                {
                    jobId = backgroundJobs.Enqueue<DraftJobs>(job => job.StartDraft(request.LeagueId.Value));
                }
                else
                {
                    throw new NBAException($"Draft has already started with leagueId = {request.LeagueId.Value}", ErrorCodes.DraftAlreadyStarted);
                }
            });

            draft.MapPost("end-draft", async ([FromBody] DraftRequest request, IConnectionMultiplexer redis, IBackgroundJobClient backgroundJobClient, 
                IHubContext<DraftHub,IDraftHubClient> draftHub, IOptions<JsonOptions> jsonOptions) => 
            {
                if (!request.LeagueId.HasValue)
                    throw new NBAException($"Missing value for leagueId", ErrorCodes.MissingValue);

                var redisDb = redis.GetDatabase();

                var jobIdDraftTimer = RedisKeys.GetStartDraftTimerJobIdKey(request.LeagueId.Value);
                var redisKeyDraftState = RedisKeys.GetDraftStateKey(request.LeagueId.Value);

                var jobId = await redisDb.StringGetDeleteAsync(jobIdDraftTimer);
                if (jobId.HasValue) 
                {
                    backgroundJobClient.Delete(jobId.ToString());
                        
                }

                var state = await redisDb.StringGetAsync(redisKeyDraftState);
                if (!state.IsNull) 
                {
                    var draftState = JsonSerializer.Deserialize<DraftState>(state.ToString(), jsonOptions.Value.JsonSerializerOptions);
                    draftState!.PickEndTime = DateTime.UtcNow;
                    draftState!.IsPaused = false;
                    await draftHub.Clients.Group(request.LeagueId.ToString()!).UpdateDraftState(draftState!);
                }

                return Results.Ok();
            });

            return draft;
        }
    }
}
