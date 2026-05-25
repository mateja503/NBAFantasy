using NBA.Data.Entities;
using NBA.Data.Enumerations;
using NBA.Data.Redis.Entities;
using NBA.Data.Redis.Keys;
using StackExchange.Redis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.Json;
using static StackExchange.Redis.Role;

namespace NBA.Data.Redis.Operations
{
    public class PlayerRedisOperations
    {
        private readonly IDatabase _redisDb;
        private readonly JsonSerializerOptions _jsonOptions;
        public PlayerRedisOperations(IDatabase redis, JsonSerializerOptions jsonOptions)
        {
            _redisDb = redis;
            _jsonOptions = jsonOptions;
        }
        public async Task<PlayerShort?> GetPlayer(long playerid)
        {
            var redisKey = RedisKeys.GetPlayerKey(playerid);
            var data = await _redisDb.StringGetAsync(redisKey);
            var player = data.HasValue ? JsonSerializer.Deserialize<PlayerShort>(data.ToString(), _jsonOptions) : null;
            return player;
        }

        public async Task<PlayerShort> SetPlayer(Player player)
        {
            var rediKey = RedisKeys.GetPlayerKey(player.Playerid);
            var entity = new PlayerShort
            {
                Playerid = player.Playerid,
                Fullname = $"{player.Name} {player.Surname}",
                Position = (long)player.Playerposition! switch
                {
                    (long)PlayerPositionEnum.G => "G",
                    (long)PlayerPositionEnum.F => "F",
                    (long)PlayerPositionEnum.C => "C",
                    (long)PlayerPositionEnum.GF => "GF",
                    (long)PlayerPositionEnum.CF => "CF",
                    (long)PlayerPositionEnum.FG => "FG",
                    _ => "UNKOWN"
                }
            };

            await _redisDb.StringSetAsync(rediKey, JsonSerializer.Serialize(entity, _jsonOptions), expiry: TimeSpan.FromDays(30));
            return entity;
        }

        public async Task<List<PlayerShort>> SetPlayersRange(List<PlayerShort> players)
        {
            var processedPlayers = new List<PlayerShort>();
            var batchEntries = new List<KeyValuePair<RedisKey, RedisValue>>();
            players.ForEach(p =>
             {
                 var redisKey = RedisKeys.GetPlayerKey(p.Playerid ?? 0);

                 processedPlayers.Add(p);

                 var json = JsonSerializer.Serialize(p, _jsonOptions);
                 batchEntries.Add(new KeyValuePair<RedisKey, RedisValue>(redisKey, json));
             });

            await _redisDb.StringSetAsync(batchEntries.ToArray());

            var expiryTasks = batchEntries.Select(entry => _redisDb.KeyExpireAsync(entry.Key, TimeSpan.FromDays(30)));

            await Task.WhenAll(expiryTasks);

            return processedPlayers;
        }



    }
}
