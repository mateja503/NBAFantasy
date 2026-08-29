# League Service & Endpoints

> 22 nodes

## Key Concepts

- **League** (18 connections) — `NBA.Data/Entities/Draftsnapshot.cs`
- **LeagueService.cs** (10 connections) — `NBA.Service/League/LeagueService.cs`
- **.MapLeaguEndpoints()** (9 connections) — `NBA.Api/Endpoints/LeagueEndpoints.cs`
- **.CreateAsync()** (9 connections) — `NBA.Service/League/LeagueService.cs`
- **.JoinAsync()** (9 connections) — `NBA.Service/League/LeagueService.cs`
- **PagedResult** (7 connections) — `NBA.Service/PagedResult.cs`
- **.GetPagedAsync()** (7 connections) — `NBA.Service/League/LeagueService.cs`
- **LeagueService** (5 connections) — `NBA.Service/League/LeagueService.cs`
- **.AddTeam()** (5 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **CreateLeagueInput** (4 connections) — `NBA.Service/League/LeagueService.cs`
- **JoinLeagueResult** (4 connections) — `NBA.Service/League/LeagueService.cs`
- **.AddLeague()** (4 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **JoinLeagueInput** (3 connections) — `NBA.Service/League/LeagueService.cs`
- **StatsValueInput** (3 connections) — `NBA.Service/League/LeagueService.cs`
- **Task** (3 connections)
- **LeagueEndpoints** (2 connections) — `NBA.Api/Endpoints/LeagueEndpoints.cs`
- **PagedResult.cs** (2 connections) — `NBA.Service/PagedResult.cs`
- **IEndpointRouteBuilder** (1 connections)
- **PagedResult** (1 connections)
- **TeamData** (1 connections)
- **IReadOnlyList** (1 connections)
- **TotalPages** (1 connections) — `NBA.Service/PagedResult.cs`

## Relationships

- [EF Core DbContext Model](EF_Core_DbContext_Model.md) (6 shared connections)
- [Team Entity](Team_Entity.md) (4 shared connections)
- [TeamPlayer & Draft Snapshot Persistence](TeamPlayer_&_Draft_Snapshot_Persistence.md) (4 shared connections)
- [Trade & Team Services](Trade_&_Team_Services.md) (4 shared connections)
- [Draft Orchestration & Hub](Draft_Orchestration_&_Hub.md) (3 shared connections)
- [Error Codes & Trade Statuses](Error_Codes_&_Trade_Statuses.md) (3 shared connections)
- [Team Endpoints & Auth Claims](Team_Endpoints_&_Auth_Claims.md) (2 shared connections)
- [Player Endpoints](Player_Endpoints.md) (2 shared connections)
- [League Entity](League_Entity.md) (1 shared connections)
- [Free Agency Service](Free_Agency_Service.md) (1 shared connections)
- [Playoff Bracket Entities](Playoff_Bracket_Entities.md) (1 shared connections)
- [Per-League Stats Values](Per-League_Stats_Values.md) (1 shared connections)

## Source Files

- `NBA.Api/Endpoints/LeagueEndpoints.cs`
- `NBA.Data/Context/NbaFantasyContextExt.cs`
- `NBA.Data/Entities/Draftsnapshot.cs`
- `NBA.Service/League/LeagueService.cs`
- `NBA.Service/PagedResult.cs`

## Audit Trail

- EXTRACTED: 57 (78%)
- INFERRED: 16 (22%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*