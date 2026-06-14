using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NBA.Data.Entities;
using NBA.Data.Redis.Entities;
using NBA.Data.Redis.Operations;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace NBA.Data.Context
{

    //Facade design pattern
    public class NbaFantasyRedis
    {
        private readonly IDatabase _redisDb;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly Lazy<DraftRedisOperations> _draft;
        private readonly Lazy<PlayerRedisOperations> _player;
        private readonly Lazy<AuthRedisOperations> _auth;
        private readonly Lazy<LockRedisOperations> _lock;

        public NbaFantasyRedis(IConnectionMultiplexer redis, IOptions<JsonOptions> jsonOptions)
        {
            _redisDb = redis.GetDatabase();
            _jsonOptions = jsonOptions.Value.JsonSerializerOptions;
            _draft = new Lazy<DraftRedisOperations>(() => new DraftRedisOperations(_redisDb,_jsonOptions));
            _player = new Lazy<PlayerRedisOperations>(() => new PlayerRedisOperations(_redisDb, _jsonOptions));
            _auth = new Lazy<AuthRedisOperations>(() => new AuthRedisOperations(_redisDb));
            _lock = new Lazy<LockRedisOperations>(() => new LockRedisOperations(_redisDb));
        }

        public DraftRedisOperations Draft => _draft.Value;
        public PlayerRedisOperations Player => _player.Value;
        public AuthRedisOperations Auth => _auth.Value;
        public LockRedisOperations Lock => _lock.Value;

    }
}
