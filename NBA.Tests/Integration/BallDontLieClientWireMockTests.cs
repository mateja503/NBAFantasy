using ApplicationDefaults.Exceptions;
using ExternalClients.Poco;
using System.Globalization;
using System.Net;
using WireMock;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;

namespace NBA.Tests.Integration
{
    // Integration coverage for BallDontLieClient against a WireMock.Net server on loopback: the request
    // really leaves the process, so what is under test is the whole outbound path - DI wiring, default
    // headers, the URLs that end up on the wire, the external-api-shield pipeline, JSON deserialization
    // and the NBAException translation of every failure mode.
    public class BallDontLieClientWireMockTests : IClassFixture<BallDontLieWireMockFixture>
    {
        private readonly BallDontLieWireMockFixture _fixture;

        public BallDontLieClientWireMockTests(BallDontLieWireMockFixture fixture)
        {
            _fixture = fixture;
            // One server is shared by the class; every test starts from an empty stub + request log.
            _fixture.Reset();
        }

        #region Helpers

        private static MetaData Meta(long? cursor = null) =>
            new() { Per_page = BallDontLieWireMockFixture.PerPage, Next_cursor = cursor };

        private static IResponseBuilder Json(HttpStatusCode status, string body) =>
            Response.Create()
                .WithStatusCode(status)
                .WithHeader("Content-Type", "application/json")
                .WithBody(body);

        private int RequestCount => _fixture.Server.LogEntries.Count();

        private IRequestMessage LastRequest
        {
            get
            {
                var request = _fixture.Server.LogEntries.Last().RequestMessage;
                Assert.NotNull(request);
                return request;
            }
        }

        // Percent-decoded so assertions can be written the way the client builds the URL ("dates[]=...").
        private string LastRequestUrl => Uri.UnescapeDataString(LastRequest.Url);

        private string LastRequestPath => LastRequest.Path;

        private string? LastRequestHeader(string name)
        {
            var headers = LastRequest.Headers;
            return headers is not null && headers.TryGetValue(name, out var values)
                ? string.Join(",", values)
                : null;
        }

        private const string EmptyPlayersPage = """{"data":[],"meta":{"per_page":100}}""";

        private const string LeBronPage = """
        {
          "data": [
            {
              "id": 237,
              "first_name": "LeBron",
              "last_name": "James",
              "position": "F",
              "height": "6-9",
              "weight": "250",
              "jersey_number": "23",
              "college": "St. Vincent-St. Mary HS (OH)",
              "country": "USA",
              "draft_year": 2003,
              "draft_round": 1,
              "draft_number": 1,
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
          "meta": { "next_cursor": 91, "per_page": 100 }
        }
        """;

        #endregion

        #region Transport & wiring

        [Fact]
        public async Task Every_request_carries_the_configured_api_key_and_accepts_json()
        {
            // The key and the Accept header come from the AddHttpClient registration, not from the client
            // itself - only a real request over the wire proves they are actually attached.
            _fixture.Server
                .Given(Request.Create().WithPath("/v1/players").UsingGet())
                .RespondWith(Json(HttpStatusCode.OK, EmptyPlayersPage));

            await _fixture.Client.GetAllPlayers(Meta(), CancellationToken.None);

            Assert.Equal(BallDontLieWireMockFixture.ApiKey, LastRequestHeader("Authorization"));
            Assert.Equal("application/json", LastRequestHeader("Accept"));
        }

        #endregion

        #region GetAllPlayers

        [Fact]
        public async Task GetAllPlayers_requests_the_first_page_without_a_cursor()
        {
            _fixture.Server
                .Given(Request.Create().WithPath("/v1/players").UsingGet())
                .RespondWith(Json(HttpStatusCode.OK, EmptyPlayersPage));

            await _fixture.Client.GetAllPlayers(Meta(), CancellationToken.None);

            Assert.Equal("/v1/players", LastRequestPath);
            Assert.Contains("per_page=100", LastRequestUrl, StringComparison.Ordinal);
            Assert.DoesNotContain("cursor", LastRequestUrl, StringComparison.Ordinal);
        }

        [Fact]
        public async Task GetAllPlayers_deserializes_a_player_with_its_team_and_paging_cursor()
        {
            _fixture.Server
                .Given(Request.Create().WithPath("/v1/players").UsingGet())
                .RespondWith(Json(HttpStatusCode.OK, LeBronPage));

            var response = await _fixture.Client.GetAllPlayers(Meta(), CancellationToken.None);

            var player = Assert.Single(response.data);
            Assert.Equal(237, player.id);
            Assert.Equal("LeBron", player.first_name);
            Assert.Equal("James", player.last_name);
            Assert.Equal("F", player.position);
            Assert.Equal("23", player.jersey_number);
            Assert.Equal(2003, player.draft_year);
            Assert.Equal(14, player.team!.id);
            Assert.Equal("Los Angeles Lakers", player.team.full_name);
            Assert.Equal(91, response.meta.Next_cursor);
            Assert.Equal(100, response.meta.Per_page);
        }

