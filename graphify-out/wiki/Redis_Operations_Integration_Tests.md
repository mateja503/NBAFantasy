# Redis Operations Integration Tests

> 20 nodes

## Key Concepts

- **NBA.Data.Redis.Entities** (34 connections) — `NBA.Data/Redis/Entities/DraftBoardTeams.cs`
- **NBA.Data.Redis.Operations** (10 connections) — `NBA.Data/Redis/Operations/AuthRedisOperations.cs`
- **NBA.Data.Redis.Dtos** (10 connections) — `NBA.Data/Redis/Dtos/PlayerShortDto.cs`
- **NBA.Data.Redis.Keys** (9 connections) — `NBA.Data/Redis/Keys/RedisKeys.cs`
- **NBA.Tests.Integration** (6 connections) — `NBA.Tests/Integration/BallDontLieClientWireMockTests.cs`
- **DraftRedisOperations.cs** (6 connections) — `NBA.Data/Redis/Operations/DraftRedisOperations.cs`
- **PlayerRedisOperations.cs** (6 connections) — `NBA.Data/Redis/Operations/PlayerRedisOperations.cs`
- **TradeHubTests.cs** (6 connections) — `NBA.Tests/Integration/TradeHubTests.cs`
- **NBA.Data.Redis.Scopes** (5 connections) — `NBA.Data/Redis/Scopes/LeagueDraft.cs`
- **NbaFantasyRedis.cs** (5 connections) — `NBA.Data/Context/NbaFantasyRedis.cs`
- **PlayerShortDto.cs** (5 connections) — `NBA.Data/Redis/Dtos/PlayerShortDto.cs`
- **LeagueDraft.cs** (5 connections) — `NBA.Data/Redis/Scopes/LeagueDraft.cs`
- **DraftState.cs** (4 connections) — `NBA.Data/Redis/Entities/DraftState.cs`
- **GameRedisOperations.cs** (4 connections) — `NBA.Data/Redis/Operations/GameRedisOperations.cs`
- **TradeRedisOperations.cs** (4 connections) — `NBA.Data/Redis/Operations/TradeRedisOperations.cs`
- **LeaguePlayers.cs** (4 connections) — `NBA.Data/Redis/Scopes/LeaguePlayers.cs`
- **LeagueTrades.cs** (4 connections) — `NBA.Data/Redis/Scopes/LeagueTrades.cs`
- **BallDontLieClientWireMockTests.cs** (4 connections) — `NBA.Tests/Integration/BallDontLieClientWireMockTests.cs`
- **BallDontLieWireMockFixture.cs** (4 connections) — `NBA.Tests/Integration/BallDontLieWireMockFixture.cs`
- **TradeRedisWriteTests.cs** (4 connections) — `NBA.Tests/Integration/TradeRedisWriteTests.cs`

## Relationships

- [Draft Endpoints Integration Tests](Draft_Endpoints_Integration_Tests.md) (14 shared connections)
- [External Client Response Tests](External_Client_Response_Tests.md) (8 shared connections)
- [Error Codes & Trade Statuses](Error_Codes_&_Trade_Statuses.md) (7 shared connections)
- [Draft Redis Operations & Keys](Draft_Redis_Operations_&_Keys.md) (6 shared connections)
- [Trade Redis & TradeHub Tests](Trade_Redis_&_TradeHub_Tests.md) (6 shared connections)
- [Player Position Extensions](Player_Position_Extensions.md) (6 shared connections)
- [Player Redis Operations](Player_Redis_Operations.md) (5 shared connections)
- [End-Draft Integration Tests](End-Draft_Integration_Tests.md) (2 shared connections)
- [Redis Lock Operations](Redis_Lock_Operations.md) (1 shared connections)
- [Hosted Service Namespaces](Hosted_Service_Namespaces.md) (1 shared connections)
- [Team Endpoints & Auth Claims](Team_Endpoints_&_Auth_Claims.md) (1 shared connections)
- [Game Redis Shapes](Game_Redis_Shapes.md) (1 shared connections)

## Source Files

- `NBA.Data/Context/NbaFantasyRedis.cs`
- `NBA.Data/Redis/Dtos/PlayerShortDto.cs`
- `NBA.Data/Redis/Entities/DraftBoardTeams.cs`
- `NBA.Data/Redis/Entities/DraftState.cs`
- `NBA.Data/Redis/Keys/RedisKeys.cs`
- `NBA.Data/Redis/Operations/AuthRedisOperations.cs`
- `NBA.Data/Redis/Operations/DraftRedisOperations.cs`
- `NBA.Data/Redis/Operations/GameRedisOperations.cs`
- `NBA.Data/Redis/Operations/PlayerRedisOperations.cs`
- `NBA.Data/Redis/Operations/TradeRedisOperations.cs`
- `NBA.Data/Redis/Scopes/LeagueDraft.cs`
- `NBA.Data/Redis/Scopes/LeaguePlayers.cs`
- `NBA.Data/Redis/Scopes/LeagueTrades.cs`
- `NBA.Tests/Integration/BallDontLieClientWireMockTests.cs`
- `NBA.Tests/Integration/BallDontLieWireMockFixture.cs`
- `NBA.Tests/Integration/TradeHubTests.cs`
- `NBA.Tests/Integration/TradeRedisWriteTests.cs`

## Audit Trail

- EXTRACTED: 101 (100%)
- INFERRED: 0 (0%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*