# Hosted Service Namespaces

> 6 nodes

## Key Concepts

- **ApplicationHostedService.cs** (8 connections) — `NBA.Api/HostedService/ApplicationHostedService.cs`
- **NBA.Api.HostedService** (4 connections) — `NBA.Api/HostedService/ApplicationHostedService.cs`
- **ApplicationDefaults.LogDefaults** (4 connections) — `ApplicationDefaults/LogDefaults/Log.cs`
- **DraftTimerHostedService.cs** (4 connections) — `NBA.Api/HostedService/DraftTimerHostedService.cs`
- **GlobalExceptionHandler.cs** (3 connections) — `NBA.Api/Handler/GlobalExceptionHandler.cs`
- **HangFireJobSchedulerHostedService.cs** (3 connections) — `NBA.Api/HostedService/HangFireJobSchedulerHostedService.cs`

## Relationships

- [Error Codes & Trade Statuses](Error_Codes_&_Trade_Statuses.md) (4 shared connections)
- [Hosted Services & Exception Handling](Hosted_Services_&_Exception_Handling.md) (4 shared connections)
- [Draft Endpoints Integration Tests](Draft_Endpoints_Integration_Tests.md) (3 shared connections)
- [External Client Response Tests](External_Client_Response_Tests.md) (3 shared connections)
- [Redis Operations Integration Tests](Redis_Operations_Integration_Tests.md) (1 shared connections)
- [Draft Timer Hosted Service](Draft_Timer_Hosted_Service.md) (1 shared connections)

## Source Files

- `ApplicationDefaults/LogDefaults/Log.cs`
- `NBA.Api/Handler/GlobalExceptionHandler.cs`
- `NBA.Api/HostedService/ApplicationHostedService.cs`
- `NBA.Api/HostedService/DraftTimerHostedService.cs`
- `NBA.Api/HostedService/HangFireJobSchedulerHostedService.cs`

## Audit Trail

- EXTRACTED: 21 (100%)
- INFERRED: 0 (0%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*