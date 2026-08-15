using ApplicationDefaults.Exceptions;
using ApplicationDefaults.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Threading;
using NBA.Data.Context;
using NBA.Data.Enumerations;
using NBA.Data.Redis.Entities;
using NBA.Service.League.Draft;
using NBA.Service.League.Roster;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NBA.Service.League.Trade
{
    public class TradeManager(NbaFantasyRedis redis, IOptions<ApplicationOptions> applicationOptions,
        DraftManager draftManager, RosterValidator rosterValidator)
    {
        private readonly NbaFantasyRedis _redis = redis;
        private readonly DraftManager _draftManager = draftManager;
        private readonly ApplicationOptions _applicationOptions = applicationOptions.Value;
        private readonly RosterValidator _rosterValidator = rosterValidator;

        public async Task ProposeDraftTrade(long leagueId, TradeBetweenTeams trade)
        {
            await _redis.League(leagueId).Trades.SetProposed(trade);
        }

        // In-season proposal: one key per recipient, expiring after ProposedTradeTtlMinutes. This is
        // only the hot copy that drives the live push — the durable record is the nba.trades row
        // TradeService writes, which outlives this key.
        public Task ProposeSeasonTrade(long leagueId, TradeBetweenTeams trade) =>
            _redis.League(leagueId).Trades.SetProposedSeason(
                trade, TimeSpan.FromMinutes(_applicationOptions.ProposedTradeTtlMinutes));

        public Task<TradeBetweenTeams?> GetProposedSeasonTrade(long leagueId, long toTeamId) =>
            _redis.League(leagueId).Trades.GetProposedSeason(toTeamId);

        // The /tradeHub connection ids a team currently has open. Only the Redis read lives here —
        // the SignalR probe that uses these ids is in NBA.Api (TradePresenceProbe), because the hub
        // client interface belongs to the API layer and NBA.Service must not depend on it.
        public Task<List<string>> GetTradeConnectionIds(long teamId) =>
            _redis.Presence.GetTradeConnections(teamId);

        public Task DropTradeConnection(long teamId, string connectionId) =>
            _redis.Presence.RemoveTradeConnection(teamId, connectionId);
        
        // Rejects a trade that would push either team over the league roster limits. Computes each
        // team's roster as it would look after the swap and checks it.
        public async Task<bool> IsTradeValid(long leagueId, TradeBetweenTeams trade)
        {
            var teamPlayers = await _redis.League(leagueId).Draft.GetAllTeamsDraftedPlayers();

            var (newFromPlayers, newToPlayers) = ComputeSwappedRosters(teamPlayers, trade, ErrorCodes.TradeIsNotValid);

            ValidateRoster(newFromPlayers);
            ValidateRoster(newToPlayers);

            return true;
        }

        public async Task<TradeBetweenTeams?> AcceptDraftTrade(long leagueId, Guid tradeId)
        {
            var trades = _redis.League(leagueId).Trades;

            // Read the proposal first, but don't consume it yet — we only remove it once the swap has
            // been validated and applied, so a failed accept leaves the proposal intact.
            var trade = await trades.GetProposed(tradeId);

            if (trade == null) throw new NBAException("Trade not found.", ErrorCodes.TradeCantBeExecuted);

            var draftState = await _draftManager.GetDraftState(leagueId);

            if (draftState == null) throw new NBAException("Draft state not found. For league " + leagueId, ErrorCodes.DraftNotStarted);

            // A state without rosters means nothing has been drafted yet — treat it as an empty board so
            // ComputeSwappedRosters reports the missing team instead of dereferencing null.
            draftState.DraftedPlayersPerTeam ??= new Dictionary<long, List<PlayerShort>>();

            var (newFromPlayers, newToPlayers) = ComputeSwappedRosters(draftState.DraftedPlayersPerTeam, trade, ErrorCodes.TradeCantBeExecuted);

            // Re-validate against the current rosters: state may have drifted since the proposal.
            ValidateRoster(newFromPlayers);
            ValidateRoster(newToPlayers);

            draftState.DraftedPlayersPerTeam[trade.FromTeam] = newFromPlayers;
            draftState.DraftedPlayersPerTeam[trade.ToTeam] = newToPlayers;

            await _draftManager.UpdaterDraftState(leagueId, draftState);

            // Keep the per-team roster sets in sync with the swap. Without this, a later pick by either
            // team would rebuild its DraftedPlayersPerTeam entry from the stale set (see
            // DraftManager.AddTeamsDrafterPlayersToDraftState) and silently revert the trade.
            //await _redis.Player.ReplaceTeamsDraftedPlayers(trade.FromTeam, newFromPlayers.Select(p => p.PlayerId ?? 0));
            //await _redis.Player.ReplaceTeamsDraftedPlayers(trade.ToTeam, newToPlayers.Select(p => p.PlayerId ?? 0));

            // Swap succeeded — now consume the proposal and record it as accepted.
            await trades.RemoveProposed(tradeId);
            await trades.SetAccepted(trade);

            return trade;
        }

        // Computes both teams' rosters as they would look after the swap defined by the trade: each
        // team keeps the players it isn't trading away and gains the other team's traded players.
        // errorCode lets each caller surface its own error (proposal vs. accept).
        private (List<PlayerShort> newFromPlayers, List<PlayerShort> newToPlayers) ComputeSwappedRosters(
            Dictionary<long, List<PlayerShort>> teamPlayers, TradeBetweenTeams trade, string errorCode)
        {
            if (!teamPlayers.TryGetValue(trade.FromTeam, out var fromTeamDraftedPlayers))
                throw new NBAException("From team not found.", errorCode);

            if (!teamPlayers.TryGetValue(trade.ToTeam, out var toTeamDraftedPlayers))
                throw new NBAException("To team not found.", errorCode);

            var newFromPlayers = fromTeamDraftedPlayers.Where(t => !trade.PlayersIds.Contains(t.PlayerId ?? 0)).ToList();
            newFromPlayers.AddRange(toTeamDraftedPlayers.Where(t => trade.PlayersIds.Contains(t.PlayerId ?? 0)));

            var newToPlayers = toTeamDraftedPlayers.Where(t => !trade.PlayersIds.Contains(t.PlayerId ?? 0)).ToList();
            newToPlayers.AddRange(fromTeamDraftedPlayers.Where(t => trade.PlayersIds.Contains(t.PlayerId ?? 0)));

            return (newFromPlayers, newToPlayers);
        }

        // Counts the roster for the shared rule. PlayerShort.Position holds PlayerPositionEnum as an
        // int — the same code as Player.Playerposition — so no string comparison is involved.
        private void ValidateRoster(List<PlayerShort> roster) =>
            _rosterValidator.Validate(roster.Count, roster.Count(p => p.Position == (int)PlayerPositionEnum.C));
    }
}
