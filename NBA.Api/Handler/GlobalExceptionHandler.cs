using ApplicationDefaults.LogDefaults;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApplicationDefaults.Exceptions
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError("{Log}", new Log($"An exception occurred: {exception.Message}").ToJson());

            if (exception is NBAException nbaException)
            {
                ErrorResponse errorResponse = new ErrorResponse
                {
                    ErrorMessage = nbaException.Message,
                    ErrorCode = nbaException.ErrorCode
                };
                httpContext.Response.StatusCode = errorResponse.ErrorCode;

                await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);
            }
            else 
            {
                var problemDetails = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = exception.GetType().Name,
                    Title = "An exception occurred",
                    Detail = exception.Message
                };

                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            }

            return true;
        }
    }
}
