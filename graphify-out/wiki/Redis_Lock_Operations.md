# Redis Lock Operations

> 7 nodes

## Key Concepts

- **LockRedisOperations** (5 connections) — `NBA.Data/Redis/Operations/LockRedisOperations.cs`
- **.TryAcquire()** (3 connections) — `NBA.Data/Redis/Operations/LockRedisOperations.cs`
- **.Release()** (2 connections) — `NBA.Data/Redis/Operations/LockRedisOperations.cs`
- **LockRedisOperations.cs** (2 connections) — `NBA.Data/Redis/Operations/LockRedisOperations.cs`
- **Task** (2 connections)
- **IDatabase** (1 connections)
- **TimeSpan** (1 connections)

## Relationships

- [Redis Facade & Draft Options](Redis_Facade_&_Draft_Options.md) (1 shared connections)
- [Redis Operations Integration Tests](Redis_Operations_Integration_Tests.md) (1 shared connections)

## Source Files

- `NBA.Data/Redis/Operations/LockRedisOperations.cs`

## Audit Trail

- EXTRACTED: 9 (100%)
- INFERRED: 0 (0%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*