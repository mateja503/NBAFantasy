# Draft Endpoints Integration Tests

> 21 nodes

## Key Concepts

- **ApplicationDefaults.Options** (25 connections) — `ApplicationDefaults/Options/ApplicationOptions.cs`
- **Program.cs** (22 connections) — `NBA.Api/Program.cs`
- **NBA.Service.Draft** (14 connections) — `NBA.Service/Draft/DraftManager.cs`
- **TradeHubFixture.cs** (13 connections) — `NBA.Tests/Integration/TradeHubFixture.cs`
- **TradeHub.cs** (12 connections) — `NBA.Api/SignalR/Hubs/TradeHub.cs`
- **DraftEndpoints.cs** (11 connections) — `NBA.Api/Endpoints/DraftEndpoints.cs`
- **TradeManager.cs** (10 connections) — `NBA.Service/Trade/TradeManager.cs`
- **DraftEndDraftTests.cs** (10 connections) — `NBA.Tests/Integration/DraftEndDraftTests.cs`
- **DraftHub.cs** (9 connections) — `NBA.Api/SignalR/Hubs/DraftHub.cs`
- **NBA.Api.SignalR.Clients** (8 connections) — `NBA.Api/SignalR/Clients/IChatHubClient.cs`
- **DraftTimerProcessor.cs** (8 connections) — `NBA.Api/Draft/DraftTimerProcessor.cs`
- **DraftManager.cs** (8 connections) — `NBA.Service/Draft/DraftManager.cs`
- **NBA.Data.Redis.Enumerations** (7 connections) — `NBA.Data/Redis/Enumerations/DraftStatus.cs`
- **NBA.Api.SignalR.Hubs** (7 connections) — `NBA.Api/SignalR/Hubs/ChatHub.cs`
- **NBA.Service.Roster** (7 connections) — `NBA.Service/Roster/RosterValidator.cs`
- **NBA.Service.Trade** (6 connections) — `NBA.Service/Trade/TradeManager.cs`
- **NBA.Api.Draft** (4 connections) — `NBA.Api/Draft/DraftTimerProcessor.cs`
- **IDraftHubClient.cs** (4 connections) — `NBA.Api/SignalR/Clients/IDraftHubClient.cs`
- **RosterValidator.cs** (4 connections) — `NBA.Service/Roster/RosterValidator.cs`
- **ChatHub.cs** (3 connections) — `NBA.Api/SignalR/Hubs/ChatHub.cs`
- **DraftEndpoints** (2 connections) — `NBA.Api/Endpoints/DraftEndpoints.cs`

## Relationships

- [Error Codes & Trade Statuses](Error_Codes_&_Trade_Statuses.md) (27 shared connections)
- [Redis Operations Integration Tests](Redis_Operations_Integration_Tests.md) (14 shared connections)
- [Team Endpoints & Auth Claims](Team_Endpoints_&_Auth_Claims.md) (13 shared connections)
- [External Client Response Tests](External_Client_Response_Tests.md) (10 shared connections)
- [TradeHub Test Fixture](TradeHub_Test_Fixture.md) (4 shared connections)
- [Redis Facade & Draft Options](Redis_Facade_&_Draft_Options.md) (4 shared connections)
- [Hosted Service Namespaces](Hosted_Service_Namespaces.md) (3 shared connections)
- [Draft Orchestration & Hub](Draft_Orchestration_&_Hub.md) (2 shared connections)
- [Chat Hub](Chat_Hub.md) (2 shared connections)
- [Trade & Team Services](Trade_&_Team_Services.md) (2 shared connections)
- [TradeHub Real-Time Trading](TradeHub_Real-Time_Trading.md) (2 shared connections)
- [Player Position Extensions](Player_Position_Extensions.md) (2 shared connections)

## Source Files

- `ApplicationDefaults/Options/ApplicationOptions.cs`
- `NBA.Api/Draft/DraftTimerProcessor.cs`
- `NBA.Api/Endpoints/DraftEndpoints.cs`
- `NBA.Api/Program.cs`
- `NBA.Api/SignalR/Clients/IChatHubClient.cs`
- `NBA.Api/SignalR/Clients/IDraftHubClient.cs`
- `NBA.Api/SignalR/Hubs/ChatHub.cs`
- `NBA.Api/SignalR/Hubs/DraftHub.cs`
- `NBA.Api/SignalR/Hubs/TradeHub.cs`
- `NBA.Data/Redis/Enumerations/DraftStatus.cs`
- `NBA.Service/Draft/DraftManager.cs`
- `NBA.Service/Roster/RosterValidator.cs`
- `NBA.Service/Trade/TradeManager.cs`
- `NBA.Tests/Integration/DraftEndDraftTests.cs`
- `NBA.Tests/Integration/TradeHubFixture.cs`

## Audit Trail

- EXTRACTED: 145 (100%)
- INFERRED: 0 (0%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*