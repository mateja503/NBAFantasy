# Hosted Services & Exception Handling

> 36 nodes

## Key Concepts

- **ApplicationHostedService** (11 connections) — `NBA.Api/HostedService/ApplicationHostedService.cs`
- **Log** (10 connections) — `ApplicationDefaults/LogDefaults/Log.cs`
- **.InitializePlayersAsync()** (9 connections) — `NBA.Api/HostedService/ApplicationHostedService.cs`
- **.TryHandleAsync()** (7 connections) — `NBA.Api/Handler/GlobalExceptionHandler.cs`
- **.StartAsync()** (6 connections) — `NBA.Api/HostedService/ApplicationHostedService.cs`
- **HangFireJobSchedulerHostedService** (5 connections) — `NBA.Api/HostedService/HangFireJobSchedulerHostedService.cs`
- **ErrorResponse** (4 connections) — `ApplicationDefaults/Exceptions/ErrorResponse.cs`
- **GlobalExceptionHandler** (4 connections) — `NBA.Api/Handler/GlobalExceptionHandler.cs`
- **.StartAsync()** (4 connections) — `NBA.Api/HostedService/HangFireJobSchedulerHostedService.cs`
- **.StopAsync()** (3 connections) — `NBA.Api/HostedService/ApplicationHostedService.cs`
- **.StopAsync()** (3 connections) — `NBA.Api/HostedService/HangFireJobSchedulerHostedService.cs`
- **CancellationToken** (3 connections)
- **Task** (3 connections)
- **.GetStartupSeedLockKey()** (2 connections) — `NBA.Data/Redis/Keys/RedisKeys.cs`
- **ErrorResponse.cs** (2 connections) — `ApplicationDefaults/Exceptions/ErrorResponse.cs`
- **Log.cs** (2 connections) — `ApplicationDefaults/LogDefaults/Log.cs`
- **IHostedService** (2 connections)
- **CancellationToken** (2 connections)
- **Task** (2 connections)
- **.ToJson()** (1 connections) — `ApplicationDefaults/LogDefaults/Log.cs`
- **ErrorCode** (1 connections) — `ApplicationDefaults/Exceptions/ErrorResponse.cs`
- **ErrorMessage** (1 connections) — `ApplicationDefaults/Exceptions/ErrorResponse.cs`
- **message** (1 connections) — `ApplicationDefaults/LogDefaults/Log.cs`
- **request** (1 connections) — `ApplicationDefaults/LogDefaults/Log.cs`
- **response** (1 connections) — `ApplicationDefaults/LogDefaults/Log.cs`
- *... and 11 more nodes in this community*

## Relationships

- [Hosted Service Namespaces](Hosted_Service_Namespaces.md) (4 shared connections)
- [Trade & Team Services](Trade_&_Team_Services.md) (2 shared connections)
- [WireMock BallDontLie Fixture](WireMock_BallDontLie_Fixture.md) (1 shared connections)
- [Redis Facade & Draft Options](Redis_Facade_&_Draft_Options.md) (1 shared connections)
- [EF Core DbContext Model](EF_Core_DbContext_Model.md) (1 shared connections)
- [Player Redis Manager](Player_Redis_Manager.md) (1 shared connections)
- [BoxScore Calculation Jobs](BoxScore_Calculation_Jobs.md) (1 shared connections)
- [BallDontLie Response Metadata](BallDontLie_Response_Metadata.md) (1 shared connections)
- [Game Service & Endpoints](Game_Service_&_Endpoints.md) (1 shared connections)
- [Draft Redis Operations & Keys](Draft_Redis_Operations_&_Keys.md) (1 shared connections)
- [Error Codes & Trade Statuses](Error_Codes_&_Trade_Statuses.md) (1 shared connections)

## Source Files

- `ApplicationDefaults/Exceptions/ErrorResponse.cs`
- `ApplicationDefaults/LogDefaults/Log.cs`
- `NBA.Api/Handler/GlobalExceptionHandler.cs`
- `NBA.Api/HostedService/ApplicationHostedService.cs`
- `NBA.Api/HostedService/HangFireJobSchedulerHostedService.cs`
- `NBA.Data/Redis/Keys/RedisKeys.cs`

## Audit Trail

- EXTRACTED: 51 (88%)
- INFERRED: 7 (12%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*