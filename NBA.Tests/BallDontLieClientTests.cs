using ApplicationDefaults.Exceptions;
using ExternalClients;
using ExternalClients.Poco;
using Microsoft.Extensions.Http.Resilience;
using NBA.Tests.Fakes;
using Polly;
using Polly.Registry;
using System.Globalization;
using System.Net;
using Xunit;

namespace NBA.Tests
{
    // True unit tests for the balldontlie client: the HttpClient sits on a StubHttpMessageHandler and
    // the external-api-shield pipeline is built in-memory, so nothing here needs the network, Docker
    // or the Aspire stack. What is worth locking down is the client's own logic — the URLs it builds,
    // the deserialization, and the translation of every failure mode into an NBAException.
    public class BallDontLieClientTests
    {
        private const string PipelineName = "external-api-shield";
        private static readonly Uri BaseAddress = new("https://api.balldontlie.io");

        private static BallDontLieClient CreateClient(
            StubHttpMessageHandler handler,
            Action<ResiliencePipelineBuilder<HttpResponseMessage>>? configurePipeline = null)
        {
            var httpClient = new HttpClient(handler) { BaseAddress = BaseAddress };

            var registry = new ResiliencePipelineRegistry<string>();
            registry.TryAddBuilder<HttpResponseMessage>(PipelineName, (builder, _) => configurePipeline?.Invoke(builder));

            return new BallDontLieClient(httpClient, registry);
        }

        private static MetaData Meta(long perPage = 25, long? cursor = null) =>
            new() { Per_page = perPage, Next_cursor = cursor };

        #region GetAllPlayers

        [Fact]
        public async Task GetAllPlayers_omits_cursor_on_the_first_page()
        {
            var handler = StubHttpMessageHandler.RespondsWith(HttpStatusCode.OK, """{"data":[],"meta":{}}""");
            var client = CreateClient(handler);

            await client.GetAllPlayers(Meta(perPage: 100), CancellationToken.None);

            Assert.Equal("/v1/players", handler.LastRequestUri.AbsolutePath);
            Assert.Equal("?per_page=100", handler.LastRequestQuery);
        }

        [Fact]
        public async Task GetAllPlayers_appends_the_cursor_for_subsequent_pages()
        {
            var handler = StubHttpMessageHandler.RespondsWith(HttpStatusCode.OK, """{"data":[],"meta":{}}""");
            var client = CreateClient(handler);

            await client.GetAllPlayers(Meta(perPage: 25, cursor: 90), CancellationToken.None);

            Assert.Equal("?per_page=25&cursor=90", handler.LastRequestQuery);
        }

        [Fact]
        public async Task GetAllPlayers_deserializes_players_team_and_paging_cursor()
        {
            const string json = """
            {
              "data": [
                {
                  "id": 237,
                  "first_name": "LeBron",
                  "last_name": "James",
                  "position": "F",
                  "height": "6-9",
                  "weight": "250",
                  "team": {
                    "id": 14,
                    "conference": "West",
                    "division": "Pacific",
                    "city": "Los Angeles",
                    "name": "Lakers",
                    "full_name": "Los Angeles Lakers",
                    "abbreviation": "LAL"
                  }
                }
              ],
              "meta": { "next_cursor": 91, "per_page": 25 }
            }
            """;

            var client = CreateClient(StubHttpMessageHandler.RespondsWith(HttpStatusCode.OK, json));

            var response = await client.GetAllPlayers(Meta(), CancellationToken.None);

            var player = Assert.Single(response.data);
            Assert.Equal(237, player.id);
            Assert.Equal("LeBron", player.first_name);
            Assert.Equal("James", player.last_name);
            Assert.Equal("F", player.position);
            Assert.Equal(14, player.team!.id);
            Assert.Equal("Los Angeles Lakers", player.team.full_name);
            Assert.Equal(91, response.meta.Next_cursor);
            Assert.Equal(25, response.meta.Per_page);
        }

