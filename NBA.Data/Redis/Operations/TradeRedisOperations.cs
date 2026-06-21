using NBA.Data.Redis.Entities;
using NBA.Data.Redis.Keys;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Text.Json;

namespace NBA.Data.Redis.Operations
{
    public class TradeRedisOperations(IDatabase database, JsonSerializerOptions jsonOptions)
    {
        private readonly IDatabase _redisDb = database;
        private readonly JsonSerializerOptions _jsonOptions = jsonOptions;
        public async Task SetProposedTrade(long leagueId, TradeBetweenTeams trade)
        {
            var redisKey = RedisKeys.GetProposedDraftTradesKey(leagueId);
            var value = new SortedSetEntry(JsonSerializer.Serialize(trade, _jsonOptions), trade.TradeDate.ToUnixTimeMilliseconds());

            await _redisDb.SortedSetAddAsync(redisKey, [value]);
            await _redisDb.KeyExpireAsync(redisKey, TimeSpan.FromDays(30));
        }

        public async Task SetAcceptedDraftTrade(long leagueId, TradeBetweenTeams trade)
        {
            var redisKey = RedisKeys.GetAcceptedDraftTradeKey(leagueId);
            var value = new SortedSetEntry(
                JsonSerializer.Serialize(trade, _jsonOptions),
                trade.TradeDate.ToUnixTimeMilliseconds()
            );

            await _redisDb.SortedSetAddAsync(redisKey, [value]);
            await _redisDb.KeyExpireAsync(redisKey, TimeSpan.FromDays(30));
        }

        public async Task<List<TradeBetweenTeams>> GetAcceptedDraftTrades(long leagueId)
        {
            var redisKey = RedisKeys.GetAcceptedDraftTradeKey(leagueId);

            var allTrades = await _redisDb.SortedSetRangeByRankAsync(redisKey, 0, -1, order: Order.Descending);

            return allTrades
                .Where(x => !x.IsNull)
                .Select(x =>
                {
                    try
                    {
                        return JsonSerializer.Deserialize<TradeBetweenTeams>(x.ToString(), _jsonOptions);
                    }
                    catch (JsonException ex)
                    {
                        return null;
                    }
                })
                .OfType<TradeBetweenTeams>()
                .ToList();
        }

        public async Task<TradeBetweenTeams?> RemoveProposedTrade(long leagueId, Guid tradeId)
        {
            var redisKey = RedisKeys.GetProposedDraftTradesKey(leagueId);

            var transaction = _redisDb.CreateTransaction();

            var task = transaction.SortedSetRangeByRankAsync(redisKey, 0, -1);
            var removeTask = transaction.SortedSetRemoveAsync(redisKey, tradeId.ToString());

            if (await transaction.ExecuteAsync())
            {
                var trades = await task;
                var tradeJson = trades
                    .FirstOrDefault(t => t.ToString().Contains(tradeId.ToString())).ToString();

                return tradeJson == null ? null : JsonSerializer.Deserialize<TradeBetweenTeams>(tradeJson, _jsonOptions);
            }

            return null;

        }

    }
}
