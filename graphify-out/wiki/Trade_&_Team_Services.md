# Trade & Team Services

> 65 nodes

## Key Concepts

- **NBAException** (47 connections) — `ApplicationDefaults/Exceptions/NBAException.cs`
- **Trade** (27 connections) — `NBA.Data/Entities/Trade.cs`
- **TradeService** (15 connections) — `NBA.Service/Trade/TradeService.cs`
- **.AddProposedTrade()** (15 connections) — `NBA.Service/Trade/TradeService.cs`
- **.AcceptProposal()** (11 connections) — `NBA.Service/Trade/TradeService.cs`
- **.Trade()** (11 connections) — `NBA.Service/Trade/TradeService.cs`
- **.GetAllTeams()** (10 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **RosterValidator** (9 connections) — `NBA.Service/Roster/RosterValidator.cs`
- **.DraftPlayer()** (9 connections) — `NBA.Service/Draft/DraftService.cs`
- **.GetUserTeamsWithPlayersAsync()** (9 connections) — `NBA.Service/Team/TeamService.cs`
- **.ValidateSeasonTrade()** (9 connections) — `NBA.Service/Trade/TradeService.cs`
- **.GetAllTrades()** (8 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **.GetTeamPlayersAsync()** (8 connections) — `NBA.Service/Team/TeamService.cs`
- **.GetLeagueTrades()** (8 connections) — `NBA.Service/Trade/TradeService.cs`
- **.RejectProposal()** (8 connections) — `NBA.Service/Trade/TradeService.cs`
- **Task** (8 connections)
- **.MapTeamEndpoints()** (7 connections) — `NBA.Api/Endpoints/TeamEndpoints.cs`
- **.GetAllPlayers()** (7 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **.GetAllTeamPlayer()** (7 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **.UpdateTradeRange()** (7 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **.AddAsync()** (7 connections) — `NBA.Service/Team/TeamService.cs`
- **IQueryable** (7 connections)
- **TeamService** (6 connections) — `NBA.Service/Team/TeamService.cs`
- **.GetLeagueTeamsAsync()** (6 connections) — `NBA.Service/Team/TeamService.cs`
- **.GetPendingProposals()** (6 connections) — `NBA.Service/Trade/TradeService.cs`
- *... and 40 more nodes in this community*

## Relationships

- [TradeHub Real-Time Trading](TradeHub_Real-Time_Trading.md) (12 shared connections)
- [TeamPlayer & Draft Snapshot Persistence](TeamPlayer_&_Draft_Snapshot_Persistence.md) (11 shared connections)
- [Draft Orchestration & Hub](Draft_Orchestration_&_Hub.md) (10 shared connections)
- [Error Codes & Trade Statuses](Error_Codes_&_Trade_Statuses.md) (9 shared connections)
- [EF Core DbContext Model](EF_Core_DbContext_Model.md) (9 shared connections)
- [BallDontLie WireMock Tests](BallDontLie_WireMock_Tests.md) (6 shared connections)
- [BallDontLie Client Tests](BallDontLie_Client_Tests.md) (5 shared connections)
- [Team Entity](Team_Entity.md) (5 shared connections)
- [Trade Redis & TradeHub Tests](Trade_Redis_&_TradeHub_Tests.md) (5 shared connections)
- [TradeHub Test Fixture](TradeHub_Test_Fixture.md) (4 shared connections)
- [User Auth Persistence](User_Auth_Persistence.md) (4 shared connections)
- [League Service & Endpoints](League_Service_&_Endpoints.md) (4 shared connections)

## Source Files

- `ApplicationDefaults/Exceptions/NBAException.cs`
- `NBA.Api/Endpoints/TeamEndpoints.cs`
- `NBA.Api/Endpoints/TradeEndpoints.cs`
- `NBA.Data/Context/NbaFantasyContextExt.cs`
- `NBA.Data/Entities/Trade.cs`
- `NBA.Service/Draft/DraftService.cs`
- `NBA.Service/Player/PlayerService.cs`
- `NBA.Service/Roster/RosterValidator.cs`
- `NBA.Service/Team/TeamService.cs`
- `NBA.Service/Trade/TradeService.cs`

## Audit Trail

- EXTRACTED: 148 (65%)
- INFERRED: 78 (35%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*