using ApplicationDefaults.Exceptions;
using Microsoft.AspNetCore.Mvc;
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
    public class TradeManager(NbaFantasyRedis redis, DraftManager draftManager)
    {
        private readonly NbaFantasyRedis _redis = redis;
        private readonly DraftManager _draftManager = draftManager;

        public async Task ProposeDraftTrade(long leagueId, TradeBetweenTeams trade)   
        {
            await _redis.Trade.SetProposedTrade(leagueId, trade);
        }
      
        public async Task<TradeBetweenTeams?> AcceptDraftTrade(long leagueId, Guid tradeId) 
        {
            var trade =  await _redis.Trade.RemoveProposedTrade(leagueId, tradeId);
            
            if(trade == null) throw new NBAException("Trade not found.", ErrorCodes.TradeCantBeExecuted);

            var teamPlayers = await _redis.Draft.GetAllTeamsDraftedPlayersForLeague(leagueId);

            if(!teamPlayers.TryGetValue(trade.FromTeam, out var fromTeamDraftedPlayers))
                throw new NBAException("From team not found.", ErrorCodes.TradeCantBeExecuted);

            if(!teamPlayers.TryGetValue(trade.ToTeam, out var toTeamDraftedPlayers))
                throw new NBAException("To team not found.", ErrorCodes.TradeCantBeExecuted);

           
            var newFromPlayers = fromTeamDraftedPlayers.Where(t => !trade.PlayersIds.Contains(t.PlayerId ?? 0)).ToList();
            newFromPlayers.AddRange(toTeamDraftedPlayers.Where(t => trade.PlayersIds.Contains(t.PlayerId ?? 0)).ToList());

            var newToPlayers = toTeamDraftedPlayers.Where(t => !trade.PlayersIds.Contains(t.PlayerId ?? 0)).ToList();
            newToPlayers.AddRange(fromTeamDraftedPlayers.Where(t => trade.PlayersIds.Contains(t.PlayerId ?? 0)).ToList());

            teamPlayers[trade.FromTeam] = newFromPlayers;
            teamPlayers[trade.ToTeam] = newToPlayers;

            //TODO need to add the new players acquire in the draft in redis for for drafted team playes.
            //await redis.Draft.SetDraftState(leagueId, teamPlayers);

            return trade;
        }

        
    }
}
