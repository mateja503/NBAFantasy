using ApplicationDefaults.Time;
using NBA.Data.Context;
using NBA.Data.Redis.Entities;

namespace NBA.Service.Game
{
    /// <summary>
    /// Rule 4: this works against Redis, so it is a Manager. It owns the schedule cache and nothing
    /// else — the balldontlie fetch and the day bucketing stay in <see cref="GameService"/>.
    /// </summary>
    public class GameManager(NbaFantasyRedis redis)
    {
        private readonly NbaFantasyRedis _redis = redis;

        // Short on purpose: tip-off times, statuses and postponements move during the day. This is
        // about shielding the balldontlie quota from an anonymous endpoint, not long-term storage.
        private static readonly TimeSpan ScheduleTtl = TimeSpan.FromMinutes(5);

        public Task<ScheduledGames?> GetScheduledGames(DateOnly nbaToday) =>
            _redis.Game.GetScheduledGames(nbaToday.ToApiDate());

        public Task SetScheduledGames(DateOnly nbaToday, ScheduledGames games) =>
            _redis.Game.SetScheduledGames(nbaToday.ToApiDate(), games, ScheduleTtl);
    }
}
