# Game Service & Endpoints

> 13 nodes

## Key Concepts

- **GameService** (11 connections) — `NBA.Service/Game/GameService.cs`
- **.GetScheduledGamesAsync()** (11 connections) — `NBA.Service/Game/GameService.cs`
- **.FetchGamesAsync()** (10 connections) — `NBA.Service/Game/GameService.cs`
- **.TodaysGames()** (7 connections) — `NBA.Service/Game/GameService.cs`
- **.MapGameEndpoints()** (3 connections) — `NBA.Api/Endpoints/GameEndpoints.cs`
- **CancellationToken** (3 connections)
- **List** (3 connections)
- **Task** (3 connections)
- **GameEndpoints** (2 connections) — `NBA.Api/Endpoints/GameEndpoints.cs`
- **DateOnly** (2 connections)
- **IBackgroundJobClient** (1 connections)
- **IEndpointRouteBuilder** (1 connections)
- **IOptions** (1 connections)

## Relationships

- [Game Redis Operations](Game_Redis_Operations.md) (4 shared connections)
- [Game Schedule Bucketing Tests](Game_Schedule_Bucketing_Tests.md) (4 shared connections)
- [IBallDontLieClient Contract](IBallDontLieClient_Contract.md) (3 shared connections)
- [Team Endpoints & Auth Claims](Team_Endpoints_&_Auth_Claims.md) (1 shared connections)
- [WireMock BallDontLie Fixture](WireMock_BallDontLie_Fixture.md) (1 shared connections)
- [Hosted Services & Exception Handling](Hosted_Services_&_Exception_Handling.md) (1 shared connections)
- [External Client Response Tests](External_Client_Response_Tests.md) (1 shared connections)
- [Redis Adapter Mappings](Redis_Adapter_Mappings.md) (1 shared connections)
- [Game Redis Shapes](Game_Redis_Shapes.md) (1 shared connections)
- [BallDontLie Response Metadata](BallDontLie_Response_Metadata.md) (1 shared connections)
- [BallDontLie Client & NBA Calendar](BallDontLie_Client_&_NBA_Calendar.md) (1 shared connections)
- [NBA Calendar Date Handling](NBA_Calendar_Date_Handling.md) (1 shared connections)

## Source Files

- `NBA.Api/Endpoints/GameEndpoints.cs`
- `NBA.Service/Game/GameService.cs`

## Audit Trail

- EXTRACTED: 34 (85%)
- INFERRED: 6 (15%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*