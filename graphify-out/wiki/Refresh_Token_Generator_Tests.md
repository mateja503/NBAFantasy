# Refresh Token Generator Tests

> 8 nodes

## Key Concepts

- **.Generate()** (4 connections) — `NBA.Api/Authentication/RefreshTokenGenerator.cs`
- **.Hash_is_deterministic_and_hides_the_token()** (4 connections) — `NBA.Tests/RefreshTokenGeneratorTests.cs`
- **RefreshTokenGenerator** (3 connections) — `NBA.Api/Authentication/RefreshTokenGenerator.cs`
- **RefreshTokenGeneratorTests** (3 connections) — `NBA.Tests/RefreshTokenGeneratorTests.cs`
- **.Generate_produces_unique_url_safe_tokens()** (3 connections) — `NBA.Tests/RefreshTokenGeneratorTests.cs`
- **RefreshTokenGeneratorTests.cs** (3 connections) — `NBA.Tests/RefreshTokenGeneratorTests.cs`
- **RefreshTokenGenerator.cs** (2 connections) — `NBA.Api/Authentication/RefreshTokenGenerator.cs`
- **Fact** (2 connections)

## Relationships

- [Auth Token Issuance](Auth_Token_Issuance.md) (3 shared connections)
- [Team Endpoints & Auth Claims](Team_Endpoints_&_Auth_Claims.md) (2 shared connections)
- [Player Position Extensions](Player_Position_Extensions.md) (1 shared connections)

## Source Files

- `NBA.Api/Authentication/RefreshTokenGenerator.cs`
- `NBA.Tests/RefreshTokenGeneratorTests.cs`

## Audit Trail

- EXTRACTED: 15 (100%)
- INFERRED: 0 (0%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*