using Hangfire;
using Microsoft.AspNetCore.Mvc;
using NBA.Api.HangFire;
using NBA.Api.Requests.Draft;
using NBA.Api.SignalR.Hubs;
using NBA.Data.Context;
namespace NBA.Api.Endpoints
{
    public static class DraftEndpoints
    {
        public static IEndpointRouteBuilder MapDraftEndpoints(this IEndpointRouteBuilder builder)
        {
            var draft = builder.MapGroup("draft").WithTags("draft");

            draft.MapPost("start-draft", ([FromBody] DraftRequest request, IBackgroundJobClient backgroundJobs) =>
            {
                backgroundJobs.Enqueue<DraftResetTimerJob>(job => job.ExecuteReset());
            });

            return draft;
        }
    }
}
