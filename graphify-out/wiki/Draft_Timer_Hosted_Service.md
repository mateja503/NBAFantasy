# Draft Timer Hosted Service

> 9 nodes

## Key Concepts

- **DraftTimerHostedService** (8 connections) — `NBA.Api/HostedService/DraftTimerHostedService.cs`
- **.AdvanceOneAsync()** (5 connections) — `NBA.Api/HostedService/DraftTimerHostedService.cs`
- **.ExecuteAsync()** (4 connections) — `NBA.Api/HostedService/DraftTimerHostedService.cs`
- **CancellationToken** (2 connections)
- **Task** (2 connections)
- **BackgroundService** (1 connections)
- **ILogger** (1 connections)
- **IServiceProvider** (1 connections)
- **TimeSpan** (1 connections)

## Relationships

- [Redis Facade & Draft Options](Redis_Facade_&_Draft_Options.md) (2 shared connections)
- [Hosted Service Namespaces](Hosted_Service_Namespaces.md) (1 shared connections)

## Source Files

- `NBA.Api/HostedService/DraftTimerHostedService.cs`

## Audit Trail

- EXTRACTED: 14 (100%)
- INFERRED: 0 (0%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*