        [Fact]
        public async Task GetAllPlayers_walks_to_the_next_page_with_the_cursor_the_api_returned()
        {
            const string secondPage = """
            {
              "data": [
                { "id": 115, "first_name": "Stephen", "last_name": "Curry", "position": "G" }
              ],
              "meta": { "per_page": 100 }
            }
            """;

            // The cursor stub is more specific, so it is given the higher priority (lower number wins);
            // a request without ?cursor= falls through to the first-page stub.
            _fixture.Server
                .Given(Request.Create().WithPath("/v1/players").WithParam("cursor", "91").UsingGet())
                .AtPriority(1)
                .RespondWith(Json(HttpStatusCode.OK, secondPage));

            _fixture.Server
                .Given(Request.Create().WithPath("/v1/players").UsingGet())
                .AtPriority(2)
                .RespondWith(Json(HttpStatusCode.OK, LeBronPage));

            var first = await _fixture.Client.GetAllPlayers(Meta(), CancellationToken.None);
            var second = await _fixture.Client.GetAllPlayers(Meta(cursor: first.meta.Next_cursor), CancellationToken.None);

            Assert.Equal(91, first.meta.Next_cursor);
            Assert.Equal("LeBron", Assert.Single(first.data).first_name);

            // Reaching the second page at all proves the cursor was on the query string.
            Assert.Contains("per_page=100&cursor=91", LastRequestUrl, StringComparison.Ordinal);
            Assert.Equal("Stephen", Assert.Single(second.data).first_name);
            Assert.Null(second.meta.Next_cursor);
        }

        #endregion

        #region GetTodaysGames

        [Fact]
        public async Task GetTodaysGames_asks_for_today_in_the_nba_timezone_not_utc()
        {
            _fixture.Server
                .Given(Request.Create().WithPath("/v1/games").UsingGet())
                .RespondWith(Json(HttpStatusCode.OK, """{"data":[],"meta":{}}"""));

            await _fixture.Client.GetTodaysGames(CancellationToken.None);

            // A 20:00 ET tip-off is already "tomorrow" in UTC, so the date has to be resolved in ET.
            var easternToday = TimeZoneInfo.ConvertTimeFromUtc(
                    DateTime.UtcNow,
                    TimeZoneInfo.FindSystemTimeZoneById("America/New_York"))
                .ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            Assert.Equal("/v1/games", LastRequestPath);
            Assert.Contains($"dates[]={easternToday}", LastRequestUrl, StringComparison.Ordinal);
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

            _fixture.Server
                .Given(Request.Create().WithPath("/v1/games").UsingGet())
                .RespondWith(Json(HttpStatusCode.OK, json));

            var response = await _fixture.Client.GetTodaysGames(CancellationToken.None);

            var game = Assert.Single(response.data);
            Assert.Equal(1038184, game.id);
            Assert.Equal("Final", game.status);
            Assert.False(game.postseason);
            Assert.False(game.postponed);
            Assert.Equal("Los Angeles Lakers", game.home_team.full_name);
            Assert.Equal(2, game.visitor_team.id);
        }

        #endregion

        #region Failure translation

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task A_non_transient_error_status_fails_fast_as_an_ExternalApiCallFailed_NBAException(HttpStatusCode status)
        {
            _fixture.Server
                .Given(Request.Create().WithPath("/v1/players").UsingGet())
                .RespondWith(Json(status, """{"message":"nope"}"""));

            var exception = await Assert.ThrowsAsync<NBAException>(
                () => _fixture.Client.GetAllPlayers(Meta(), CancellationToken.None));

            Assert.Equal(ErrorCodes.ExternalApiCallFailed, exception.ErrorCode);
            Assert.Contains(((int)status).ToString(CultureInfo.InvariantCulture), exception.Message);
            Assert.Contains("/v1/players", exception.Message);

            // Only transient failures are worth retrying; a bad key or a wrong route must not be hammered.
            Assert.Equal(1, RequestCount);
        }

        [Fact]
        public async Task An_unstubbed_route_surfaces_as_an_ExternalApiCallFailed_NBAException()
        {
            // Nothing is registered, so WireMock answers 404 - the client must still translate it.
            var exception = await Assert.ThrowsAsync<NBAException>(
                () => _fixture.Client.GetTodaysGames(CancellationToken.None));

            Assert.Equal(ErrorCodes.ExternalApiCallFailed, exception.ErrorCode);
            Assert.Contains("/v1/games", exception.Message);
        }

