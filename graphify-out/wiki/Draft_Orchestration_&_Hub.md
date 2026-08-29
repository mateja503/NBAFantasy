# Draft Orchestration & Hub

> 38 nodes

## Key Concepts

- **.League()** (24 connections) — `NBA.Data/Context/NbaFantasyRedis.cs`
- **DraftManager** (22 connections) — `NBA.Service/Draft/DraftManager.cs`
- **.EnsureRehydratedAsync()** (14 connections) — `NBA.Service/Draft/DraftSnapshotService.cs`
- **.GetAllLeagues()** (13 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **.NextPick()** (11 connections) — `NBA.Service/Draft/DraftManager.cs`
- **.DraftOrder()** (11 connections) — `NBA.Service/Draft/DraftService.cs`
- **.StartDraftAsync()** (10 connections) — `NBA.Api/Draft/DraftTimerProcessor.cs`
- **.OnConnectedAsync()** (10 connections) — `NBA.Api/SignalR/Hubs/DraftHub.cs`
- **.DraftPlayer()** (9 connections) — `NBA.Api/SignalR/Hubs/DraftHub.cs`
- **.EndDraft()** (9 connections) — `NBA.Service/Draft/DraftManager.cs`
- **.GetDraftState()** (9 connections) — `NBA.Service/Draft/DraftManager.cs`
- **.ResetTimer()** (9 connections) — `NBA.Service/Draft/DraftManager.cs`
- **.EndDraft()** (9 connections) — `NBA.Service/Draft/DraftService.cs`
- **.PrepareDraftBoard()** (9 connections) — `NBA.Service/Draft/DraftService.cs`
- **Task** (9 connections)
- **.AdvanceAsync()** (8 connections) — `NBA.Api/Draft/DraftTimerProcessor.cs`
- **.BuildEndedState()** (8 connections) — `NBA.Service/Draft/DraftManager.cs`
- **.CreateDraftState()** (8 connections) — `NBA.Service/Draft/DraftManager.cs`
- **.UpdaterDraftState()** (8 connections) — `NBA.Service/Draft/DraftManager.cs`
- **.MapDraftEndpoints()** (7 connections) — `NBA.Api/Endpoints/DraftEndpoints.cs`
- **.PersistAsync()** (7 connections) — `NBA.Service/Draft/DraftSnapshotService.cs`
- **.ArmNextDeadlineAsync()** (6 connections) — `NBA.Service/Draft/DraftManager.cs`
- **.CheckDraftCompleted()** (6 connections) — `NBA.Service/Draft/DraftService.cs`
- **.ResetTimer()** (5 connections) — `NBA.Api/SignalR/Hubs/DraftHub.cs`
- **.AddTeamsDrafterPlayersToDraftState()** (4 connections) — `NBA.Service/Draft/DraftManager.cs`
- *... and 13 more nodes in this community*

## Relationships

- [Redis Facade & Draft Options](Redis_Facade_&_Draft_Options.md) (19 shared connections)
- [TradeHub Real-Time Trading](TradeHub_Real-Time_Trading.md) (11 shared connections)
- [Trade & Team Services](Trade_&_Team_Services.md) (10 shared connections)
- [End-Draft Integration Tests](End-Draft_Integration_Tests.md) (10 shared connections)
- [TeamPlayer & Draft Snapshot Persistence](TeamPlayer_&_Draft_Snapshot_Persistence.md) (6 shared connections)
- [Player Redis Manager](Player_Redis_Manager.md) (5 shared connections)
- [Draft Redis Operations & Keys](Draft_Redis_Operations_&_Keys.md) (4 shared connections)
- [League Service & Endpoints](League_Service_&_Endpoints.md) (3 shared connections)
- [EF Core DbContext Model](EF_Core_DbContext_Model.md) (2 shared connections)
- [Draft Endpoints Integration Tests](Draft_Endpoints_Integration_Tests.md) (2 shared connections)
- [TradeHub Test Fixture](TradeHub_Test_Fixture.md) (1 shared connections)
- [Testing Endpoints](Testing_Endpoints.md) (1 shared connections)

## Source Files

- `NBA.Api/Draft/DraftTimerProcessor.cs`
- `NBA.Api/Endpoints/DraftEndpoints.cs`
- `NBA.Api/SignalR/Hubs/DraftHub.cs`
- `NBA.Data/Context/NbaFantasyContextExt.cs`
- `NBA.Data/Context/NbaFantasyRedis.cs`
- `NBA.Service/Draft/DraftManager.cs`
- `NBA.Service/Draft/DraftService.cs`
- `NBA.Service/Draft/DraftSnapshotService.cs`

## Audit Trail

- EXTRACTED: 85 (49%)
- INFERRED: 90 (51%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*