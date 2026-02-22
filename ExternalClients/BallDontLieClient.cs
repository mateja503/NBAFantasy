
using ApplicationDefaults.Exceptions;
using ApplicationDefaults.LogDefaults;
using ExternalClients.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace ExternalClients
{
    public class BallDontLieClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor,
        ILogger<BallDontLieClient> logger)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly ILogger<BallDontLieClient> _logger = logger;
        private readonly HttpContext httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentException();
        public async Task<List<PlayerInfoResponse>> GetAllPlayers()
        {
            var res = await _httpClient.GetAsync("/v1/players");

            if (!res.IsSuccessStatusCode)
            {
                _logger.LogWarning("{Log}", new Log($"GET {httpContext.Request.Path} failed").ToLogString());
                throw new NBAException($"GET {httpContext.Request.Path} failed ", (int)res.StatusCode);
            }
            var response = await res.Content.ReadFromJsonAsync<GetAllPlayersResponse>();

            if (response is null)
            {
                _logger.LogWarning("{Log}", new Log($"GET {httpContext.Request.Path} failed to read the api response").ToLogString());
                throw new NBAException($"GET {httpContext.Request.Path} failed ", (int)res.StatusCode);
            }
            return response!.data;
        }

    }
}
