# JWT Options & Token Tests

> 16 nodes

## Key Concepts

- **JwtOptions** (10 connections) — `ApplicationDefaults/Options/JwtOptions.cs`
- **JwtTokenService** (7 connections) — `NBA.Api/Authentication/JwtTokenService.cs`
- **.CreateToken_throws_when_signing_key_missing()** (7 connections) — `NBA.Tests/JwtTokenServiceTests.cs`
- **JwtTokenServiceTests** (6 connections) — `NBA.Tests/JwtTokenServiceTests.cs`
- **.CreateToken_issues_a_token_validatable_with_the_same_parameters()** (5 connections) — `NBA.Tests/JwtTokenServiceTests.cs`
- **.CreateSut()** (4 connections) — `NBA.Tests/JwtTokenServiceTests.cs`
- **.CreateToken_puts_user_id_and_username_in_claims()** (4 connections) — `NBA.Tests/JwtTokenServiceTests.cs`
- **Fact** (3 connections)
- **JwtOptions.cs** (2 connections) — `ApplicationDefaults/Options/JwtOptions.cs`
- **AccessTokenMinutes** (1 connections) — `ApplicationDefaults/Options/JwtOptions.cs`
- **Audience** (1 connections) — `ApplicationDefaults/Options/JwtOptions.cs`
- **Issuer** (1 connections) — `ApplicationDefaults/Options/JwtOptions.cs`
- **RefreshTokenDays** (1 connections) — `ApplicationDefaults/Options/JwtOptions.cs`
- **SigningKey** (1 connections) — `ApplicationDefaults/Options/JwtOptions.cs`
- **IOptions** (1 connections)
- **Task** (1 connections)

## Relationships

- [JWT Token Service](JWT_Token_Service.md) (3 shared connections)
- [User Auth Persistence](User_Auth_Persistence.md) (3 shared connections)
- [Team Endpoints & Auth Claims](Team_Endpoints_&_Auth_Claims.md) (2 shared connections)
- [Auth Token Issuance](Auth_Token_Issuance.md) (1 shared connections)
- [Hangfire Job Expiration Filter](Hangfire_Job_Expiration_Filter.md) (1 shared connections)
- [Draft Endpoints Integration Tests](Draft_Endpoints_Integration_Tests.md) (1 shared connections)

## Source Files

- `ApplicationDefaults/Options/JwtOptions.cs`
- `NBA.Api/Authentication/JwtTokenService.cs`
- `NBA.Tests/JwtTokenServiceTests.cs`

## Audit Trail

- EXTRACTED: 29 (88%)
- INFERRED: 4 (12%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*