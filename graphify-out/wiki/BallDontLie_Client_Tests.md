# BallDontLie Client Tests

> 51 nodes

## Key Concepts

- **.CreateClient()** (24 connections) — `NBA.Tests/BallDontLieClientTests.cs`
- **BallDontLieClientTests** (21 connections) — `NBA.Tests/BallDontLieClientTests.cs`
- **StubHttpMessageHandler** (17 connections) — `NBA.Tests/Fakes/StubHttpMessageHandler.cs`
- **Task** (17 connections)
- **.RespondsWith()** (16 connections) — `NBA.Tests/Fakes/StubHttpMessageHandler.cs`
- **Fact** (16 connections)
- **.Meta()** (14 connections) — `NBA.Tests/BallDontLieClientTests.cs`
- **.A_non_success_status_becomes_an_ExternalApiCallFailed_NBAException()** (9 connections) — `NBA.Tests/BallDontLieClientTests.cs`
- **.A_body_that_is_not_json_becomes_an_ExternalApiResponseInvalid_NBAException()** (8 connections) — `NBA.Tests/BallDontLieClientTests.cs`
- **.A_first_attempt_that_succeeds_after_a_retry_is_returned_normally()** (7 connections) — `NBA.Tests/BallDontLieClientTests.cs`
- **.A_null_body_becomes_an_ExternalApiResponseInvalid_NBAException()** (7 connections) — `NBA.Tests/BallDontLieClientTests.cs`
- **.Requests_run_through_the_external_api_shield_pipeline_and_are_retried()** (7 connections) — `NBA.Tests/BallDontLieClientTests.cs`
- **.A_cancelled_token_stops_the_call_before_it_reaches_the_wire()** (6 connections) — `NBA.Tests/BallDontLieClientTests.cs`
- **.A_response_missing_required_fields_becomes_an_ExternalApiResponseInvalid_NBAException()** (6 connections) — `NBA.Tests/BallDontLieClientTests.cs`
- **.GetAllPlayers_appends_the_cursor_for_subsequent_pages()** (6 connections) — `NBA.Tests/BallDontLieClientTests.cs`
- **.GetAllPlayers_deserializes_players_team_and_paging_cursor()** (6 connections) — `NBA.Tests/BallDontLieClientTests.cs`
- **.GetAllPlayers_omits_cursor_on_the_first_page()** (6 connections) — `NBA.Tests/BallDontLieClientTests.cs`
- **.GetGames_appends_the_cursor_for_subsequent_pages()** (6 connections) — `NBA.Tests/BallDontLieClientTests.cs`
- **.GetGames_asks_for_the_whole_window_in_one_request()** (6 connections) — `NBA.Tests/BallDontLieClientTests.cs`
- **.GetGames_deserializes_the_team_details_and_scores()** (6 connections) — `NBA.Tests/BallDontLieClientTests.cs`
- **.GetGames_still_deserializes_a_payload_without_the_optional_team_details()** (6 connections) — `NBA.Tests/BallDontLieClientTests.cs`
- **.AlwaysFails()** (6 connections) — `NBA.Tests/Fakes/StubHttpMessageHandler.cs`
- **.JsonResponse()** (6 connections) — `NBA.Tests/Fakes/StubHttpMessageHandler.cs`
- **.GetPlayerStats_fabricates_one_row_per_player_without_calling_the_api()** (5 connections) — `NBA.Tests/BallDontLieClientTests.cs`
- **.GetTodaysGames_asks_for_today_in_the_nba_timezone_not_utc()** (5 connections) — `NBA.Tests/BallDontLieClientTests.cs`
- *... and 26 more nodes in this community*

## Relationships

- [Trade & Team Services](Trade_&_Team_Services.md) (5 shared connections)
- [External Client Response Tests](External_Client_Response_Tests.md) (2 shared connections)
- [BallDontLie Client & NBA Calendar](BallDontLie_Client_&_NBA_Calendar.md) (2 shared connections)
- [BallDontLie Response Metadata](BallDontLie_Response_Metadata.md) (1 shared connections)

## Source Files

- `NBA.Tests/BallDontLieClientTests.cs`
- `NBA.Tests/Fakes/StubHttpMessageHandler.cs`

## Audit Trail

- EXTRACTED: 144 (99%)
- INFERRED: 1 (1%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*