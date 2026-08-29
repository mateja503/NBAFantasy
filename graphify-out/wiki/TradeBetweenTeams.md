# TradeBetweenTeams

> God node · 52 connections · `NBA.Data/Redis/Entities/TradeBetweenTeams.cs`

**Community:** [Trade Redis & TradeHub Tests](Trade_Redis_&_TradeHub_Tests.md)

## Connections by Relation

### calls
- .ProposeSeasonTrade() `EXTRACTED`
- .AcceptProposal() `EXTRACTED`
- .ProposeTrade() `EXTRACTED`
- .GetPendingProposals() `EXTRACTED`
- .RemoveProposedTrade_removes_only_the_matching_trade_and_returns_it() `INFERRED`
- .RemoveProposedTrade_returns_null_for_unknown_id() `INFERRED`
- .SetAcceptedDraftTrade_is_returned_by_GetAcceptedDraftTrades() `INFERRED`

### contains
- TradeBetweenTeams.cs `EXTRACTED`

### defines
- TradeDate `EXTRACTED`
- TradeId `EXTRACTED`
- FromTeam `EXTRACTED`
- ToTeam `EXTRACTED`
- PlayersIds `EXTRACTED`

### references
- .AddProposedTrade() `EXTRACTED`
- .AcceptTrade_revalidates_roster_limits_against_current_state() `EXTRACTED`
- .AcceptTrade_throws_when_a_team_is_missing_from_draft_state() `EXTRACTED`
- .AcceptDraftTrade() `EXTRACTED`
- .AcceptTrade_throws_when_draft_state_is_missing() `EXTRACTED`
- .Trade() `EXTRACTED`
- .AcceptTrade_cannot_be_accepted_twice() `EXTRACTED`
- .AcceptTrade_swaps_rosters_broadcasts_to_league_and_removes_proposed() `EXTRACTED`
- .ComputeSwappedRosters() `EXTRACTED`
- .ProposeTrade_valid_notifies_target_team_and_stores_proposed_trade() `EXTRACTED`
- .ValidateSeasonTrade() `EXTRACTED`
- .SetProposedSeasonTrade() `EXTRACTED`
- .GetProposedSeasonTrades() `EXTRACTED`
- .IsTradeValid() `EXTRACTED`
- .GetAcceptedDraftTrades() `EXTRACTED`
- .GetProposedTrade() `EXTRACTED`
- .RemoveProposedTrade() `EXTRACTED`
- .GetProposedSeasonTrades() `EXTRACTED`
- .ProposeSeasonTrade() `EXTRACTED`
- .ToSettled() `EXTRACTED`

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*