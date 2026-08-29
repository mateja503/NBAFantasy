# TradeHub Real-Time Trading

> 31 nodes

## Key Concepts

- **TradeHub** (22 connections) — `NBA.Api/SignalR/Hubs/TradeHub.cs`
- **TradeManager** (16 connections) — `NBA.Service/Trade/TradeManager.cs`
- **.AcceptDraftTrade()** (12 connections) — `NBA.Service/Trade/TradeManager.cs`
- **.ProposeSeasonTrade()** (11 connections) — `NBA.Api/SignalR/Hubs/TradeHub.cs`
- **.ComputeSwappedRosters()** (10 connections) — `NBA.Service/Trade/TradeManager.cs`
- **.AcceptSeasonTrade()** (7 connections) — `NBA.Api/SignalR/Hubs/TradeHub.cs`
- **.RejectSeasonTrade()** (7 connections) — `NBA.Api/SignalR/Hubs/TradeHub.cs`
- **.IsTradeValid()** (7 connections) — `NBA.Service/Trade/TradeManager.cs`
- **.RemoveProposedSeasonTrade()** (7 connections) — `NBA.Service/Trade/TradeManager.cs`
- **Task** (7 connections)
- **.AcceptTrade()** (6 connections) — `NBA.Api/SignalR/Hubs/TradeHub.cs`
- **.ProposeTrade()** (6 connections) — `NBA.Api/SignalR/Hubs/TradeHub.cs`
- **.SendPendingProposals()** (6 connections) — `NBA.Api/SignalR/Hubs/TradeHub.cs`
- **.ToSettled()** (6 connections) — `NBA.Api/SignalR/Hubs/TradeHub.cs`
- **.GetProposedSeasonTrades()** (6 connections) — `NBA.Service/Trade/TradeManager.cs`
- **.ProposeSeasonTrade()** (6 connections) — `NBA.Service/Trade/TradeManager.cs`
- **.ValidateRoster()** (6 connections) — `NBA.Service/Trade/TradeManager.cs`
- **Task** (6 connections)
- **.ProposeDraftTrade()** (5 connections) — `NBA.Service/Trade/TradeManager.cs`
- **.OnConnectedAsync()** (3 connections) — `NBA.Api/SignalR/Hubs/TradeHub.cs`
- **Guid** (3 connections)
- **List** (3 connections)
- **List** (2 connections)
- **Dictionary** (2 connections)
- **Guid** (2 connections)
- *... and 6 more nodes in this community*

## Relationships

- [Trade & Team Services](Trade_&_Team_Services.md) (12 shared connections)
- [Draft Orchestration & Hub](Draft_Orchestration_&_Hub.md) (11 shared connections)
- [Trade Redis & TradeHub Tests](Trade_Redis_&_TradeHub_Tests.md) (10 shared connections)
- [TradeHub Test Fixture](TradeHub_Test_Fixture.md) (4 shared connections)
- [Redis Facade & Draft Options](Redis_Facade_&_Draft_Options.md) (4 shared connections)
- [Trade DTO](Trade_DTO.md) (3 shared connections)
- [Player Redis Manager](Player_Redis_Manager.md) (2 shared connections)
- [Draft Endpoints Integration Tests](Draft_Endpoints_Integration_Tests.md) (2 shared connections)
- [End-Draft Integration Tests](End-Draft_Integration_Tests.md) (2 shared connections)

## Source Files

- `NBA.Api/SignalR/Hubs/TradeHub.cs`
- `NBA.Service/Trade/TradeManager.cs`

## Audit Trail

- EXTRACTED: 87 (76%)
- INFERRED: 28 (24%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*