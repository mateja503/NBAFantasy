using NBA.Data.Context;
using NBA.Data.Redis.Entities;

namespace NBA.Service.Draft
{
    // Redis-only owner of the draft-order key (rule 4). DraftService generates the order from
    // Postgres but the order itself lives in Redis, so the two Redis calls it needs come from here
    // instead of DraftService holding NbaFantasyRedis directly.
    public class DraftOrderManager(NbaFantasyRedis redis)
    {
        private readonly NbaFantasyRedis _redis = redis;

        // Null when Redis holds no order for the league — the caller treats that as "generate one".
        public Task<Dictionary<long, Queue<TeamDraftBoard>>?> GetTeams(long leagueId) =>
            _redis.League(leagueId).Draft.GetTeams();

        public Task SetTeams(long leagueId, Dictionary<long, Queue<TeamDraftBoard>> teams) =>
            _redis.League(leagueId).Draft.SetTeams(teams);
    }
}
