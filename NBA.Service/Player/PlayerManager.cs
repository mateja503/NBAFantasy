using ApplicationDefaults.Options;
using ExternalClients.Response;
using Hangfire;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using NBA.Data.Context;
using NBA.Service.League.Draft;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using PlayerData = NBA.Data.Entities.Player;

namespace NBA.Service.Player
{
    public class PlayerManager(NbaFantasyContext context,IOptions<JsonOptions> jsonOptions,
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

        //temporary
        public async Task AddPlayerToRedisFromDB(List<PlayerData> players) 
        {
            var playersToRedis = Addapter.ToPlayerRedisFromDB(players);
            await _redis.Player.SetPlayersRange(playersToRedis);
        }


    }
}
