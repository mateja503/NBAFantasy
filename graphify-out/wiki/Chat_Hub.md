# Chat Hub

> 7 nodes

## Key Concepts

- **ChatHub** (4 connections) — `NBA.Api/SignalR/Hubs/ChatHub.cs`
- **IChatHubClient** (3 connections) — `NBA.Api/SignalR/Clients/IChatHubClient.cs`
- **.ReceiveMessage()** (2 connections) — `NBA.Api/SignalR/Clients/IChatHubClient.cs`
- **.SendMessage()** (2 connections) — `NBA.Api/SignalR/Hubs/ChatHub.cs`
- **IChatHubClient.cs** (2 connections) — `NBA.Api/SignalR/Clients/IChatHubClient.cs`
- **Task** (1 connections)
- **Task** (1 connections)

## Relationships

- [Draft Endpoints Integration Tests](Draft_Endpoints_Integration_Tests.md) (2 shared connections)
- [Redis Facade & Draft Options](Redis_Facade_&_Draft_Options.md) (1 shared connections)

## Source Files

- `NBA.Api/SignalR/Clients/IChatHubClient.cs`
- `NBA.Api/SignalR/Hubs/ChatHub.cs`

## Audit Trail

- EXTRACTED: 9 (100%)
- INFERRED: 0 (0%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*