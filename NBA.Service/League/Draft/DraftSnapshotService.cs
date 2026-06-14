using ApplicationDefaults.Options;
using Microsoft.Extensions.Options;
using NBA.Data.Context;
using NBA.Data.Entities;
using NBA.Data.Redis;
using NBA.Data.Redis.Entities;
using NBA.Data.Redis.Enumerations;
using System.Text.Json;

namespace NBA.Service.League.Draft
{
    // Durability layer for the live draft. The authoritative live copy stays in Redis (fast);
    // this mirrors it into Postgres so a Redis eviction/restart mid-draft can be recovered.
    // Write-through on structural changes, read-through recovery on a Redis miss.
    public class DraftSnapshotService(NbaFantasyContext context, NbaFantasyRedis redis,
        IOptions<DraftOptions> draftOptions)
    {
        private readonly NbaFantasyContext _context = context;
        private readonly NbaFantasyRedis _redis = redis;
        private readonly DraftOptions _draftOptions = draftOptions.Value;
        // Same canonical serializer the Redis layer uses, so snapshot JSON round-trips identically.
        private static readonly JsonSerializerOptions _json = RedisSerializer.Options;

        // Mirrors the current Redis state + remaining order into Postgres. Called after the draft
        // structurally changes (created, advanced). Cheap enough at draft cadence (one pick / ~10s).
        public async Task PersistAsync(long leagueId)
        {
            var state = await _redis.Draft.GetCurrentDraftState(leagueId);
            var teams = await _redis.Draft.GetDraftTeams(leagueId);

            if (state is null && teams is null)
                return;

            await _context.UpsertDraftSnapshot(new Draftsnapshot
            {
                Leagueid = leagueId,
                Draftstate = state is null ? null : JsonSerializer.Serialize(state, _json),
                Draftteams = teams is null ? null : JsonSerializer.Serialize(teams, _json),
                Tsupdated = DateTime.UtcNow,
            });
        }

        // If Redis still has the draft, this is a no-op (one GET). Otherwise it restores the state
        // and remaining order from the Postgres snapshot and re-arms the pick timer if the draft was
        // mid-flight. Crucially this must run before DraftService.DraftOrder re-reads Redis, so a
        // flush never causes the draft order to be regenerated (which would reshuffle picks).
        public async Task<bool> EnsureRehydratedAsync(long leagueId)
        {
            // Check BOTH keys: Redis can evict them independently under a maxmemory policy, and
            // recovering only on missing state would let an evicted teams key slip through and cause
            // DraftService.DraftOrder to regenerate (reshuffle) the order.
            var stateExists = await _redis.Draft.DraftStateExists(leagueId);
            var teamsExist = await _redis.Draft.DraftTeamsExist(leagueId);
            if (stateExists && teamsExist)
                return false;

            var snapshot = await _context.GetDraftSnapshot(leagueId);
            if (snapshot is null)
                return false;

            if (!stateExists && !string.IsNullOrEmpty(snapshot.Draftstate))
            {
                var state = JsonSerializer.Deserialize<DraftState>(snapshot.Draftstate, _json);
                if (state is not null)
                {
                    await _redis.Draft.SetDraftState(leagueId, state);

                    // Only an actively-running draft needs its clock restarted.
                    if (state.DraftStatus == (int)DraftStatus.DraftStarted)
                        await _redis.Draft.ScheduleDraftTimer(leagueId, DateTimeOffset.UtcNow.AddSeconds(_draftOptions.DraftPickTime));
                }
            }

            if (!teamsExist && !string.IsNullOrEmpty(snapshot.Draftteams))
            {
                var teams = JsonSerializer.Deserialize<Dictionary<long, Queue<TeamDraftBoard>>>(snapshot.Draftteams, _json);
                if (teams is not null)
                    await _redis.Draft.SetDraftTeams(teams, leagueId);
            }

            return true;
        }

        public Task DeleteAsync(long leagueId) => _context.DeleteDraftSnapshot(leagueId);
    }
}