        #endregion

        #region GetTodaysGames

        [Fact]
        public async Task GetTodaysGames_asks_for_today_in_the_nba_timezone_not_utc()
        {
            var handler = StubHttpMessageHandler.RespondsWith(HttpStatusCode.OK, """{"data":[],"meta":{}}""");
            var client = CreateClient(handler);

            await client.GetTodaysGames(CancellationToken.None);

            // A 20:00 ET tip-off is already "tomorrow" in UTC, so the date has to be resolved in ET.
            var easternToday = TimeZoneInfo.ConvertTimeFromUtc(
                    DateTime.UtcNow,
                    TimeZoneInfo.FindSystemTimeZoneById("America/New_York"))
                .ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            Assert.Equal("/v1/games", handler.LastRequestUri.AbsolutePath);
            Assert.Equal($"?dates[]={easternToday}", handler.LastRequestQuery);
        }

        [Fact]
        public async Task GetTodaysGames_deserializes_the_matchup()
        {
            const string json = """
            {
              "data": [
                {
                  "id": 1038184,
                  "date": "2026-08-03",
                  "status": "Final",
                  "datetime": "2026-08-03T23:30:00Z",
                  "time": "23:30",
                  "postseason": false,
                  "postponed": false,
                  "home_team": { "id": 14, "full_name": "Los Angeles Lakers" },
                  "visitor_team": { "id": 2, "full_name": "Boston Celtics" }
                }
              ],
              "meta": {}
            }
            """;

            var client = CreateClient(StubHttpMessageHandler.RespondsWith(HttpStatusCode.OK, json));

            var response = await client.GetTodaysGames(CancellationToken.None);

            var game = Assert.Single(response.data);
            Assert.Equal(1038184, game.id);
            Assert.Equal("Final", game.status);
            Assert.False(game.postponed);
            Assert.Equal("Los Angeles Lakers", game.home_team.full_name);
            Assert.Equal(2, game.visitor_team.id);
        }

        #endregion

        #region GetGames

        [Fact]
        public async Task GetGames_asks_for_the_whole_window_in_one_request()
        {
            var handler = StubHttpMessageHandler.RespondsWith(HttpStatusCode.OK, """{"data":[],"meta":{}}""");
            var client = CreateClient(handler);

            await client.GetGames(new DateOnly(2026, 8, 3), new DateOnly(2026, 8, 9), Meta(perPage: 100), CancellationToken.None);

            // One start_date/end_date call rather than seven dates[] calls: external-api-shield
            // permits a single concurrent request and has no queue.
            Assert.Equal("/v1/games", handler.LastRequestUri.AbsolutePath);
            Assert.Equal("?start_date=2026-08-03&end_date=2026-08-09&per_page=100", handler.LastRequestQuery);
        }

        [Fact]
        public async Task GetGames_appends_the_cursor_for_subsequent_pages()
        {
            var handler = StubHttpMessageHandler.RespondsWith(HttpStatusCode.OK, """{"data":[],"meta":{}}""");
            var client = CreateClient(handler);

            await client.GetGames(new DateOnly(2026, 8, 3), new DateOnly(2026, 8, 9), Meta(perPage: 25, cursor: 40), CancellationToken.None);

            Assert.Equal("?start_date=2026-08-03&end_date=2026-08-09&per_page=25&cursor=40", handler.LastRequestQuery);
        }

