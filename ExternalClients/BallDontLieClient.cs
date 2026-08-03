
using ApplicationDefaults.Exceptions;
using BoxScoreBuilder;
using BoxScoreBuilder.Model;
using ExternalClients.Poco;
using ExternalClients.Response;
using Polly;
using Polly.Registry;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;

namespace ExternalClients
{
    public class BallDontLieClient(HttpClient httpClient, ResiliencePipelineProvider<string> pipelineProvider)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly ResiliencePipeline<HttpResponseMessage> _pipeline = pipelineProvider.GetPipeline<HttpResponseMessage>("external-api-shield");

        // balldontlie dates are US dates, so "today" has to be resolved in the NBA's timezone,
        // not in UTC (a 20:00 ET tip-off is already the next day in UTC).
        private static readonly TimeZoneInfo NbaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");

        public Task<GetAllPlayersResponse> GetAllPlayers(MetaData metaData, CancellationToken cancellationToken)
        {
            var url = metaData.Next_cursor is null
                ? $"/v1/players?per_page={metaData.Per_page}"
                : $"/v1/players?per_page={metaData.Per_page}&cursor={metaData.Next_cursor}";

            return GetAsync<GetAllPlayersResponse>(url, cancellationToken);
        }

        public Task<GetTodaysGamesResponse> GetTodaysGames(CancellationToken cancellationToken)
        {
            var today = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, NbaTimeZone)
                .ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            return GetAsync<GetTodaysGamesResponse>($"/v1/games?dates[]={today}", cancellationToken);
        }

        /// <summary>
        /// Randomized stand-in stats until the balldontlie player-stats subscription is paid for;
        /// becomes a real call to /v1/stats through <see cref="GetAsync{T}"/> at that point.
        /// </summary>
        public Task<List<PlayerStatsResponse>> GetPlayerStats(List<long> playerIds, long gameId, CancellationToken cancellationToken)
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
                    fga = playerStats.fga,
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

            return Task.FromResult(result);
        }

        /// <summary>
        /// Runs a GET through the external-api-shield pipeline and deserializes the body,
        /// translating every failure mode into an <see cref="NBAException"/>.
        /// </summary>
        private async Task<T> GetAsync<T>(string relativeUrl, CancellationToken cancellationToken)
        {
            using var res = await _pipeline.ExecuteAsync(
                async token => await _httpClient.GetAsync(relativeUrl, token),
                cancellationToken);

            if (!res.IsSuccessStatusCode)
            {
                throw new NBAException(
                    $"GET {relativeUrl} failed, {(int)res.StatusCode} {res.ReasonPhrase}",
                    ErrorCodes.ExternalApiCallFailed);
            }

            try
            {
                return await res.Content.ReadFromJsonAsync<T>(cancellationToken)
                    ?? throw new NBAException(
                        $"GET {relativeUrl} returned an empty body",
                        ErrorCodes.ExternalApiResponseInvalid);
            }
            catch (JsonException exception)
            {
                throw new NBAException(
                    $"GET {relativeUrl} returned a body that could not be read as {typeof(T).Name}",
                    ErrorCodes.ExternalApiResponseInvalid,
                    exception);
            }
        }
    }
}
