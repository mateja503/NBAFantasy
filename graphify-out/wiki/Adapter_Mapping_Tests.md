# Adapter Mapping Tests

> 13 nodes

## Key Concepts

- **.ToPlayerDb()** (11 connections) — `NBA.Service/Adapter.cs`
- **AdapterTests** (8 connections) — `NBA.Tests/AdapterTests.cs`
- **Fact** (6 connections)
- **.ToPlayerDb_copies_identity_and_team_fields()** (4 connections) — `NBA.Tests/AdapterTests.cs`
- **.ToPlayerDb_maps_position_string_to_enum()** (4 connections) — `NBA.Tests/AdapterTests.cs`
- **.ToPlayerRedisFromDB_carries_the_position_code_across()** (4 connections) — `NBA.Tests/AdapterTests.cs`
- **.ToGameRedis_flattens_both_sides_and_normalises_the_date()** (3 connections) — `NBA.Tests/AdapterTests.cs`
- **.ToGameRedis_nulls_a_missing_datetime_rather_than_emitting_year_one()** (3 connections) — `NBA.Tests/AdapterTests.cs`
- **.ToPlayerDb_handles_null_team()** (3 connections) — `NBA.Tests/AdapterTests.cs`
- **.ToPlayerRedis_builds_full_name_from_response()** (3 connections) — `NBA.Tests/AdapterTests.cs`
- **PlayerData** (1 connections)
- **InlineData** (1 connections)
- **Theory** (1 connections)

## Relationships

- [Redis Adapter Mappings](Redis_Adapter_Mappings.md) (7 shared connections)
- [Player Position Extensions](Player_Position_Extensions.md) (1 shared connections)
- [BoxScore Calculation Jobs](BoxScore_Calculation_Jobs.md) (1 shared connections)
- [Error Codes & Trade Statuses](Error_Codes_&_Trade_Statuses.md) (1 shared connections)
- [PlayerInfo Response Shape](PlayerInfo_Response_Shape.md) (1 shared connections)
- [Team Info Response](Team_Info_Response.md) (1 shared connections)

## Source Files

- `NBA.Service/Adapter.cs`
- `NBA.Tests/AdapterTests.cs`

## Audit Trail

- EXTRACTED: 31 (97%)
- INFERRED: 1 (3%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*