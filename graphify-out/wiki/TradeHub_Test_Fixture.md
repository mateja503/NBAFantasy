# TradeHub Test Fixture

> 29 nodes

## Key Concepts

- **TradeHubFixture** (19 connections) — `NBA.Tests/Integration/TradeHubFixture.cs`
- **.InitializeAsync()** (18 connections) — `NBA.Tests/Integration/TradeHubFixture.cs`
- **ApplicationOptions** (10 connections) — `ApplicationDefaults/Options/ApplicationOptions.cs`
- **TestAuthHandler** (5 connections) — `NBA.Tests/Integration/TradeHubFixture.cs`
- **.HandleAuthenticateAsync()** (4 connections) — `NBA.Tests/Integration/TradeHubFixture.cs`
- **.SeedLeaguesAsync()** (4 connections) — `NBA.Tests/Integration/TradeHubFixture.cs`
- **Task** (4 connections)
- **TradeIntegrationCollection** (3 connections) — `NBA.Tests/Integration/TradeHubFixture.cs`
- **.GetUserId()** (3 connections) — `NBA.Api/Authentication/ClaimsPrincipalExtensions.cs`
- **.DisposeAsync()** (2 connections) — `NBA.Tests/Integration/TradeHubFixture.cs`
- **ApplicationOptions.cs** (2 connections) — `ApplicationDefaults/Options/ApplicationOptions.cs`
- **AuthenticationSchemeOptions** (2 connections)
- **ClaimsPrincipal** (2 connections)
- **CenterLimit** (1 connections) — `ApplicationDefaults/Options/ApplicationOptions.cs`
- **MaxPlayersPerTeam** (1 connections) — `ApplicationDefaults/Options/ApplicationOptions.cs`
- **ProposedTradeTtlMinutes** (1 connections) — `ApplicationDefaults/Options/ApplicationOptions.cs`
- **AuthenticateResult** (1 connections)
- **AuthenticationHandler** (1 connections)
- **ICollectionFixture** (1 connections)
- **IConnectionMultiplexer** (1 connections)
- **IHost** (1 connections)
- **IDatabase** (1 connections)
- **IOptions** (1 connections)
- **JsonOptions** (1 connections)
- **Database** (1 connections) — `NBA.Tests/Integration/TradeHubFixture.cs`
- *... and 4 more nodes in this community*

## Relationships

- [Redis Facade & Draft Options](Redis_Facade_&_Draft_Options.md) (5 shared connections)
- [TradeHub Real-Time Trading](TradeHub_Real-Time_Trading.md) (4 shared connections)
- [Trade & Team Services](Trade_&_Team_Services.md) (4 shared connections)
- [Draft Endpoints Integration Tests](Draft_Endpoints_Integration_Tests.md) (4 shared connections)
- [Trade Redis & TradeHub Tests](Trade_Redis_&_TradeHub_Tests.md) (3 shared connections)
- [End-Draft Integration Tests](End-Draft_Integration_Tests.md) (2 shared connections)
- [EF Core DbContext Model](EF_Core_DbContext_Model.md) (2 shared connections)
- [WireMock BallDontLie Fixture](WireMock_BallDontLie_Fixture.md) (1 shared connections)
- [Team Endpoints & Auth Claims](Team_Endpoints_&_Auth_Claims.md) (1 shared connections)
- [Player Redis Manager](Player_Redis_Manager.md) (1 shared connections)
- [Draft Orchestration & Hub](Draft_Orchestration_&_Hub.md) (1 shared connections)

## Source Files

- `ApplicationDefaults/Options/ApplicationOptions.cs`
- `NBA.Api/Authentication/ClaimsPrincipalExtensions.cs`
- `NBA.Tests/Integration/TradeHubFixture.cs`

## Audit Trail

- EXTRACTED: 56 (92%)
- INFERRED: 5 (8%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*