# NBA Calendar Date Handling

> 11 nodes

## Key Concepts

- **NbaCalendar** (6 connections) — `ApplicationDefaults/Time/NbaCalendar.cs`
- **.EndOfWeek()** (4 connections) — `ApplicationDefaults/Time/NbaCalendar.cs`
- **.EndOfWeek_returns_the_sunday_that_closes_the_week()** (4 connections) — `NBA.Tests/GameScheduleTests.cs`
- **.ToApiDatePart_keeps_only_the_day_and_never_throws()** (4 connections) — `NBA.Tests/GameScheduleTests.cs`
- **.ToApiDatePart()** (3 connections) — `ApplicationDefaults/Time/NbaCalendar.cs`
- **DateOnly** (3 connections)
- **.ToApiDate()** (2 connections) — `ApplicationDefaults/Time/NbaCalendar.cs`
- **NbaCalendar.cs** (2 connections) — `ApplicationDefaults/Time/NbaCalendar.cs`
- **InlineData** (2 connections)
- **Theory** (2 connections)
- **TimeZoneInfo** (1 connections)

## Relationships

- [BallDontLie Client & NBA Calendar](BallDontLie_Client_&_NBA_Calendar.md) (2 shared connections)
- [Game Schedule Bucketing Tests](Game_Schedule_Bucketing_Tests.md) (2 shared connections)
- [Game Service & Endpoints](Game_Service_&_Endpoints.md) (1 shared connections)
- [Redis Adapter Mappings](Redis_Adapter_Mappings.md) (1 shared connections)
- [External Client Response Tests](External_Client_Response_Tests.md) (1 shared connections)

## Source Files

- `ApplicationDefaults/Time/NbaCalendar.cs`
- `NBA.Tests/GameScheduleTests.cs`

## Audit Trail

- EXTRACTED: 20 (100%)
- INFERRED: 0 (0%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*