# Game Redis Operations

> 19 nodes

## Key Concepts

- **ScheduledGames** (13 connections) — `NBA.Data/Redis/Entities/GameShort.cs`
- **GameRedisOperations** (6 connections) — `NBA.Data/Redis/Operations/GameRedisOperations.cs`
- **GameManager** (6 connections) — `NBA.Service/Game/GameManager.cs`
- **.SetScheduledGames()** (5 connections) — `NBA.Data/Redis/Operations/GameRedisOperations.cs`
- **.GetScheduledGames()** (5 connections) — `NBA.Service/Game/GameManager.cs`
- **.SetScheduledGames()** (5 connections) — `NBA.Service/Game/GameManager.cs`
- **.GetScheduledGames()** (4 connections) — `NBA.Data/Redis/Operations/GameRedisOperations.cs`
- **.GetScheduledGamesKey()** (3 connections) — `NBA.Data/Redis/Keys/RedisKeys.cs`
- **Task** (2 connections)
- **DateOnly** (2 connections)
- **Task** (2 connections)
- **List** (1 connections)
- **RestOfWeek** (1 connections) — `NBA.Data/Redis/Entities/GameShort.cs`
- **Today** (1 connections) — `NBA.Data/Redis/Entities/GameShort.cs`
- **Tomorrow** (1 connections) — `NBA.Data/Redis/Entities/GameShort.cs`
- **IDatabase** (1 connections)
- **JsonSerializerOptions** (1 connections)
- **TimeSpan** (1 connections)
- **TimeSpan** (1 connections)

## Relationships

- [Game Service & Endpoints](Game_Service_&_Endpoints.md) (4 shared connections)
- [Game Redis Shapes](Game_Redis_Shapes.md) (2 shared connections)
- [Redis Facade & Draft Options](Redis_Facade_&_Draft_Options.md) (2 shared connections)
- [Game Schedule Bucketing Tests](Game_Schedule_Bucketing_Tests.md) (1 shared connections)
- [Entity to DTO Mappers](Entity_to_DTO_Mappers.md) (1 shared connections)
- [Redis Operations Integration Tests](Redis_Operations_Integration_Tests.md) (1 shared connections)
- [External Client Response Tests](External_Client_Response_Tests.md) (1 shared connections)
- [Draft Redis Operations & Keys](Draft_Redis_Operations_&_Keys.md) (1 shared connections)

## Source Files

- `NBA.Data/Redis/Entities/GameShort.cs`
- `NBA.Data/Redis/Keys/RedisKeys.cs`
- `NBA.Data/Redis/Operations/GameRedisOperations.cs`
- `NBA.Service/Game/GameManager.cs`

## Audit Trail

- EXTRACTED: 35 (95%)
- INFERRED: 2 (5%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*