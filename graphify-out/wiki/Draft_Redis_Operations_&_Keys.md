# Draft Redis Operations & Keys

> 70 nodes

## Key Concepts

- **DraftRedisOperations** (20 connections) — `NBA.Data/Redis/Operations/DraftRedisOperations.cs`
- **LeagueDraft** (18 connections) — `NBA.Data/Redis/Scopes/LeagueDraft.cs`
- **RedisKeys** (16 connections) — `NBA.Data/Redis/Keys/RedisKeys.cs`
- **Task** (15 connections)
- **Task** (14 connections)
- **TeamDraftBoard** (12 connections) — `NBA.Data/Redis/Entities/DraftBoardTeams.cs`
- **DraftBoardTeams** (9 connections) — `NBA.Data/Redis/Entities/DraftBoardTeams.cs`
- **.GetAllTeamsDraftedPlayersForLeague()** (7 connections) — `NBA.Data/Redis/Operations/DraftRedisOperations.cs`
- **.GetDraftTeams()** (7 connections) — `NBA.Data/Redis/Operations/DraftRedisOperations.cs`
- **.SetDraftTeams()** (7 connections) — `NBA.Data/Redis/Operations/DraftRedisOperations.cs`
- **.GetDraftStateKey()** (6 connections) — `NBA.Data/Redis/Keys/RedisKeys.cs`
- **.GetCurrentDraftState()** (6 connections) — `NBA.Data/Redis/Operations/DraftRedisOperations.cs`
- **.GetAllTeamsDraftedPlayers()** (6 connections) — `NBA.Data/Redis/Scopes/LeagueDraft.cs`
- **.GetTeams()** (6 connections) — `NBA.Data/Redis/Scopes/LeagueDraft.cs`
- **.SetTeams()** (6 connections) — `NBA.Data/Redis/Scopes/LeagueDraft.cs`
- **AuthRedisOperations** (5 connections) — `NBA.Data/Redis/Operations/AuthRedisOperations.cs`
- **.GetDraftTeamsKey()** (5 connections) — `NBA.Data/Redis/Keys/RedisKeys.cs`
- **.GetDraftTimersKey()** (5 connections) — `NBA.Data/Redis/Keys/RedisKeys.cs`
- **.DeleteStringDraftState()** (5 connections) — `NBA.Data/Redis/Operations/DraftRedisOperations.cs`
- **.ScheduleDraftTimer()** (5 connections) — `NBA.Data/Redis/Operations/DraftRedisOperations.cs`
- **.SetDraftState()** (5 connections) — `NBA.Data/Redis/Operations/DraftRedisOperations.cs`
- **.TryAcquireDraftCycleLock()** (5 connections) — `NBA.Data/Redis/Operations/DraftRedisOperations.cs`
- **.StoreRefreshToken()** (4 connections) — `NBA.Data/Redis/Operations/AuthRedisOperations.cs`
- **.CancelDraftTimer()** (4 connections) — `NBA.Data/Redis/Operations/DraftRedisOperations.cs`
- **.ClaimDueDraftTimer()** (4 connections) — `NBA.Data/Redis/Operations/DraftRedisOperations.cs`
- *... and 45 more nodes in this community*

## Relationships

- [End-Draft Integration Tests](End-Draft_Integration_Tests.md) (9 shared connections)
- [Player Redis Operations](Player_Redis_Operations.md) (6 shared connections)
- [Redis Operations Integration Tests](Redis_Operations_Integration_Tests.md) (6 shared connections)
- [Draft Orchestration & Hub](Draft_Orchestration_&_Hub.md) (4 shared connections)
- [Trade Redis & TradeHub Tests](Trade_Redis_&_TradeHub_Tests.md) (4 shared connections)
- [Redis Facade & Draft Options](Redis_Facade_&_Draft_Options.md) (3 shared connections)
- [Hosted Services & Exception Handling](Hosted_Services_&_Exception_Handling.md) (1 shared connections)
- [Game Redis Operations](Game_Redis_Operations.md) (1 shared connections)

## Source Files

- `NBA.Data/Redis/Entities/DraftBoardTeams.cs`
- `NBA.Data/Redis/Keys/RedisKeys.cs`
- `NBA.Data/Redis/Operations/AuthRedisOperations.cs`
- `NBA.Data/Redis/Operations/DraftRedisOperations.cs`
- `NBA.Data/Redis/Scopes/LeagueDraft.cs`

## Audit Trail

- EXTRACTED: 157 (92%)
- INFERRED: 14 (8%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*