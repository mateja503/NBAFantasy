# DraftState

> God node · 34 connections · `NBA.Data/Redis/Entities/DraftState.cs`

**Community:** [End-Draft Integration Tests](End-Draft_Integration_Tests.md)

## Connections by Relation

### calls
- .AcceptTrade_revalidates_roster_limits_against_current_state() `EXTRACTED`
- .AcceptTrade_throws_when_a_team_is_missing_from_draft_state() `EXTRACTED`
- .BuildEndedState() `EXTRACTED`
- .CreateDraftState() `EXTRACTED`
- .EndDraft_inserts_every_teams_redis_players_into_the_db_and_completes_the_league() `INFERRED`
- .EndDraft_is_a_no_op_when_the_league_draft_is_already_completed() `INFERRED`

### contains
- DraftState.cs `EXTRACTED`

### defines
- DraftStatus `EXTRACTED`
- DraftBoardTeams `EXTRACTED`
- DraftPlayers `EXTRACTED`
- DraftedPlayersPerTeam `EXTRACTED`
- LeagueName `EXTRACTED`
- PickEndTime `EXTRACTED`

### references
- PlayerShortDto `EXTRACTED`
- .EnsureRehydratedAsync() `EXTRACTED`
- .NextPick() `EXTRACTED`
- .BuildDraftState() `EXTRACTED`
- DraftBoardTeams `EXTRACTED`
- .GetDraftState() `EXTRACTED`
- .ResetTimer() `EXTRACTED`
- .UpdaterDraftState() `EXTRACTED`
- .GetCurrentDraftState() `EXTRACTED`
- .ResetTimer() `EXTRACTED`
- .DeleteStringDraftState() `EXTRACTED`
- .SetDraftState() `EXTRACTED`
- .DeleteState() `EXTRACTED`
- .GetState() `EXTRACTED`
- .SetState() `EXTRACTED`
- .AddTeamsDrafterPlayersToDraftState() `EXTRACTED`
- .MarkEnded() `EXTRACTED`
- .UpdateDraftState() `EXTRACTED`
- DateTime `EXTRACTED`
- Dictionary `EXTRACTED`

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*