using ApplicationDefaults.Exceptions;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NBA.Api.HangFire;
using NBA.Api.Requests.Draft;
using NBA.Api.SignalR.Clients;
using NBA.Api.SignalR.Hubs;
using NBA.Data.Context;
using NBA.Service.Redis;
using StackExchange.Redis;
namespace NBA.Api.Endpoints
{
    public static class DraftEndpoints
    {
        public static IEndpointRouteBuilder MapDraftEndpoints(this IEndpointRouteBuilder builder)
        {
            var draft = builder.MapGroup("draft").WithTags("draft");

            draft.MapPost("start-draft", async (NbaFantasyContext context, [FromBody] DraftRequest request,
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

            return draft;
        }
    }
}
