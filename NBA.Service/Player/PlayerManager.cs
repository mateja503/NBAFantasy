using ApplicationDefaults.Options;
using ExternalClients.Response;
using Hangfire;
using Hangfire.States;
using k8s.ClientSets;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using NBA.Data.Context;
using NBA.Data.Redis.Entities;
using NBA.Service.League.Draft;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using PlayerData = NBA.Data.Entities.Player;

namespace NBA.Service.Player
{
    public class PlayerManager(NbaFantasyContext context, IOptions<JsonOptions> jsonOptions,
        NbaFantasyRedis redis, PlayerService playerService)
    {
        private readonly NbaFantasyContext _context = context;
        private readonly JsonSerializerOptions _jsonOptions = jsonOptions.Value.SerializerOptions;
        private readonly NbaFantasyRedis _redis = redis;
        private readonly PlayerService _playerService = playerService;

        public async Task AddPlayersToRedis(List<PlayerInfoResponse> players)
        {
            var playersToRedis = Addapter.ToPlayerRedis(players);
            await _redis.Player.SetPlayersRange(playersToRedis);
        }

        public async Task AddPlayerToRedisFromDB(List<PlayerData> players)
        {
            var playersToRedis = Addapter.ToPlayerRedisFromDB(players);
            await _redis.Player.SetPlayersRange(playersToRedis);
        }

        public async Task AddDraftedPlayers(long leagueId, long playerId, int pick) 
        {
            await _redis.Player.AddLeaguesDraftedPlayer(leagueId, playerId, pick);

            var draftState = await _redis.Draft.GetCurrentDraftState(leagueId);

            var teamId = draftState!.DraftBoardTeams!.onTheClockTeam!.TeamId;

            await _redis.Player.AddTeamsDrafterPlayer(teamId, playerId);
        }
    

     
        public async Task<List<PlayerShort>> GetPlayersOnDraftBoard(long leagueid) 
        {
            
            var leaguesAvailablePlayers = await _redis.Player.GetLeaguesAvailableDraftPlayers(leagueid);

            if(leaguesAvailablePlayers is null) 
            {
                var players = await _redis.Player.GetAllPlayers();
                leaguesAvailablePlayers = await _redis.Player.AddLeaguesAvailableDraftPlayers(leagueid,players);
            }

            var draftedPlayers = await _redis.Player.GetLeaguesDrafterPlayers(leagueid);

            if (draftedPlayers is null) 
            {
                return leaguesAvailablePlayers.ToList();
            }

            return leaguesAvailablePlayers.Where(p => !draftedPlayers.Contains(p.PlayerId ?? 0))
                .ToList();
        }
      
    }
}
