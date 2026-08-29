# Hangfire Job Expiration Filter

> 18 nodes

## Key Concepts

- **ShortenJobExpirationFilter** (6 connections) — `NBA.Api/HangFire/ShortenJobExpirationFilter.cs`
- **.RegisterHangFire()** (5 connections) — `NBA.Api/Extentions.cs`
- **Extentions** (3 connections) — `NBA.Api/Extentions.cs`
- **.CreateResiliencePipeline()** (3 connections) — `NBA.Api/Extentions.cs`
- **.OnStateApplied()** (3 connections) — `NBA.Api/HangFire/ShortenJobExpirationFilter.cs`
- **.OnStateUnapplied()** (3 connections) — `NBA.Api/HangFire/ShortenJobExpirationFilter.cs`
- **InvalidOperationException** (3 connections)
- **Extentions.cs** (3 connections) — `NBA.Api/Extentions.cs`
- **ApplyStateContext** (2 connections)
- **NBA.Api.HangFire** (2 connections) — `NBA.Api/HangFire/ShortenJobExpirationFilter.cs`
- **NBA.Api** (2 connections) — `NBA.Api/Extentions.cs`
- **IServiceCollection** (2 connections)
- **IWriteOnlyTransaction** (2 connections)
- **ShortenJobExpirationFilter.cs** (2 connections) — `NBA.Api/HangFire/ShortenJobExpirationFilter.cs`
- **IApplyStateFilter** (1 connections)
- **IConfiguration** (1 connections)
- **JobFilterAttribute** (1 connections)
- **HttpResponseMessage** (1 connections)

## Relationships

- [Draft Endpoints Integration Tests](Draft_Endpoints_Integration_Tests.md) (1 shared connections)
- [JWT Options & Token Tests](JWT_Options_&_Token_Tests.md) (1 shared connections)
- [JWT Token Service](JWT_Token_Service.md) (1 shared connections)

## Source Files

- `NBA.Api/Extentions.cs`
- `NBA.Api/HangFire/ShortenJobExpirationFilter.cs`

## Audit Trail

- EXTRACTED: 21 (88%)
- INFERRED: 3 (12%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*