# Game Schedule Bucketing Tests

> 8 nodes

## Key Concepts

- **.BucketByDay()** (10 connections) — `NBA.Service/Game/GameService.cs`
- **GameScheduleTests** (8 connections) — `NBA.Tests/GameScheduleTests.cs`
- **.Game()** (7 connections) — `NBA.Tests/GameScheduleTests.cs`
- **.BucketByDay_drops_games_with_an_unusable_date_instead_of_throwing()** (5 connections) — `NBA.Tests/GameScheduleTests.cs`
- **.BucketByDay_leaves_the_week_empty_when_nothing_falls_after_tomorrow()** (4 connections) — `NBA.Tests/GameScheduleTests.cs`
- **.BucketByDay_splits_today_tomorrow_and_the_rest_of_the_week_without_overlap()** (4 connections) — `NBA.Tests/GameScheduleTests.cs`
- **.BucketByDay_still_fills_tomorrow_when_it_lands_in_the_next_week()** (4 connections) — `NBA.Tests/GameScheduleTests.cs`
- **Fact** (4 connections)

## Relationships

- [Game Service & Endpoints](Game_Service_&_Endpoints.md) (4 shared connections)
- [Game Redis Shapes](Game_Redis_Shapes.md) (4 shared connections)
- [NBA Calendar Date Handling](NBA_Calendar_Date_Handling.md) (2 shared connections)
- [External Client Response Tests](External_Client_Response_Tests.md) (1 shared connections)
- [Game Redis Operations](Game_Redis_Operations.md) (1 shared connections)

## Source Files

- `NBA.Service/Game/GameService.cs`
- `NBA.Tests/GameScheduleTests.cs`

## Audit Trail

- EXTRACTED: 28 (97%)
- INFERRED: 1 (3%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*