        [Fact]
        public async Task GetGames_deserializes_the_team_details_and_scores()
        {
            const string json = """
            {
              "data": [
                {
                  "id": 1038184,
                  "date": "2026-08-05",
                  "status": "7:30 pm ET",
                  "datetime": "2026-08-05T23:30:00Z",
                  "time": "23:30",
                  "postseason": true,
                  "postponed": false,
                  "home_team_score": 112,
                  "visitor_team_score": 109,
                  "home_team": { "id": 14, "full_name": "Los Angeles Lakers", "abbreviation": "LAL", "city": "Los Angeles" },
                  "visitor_team": { "id": 2, "full_name": "Boston Celtics", "abbreviation": "BOS", "city": "Boston" }
                }
              ],
              "meta": { "next_cursor": 5, "per_page": 25 }
            }
            """;

            var client = CreateClient(StubHttpMessageHandler.RespondsWith(HttpStatusCode.OK, json));

            var response = await client.GetGames(new DateOnly(2026, 8, 5), new DateOnly(2026, 8, 9), Meta(), CancellationToken.None);

            var game = Assert.Single(response.data);
            Assert.True(game.postseason);
            Assert.Equal(112, game.home_team_score);
            Assert.Equal(109, game.visitor_team_score);
            Assert.Equal("LAL", game.home_team.abbreviation);
            Assert.Equal("Boston", game.visitor_team.city);
            Assert.Equal(5, response.meta.Next_cursor);
        }

        [Fact]
        public async Task GetGames_still_deserializes_a_payload_without_the_optional_team_details()
        {
            // abbreviation/city/scores are deliberately not `required` so an older or trimmed body
            // does not fail deserialization outright.
            const string json = """
            {
              "data": [
                {
                  "id": 1,
                  "date": "2026-08-05",
                  "status": "7:30 pm ET",
                  "time": "23:30",
                  "postseason": false,
                  "postponed": false,
                  "home_team": { "id": 14, "full_name": "Los Angeles Lakers" },
                  "visitor_team": { "id": 2, "full_name": "Boston Celtics" }
                }
              ],
              "meta": {}
            }
            """;

            var client = CreateClient(StubHttpMessageHandler.RespondsWith(HttpStatusCode.OK, json));

            var response = await client.GetGames(new DateOnly(2026, 8, 5), new DateOnly(2026, 8, 9), Meta(), CancellationToken.None);

            var game = Assert.Single(response.data);
            Assert.Equal(string.Empty, game.home_team.abbreviation);
            Assert.Equal(string.Empty, game.home_team.city);
            Assert.Equal(0, game.home_team_score);
            Assert.Null(response.meta.Next_cursor);
        }

        #endregion

        #region Failure translation

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task A_non_success_status_becomes_an_ExternalApiCallFailed_NBAException(HttpStatusCode status)
        {
            var client = CreateClient(StubHttpMessageHandler.AlwaysFails(status));

            var exception = await Assert.ThrowsAsync<NBAException>(
                () => client.GetAllPlayers(Meta(), CancellationToken.None));

            Assert.Equal(ErrorCodes.ExternalApiCallFailed, exception.ErrorCode);
            Assert.Contains(((int)status).ToString(CultureInfo.InvariantCulture), exception.Message);
            Assert.Contains("/v1/players", exception.Message);
        }

        [Fact]
        public async Task A_body_that_is_not_json_becomes_an_ExternalApiResponseInvalid_NBAException()
        {
            var client = CreateClient(StubHttpMessageHandler.RespondsWith(HttpStatusCode.OK, "<html>gateway</html>"));

            var exception = await Assert.ThrowsAsync<NBAException>(
                () => client.GetAllPlayers(Meta(), CancellationToken.None));

            Assert.Equal(ErrorCodes.ExternalApiResponseInvalid, exception.ErrorCode);
            Assert.IsType<System.Text.Json.JsonException>(exception.InnerException);
        }

        [Fact]
        public async Task A_null_body_becomes_an_ExternalApiResponseInvalid_NBAException()
        {
            var client = CreateClient(StubHttpMessageHandler.RespondsWith(HttpStatusCode.OK, "null"));

            var exception = await Assert.ThrowsAsync<NBAException>(
                () => client.GetAllPlayers(Meta(), CancellationToken.None));

            Assert.Equal(ErrorCodes.ExternalApiResponseInvalid, exception.ErrorCode);
            Assert.Contains("empty body", exception.Message);
        }

