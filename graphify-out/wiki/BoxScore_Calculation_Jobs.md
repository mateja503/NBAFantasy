# BoxScore Calculation Jobs

> 14 nodes

## Key Concepts

- **PlayerService** (14 connections) — `NBA.Service/Player/PlayerService.cs`
- **.GetPlayersGameStats()** (10 connections) — `NBA.Service/Player/PlayerService.cs`
- **.PerformCalculations()** (9 connections) — `NBA.Service/CalculateBoxScore/BoxScoreCalculationService.cs`
- **.AddPlayersToDb()** (9 connections) — `NBA.Service/Player/PlayerService.cs`
- **.GetPlayersForTeams()** (6 connections) — `NBA.Service/Player/PlayerService.cs`
- **Task** (5 connections)
- **BoxScoreCalculationService** (4 connections) — `NBA.Service/CalculateBoxScore/BoxScoreCalculationService.cs`
- **CancellationToken** (2 connections)
- **List** (2 connections)
- **AutomaticRetry** (1 connections)
- **JobDisplayName** (1 connections)
- **Dictionary** (1 connections)
- **List** (1 connections)
- **Task** (1 connections)

## Relationships

- [Error Codes & Trade Statuses](Error_Codes_&_Trade_Statuses.md) (3 shared connections)
- [IBallDontLieClient Contract](IBallDontLieClient_Contract.md) (3 shared connections)
- [Player Endpoints](Player_Endpoints.md) (3 shared connections)
- [Trade & Team Services](Trade_&_Team_Services.md) (3 shared connections)
- [EF Core DbContext Model](EF_Core_DbContext_Model.md) (2 shared connections)
- [Player Stats Response](Player_Stats_Response.md) (2 shared connections)
- [TeamPlayer & Draft Snapshot Persistence](TeamPlayer_&_Draft_Snapshot_Persistence.md) (2 shared connections)
- [BallDontLie Response Metadata](BallDontLie_Response_Metadata.md) (2 shared connections)
- [Player Redis Manager](Player_Redis_Manager.md) (1 shared connections)
- [Hosted Services & Exception Handling](Hosted_Services_&_Exception_Handling.md) (1 shared connections)
- [Game Service & Endpoints](Game_Service_&_Endpoints.md) (1 shared connections)
- [External Client Response Tests](External_Client_Response_Tests.md) (1 shared connections)

## Source Files

- `NBA.Service/CalculateBoxScore/BoxScoreCalculationService.cs`
- `NBA.Service/Player/PlayerService.cs`

## Audit Trail

- EXTRACTED: 40 (85%)
- INFERRED: 7 (15%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*