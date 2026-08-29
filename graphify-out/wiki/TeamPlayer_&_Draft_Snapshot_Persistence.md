# TeamPlayer & Draft Snapshot Persistence

> 28 nodes

## Key Concepts

- **Task** (21 connections)
- **Teamplayer** (19 connections) — `NBA.Data/Entities/Teamplayer.cs`
- **Draftsnapshot** (12 connections) — `NBA.Data/Entities/Draftsnapshot.cs`
- **List** (7 connections)
- **.AddPlayers()** (6 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **.AddTeamPlayerRange()** (6 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **.UpdatePlayersRange()** (6 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **.DeleteTeamPlayerRange()** (5 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **.AddTeamPlayer()** (4 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **.AddTeamRange()** (4 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **.GetDraftSnapshot()** (4 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **.UpdateLeague()** (4 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **.UpsertDraftSnapshot()** (4 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **.DeleteDraftSnapshot()** (3 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **Player** (3 connections)
- **CancellationToken** (2 connections)
- **Draftsnapshot.cs** (2 connections) — `NBA.Data/Entities/Draftsnapshot.cs`
- **Teamplayer.cs** (2 connections) — `NBA.Data/Entities/Teamplayer.cs`
- **DateTime** (1 connections)
- **Draftstate** (1 connections) — `NBA.Data/Entities/Draftsnapshot.cs`
- **Draftteams** (1 connections) — `NBA.Data/Entities/Draftsnapshot.cs`
- **Leagueid** (1 connections) — `NBA.Data/Entities/Draftsnapshot.cs`
- **Tsupdated** (1 connections) — `NBA.Data/Entities/Draftsnapshot.cs`
- **Player** (1 connections) — `NBA.Data/Entities/Teamplayer.cs`
- **Playerid** (1 connections) — `NBA.Data/Entities/Teamplayer.cs`
- *... and 3 more nodes in this community*

## Relationships

- [EF Core DbContext Model](EF_Core_DbContext_Model.md) (15 shared connections)
- [Trade & Team Services](Trade_&_Team_Services.md) (11 shared connections)
- [Draft Orchestration & Hub](Draft_Orchestration_&_Hub.md) (6 shared connections)
- [User Auth Persistence](User_Auth_Persistence.md) (5 shared connections)
- [League Service & Endpoints](League_Service_&_Endpoints.md) (4 shared connections)
- [Error Codes & Trade Statuses](Error_Codes_&_Trade_Statuses.md) (3 shared connections)
- [Team Entity](Team_Entity.md) (2 shared connections)
- [BoxScore Calculation Jobs](BoxScore_Calculation_Jobs.md) (2 shared connections)
- [Free Agency Service](Free_Agency_Service.md) (2 shared connections)
- [League Entity](League_Entity.md) (1 shared connections)
- [Player Memento Stats](Player_Memento_Stats.md) (1 shared connections)

## Source Files

- `NBA.Data/Context/NbaFantasyContextExt.cs`
- `NBA.Data/Entities/Draftsnapshot.cs`
- `NBA.Data/Entities/Teamplayer.cs`

## Audit Trail

- EXTRACTED: 76 (86%)
- INFERRED: 12 (14%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*