using k8s.Models;
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

        public async Task<List<PlayerShort>> GetAllPlayers()
        {
            var redisKey = RedisKeys.GetMasterPlayerKey();
            var playerIds = await _redisDb.SetMembersAsync(redisKey);

            if (playerIds.Length == 0) return new List<PlayerShort>();

            RedisKey[] playerKeys = Array.ConvertAll(playerIds, id => (RedisKey)RedisKeys.GetPlayerKey((long)id));

            RedisValue[] jsonResults = await _redisDb.StringGetAsync(playerKeys);

            var players = new List<PlayerShort>(jsonResults.Length);
            foreach (var json in jsonResults)
            {
                if (json.HasValue)
                {
                    var player = JsonSerializer.Deserialize<PlayerShort>(json.ToString(), _jsonOptions);
                    if (player != null) players.Add(player);
                }
            }

            return players;
        }

        public async Task<PlayerShort> SetPlayer(Player player)
        {
            var rediKey = RedisKeys.GetPlayerKey(player.Playerid);
            var entity = new PlayerShort
            {
                PlayerId = player.Playerid,
                FullName = $"{player.Name} {player.Surname}",
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

            var masterBatchEntries = new List<RedisValue>();
            var masterRedisKey = RedisKeys.GetMasterPlayerKey();

            players.ForEach(p =>
             {
                 var redisKey = RedisKeys.GetPlayerKey(p.PlayerId ?? 0);

                 processedPlayers.Add(p);

                 var json = JsonSerializer.Serialize(p, _jsonOptions);
                 batchEntries.Add(new KeyValuePair<RedisKey, RedisValue>(redisKey, json));
                 masterBatchEntries.Add(p.PlayerId);
             });

            await _redisDb.StringSetAsync(batchEntries.ToArray());

            await _redisDb.SetAddAsync(masterRedisKey, masterBatchEntries.ToArray());

            var expiryTasks = batchEntries.Select(entry => _redisDb.KeyExpireAsync(entry.Key, TimeSpan.FromDays(30))).ToList();

            expiryTasks.Add(_redisDb.KeyExpireAsync(masterRedisKey, TimeSpan.FromDays(30)));


            await Task.WhenAll(expiryTasks);

            return processedPlayers;
        }

       

        public async Task<HashSet<PlayerShort>> AddLeaguesAvailableDraftPlayers(long leagueid, List<PlayerShort> playerids)
        {
            var redisKey = RedisKeys.GetLeaguesAvailablePlayersKey(leagueid);

            RedisValue[] redisValues = Array.ConvertAll(playerids.ToArray(), player => (RedisValue)JsonSerializer.Serialize(player, _jsonOptions));

            await _redisDb.SetAddAsync(redisKey, redisValues);
            await _redisDb.KeyExpireAsync(redisKey, TimeSpan.FromDays(30));
            return playerids.ToHashSet();
        }

        public async Task<HashSet<PlayerShort>?> GetLeaguesAvailableDraftPlayers(long leagueid)
        {
            var redisKey = RedisKeys.GetLeaguesAvailablePlayersKey(leagueid);
            var redisValues = await _redisDb.SetMembersAsync(redisKey);

            if (redisValues.Length == 0) return null;

            return redisValues.Where(v => v.HasValue)
                .Select(v => JsonSerializer.Deserialize<PlayerShort>(v.ToString(), _jsonOptions))
                .Where(player => player != null)
                .Select(player => player!)
                .ToHashSet();
        }

        public async Task AddTeamsDrafterPlayer(long teamid, long playerid)
        {
            var redisKey = RedisKeys.GetTeamsDrafterPlayersKey(teamid);
            await _redisDb.SetAddAsync(redisKey, playerid);
            await _redisDb.KeyExpireAsync(redisKey, TimeSpan.FromDays(30));
        }

        public async Task AddLeaguesDraftedPlayer(long leagueid, long playerid, int pick)
        {
            var redisKey = RedisKeys.GetLeaguesDraftedPlayersKey(leagueid);
            await _redisDb.SortedSetAddAsync(redisKey, playerid, pick);
            await _redisDb.KeyExpireAsync(redisKey, TimeSpan.FromDays(30));
        }
        public async Task<HashSet<long>?> GetLeaguesDrafterPlayers(long leagueid)
        {
            var redisKey = RedisKeys.GetLeaguesDraftedPlayersKey(leagueid);
            var redisValues = await _redisDb.SortedSetRangeByRankAsync(redisKey, 0, -1, Order.Ascending);
            return redisValues.Select(v => (long)v).ToHashSet();
        }

        public async Task<bool> IsPlayerDrafted(long leagueid, long playerid)
        {
            var redisKey = RedisKeys.GetLeaguesDraftedPlayersKey(leagueid);
            var score = await _redisDb.SortedSetScoreAsync(redisKey, playerid);
            return score.HasValue;
        }
    }
}
