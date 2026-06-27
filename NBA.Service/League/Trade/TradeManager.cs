using ApplicationDefaults.Exceptions;
using ApplicationDefaults.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Threading;
using NBA.Data.Context;
using NBA.Data.Redis.Entities;
using NBA.Service.League.Draft;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NBA.Service.League.Trade
{
    public class TradeManager(NbaFantasyRedis redis, DraftManager draftManager, IOptions<ApplicationOptions> applicationOptions)
    {
        private readonly NbaFantasyRedis _redis = redis;
        private readonly DraftManager _draftManager = draftManager;
        private readonly ApplicationOptions _applicationOptions = applicationOptions.Value;

        public async Task ProposeDraftTrade(long leagueId, TradeBetweenTeams trade)
        {
            await _redis.Trade.SetProposedTrade(leagueId, trade);
        }

        // Rejects a trade that would push either team over the league roster limits. Computes each
        // team's roster as it would look after the swap and checks it.
        public async Task<bool> IsTradeValid(long leagueId, TradeBetweenTeams trade)
        {
            var (newFromPlayers, newToPlayers) = await ComputeSwappedRosters(leagueId, trade, ErrorCodes.TradeIsNotValid);

            ValidateRoster(newFromPlayers);
            ValidateRoster(newToPlayers);

            return true;
        }

        public async Task<TradeBetweenTeams?> AcceptDraftTrade(long leagueId, Guid tradeId)
        {
            var trade = await _redis.Trade.RemoveProposedTrade(leagueId, tradeId);

            if (trade == null) throw new NBAException("Trade not found.", ErrorCodes.TradeCantBeExecuted);

            var (newFromPlayers, newToPlayers) = await ComputeSwappedRosters(leagueId, trade, ErrorCodes.TradeCantBeExecuted);

            var draftState = await _redis.Draft.GetCurrentDraftState(leagueId);

            if (draftState == null) throw new NBAException("Draft state not found. For league " + leagueId, ErrorCodes.DraftNotStarted);

            draftState.DraftedPlayersPerTeam[trade.FromTeam] = newFromPlayers;
            draftState.DraftedPlayersPerTeam[trade.ToTeam] = newToPlayers;

            //TODO need to add the new players acquire in the draft in redis for for drafted team playes.
            await _redis.Draft.SetDraftState(leagueId, draftState);

            await _redis.Trade.SetAcceptedDraftTrade(leagueId, trade);

            return trade;
        }

        // Computes both teams' rosters as they would look after the swap defined by the trade: each
        // team keeps the players it isn't trading away and gains the other team's traded players.
        // errorCode lets each caller surface its own error (proposal vs. accept).
        private async Task<(List<PlayerShort> newFromPlayers, List<PlayerShort> newToPlayers)> ComputeSwappedRosters(
            long leagueId, TradeBetweenTeams trade, string errorCode)
        {
            var teamPlayers = await _redis.Draft.GetAllTeamsDraftedPlayersForLeague(leagueId);

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

        private void ValidateRoster(List<PlayerShort> roster)
        {
            if (roster.Count > _applicationOptions.MaxPlayersPerTeam)
                throw new NBAException("Trade exceeds the maximum number of players per team.", ErrorCodes.TradeIsNotValid);

            // A center is the literal position string "C" (matches DraftService.DraftPlayer).
            if (roster.Count(p => p.Position == "C") > _applicationOptions.CenterLimit)
                throw new NBAException("Trade exceeds the maximum number of centers per team.", ErrorCodes.TradeIsNotValid);
        }
      
      

        
    }
}
