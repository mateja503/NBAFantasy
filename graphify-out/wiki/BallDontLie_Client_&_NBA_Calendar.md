# BallDontLie Client & NBA Calendar

> 15 nodes

## Key Concepts

- **BallDontLieClient** (13 connections) — `ExternalClients/BallDontLieClient.cs`
- **.GetGames()** (7 connections) — `ExternalClients/BallDontLieClient.cs`
- **.GetPlayerStats()** (7 connections) — `ExternalClients/BallDontLieClient.cs`
- **.GetAllPlayers()** (6 connections) — `ExternalClients/BallDontLieClient.cs`
- **.GetTodaysGames()** (6 connections) — `ExternalClients/BallDontLieClient.cs`
- **CancellationToken** (5 connections)
- **Task** (5 connections)
- **.Today()** (4 connections) — `ApplicationDefaults/Time/NbaCalendar.cs`
- **.GetAsync()** (4 connections) — `ExternalClients/BallDontLieClient.cs`
- **HttpClient** (2 connections)
- **DateOnly** (1 connections)
- **HttpResponseMessage** (1 connections)
- **List** (1 connections)
- **ResiliencePipeline** (1 connections)
- **ResiliencePipelineProvider** (1 connections)

## Relationships

- [IBallDontLieClient Contract](IBallDontLieClient_Contract.md) (7 shared connections)
- [BallDontLie Response Metadata](BallDontLie_Response_Metadata.md) (3 shared connections)
- [BallDontLie Client Tests](BallDontLie_Client_Tests.md) (2 shared connections)
- [NBA Calendar Date Handling](NBA_Calendar_Date_Handling.md) (2 shared connections)
- [WireMock BallDontLie Fixture](WireMock_BallDontLie_Fixture.md) (1 shared connections)
- [External Client Response Tests](External_Client_Response_Tests.md) (1 shared connections)
- [Game Service & Endpoints](Game_Service_&_Endpoints.md) (1 shared connections)
- [Trade & Team Services](Trade_&_Team_Services.md) (1 shared connections)
- [Player Stats Response](Player_Stats_Response.md) (1 shared connections)
- [BoxScore Stats Builder](BoxScore_Stats_Builder.md) (1 shared connections)

## Source Files

- `ApplicationDefaults/Time/NbaCalendar.cs`
- `ExternalClients/BallDontLieClient.cs`

## Audit Trail

- EXTRACTED: 35 (83%)
- INFERRED: 7 (17%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*