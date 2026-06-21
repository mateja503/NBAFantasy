using NBA.Data.Redis;
using NBA.Data.Redis.Operations;
using StackExchange.Redis;
using System;

namespace NBA.Data.Context
{

    //Facade design pattern
    public class NbaFantasyRedis
    {
        private readonly IDatabase _redisDb;
        private readonly Lazy<DraftRedisOperations> _draft;
        private readonly Lazy<PlayerRedisOperations> _player;
        private readonly Lazy<AuthRedisOperations> _auth;
        private readonly Lazy<TradeRedisOperations> _trade;
        private readonly Lazy<LockRedisOperations> _lock;

        public NbaFantasyRedis(IConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
            // One canonical serializer for every Redis operation (see RedisSerializer).
            _draft = new Lazy<DraftRedisOperations>(() => new DraftRedisOperations(_redisDb, RedisSerializer.Options));
            _player = new Lazy<PlayerRedisOperations>(() => new PlayerRedisOperations(_redisDb, RedisSerializer.Options));
            _auth = new Lazy<AuthRedisOperations>(() => new AuthRedisOperations(_redisDb));
            _lock = new Lazy<LockRedisOperations>(() => new LockRedisOperations(_redisDb));
            _trade = new Lazy<TradeRedisOperations>(() => new TradeRedisOperations(_redisDb, RedisSerializer.Options));
        }

        public DraftRedisOperations Draft => _draft.Value;
        public PlayerRedisOperations Player => _player.Value;
        public AuthRedisOperations Auth => _auth.Value;
        public LockRedisOperations Lock => _lock.Value;

        public TradeRedisOperations Trade => _trade.Value;

    }
}
