# Player Redis Manager

> 10 nodes

## Key Concepts

- **PlayerManager** (15 connections) — `NBA.Service/Player/PlayerManager.cs`
- **.GetPlayersOnDraftBoard()** (8 connections) — `NBA.Service/Player/PlayerManager.cs`
- **.AddPlayersToRedis()** (5 connections) — `NBA.Service/Player/PlayerManager.cs`
- **.AddPlayerToRedisFromDB()** (5 connections) — `NBA.Service/Player/PlayerManager.cs`
- **.AddDraftedPlayers()** (4 connections) — `NBA.Service/Player/PlayerManager.cs`
- **Task** (4 connections)
- **List** (3 connections)
- **IOptions** (1 connections)
- **JsonOptions** (1 connections)
- **JsonSerializerOptions** (1 connections)

## Relationships

- [Draft Orchestration & Hub](Draft_Orchestration_&_Hub.md) (5 shared connections)
- [Redis Facade & Draft Options](Redis_Facade_&_Draft_Options.md) (2 shared connections)
- [TradeHub Real-Time Trading](TradeHub_Real-Time_Trading.md) (2 shared connections)
- [Redis Adapter Mappings](Redis_Adapter_Mappings.md) (2 shared connections)
- [EF Core DbContext Model](EF_Core_DbContext_Model.md) (1 shared connections)
- [BoxScore Calculation Jobs](BoxScore_Calculation_Jobs.md) (1 shared connections)
- [Hosted Services & Exception Handling](Hosted_Services_&_Exception_Handling.md) (1 shared connections)
- [External Client Response Tests](External_Client_Response_Tests.md) (1 shared connections)
- [TradeHub Test Fixture](TradeHub_Test_Fixture.md) (1 shared connections)
- [PlayerInfo Response Shape](PlayerInfo_Response_Shape.md) (1 shared connections)
- [Error Codes & Trade Statuses](Error_Codes_&_Trade_Statuses.md) (1 shared connections)
- [End-Draft Integration Tests](End-Draft_Integration_Tests.md) (1 shared connections)

## Source Files

- `NBA.Service/Player/PlayerManager.cs`

## Audit Trail

- EXTRACTED: 26 (79%)
- INFERRED: 7 (21%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*