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

        // Reads a proposed trade by id without removing it, so callers can validate before consuming.
        public async Task<TradeBetweenTeams?> GetProposedTrade(long leagueId, Guid tradeId)
        {
            var redisKey = RedisKeys.GetProposedDraftTradesKey(leagueId);

            var allTrades = await _redisDb.SortedSetRangeByRankAsync(redisKey, 0, -1);

            foreach (var entry in allTrades.Where(t => !t.IsNull))
            {
                var trade = JsonSerializer.Deserialize<TradeBetweenTeams>(entry.ToString(), _jsonOptions);
                if (trade?.TradeId == tradeId) return trade;
            }

            return null;
        }

        public async Task<TradeBetweenTeams?> RemoveProposedTrade(long leagueId, Guid tradeId)
        {
            var redisKey = RedisKeys.GetProposedDraftTradesKey(leagueId);

            // The sorted-set member is the full trade JSON (see SetProposedTrade), not the trade id, so
            // we have to find the member whose deserialized TradeId matches and remove that exact member.
            var allTrades = await _redisDb.SortedSetRangeByRankAsync(redisKey, 0, -1);

            foreach (var entry in allTrades.Where(t => !t.IsNull))
            {
                var trade = JsonSerializer.Deserialize<TradeBetweenTeams>(entry.ToString(), _jsonOptions);

                if (trade?.TradeId == tradeId)
                {
                    await _redisDb.SortedSetRemoveAsync(redisKey, entry);
                    return trade;
                }
            }

            return null;
        }

    }
}
