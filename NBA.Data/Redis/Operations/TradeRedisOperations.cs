using NBA.Data.Redis.Entities;
using NBA.Data.Redis.Keys;
using StackExchange.Redis;
using System.Text.Json;

namespace NBA.Data.Redis.Operations
{
    public class TradeRedisOperations(IDatabase database, JsonSerializerOptions jsonOptions)
    {
        private readonly IDatabase _redisDb = database;
        private readonly JsonSerializerOptions _jsonOptions = jsonOptions;

        // In-season proposal. A recipient can hold offers from several teams at once, so this is a
        // sorted set rather than a single value: member = the trade JSON, score = when that proposal's
        // live window ends. Redis has no per-member TTL, which is why the expiry lives in the score.
        public async Task SetProposedSeasonTrade(long leagueId, TradeBetweenTeams trade, TimeSpan ttl)
        {
            var redisKey = RedisKeys.GetProposedTradeKey(leagueId, trade.ToTeam);

            await PruneExpiredSeasonTrades(redisKey);

            // A team replaces its own standing offer rather than stacking another on top of it.
            // Members are JSON blobs, so the previous one is found by deserialising rather than by key.
            foreach (var member in await _redisDb.SortedSetRangeByRankAsync(redisKey))
            {
                if (TryParseTrade(member)?.FromTeam == trade.FromTeam)
                    await _redisDb.SortedSetRemoveAsync(redisKey, member);
            }

            var expiresAt = DateTimeOffset.UtcNow.Add(ttl).ToUnixTimeMilliseconds();

            await _redisDb.SortedSetAddAsync(redisKey, JsonSerializer.Serialize(trade, _jsonOptions), expiresAt);

            // Backstop against an abandoned key: every member expires within ttl of being written and
            // this is re-armed on each write, so the key dies once its newest member has lapsed.
            await _redisDb.KeyExpireAsync(redisKey, ttl);
        }

        // Every live proposal aimed at this team, newest first. Returns an empty list rather than null
        // so callers can treat "no cache" and "nothing pending" the same way.
        public async Task<List<TradeBetweenTeams>> GetProposedSeasonTrades(long leagueId, long toTeamId)
        {
            var redisKey = RedisKeys.GetProposedTradeKey(leagueId, toTeamId);

            await PruneExpiredSeasonTrades(redisKey);

            // Descending by score: with a uniform TTL a later expiry means a later proposal.
            var members = await _redisDb.SortedSetRangeByRankAsync(redisKey, 0, -1, Order.Descending);

            return members.Select(TryParseTrade).OfType<TradeBetweenTeams>().ToList();
        }

        // Drops one proposal from a recipient's live set — used when it is accepted or rejected, so an
        // already-settled trade cannot keep reappearing in the connect-time backlog until its score
        // happens to lapse.
        public async Task<bool> RemoveProposedSeasonTrade(long leagueId, long toTeamId, Guid tradeId)
        {
            var redisKey = RedisKeys.GetProposedTradeKey(leagueId, toTeamId);

            foreach (var member in await _redisDb.SortedSetRangeByRankAsync(redisKey))
            {
                if (TryParseTrade(member)?.TradeId == tradeId)
                    return await _redisDb.SortedSetRemoveAsync(redisKey, member);
            }

            return false;
        }

        // Score is the expiry, so anything scored at or before now is gone. Cheap enough to run on
        // every read and write, which keeps the set from accumulating dead proposals.
        private Task PruneExpiredSeasonTrades(RedisKey redisKey) =>
            _redisDb.SortedSetRemoveRangeByScoreAsync(
                redisKey, double.NegativeInfinity, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

        // A member written by an older shape reads as "not a trade" rather than blowing up the
        // request — Postgres remains the source of truth either way.
        private TradeBetweenTeams? TryParseTrade(RedisValue member)
        {
            if (member.IsNullOrEmpty) return null;

            try
            {
                return JsonSerializer.Deserialize<TradeBetweenTeams>(member.ToString(), _jsonOptions);
            }
            catch (JsonException)
            {
                return null;
            }
        }
    }
}
