
using ApplicationDefaults;
using ExternalClients.Options;
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
        private readonly HttpContext httpContext = httpContextAccessor.HttpContext;
        public async Task<List<PlayerInfoResponse>> GetAllActivePlayers()
        {

            //TODO call this method until meta.next_cursor is null
            var res = await _httpClient.GetAsync("/v1/players");

            if (!res.IsSuccessStatusCode) 
            {
                _logger.LogWarning("{Log}", new Log($"GET {httpContext.Request.Path} failed ").ToLogString());
            }
            var content = await res.Content.ReadFromJsonAsync<GetAllPlayersResponse>();
            return content.data;
        }

    }
}
