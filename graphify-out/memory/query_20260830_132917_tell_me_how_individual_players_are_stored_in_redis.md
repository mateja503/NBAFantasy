---
type: "query"
date: "2026-08-30T13:29:17.358354+00:00"
question: "Tell me how individual players are stored in redis"
contributor: "graphify"
outcome: "useful"
source_nodes: ["PlayerShort", "RedisKeys", "PlayerRedisOperations", "RedisSerializer", "PlayerShortDto", "LeaguePlayers"]
---

# Q: Tell me how individual players are stored in redis

## Answer

Expanded from original query via vocab: [redis, player, players, short, key, keys, serialized, serializer, store, hash, playerid]. Then traversed BFS depth=2 from PlayerShort/RedisKeys/PlayerRedisOperations. An individual player is stored as JSON-serialized PlayerShort (PlayerId, FullName flattened from Name+Surname, Position as PlayerPositionEnum int code) under the STRING key nba:player:{playerid} (RedisKeys.GetPlayerKey), with a 30-day TTL. A SET nba:master:players holds every playerId so GetAllPlayers can do one batched MGET. League pool nba:available:players:league:{id} is a SET of full PlayerShort JSON; drafted players are a SORTED SET nba:drafted:players:league:{id} (member=playerId, score=pick); team rosters are a SET of ids at nba:players:league:team:{teamId}. Serialization uses the canonical RedisSerializer.Options. PlayerShort keeps the position int code; PlayerShortDto converts it to a label for clients. Found bug: GetTeamsDraftedPlayers at PlayerRedisOperations.cs:129 guards on redisKey.Length instead of redisValues.Length.

## Outcome

- Signal: useful

## Source Nodes

- PlayerShort
- RedisKeys
- PlayerRedisOperations
- RedisSerializer
- PlayerShortDto
- LeaguePlayers