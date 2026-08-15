using System.Text.Json;
using ApplicationDefaults.Exceptions;
using Microsoft.AspNetCore.SignalR;

namespace NBA.Api.SignalR
{
    // The SignalR counterpart of GlobalExceptionHandler.
    //
    // SignalR only ever sends the client a HubException message string; anything else is replaced with
    // "An unexpected error occurred" unless EnableDetailedErrors is on. That would flatten every
    // NBAException and lose its ErrorCode — which matters here because a trade can be rejected for
    // several distinct reasons (roster limits, unowned players, cross-league) and the UI needs to tell
    // them apart. So the code is serialised into the message as the same
    // { ErrorMessage, ErrorCode } shape the HTTP handler produces, and clients parse it from
    // HubException.Message.
    //
    // Non-NBAExceptions are deliberately left alone: they keep SignalR's generic message so internal
    // details never reach a client.
    public class NBAExceptionHubFilter : IHubFilter
    {
        public async ValueTask<object?> InvokeMethodAsync(
            HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object?>> next)
        {
            try
            {
                return await next(invocationContext);
            }
            catch (NBAException ex)
            {
                throw new HubException(JsonSerializer.Serialize(new
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = ex.ErrorCode,
                }));
            }
        }
    }
}
