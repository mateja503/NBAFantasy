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

        public NbaFantasyRedis(IConnectionMultiplexer redis, IOptions<JsonOptions> jsonOptions)
        {
            _redisDb = redis.GetDatabase();
            _jsonOptions = jsonOptions.Value.JsonSerializerOptions;
            _draft = new Lazy<DraftRedisOperations>(() => new DraftRedisOperations(_redisDb,_jsonOptions));
        }

        public DraftRedisOperations Draft => _draft.Value;

    }
}
