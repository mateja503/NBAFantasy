# Free Agency Service

> 13 nodes

## Key Concepts

- **Leagueplayer** (14 connections) — `NBA.Data/Entities/Leagueplayer.cs`
- **.ToggleFreeAgencyStatus()** (6 connections) — `NBA.Service/FreeAgency/FreeAgencyService.cs`
- **.UpdatLeaguePlayersRange()** (5 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **.GetAllLeaguePlayers()** (4 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **FreeAgencyService** (3 connections) — `NBA.Service/FreeAgency/FreeAgencyService.cs`
- **Leagueplayer.cs** (2 connections) — `NBA.Data/Entities/Leagueplayer.cs`
- **Isfreeagent** (1 connections) — `NBA.Data/Entities/Leagueplayer.cs`
- **League** (1 connections) — `NBA.Data/Entities/Leagueplayer.cs`
- **Leagueid** (1 connections) — `NBA.Data/Entities/Leagueplayer.cs`
- **Leagueplayerid** (1 connections) — `NBA.Data/Entities/Leagueplayer.cs`
- **Playerid** (1 connections) — `NBA.Data/Entities/Leagueplayer.cs`
- **List** (1 connections)
- **Task** (1 connections)

## Relationships

- [EF Core DbContext Model](EF_Core_DbContext_Model.md) (5 shared connections)
- [Error Codes & Trade Statuses](Error_Codes_&_Trade_Statuses.md) (4 shared connections)
- [TeamPlayer & Draft Snapshot Persistence](TeamPlayer_&_Draft_Snapshot_Persistence.md) (2 shared connections)
- [League Service & Endpoints](League_Service_&_Endpoints.md) (1 shared connections)
- [League Entity](League_Entity.md) (1 shared connections)
- [Player Memento Stats](Player_Memento_Stats.md) (1 shared connections)
- [Trade & Team Services](Trade_&_Team_Services.md) (1 shared connections)

## Source Files

- `NBA.Data/Context/NbaFantasyContextExt.cs`
- `NBA.Data/Entities/Leagueplayer.cs`
- `NBA.Service/FreeAgency/FreeAgencyService.cs`

## Audit Trail

- EXTRACTED: 26 (93%)
- INFERRED: 2 (7%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*