        [Fact]
        public async Task A_body_that_is_not_json_becomes_an_ExternalApiResponseInvalid_NBAException()
        {
            // A gateway error page served under a JSON content type - the shape a proxy in front of
            // balldontlie produces when it fails.
            _fixture.Server
                .Given(Request.Create().WithPath("/v1/players").UsingGet())
                .RespondWith(Json(HttpStatusCode.OK, "<html>gateway</html>"));

            var exception = await Assert.ThrowsAsync<NBAException>(
                () => _fixture.Client.GetAllPlayers(Meta(), CancellationToken.None));

            Assert.Equal(ErrorCodes.ExternalApiResponseInvalid, exception.ErrorCode);
            Assert.IsType<System.Text.Json.JsonException>(exception.InnerException);
        }

        [Fact]
        public async Task A_null_body_becomes_an_ExternalApiResponseInvalid_NBAException()
        {
            _fixture.Server
                .Given(Request.Create().WithPath("/v1/players").UsingGet())
                .RespondWith(Json(HttpStatusCode.OK, "null"));

            var exception = await Assert.ThrowsAsync<NBAException>(
                () => _fixture.Client.GetAllPlayers(Meta(), CancellationToken.None));

            Assert.Equal(ErrorCodes.ExternalApiResponseInvalid, exception.ErrorCode);
            Assert.Contains("empty body", exception.Message);
        }

        [Fact]
        public async Task A_response_missing_required_fields_becomes_an_ExternalApiResponseInvalid_NBAException()
        {
            // GameInfoResponse marks home_team/visitor_team as required, so a truncated payload must fail
            // loudly here rather than surfacing as a NullReferenceException in GameService.
            _fixture.Server
                .Given(Request.Create().WithPath("/v1/games").UsingGet())
                .RespondWith(Json(HttpStatusCode.OK, """{"data":[{"id":1,"date":"2026-08-03"}],"meta":{}}"""));

            var exception = await Assert.ThrowsAsync<NBAException>(
                () => _fixture.Client.GetTodaysGames(CancellationToken.None));

            Assert.Equal(ErrorCodes.ExternalApiResponseInvalid, exception.ErrorCode);
        }

        #endregion

        #region external-api-shield

        [Fact]
        public async Task A_server_error_is_retried_by_the_shield_before_it_gives_up()
        {
            // Rule 6: outbound calls go through the named pipeline. Over a real socket that is visible as
            // repeated requests in the server's log.
            _fixture.Server
                .Given(Request.Create().WithPath("/v1/players").UsingGet())
                .RespondWith(Json(HttpStatusCode.InternalServerError, """{"message":"boom"}"""));

            var exception = await Assert.ThrowsAsync<NBAException>(
                () => _fixture.Client.GetAllPlayers(Meta(), CancellationToken.None));

            Assert.Equal(ErrorCodes.ExternalApiCallFailed, exception.ErrorCode);
            Assert.Equal(BallDontLieWireMockFixture.MaxRetryAttempts + 1, RequestCount);
        }

        [Fact]
        public async Task A_transient_failure_that_recovers_is_returned_as_a_normal_result()
        {
            const string scenario = "flaky-players";

            _fixture.Server
                .Given(Request.Create().WithPath("/v1/players").UsingGet())
                .InScenario(scenario)
                .WillSetStateTo("recovered")
                .RespondWith(Json(HttpStatusCode.ServiceUnavailable, """{"message":"try again"}"""));

            _fixture.Server
                .Given(Request.Create().WithPath("/v1/players").UsingGet())
                .InScenario(scenario)
                .WhenStateIs("recovered")
                .RespondWith(Json(HttpStatusCode.OK, LeBronPage));

            var response = await _fixture.Client.GetAllPlayers(Meta(), CancellationToken.None);

            Assert.Equal("LeBron", Assert.Single(response.data).first_name);
            Assert.Equal(2, RequestCount); // the 503, then the retry that succeeded
        }

        [Fact]
        public async Task A_cancelled_token_aborts_a_request_that_is_still_in_flight()
        {
            _fixture.Server
                .Given(Request.Create().WithPath("/v1/games").UsingGet())
                .RespondWith(Json(HttpStatusCode.OK, """{"data":[],"meta":{}}""")
                    .WithDelay(TimeSpan.FromSeconds(10)));

            using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(250));

            await Assert.ThrowsAnyAsync<OperationCanceledException>(
                () => _fixture.Client.GetTodaysGames(cts.Token));
        }

        #endregion

        #region GetPlayerStats (randomized stand-in)

        [Fact]
        public async Task GetPlayerStats_fabricates_one_row_per_player_without_touching_the_api()
        {
            // Stand-in until the player-stats subscription is paid for. When it becomes a real /v1/stats
            // call this test is the one that has to change - and the empty request log is what will fail.
            var playerIds = new List<long> { 237, 115, 3 };

            var stats = await _fixture.Client.GetPlayerStats(playerIds, gameId: 1038184, CancellationToken.None);

            Assert.Equal(playerIds, stats.Select(s => s.player_id));
            Assert.Equal(0, RequestCount);
        }

        #endregion
    }
}
