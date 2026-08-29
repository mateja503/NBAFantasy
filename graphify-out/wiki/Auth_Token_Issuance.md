# Auth Token Issuance

> 12 nodes

## Key Concepts

- **AuthTokenIssuer** (9 connections) — `NBA.Api/Authentication/AuthTokenIssuer.cs`
- **.IssueAsync()** (9 connections) — `NBA.Api/Authentication/AuthTokenIssuer.cs`
- **.RefreshAsync()** (8 connections) — `NBA.Api/Authentication/AuthTokenIssuer.cs`
- **.MapAuthenticationEndpoints()** (8 connections) — `NBA.Api/Endpoints/AuthenticationEndpoints.cs`
- **.Hash()** (5 connections) — `NBA.Api/Authentication/RefreshTokenGenerator.cs`
- **TokenPair** (4 connections) — `NBA.Api/Authentication/AuthTokenIssuer.cs`
- **.RevokeAsync()** (4 connections) — `NBA.Api/Authentication/AuthTokenIssuer.cs`
- **Task** (3 connections)
- **AuthenticationEndpoints** (2 connections) — `NBA.Api/Endpoints/AuthenticationEndpoints.cs`
- **DateTime** (1 connections)
- **IOptions** (1 connections)
- **IEndpointRouteBuilder** (1 connections)

## Relationships

- [User Auth Persistence](User_Auth_Persistence.md) (4 shared connections)
- [Refresh Token Generator Tests](Refresh_Token_Generator_Tests.md) (3 shared connections)
- [JWT Token Service](JWT_Token_Service.md) (2 shared connections)
- [Error Codes & Trade Statuses](Error_Codes_&_Trade_Statuses.md) (2 shared connections)
- [JWT Options & Token Tests](JWT_Options_&_Token_Tests.md) (1 shared connections)
- [EF Core DbContext Model](EF_Core_DbContext_Model.md) (1 shared connections)
- [Redis Facade & Draft Options](Redis_Facade_&_Draft_Options.md) (1 shared connections)
- [Team Endpoints & Auth Claims](Team_Endpoints_&_Auth_Claims.md) (1 shared connections)
- [Trade & Team Services](Trade_&_Team_Services.md) (1 shared connections)
- [Login DTO](Login_DTO.md) (1 shared connections)

## Source Files

- `NBA.Api/Authentication/AuthTokenIssuer.cs`
- `NBA.Api/Authentication/RefreshTokenGenerator.cs`
- `NBA.Api/Endpoints/AuthenticationEndpoints.cs`

## Audit Trail

- EXTRACTED: 27 (75%)
- INFERRED: 9 (25%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*