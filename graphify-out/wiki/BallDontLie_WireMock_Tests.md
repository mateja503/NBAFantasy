# BallDontLie WireMock Tests

> 33 nodes

## Key Concepts

- **BallDontLieClientWireMockTests** (26 connections) — `NBA.Tests/Integration/BallDontLieClientWireMockTests.cs`
- **.Json()** (16 connections) — `NBA.Tests/Integration/BallDontLieClientWireMockTests.cs`
- **Task** (15 connections)
- **Fact** (14 connections)
- **.Meta()** (11 connections) — `NBA.Tests/Integration/BallDontLieClientWireMockTests.cs`
- **.A_non_transient_error_status_fails_fast_as_an_ExternalApiCallFailed_NBAException()** (8 connections) — `NBA.Tests/Integration/BallDontLieClientWireMockTests.cs`
- **.A_body_that_is_not_json_becomes_an_ExternalApiResponseInvalid_NBAException()** (7 connections) — `NBA.Tests/Integration/BallDontLieClientWireMockTests.cs`
- **.A_null_body_becomes_an_ExternalApiResponseInvalid_NBAException()** (6 connections) — `NBA.Tests/Integration/BallDontLieClientWireMockTests.cs`
- **.A_server_error_is_retried_by_the_shield_before_it_gives_up()** (6 connections) — `NBA.Tests/Integration/BallDontLieClientWireMockTests.cs`
- **.Every_request_carries_the_configured_api_key_and_accepts_json()** (6 connections) — `NBA.Tests/Integration/BallDontLieClientWireMockTests.cs`
- **.A_cancelled_token_aborts_a_request_that_is_still_in_flight()** (5 connections) — `NBA.Tests/Integration/BallDontLieClientWireMockTests.cs`
- **.A_response_missing_required_fields_becomes_an_ExternalApiResponseInvalid_NBAException()** (5 connections) — `NBA.Tests/Integration/BallDontLieClientWireMockTests.cs`
- **.A_transient_failure_that_recovers_is_returned_as_a_normal_result()** (5 connections) — `NBA.Tests/Integration/BallDontLieClientWireMockTests.cs`
- **.GetAllPlayers_deserializes_a_player_with_its_team_and_paging_cursor()** (5 connections) — `NBA.Tests/Integration/BallDontLieClientWireMockTests.cs`
- **.GetAllPlayers_requests_the_first_page_without_a_cursor()** (5 connections) — `NBA.Tests/Integration/BallDontLieClientWireMockTests.cs`
- **.GetAllPlayers_walks_to_the_next_page_with_the_cursor_the_api_returned()** (5 connections) — `NBA.Tests/Integration/BallDontLieClientWireMockTests.cs`
- **.An_unstubbed_route_surfaces_as_an_ExternalApiCallFailed_NBAException()** (4 connections) — `NBA.Tests/Integration/BallDontLieClientWireMockTests.cs`
- **.GetTodaysGames_asks_for_today_in_the_nba_timezone_not_utc()** (4 connections) — `NBA.Tests/Integration/BallDontLieClientWireMockTests.cs`
- **.GetTodaysGames_deserializes_the_matchup()** (4 connections) — `NBA.Tests/Integration/BallDontLieClientWireMockTests.cs`
- **.GetPlayerStats_fabricates_one_row_per_player_without_touching_the_api()** (3 connections) — `NBA.Tests/Integration/BallDontLieClientWireMockTests.cs`
- **.LastRequestHeader()** (2 connections) — `NBA.Tests/Integration/BallDontLieClientWireMockTests.cs`
- **HttpStatusCode** (2 connections)
- **IClassFixture** (1 connections)
- **IRequestMessage** (1 connections)
- **IResponseBuilder** (1 connections)
- *... and 8 more nodes in this community*

## Relationships

- [Trade & Team Services](Trade_&_Team_Services.md) (6 shared connections)
- [WireMock BallDontLie Fixture](WireMock_BallDontLie_Fixture.md) (1 shared connections)
- [Redis Operations Integration Tests](Redis_Operations_Integration_Tests.md) (1 shared connections)
- [BallDontLie Response Metadata](BallDontLie_Response_Metadata.md) (1 shared connections)

## Source Files

- `NBA.Tests/Integration/BallDontLieClientWireMockTests.cs`

## Audit Trail

- EXTRACTED: 92 (100%)
- INFERRED: 0 (0%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*