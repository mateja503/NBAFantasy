# Trade Redis & TradeHub Tests

> 78 nodes

## Key Concepts

- **TradeBetweenTeams** (52 connections) — `NBA.Data/Redis/Entities/TradeBetweenTeams.cs`
- **TradeRedisOperations** (15 connections) — `NBA.Data/Redis/Operations/TradeRedisOperations.cs`
- **.GetProposedDraftTradesKey()** (13 connections) — `NBA.Data/Redis/Keys/RedisKeys.cs`
- **.AcceptTrade_revalidates_roster_limits_against_current_state()** (13 connections) — `NBA.Tests/Integration/TradeHubTests.cs`
- **.AcceptTrade_throws_when_a_team_is_missing_from_draft_state()** (13 connections) — `NBA.Tests/Integration/TradeHubTests.cs`
- **LeagueTrades** (12 connections) — `NBA.Data/Redis/Scopes/LeagueTrades.cs`
- **TradeHubTests** (12 connections) — `NBA.Tests/Integration/TradeHubTests.cs`
- **.AcceptTrade_throws_when_draft_state_is_missing()** (12 connections) — `NBA.Tests/Integration/TradeHubTests.cs`
- **.AcceptTrade_cannot_be_accepted_twice()** (11 connections) — `NBA.Tests/Integration/TradeHubTests.cs`
- **.BuildDraftState()** (11 connections) — `NBA.Tests/Integration/TradeHubTests.cs`
- **.BuildClient()** (10 connections) — `NBA.Tests/Integration/TradeHubFixture.cs`
- **.AcceptTrade_swaps_rosters_broadcasts_to_league_and_removes_proposed()** (10 connections) — `NBA.Tests/Integration/TradeHubTests.cs`
- **.ProposeTrade_valid_notifies_target_team_and_stores_proposed_trade()** (9 connections) — `NBA.Tests/Integration/TradeHubTests.cs`
- **.WaitFor()** (9 connections) — `NBA.Tests/Integration/TradeHubTests.cs`
- **Task** (9 connections)
- **Task** (9 connections)
- **.GetAcceptedDraftTradeKey()** (8 connections) — `NBA.Data/Redis/Keys/RedisKeys.cs`
- **.SetProposedSeasonTrade()** (8 connections) — `NBA.Data/Redis/Operations/TradeRedisOperations.cs`
- **Task** (8 connections)
- **Fact** (8 connections)
- **ITradeHubClient** (7 connections) — `NBA.Api/SignalR/Clients/ITradeHubClient.cs`
- **.GetProposedSeasonTrades()** (7 connections) — `NBA.Data/Redis/Operations/TradeRedisOperations.cs`
- **.ProposeTrade_over_center_limit_is_rejected_and_stores_nothing()** (7 connections) — `NBA.Tests/Integration/TradeHubTests.cs`
- **HubException** (7 connections)
- **TaskCompletionSource** (7 connections)
- *... and 53 more nodes in this community*

## Relationships

- [TradeHub Real-Time Trading](TradeHub_Real-Time_Trading.md) (10 shared connections)
- [Redis Operations Integration Tests](Redis_Operations_Integration_Tests.md) (6 shared connections)
- [End-Draft Integration Tests](End-Draft_Integration_Tests.md) (6 shared connections)
- [Trade & Team Services](Trade_&_Team_Services.md) (5 shared connections)
- [Draft Redis Operations & Keys](Draft_Redis_Operations_&_Keys.md) (4 shared connections)
- [TradeHub Test Fixture](TradeHub_Test_Fixture.md) (3 shared connections)
- [Redis Facade & Draft Options](Redis_Facade_&_Draft_Options.md) (1 shared connections)
- [Player Redis Operations](Player_Redis_Operations.md) (1 shared connections)
- [SignalR Exception Hub Filter](SignalR_Exception_Hub_Filter.md) (1 shared connections)
- [Draft Endpoints Integration Tests](Draft_Endpoints_Integration_Tests.md) (1 shared connections)

## Source Files

- `NBA.Api/SignalR/Clients/ITradeHubClient.cs`
- `NBA.Data/Redis/Entities/TradeBetweenTeams.cs`
- `NBA.Data/Redis/Keys/RedisKeys.cs`
- `NBA.Data/Redis/Operations/TradeRedisOperations.cs`
- `NBA.Data/Redis/Scopes/LeagueTrades.cs`
- `NBA.Tests/Integration/TradeHubFixture.cs`
- `NBA.Tests/Integration/TradeHubTests.cs`
- `NBA.Tests/Integration/TradeRedisWriteTests.cs`

## Audit Trail

- EXTRACTED: 224 (91%)
- INFERRED: 23 (9%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*