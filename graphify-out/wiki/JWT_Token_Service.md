# JWT Token Service

> 6 nodes

## Key Concepts

- **.CreateToken()** (6 connections) — `NBA.Api/Authentication/JwtTokenService.cs`
- **.CreateToken()** (5 connections) — `NBA.Api/Authentication/ITokenService.cs`
- **AuthToken** (4 connections) — `NBA.Api/Authentication/ITokenService.cs`
- **ITokenService** (4 connections) — `NBA.Api/Authentication/ITokenService.cs`
- **ITokenService.cs** (4 connections) — `NBA.Api/Authentication/ITokenService.cs`
- **DateTime** (1 connections)

## Relationships

- [JWT Options & Token Tests](JWT_Options_&_Token_Tests.md) (3 shared connections)
- [Auth Token Issuance](Auth_Token_Issuance.md) (2 shared connections)
- [User Auth Persistence](User_Auth_Persistence.md) (2 shared connections)
- [Hangfire Job Expiration Filter](Hangfire_Job_Expiration_Filter.md) (1 shared connections)
- [Error Codes & Trade Statuses](Error_Codes_&_Trade_Statuses.md) (1 shared connections)
- [Team Endpoints & Auth Claims](Team_Endpoints_&_Auth_Claims.md) (1 shared connections)

## Source Files

- `NBA.Api/Authentication/ITokenService.cs`
- `NBA.Api/Authentication/JwtTokenService.cs`

## Audit Trail

- EXTRACTED: 13 (76%)
- INFERRED: 4 (24%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*