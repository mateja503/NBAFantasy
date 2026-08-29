# Player Redis Operations

> 61 nodes

## Key Concepts

- **PlayerShort** (20 connections) — `NBA.Data/Redis/Entities/PlayerShort.cs`
- **PlayerRedisOperations** (18 connections) — `NBA.Data/Redis/Operations/PlayerRedisOperations.cs`
- **Task** (13 connections)
- **LeaguePlayers** (10 connections) — `NBA.Data/Redis/Scopes/LeaguePlayers.cs`
- **LeagueScope** (10 connections) — `NBA.Data/Redis/Scopes/LeagueScope.cs`
- **.DeleteLeagueDraftPlayers()** (8 connections) — `NBA.Data/Redis/Operations/PlayerRedisOperations.cs`
- **.AddLeaguesAvailableDraftPlayers()** (7 connections) — `NBA.Data/Redis/Operations/PlayerRedisOperations.cs`
- **.GetAllPlayers()** (7 connections) — `NBA.Data/Redis/Operations/PlayerRedisOperations.cs`
- **.GetLeaguesAvailableDraftPlayers()** (6 connections) — `NBA.Data/Redis/Operations/PlayerRedisOperations.cs`
- **.GetTeamsDraftedPlayers()** (6 connections) — `NBA.Data/Redis/Operations/PlayerRedisOperations.cs`
- **.SetPlayersRange()** (6 connections) — `NBA.Data/Redis/Operations/PlayerRedisOperations.cs`
- **.AddAvailableDraftPlayers()** (6 connections) — `NBA.Data/Redis/Scopes/LeaguePlayers.cs`
- **Task** (6 connections)
- **PlayerShortDtoTests** (5 connections) — `NBA.Tests/PlayerShortDtoTests.cs`
- **.ToPlayerShortDtos()** (5 connections) — `NBA.Data/Redis/Dtos/PlayerShortDto.cs`
- **.GetLeaguesDraftedPlayersKey()** (5 connections) — `NBA.Data/Redis/Keys/RedisKeys.cs`
- **.GetPlayerKey()** (5 connections) — `NBA.Data/Redis/Keys/RedisKeys.cs`
- **.GetTeamsDraftedPlayersKey()** (5 connections) — `NBA.Data/Redis/Keys/RedisKeys.cs`
- **.GetLeaguesDrafterPlayers()** (5 connections) — `NBA.Data/Redis/Operations/PlayerRedisOperations.cs`
- **.SetPlayer()** (5 connections) — `NBA.Data/Redis/Operations/PlayerRedisOperations.cs`
- **.GetAvailableDraftPlayers()** (5 connections) — `NBA.Data/Redis/Scopes/LeaguePlayers.cs`
- **List** (5 connections)
- **.GetLeaguesAvailablePlayersKey()** (4 connections) — `NBA.Data/Redis/Keys/RedisKeys.cs`
- **.AddLeaguesDraftedPlayer()** (4 connections) — `NBA.Data/Redis/Operations/PlayerRedisOperations.cs`
- **.GetPlayer()** (4 connections) — `NBA.Data/Redis/Operations/PlayerRedisOperations.cs`
- *... and 36 more nodes in this community*

## Relationships

- [Draft Redis Operations & Keys](Draft_Redis_Operations_&_Keys.md) (6 shared connections)
- [Redis Operations Integration Tests](Redis_Operations_Integration_Tests.md) (5 shared connections)
- [Redis Adapter Mappings](Redis_Adapter_Mappings.md) (2 shared connections)
- [Redis Facade & Draft Options](Redis_Facade_&_Draft_Options.md) (2 shared connections)
- [End-Draft Integration Tests](End-Draft_Integration_Tests.md) (2 shared connections)
- [Error Codes & Trade Statuses](Error_Codes_&_Trade_Statuses.md) (2 shared connections)
- [Trade Redis & TradeHub Tests](Trade_Redis_&_TradeHub_Tests.md) (1 shared connections)
- [Draft Orchestration & Hub](Draft_Orchestration_&_Hub.md) (1 shared connections)
- [Player Position Extensions](Player_Position_Extensions.md) (1 shared connections)

## Source Files

- `NBA.Data/Redis/Dtos/PlayerShortDto.cs`
- `NBA.Data/Redis/Entities/PlayerShort.cs`
- `NBA.Data/Redis/Keys/RedisKeys.cs`
- `NBA.Data/Redis/Operations/PlayerRedisOperations.cs`
- `NBA.Data/Redis/Scopes/LeaguePlayers.cs`
- `NBA.Data/Redis/Scopes/LeagueScope.cs`
- `NBA.Tests/PlayerShortDtoTests.cs`

## Audit Trail

- EXTRACTED: 131 (94%)
- INFERRED: 9 (6%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*