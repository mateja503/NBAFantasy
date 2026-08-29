# Player Endpoints

> 8 nodes

## Key Concepts

- **.GetPagedAsync()** (10 connections) — `NBA.Service/Player/PlayerService.cs`
- **.MapPlayerEndpoints()** (4 connections) — `NBA.Api/Endpoints/PlayerEndpoints.cs`
- **PlayerEndpoints** (3 connections) — `NBA.Api/Endpoints/PlayerEndpoints.cs`
- **.ToPlayerSearchInput()** (3 connections) — `NBA.Api/Endpoints/PlayerEndpoints.cs`
- **.ToUtc()** (3 connections) — `NBA.Service/Player/PlayerService.cs`
- **DateTime** (2 connections)
- **IEndpointRouteBuilder** (1 connections)
- **PagedResult** (1 connections)

## Relationships

- [Player Service Search](Player_Service_Search.md) (3 shared connections)
- [BoxScore Calculation Jobs](BoxScore_Calculation_Jobs.md) (3 shared connections)
- [League Service & Endpoints](League_Service_&_Endpoints.md) (2 shared connections)
- [Player Position Extensions](Player_Position_Extensions.md) (1 shared connections)
- [Player Search Request DTOs](Player_Search_Request_DTOs.md) (1 shared connections)
- [Player Position Enum](Player_Position_Enum.md) (1 shared connections)
- [Error Codes & Trade Statuses](Error_Codes_&_Trade_Statuses.md) (1 shared connections)
- [Trade & Team Services](Trade_&_Team_Services.md) (1 shared connections)

## Source Files

- `NBA.Api/Endpoints/PlayerEndpoints.cs`
- `NBA.Service/Player/PlayerService.cs`

## Audit Trail

- EXTRACTED: 17 (85%)
- INFERRED: 3 (15%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*