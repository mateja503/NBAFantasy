# Redis Facade & Draft Options

> 31 nodes

## Key Concepts

- **NbaFantasyRedis** (30 connections) — `NBA.Data/Context/NbaFantasyRedis.cs`
- **DraftService** (19 connections) — `NBA.Service/Draft/DraftService.cs`
- **DraftHub** (14 connections) — `NBA.Api/SignalR/Hubs/DraftHub.cs`
- **DraftSnapshotService** (13 connections) — `NBA.Service/Draft/DraftSnapshotService.cs`
- **DraftTimerProcessor** (12 connections) — `NBA.Api/Draft/DraftTimerProcessor.cs`
- **DraftOptions** (11 connections) — `ApplicationDefaults/Options/DraftOptions.cs`
- **IDraftHubClient** (6 connections) — `NBA.Api/SignalR/Clients/IDraftHubClient.cs`
- **.TeamDraftBoard()** (3 connections) — `NBA.Api/SignalR/Clients/IDraftHubClient.cs`
- **.UpdateDraftState()** (3 connections) — `NBA.Api/SignalR/Clients/IDraftHubClient.cs`
- **Hub** (3 connections)
- **DraftOptions.cs** (2 connections) — `ApplicationDefaults/Options/DraftOptions.cs`
- **Task** (2 connections)
- **DraftPickTime** (1 connections) — `ApplicationDefaults/Options/DraftOptions.cs`
- **Rounds** (1 connections) — `ApplicationDefaults/Options/DraftOptions.cs`
- **ShowTeamDraftBoardCount** (1 connections) — `ApplicationDefaults/Options/DraftOptions.cs`
- **Lazy** (1 connections)
- **IHubContext** (1 connections)
- **IOptions** (1 connections)
- **IOptions** (1 connections)
- **IDatabase** (1 connections)
- **Auth** (1 connections) — `NBA.Data/Context/NbaFantasyRedis.cs`
- **Draft** (1 connections) — `NBA.Data/Context/NbaFantasyRedis.cs`
- **Game** (1 connections) — `NBA.Data/Context/NbaFantasyRedis.cs`
- **Lock** (1 connections) — `NBA.Data/Context/NbaFantasyRedis.cs`
- **Player** (1 connections) — `NBA.Data/Context/NbaFantasyRedis.cs`
- *... and 6 more nodes in this community*

## Relationships

- [Draft Orchestration & Hub](Draft_Orchestration_&_Hub.md) (19 shared connections)
- [TradeHub Test Fixture](TradeHub_Test_Fixture.md) (5 shared connections)
- [End-Draft Integration Tests](End-Draft_Integration_Tests.md) (4 shared connections)
- [Draft Endpoints Integration Tests](Draft_Endpoints_Integration_Tests.md) (4 shared connections)
- [TradeHub Real-Time Trading](TradeHub_Real-Time_Trading.md) (4 shared connections)
- [Draft Redis Operations & Keys](Draft_Redis_Operations_&_Keys.md) (3 shared connections)
- [Draft Timer Hosted Service](Draft_Timer_Hosted_Service.md) (2 shared connections)
- [Player Redis Manager](Player_Redis_Manager.md) (2 shared connections)
- [Game Redis Operations](Game_Redis_Operations.md) (2 shared connections)
- [Player Redis Operations](Player_Redis_Operations.md) (2 shared connections)
- [EF Core DbContext Model](EF_Core_DbContext_Model.md) (2 shared connections)
- [Trade & Team Services](Trade_&_Team_Services.md) (2 shared connections)

## Source Files

- `ApplicationDefaults/Options/DraftOptions.cs`
- `NBA.Api/Draft/DraftTimerProcessor.cs`
- `NBA.Api/SignalR/Clients/IDraftHubClient.cs`
- `NBA.Api/SignalR/Hubs/DraftHub.cs`
- `NBA.Data/Context/NbaFantasyRedis.cs`
- `NBA.Service/Draft/DraftService.cs`
- `NBA.Service/Draft/DraftSnapshotService.cs`

## Audit Trail

- EXTRACTED: 96 (98%)
- INFERRED: 2 (2%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*