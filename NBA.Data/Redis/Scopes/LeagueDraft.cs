using NBA.Data.Redis.Dtos;
using NBA.Data.Redis.Entities;
using NBA.Data.Redis.Operations;

namespace NBA.Data.Redis.Scopes
{
    // League-bound view over DraftRedisOperations. Every method here already took a leagueId; binding
    // it once lets a caller drop the id from the rest of the chain. A thin readonly struct — no
    // allocation, no state, no logic. DraftRedisOperations keeps its existing public methods, so this
    // is purely additive and nothing breaks by not using it.
    //
    // ClaimDueDraftTimer is deliberately absent: it claims a due deadline across *all* leagues, so it
    // has no meaning on a single-league scope. Call it on DraftRedisOperations directly.
    public readonly struct LeagueDraft(DraftRedisOperations operations, long leagueId)
    {
        public long LeagueId => leagueId;

        public Task<DraftState?> GetState() => operations.GetCurrentDraftState(leagueId);

        public Task<DraftState> SetState(DraftState state) => operations.SetDraftState(leagueId, state);

        public Task<DraftState?> DeleteState() => operations.DeleteStringDraftState(leagueId);

        public Task<bool> StateExists() => operations.DraftStateExists(leagueId);

        public Task<bool> TeamsExist() => operations.DraftTeamsExist(leagueId);

        public Task<Dictionary<long, Queue<TeamDraftBoard>>?> GetTeams() => operations.GetDraftTeams(leagueId);

        // Note the underlying overload takes (draft, leagueId) — leagueId second, unlike everything
        // else in the layer. The scope hides that inconsistency from callers.
        public Task SetTeams(Dictionary<long, Queue<TeamDraftBoard>> teams) => operations.SetDraftTeams(teams, leagueId);

        public Task DeleteTeams() => operations.DeleteDraftTeams(leagueId);

        public Task<Dictionary<long, List<PlayerShortDto>>> GetAllTeamsDraftedPlayers() =>
            operations.GetAllTeamsDraftedPlayersForLeague(leagueId);

        public Task ScheduleTimer(DateTimeOffset dueAt) => operations.ScheduleDraftTimer(leagueId, dueAt);

        public Task CancelTimer() => operations.CancelDraftTimer(leagueId);

        public Task<bool> IsTimerScheduled() => operations.IsDraftTimerScheduled(leagueId);

        public Task<string?> TryAcquireCycleLock(TimeSpan expiry) => operations.TryAcquireDraftCycleLock(leagueId, expiry);

        public Task ReleaseCycleLock(string token) => operations.ReleaseDraftCycleLock(leagueId, token);
    }
}
