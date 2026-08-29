# Team Endpoints & Auth Claims

> 24 nodes

## Key Concepts

- **NBA.Api.Authentication** (12 connections) — `NBA.Api/Authentication/AuthTokenIssuer.cs`
- **NBA.Api.Mappings** (9 connections) — `NBA.Api/Mappings/EntityMappings.cs`
- **NBA.Api.Endpoints** (9 connections) — `NBA.Api/Endpoints/AuthenticationEndpoints.cs`
- **LeagueEndpoints.cs** (9 connections) — `NBA.Api/Endpoints/LeagueEndpoints.cs`
- **AuthenticationEndpoints.cs** (7 connections) — `NBA.Api/Endpoints/AuthenticationEndpoints.cs`
- **TeamEndpoints.cs** (7 connections) — `NBA.Api/Endpoints/TeamEndpoints.cs`
- **EntityMappings.cs** (6 connections) — `NBA.Api/Mappings/EntityMappings.cs`
- **NBA.Service.Authentication** (5 connections) — `NBA.Service/Authentication/Argon2idPasswordHasher.cs`
- **TestingEndpoints.cs** (5 connections) — `NBA.Api/Endpoints/TestingEndpoints.cs`
- **TradeEndpoints.cs** (5 connections) — `NBA.Api/Endpoints/TradeEndpoints.cs`
- **Argon2idPasswordHasherTests.cs** (5 connections) — `NBA.Tests/Argon2idPasswordHasherTests.cs`
- **JwtTokenServiceTests.cs** (5 connections) — `NBA.Tests/JwtTokenServiceTests.cs`
- **JwtTokenService.cs** (4 connections) — `NBA.Api/Authentication/JwtTokenService.cs`
- **GameEndpoints.cs** (4 connections) — `NBA.Api/Endpoints/GameEndpoints.cs`
- **Argon2idPasswordHasher.cs** (4 connections) — `NBA.Service/Authentication/Argon2idPasswordHasher.cs`
- **NBA.Service.League** (3 connections) — `NBA.Service/League/LeagueService.cs`
- **NBA.Service.Team** (3 connections) — `NBA.Service/Team/TeamService.cs`
- **ClaimsPrincipalExtensions.cs** (3 connections) — `NBA.Api/Authentication/ClaimsPrincipalExtensions.cs`
- **ClaimsPrincipalExtensions** (2 connections) — `NBA.Api/Authentication/ClaimsPrincipalExtensions.cs`
- **TeamEndpoints** (2 connections) — `NBA.Api/Endpoints/TeamEndpoints.cs`
- **TeamRequest** (2 connections) — `NBA.Api/Requests/Team/TeamRequest.cs`
- **NBA.Api.Requests.Team** (2 connections) — `NBA.Api/Requests/Team/TeamRequest.cs`
- **TeamRequest.cs** (2 connections) — `NBA.Api/Requests/Team/TeamRequest.cs`
- **teamName** (1 connections) — `NBA.Api/Requests/Team/TeamRequest.cs`

## Relationships

- [Draft Endpoints Integration Tests](Draft_Endpoints_Integration_Tests.md) (13 shared connections)
- [Error Codes & Trade Statuses](Error_Codes_&_Trade_Statuses.md) (11 shared connections)
- [Player Position Extensions](Player_Position_Extensions.md) (7 shared connections)
- [Draft Order DTO](Draft_Order_DTO.md) (3 shared connections)
- [Trade & Team Services](Trade_&_Team_Services.md) (2 shared connections)
- [League Service & Endpoints](League_Service_&_Endpoints.md) (2 shared connections)
- [Refresh Token Generator Tests](Refresh_Token_Generator_Tests.md) (2 shared connections)
- [JWT Options & Token Tests](JWT_Options_&_Token_Tests.md) (2 shared connections)
- [External Client Response Tests](External_Client_Response_Tests.md) (2 shared connections)
- [TradeHub Test Fixture](TradeHub_Test_Fixture.md) (1 shared connections)
- [JWT Token Service](JWT_Token_Service.md) (1 shared connections)
- [Auth Request DTOs](Auth_Request_DTOs.md) (1 shared connections)

## Source Files

- `NBA.Api/Authentication/AuthTokenIssuer.cs`
- `NBA.Api/Authentication/ClaimsPrincipalExtensions.cs`
- `NBA.Api/Authentication/JwtTokenService.cs`
- `NBA.Api/Endpoints/AuthenticationEndpoints.cs`
- `NBA.Api/Endpoints/GameEndpoints.cs`
- `NBA.Api/Endpoints/LeagueEndpoints.cs`
- `NBA.Api/Endpoints/TeamEndpoints.cs`
- `NBA.Api/Endpoints/TestingEndpoints.cs`
- `NBA.Api/Endpoints/TradeEndpoints.cs`
- `NBA.Api/Mappings/EntityMappings.cs`
- `NBA.Api/Requests/Team/TeamRequest.cs`
- `NBA.Service/Authentication/Argon2idPasswordHasher.cs`
- `NBA.Service/League/LeagueService.cs`
- `NBA.Service/Team/TeamService.cs`
- `NBA.Tests/Argon2idPasswordHasherTests.cs`
- `NBA.Tests/JwtTokenServiceTests.cs`

## Audit Trail

- EXTRACTED: 86 (100%)
- INFERRED: 0 (0%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*