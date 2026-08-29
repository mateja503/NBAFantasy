# End-Draft Integration Tests

> 21 nodes

## Key Concepts

- **DraftState** (34 connections) — `NBA.Data/Redis/Entities/DraftState.cs`
- **PlayerShortDto** (17 connections) — `NBA.Data/Redis/Dtos/PlayerShortDto.cs`
- **.BuildService()** (9 connections) — `NBA.Tests/Integration/DraftEndDraftTests.cs`
- **.EndDraft_inserts_every_teams_redis_players_into_the_db_and_completes_the_league()** (7 connections) — `NBA.Tests/Integration/DraftEndDraftTests.cs`
- **.EndDraft_is_a_no_op_when_the_league_draft_is_already_completed()** (7 connections) — `NBA.Tests/Integration/DraftEndDraftTests.cs`
- **DraftEndDraftTests** (6 connections) — `NBA.Tests/Integration/DraftEndDraftTests.cs`
- **.NewContext()** (4 connections) — `NBA.Tests/Integration/DraftEndDraftTests.cs`
- **Fact** (2 connections)
- **Task** (2 connections)
- **FullName** (1 connections) — `NBA.Data/Redis/Dtos/PlayerShortDto.cs`
- **PlayerId** (1 connections) — `NBA.Data/Redis/Dtos/PlayerShortDto.cs`
- **Position** (1 connections) — `NBA.Data/Redis/Dtos/PlayerShortDto.cs`
- **DateTime** (1 connections)
- **Dictionary** (1 connections)
- **List** (1 connections)
- **DraftBoardTeams** (1 connections) — `NBA.Data/Redis/Entities/DraftState.cs`
- **DraftedPlayersPerTeam** (1 connections) — `NBA.Data/Redis/Entities/DraftState.cs`
- **DraftPlayers** (1 connections) — `NBA.Data/Redis/Entities/DraftState.cs`
- **DraftStatus** (1 connections) — `NBA.Data/Redis/Entities/DraftState.cs`
- **LeagueName** (1 connections) — `NBA.Data/Redis/Entities/DraftState.cs`
- **PickEndTime** (1 connections) — `NBA.Data/Redis/Entities/DraftState.cs`

## Relationships

- [Draft Orchestration & Hub](Draft_Orchestration_&_Hub.md) (10 shared connections)
- [Draft Redis Operations & Keys](Draft_Redis_Operations_&_Keys.md) (9 shared connections)
- [Trade Redis & TradeHub Tests](Trade_Redis_&_TradeHub_Tests.md) (6 shared connections)
- [Redis Facade & Draft Options](Redis_Facade_&_Draft_Options.md) (4 shared connections)
- [Player Redis Operations](Player_Redis_Operations.md) (2 shared connections)
- [TradeHub Real-Time Trading](TradeHub_Real-Time_Trading.md) (2 shared connections)
- [Redis Operations Integration Tests](Redis_Operations_Integration_Tests.md) (2 shared connections)
- [TradeHub Test Fixture](TradeHub_Test_Fixture.md) (2 shared connections)
- [EF Core DbContext Model](EF_Core_DbContext_Model.md) (2 shared connections)
- [Player Redis Manager](Player_Redis_Manager.md) (1 shared connections)
- [Draft Endpoints Integration Tests](Draft_Endpoints_Integration_Tests.md) (1 shared connections)
- [Trade & Team Services](Trade_&_Team_Services.md) (1 shared connections)

## Source Files

- `NBA.Data/Redis/Dtos/PlayerShortDto.cs`
- `NBA.Data/Redis/Entities/DraftState.cs`
- `NBA.Tests/Integration/DraftEndDraftTests.cs`

## Audit Trail

- EXTRACTED: 60 (85%)
- INFERRED: 11 (15%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*