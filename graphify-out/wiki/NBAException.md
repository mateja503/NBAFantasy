# NBAException

> God node · 47 connections · `ApplicationDefaults/Exceptions/NBAException.cs`

**Community:** [Trade & Team Services](Trade_&_Team_Services.md)

## Connections by Relation

### calls
- .AddProposedTrade() `INFERRED`
- .EnsureRehydratedAsync() `INFERRED`
- .AcceptDraftTrade() `INFERRED`
- .ProposeSeasonTrade() `INFERRED`
- .NextPick() `INFERRED`
- .DraftOrder() `INFERRED`
- .AcceptProposal() `INFERRED`
- .Trade() `INFERRED`
- .ComputeSwappedRosters() `INFERRED`
- .LoginAsync() `INFERRED`
- .ResetTimer() `INFERRED`
- .DraftPlayer() `INFERRED`
- .EndDraft() `INFERRED`
- .CreateAsync() `INFERRED`
- .JoinAsync() `INFERRED`
- .GetUserTeamsWithPlayersAsync() `INFERRED`
- .ValidateSeasonTrade() `INFERRED`
- .RefreshAsync() `INFERRED`
- .GetTeamPlayersAsync() `INFERRED`
- .GetLeagueTrades() `INFERRED`

### contains
- NBAException.cs `EXTRACTED`

### defines
- ErrorCode `EXTRACTED`

### inherits
- Exception `EXTRACTED`

### references
- .A_non_success_status_becomes_an_ExternalApiCallFailed_NBAException() `EXTRACTED`
- .A_body_that_is_not_json_becomes_an_ExternalApiResponseInvalid_NBAException() `EXTRACTED`
- .A_non_transient_error_status_fails_fast_as_an_ExternalApiCallFailed_NBAException() `EXTRACTED`
- .A_null_body_becomes_an_ExternalApiResponseInvalid_NBAException() `EXTRACTED`
- .Requests_run_through_the_external_api_shield_pipeline_and_are_retried() `EXTRACTED`
- .A_body_that_is_not_json_becomes_an_ExternalApiResponseInvalid_NBAException() `EXTRACTED`
- .A_response_missing_required_fields_becomes_an_ExternalApiResponseInvalid_NBAException() `EXTRACTED`
- .A_null_body_becomes_an_ExternalApiResponseInvalid_NBAException() `EXTRACTED`
- .A_server_error_is_retried_by_the_shield_before_it_gives_up() `EXTRACTED`
- .A_response_missing_required_fields_becomes_an_ExternalApiResponseInvalid_NBAException() `EXTRACTED`
- .An_unstubbed_route_surfaces_as_an_ExternalApiCallFailed_NBAException() `EXTRACTED`

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*