        [Fact]
        public async Task A_response_missing_required_fields_becomes_an_ExternalApiResponseInvalid_NBAException()
        {
            // GameInfoResponse marks home_team/visitor_team as required, so a truncated payload must
            // fail loudly here rather than surfacing as a NullReferenceException in GameService.
            const string json = """{"data":[{"id":1,"date":"2026-08-03"}],"meta":{}}""";

            var client = CreateClient(StubHttpMessageHandler.RespondsWith(HttpStatusCode.OK, json));

            var exception = await Assert.ThrowsAsync<NBAException>(
                () => client.GetTodaysGames(CancellationToken.None));

            Assert.Equal(ErrorCodes.ExternalApiResponseInvalid, exception.ErrorCode);
        }

        #endregion

        #region Resilience & cancellation

        [Fact]
        public async Task Requests_run_through_the_external_api_shield_pipeline_and_are_retried()
        {
            // Rule 6: outbound calls must go through the named pipeline. Proven by registering a retry
            // strategy under that name (zero delay to keep the test fast) and counting the attempts.
            var handler = StubHttpMessageHandler.AlwaysFails(HttpStatusCode.InternalServerError);
            var client = CreateClient(handler, builder => builder.AddRetry(new HttpRetryStrategyOptions
            {
                MaxRetryAttempts = 2,
                BackoffType = DelayBackoffType.Constant,
                UseJitter = false,
                Delay = TimeSpan.Zero
            }));

            await Assert.ThrowsAsync<NBAException>(() => client.GetAllPlayers(Meta(), CancellationToken.None));

            Assert.Equal(3, handler.CallCount); // 1 initial attempt + 2 retries
        }

        [Fact]
        public async Task A_first_attempt_that_succeeds_after_a_retry_is_returned_normally()
        {
            var handler = new StubHttpMessageHandler((_, attempt) => attempt == 0
                ? StubHttpMessageHandler.JsonResponse(HttpStatusCode.ServiceUnavailable, "{}")
                : StubHttpMessageHandler.JsonResponse(HttpStatusCode.OK, """{"data":[],"meta":{"per_page":25}}"""));

            var client = CreateClient(handler, builder => builder.AddRetry(new HttpRetryStrategyOptions
            {
                MaxRetryAttempts = 1,
                BackoffType = DelayBackoffType.Constant,
                UseJitter = false,
                Delay = TimeSpan.Zero
            }));

            var response = await client.GetAllPlayers(Meta(), CancellationToken.None);

            Assert.Empty(response.data);
            Assert.Equal(2, handler.CallCount);
        }

        [Fact]
        public async Task A_cancelled_token_stops_the_call_before_it_reaches_the_wire()
        {
            var handler = StubHttpMessageHandler.RespondsWith(HttpStatusCode.OK, """{"data":[],"meta":{}}""");
            var client = CreateClient(handler);

            using var cts = new CancellationTokenSource();
            await cts.CancelAsync();

            await Assert.ThrowsAnyAsync<OperationCanceledException>(
                () => client.GetTodaysGames(cts.Token));

            Assert.Equal(0, handler.CallCount);
        }

        #endregion

        #region GetPlayerStats (randomized stand-in)

        [Fact]
        public async Task GetPlayerStats_fabricates_one_row_per_player_without_calling_the_api()
        {
            // Stand-in until the player-stats subscription is paid for: the values are random, but the
            // shape (one row per requested id, ids preserved) is what callers depend on.
            var handler = StubHttpMessageHandler.AlwaysFails(HttpStatusCode.InternalServerError);
            var client = CreateClient(handler);

            var playerIds = new List<long> { 237, 115, 3 };

            var stats = await client.GetPlayerStats(playerIds, gameId: 1038184, CancellationToken.None);

            Assert.Equal(0, handler.CallCount);
            Assert.Equal(playerIds, stats.Select(s => s.player_id));
        }

        #endregion
    }
}
