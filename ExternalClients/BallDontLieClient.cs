
using ApplicationDefaults.Exceptions;
using ApplicationDefaults.LogDefaults;
using BoxScoreBuilder;
using BoxScoreBuilder.Model;
using ExternalClients.Poco;
using ExternalClients.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Registry;
using System.Net.Http.Json;
using System.Text.Json;

namespace ExternalClients
{
    public class BallDontLieClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, ILogger<BallDontLieClient> logger, ResiliencePipelineProvider<string> pipelineProvider)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly ILogger<BallDontLieClient> _logger = logger;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly ResiliencePipeline<HttpResponseMessage> _pipeline = pipelineProvider.GetPipeline<HttpResponseMessage>("external-api-shield");

        public async Task<GetAllPlayersResponse> GetAllPlayers(MetaData metaData, CancellationToken cancellationToken)//maybe add parameter for pagination and next Next_cursor
        {
            var res = await _pipeline.ExecuteAsync(async token =>
            {
                return await _httpClient.GetAsync($"/v1/players?per_page={metaData.Per_page}&cursor={metaData.Next_cursor}", token);

            }, cancellationToken);

            string requestPath = _httpContextAccessor.HttpContext?.Request.Path.Value ?? $"/v1/players?per_page={metaData.Per_page}&cursor={metaData.Next_cursor}";

            if (!res.IsSuccessStatusCode)
            {
                _logger.LogWarning("{Log}", new Log($"GET {requestPath} failed, {res.ReasonPhrase}").ToJson());
                throw new NBAException($"GET {requestPath} failed, {res.ReasonPhrase}", res.StatusCode.ToString());
            }
            var response = await res.Content.ReadFromJsonAsync<GetAllPlayersResponse>(cancellationToken);

            if (response is null)
            {
                _logger.LogWarning("{Log}", new Log($"GET {requestPath} failed to read the api response").ToJson());
                throw new NBAException($"GET {requestPath} failed ", res.StatusCode.ToString());
            }
            return response;
        }

        public async Task<GetTodaysGamesResponse> GetTodaysGames(CancellationToken cancellationToken)
        {
            var today = DateTime.UtcNow.Date.ToString("yyy-MM-dd");
            var res = await _pipeline.ExecuteAsync(async token =>
            {
                return await _httpClient.GetAsync($"/v1/games?dates[]={today}", token);
            }, cancellationToken);

            string requestPath = _httpContextAccessor.HttpContext?.Request.Path.Value ?? $"/v1/games?dates[]={today}";
            if (!res.IsSuccessStatusCode)
            {
                _logger.LogWarning("{Log}", new Log($"GET {requestPath} failed, {res.ReasonPhrase}").ToJson());
                throw new NBAException($"GET {requestPath} failed, {res.ReasonPhrase}", res.StatusCode.ToString());
            }
            var response = await res.Content.ReadFromJsonAsync<GetTodaysGamesResponse>(cancellationToken);

            if (response is null)
            {
                _logger.LogWarning("{Log}", new Log($"GET {requestPath} failed to read the api response").ToJson());
                throw new NBAException($"GET {requestPath} failed ", res.StatusCode.ToString());
            }
            return response;
        }


        public async Task<List<PlayerStatsResponse>> GetPlayerStats(List<long> playerIds,long gameId, CancellationToken cancellationToken)
        {
            List<PlayerStatsResponse> result = new List<PlayerStatsResponse>();
            foreach (var id in playerIds) 
            {
                PlayerStats playerStats = new BoxScoreStatsBuilder().AddPoints()
                   .AddAssists()
                   .AddRebounds()
                   .AddBlocks()
                   .AddSteals()
                   .AddThreePointersMade()
                   .AddThreePointersAttemped()
                   .AddTurnovers()
                   .AddFieldGoalsMade()
                   .AddFieldGoalsAttempted()
                   .AddFreeThrowsMade()
                   .AddFreeThrowsAttempted()
                   .Build();

                result.Add(new PlayerStatsResponse
                {
                    player_id = id,
                    fgm = playerStats.fgm,
                    fga= playerStats.fga,
                    fg3m = playerStats.fg3m,
                    fg3a = playerStats.fg3a,
                    ftm = playerStats.ftm,
                    fta = playerStats.fta,
                    reb = playerStats.reb,
                    ast = playerStats.ast,
                    stl = playerStats.stl,
                    blk = playerStats.blk,
                    turnover = playerStats.turnover,
                    pts = playerStats.pts
                });
            }


            return result;
        }

    }
}
