using ApplicationDefaults.Options;
using ExternalClients.Response;
using Hangfire;
using Hangfire.States;
using k8s.ClientSets;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using NBA.Data.Context;
using NBA.Data.Redis.Dtos;
using NBA.Data.Redis.Entities;
using NBA.Service.Draft;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using PlayerData = NBA.Data.Entities.Player;

namespace NBA.Service.Player
{
    public class PlayerManager(IOptions<JsonOptions> jsonOptions,
        NbaFantasyRedis redis, PlayerService playerService)
    {
        private readonly JsonSerializerOptions _jsonOptions = jsonOptions.Value.SerializerOptions;
        private readonly NbaFantasyRedis _redis = redis;
        private readonly PlayerService _playerService = playerService;

        public async Task AddPlayersToRedis(List<PlayerInfoResponse> players)
        {
            var playersToRedis = Adapter.ToPlayerRedis(players);
            await _redis.Player.SetPlayersRange(playersToRedis);
        }

        public async Task AddPlayerToRedisFromDB(List<PlayerData> players)
        {
            var playersToRedis = Adapter.ToPlayerRedisFromDB(players);
            await _redis.Player.SetPlayersRange(playersToRedis);
        }

        public async Task AddDraftedPlayers(long leagueId, long playerId, int pick)
        {
            var league = _redis.League(leagueId);

            await league.Players.AddDraftedPlayer(playerId, pick);

            var draftState = await league.Draft.GetState();

            if (draftState?.DraftBoardTeams is not null)
            {
                var teamId = draftState!.DraftBoardTeams!.onTheClockTeam!.TeamId;
                // Team-scoped, not league-scoped — stays on the unbound operations class.
                await _redis.Player.AddTeamsDrafterPlayer(teamId, playerId);

            }

        }
    

     
        // Returns the DTO shape: this feeds DraftState.DraftPlayers, which is serialized straight to
        // clients, so positions leave here as labels rather than PlayerPositionEnum codes.
        public async Task<List<PlayerShortDto>> GetPlayersOnDraftBoard(long leagueid) 
        {
            
            var leaguePlayers = _redis.League(leagueid).Players;

            var leaguesAvailablePlayers = await leaguePlayers.GetAvailableDraftPlayers();

            if(leaguesAvailablePlayers is null)
            {
                // The master player pool is global, so it comes off the unbound operations class.
                var players = await _redis.Player.GetAllPlayers();
                leaguesAvailablePlayers = await leaguePlayers.AddAvailableDraftPlayers(players);
            }

            var draftedPlayers = await leaguePlayers.GetDraftedPlayers();

            if (draftedPlayers is null) 
            {
                return leaguesAvailablePlayers.ToPlayerShortDtos();
            }

            return leaguesAvailablePlayers.Where(p => !draftedPlayers.Contains(p.PlayerId ?? 0))
                .ToPlayerShortDtos();
        }
      
    }
}
