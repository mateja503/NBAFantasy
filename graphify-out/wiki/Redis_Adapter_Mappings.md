# Redis Adapter Mappings

> 7 nodes

## Key Concepts

- **.ToGameRedis()** (9 connections) — `NBA.Service/Adapter.cs`
- **Adapter** (7 connections) — `NBA.Service/Adapter.cs`
- **.ToPlayerRedis()** (7 connections) — `NBA.Service/Adapter.cs`
- **.ToPlayerRedisFromDB()** (6 connections) — `NBA.Service/Adapter.cs`
- **.ToGameTeamRedis()** (4 connections) — `NBA.Service/Adapter.cs`
- **List** (4 connections)
- **.ToPositionCode()** (3 connections) — `NBA.Service/Adapter.cs`

## Relationships

- [Adapter Mapping Tests](Adapter_Mapping_Tests.md) (7 shared connections)
- [Game Redis Shapes](Game_Redis_Shapes.md) (2 shared connections)
- [Player Redis Operations](Player_Redis_Operations.md) (2 shared connections)
- [Player Redis Manager](Player_Redis_Manager.md) (2 shared connections)
- [Player Position Extensions](Player_Position_Extensions.md) (1 shared connections)
- [NBA Calendar Date Handling](NBA_Calendar_Date_Handling.md) (1 shared connections)
- [Game Service & Endpoints](Game_Service_&_Endpoints.md) (1 shared connections)
- [Game Info Response](Game_Info_Response.md) (1 shared connections)
- [Game Team Response](Game_Team_Response.md) (1 shared connections)
- [PlayerInfo Response Shape](PlayerInfo_Response_Shape.md) (1 shared connections)
- [Error Codes & Trade Statuses](Error_Codes_&_Trade_Statuses.md) (1 shared connections)

## Source Files

- `NBA.Service/Adapter.cs`

## Audit Trail

- EXTRACTED: 30 (100%)
- INFERRED: 0 (0%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*