using ApplicationDefaults.Exceptions;
using ApplicationDefaults.Options;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NBA.Data.Context;
using NBA.Data.Redis.Entities;
using NBA.Data.Redis.Enumerations;
using System.Text.Json;

namespace NBA.Service.League.Draft
{
    public class DraftManager(NbaFantasyContext context,
        IOptions<JsonOptions> jsonOptions, IOptions<DraftOptions> draftOptions,
        NbaFantasyRedis redis, DraftService draftService, DraftSnapshotService snapshot)
    {
        private readonly NbaFantasyContext _context = context;
        private readonly JsonSerializerOptions _jsonOptions = jsonOptions.Value.SerializerOptions;
        private readonly DraftOptions _draftOptions = draftOptions.Value;
        private readonly NbaFantasyRedis _redis = redis;
        private readonly DraftService _draftService = draftService;
        private readonly DraftSnapshotService _snapshot = snapshot;
        public DraftState currentState { get; private set; }
        public async Task<DraftState> CreateDraftState(long leagueId) 
        {
            var leagueName = await _context.GetAllLeagues().Where(u => u.Leagueid == leagueId).Select(u => u.Name).SingleOrDefaultAsync();

            currentState = new DraftState
            {
                LeagueName = leagueName ?? "NO LEAGUE",
                PickEndTime = DateTime.UtcNow,
                DraftStatus = (int)DraftStatus.Initial,
                DraftBoardTeams = new DraftBoardTeams { CurrentRound = 1 },
            };
            await _redis.Draft.SetDraftState(leagueId, currentState);
            await _snapshot.PersistAsync(leagueId);

            return currentState;
        }

        public async Task<DraftState> UpdaterDraftState(long leagueId, DraftState state)
        {
            var saved = await _redis.Draft.SetDraftState(leagueId, state);
            await _snapshot.PersistAsync(leagueId);
            return saved;
        }

        public async Task<DraftState?> GetDraftState(long leagueId)
        {
            // Restore from the Postgres snapshot first if Redis lost the draft.
            await _snapshot.EnsureRehydratedAsync(leagueId);
            return await _redis.Draft.GetCurrentDraftState(leagueId);
        }
        public async Task<DraftState> ResetTimer(long leagueId)
        {
            // Recover from the durable snapshot if Redis lost the state, then fail loudly rather than
            // serializing a null state back into Redis (which previously stored the literal "null").
            await _snapshot.EnsureRehydratedAsync(leagueId);

            var state = await _redis.Draft.GetCurrentDraftState(leagueId)
                ?? throw new NBAException($"No active draft state for league {leagueId}", ErrorCodes.DataBaseRecordNotFound);

            state.PickEndTime = DateTime.UtcNow.AddSeconds(_draftOptions.DraftPickTime);
            await _redis.Draft.SetDraftState(leagueId, state);
            return state;
        }

        public async Task EndDraft(long leagueId)
        {
            // Remove any pending pick deadline from the timer sorted set.
            await _redis.Draft.CancelDraftTimer(leagueId);

            await _draftService.EndDraft(leagueId);

            _ = await _redis.Draft.DeleteStringDraftState(leagueId);
            await _redis.Draft.DeleteDraftTeams(leagueId);
            await _snapshot.DeleteAsync(leagueId);
        }

        public async Task<DraftState?> NextPick(DraftState state, long leagueId)
        {
            // Make sure Redis holds the order before we read/mutate it (recover from snapshot on miss),
            // otherwise a Redis flush could drop the draft order mid-advance.
            await _snapshot.EnsureRehydratedAsync(leagueId);

            var draftTeams = await _redis.Draft.GetDraftTeams(leagueId);

            TeamDraftBoard? teamToPick = null;
            var currentRound = draftTeams.Keys!.FirstOrDefault();

            while (teamToPick is null)
            {
                if (draftTeams!.TryGetValue(currentRound, out var teams))
                {
                    if (teams.Count != 0)
                    {
                        teamToPick = teams.Dequeue();
                        if (teams.Count == 0) draftTeams.Remove(currentRound);
                    }
                    else
                    {
                        currentRound = currentRound + 1;
                    }
                }
                else
                {
                    await EndDraft(leagueId);
                    state.DraftStatus = (int)DraftStatus.DraftEnded;
                    break;
                    //state.IsDraftStarted = true;
                }
            }

            await _redis.Draft.SetDraftTeams(draftTeams, leagueId);

            state.DraftBoardTeams = _draftService.PrepareDraftBoard(draftTeams);
            var saved = await _redis.Draft.SetDraftState(leagueId, state);

            // Checkpoint the advanced state + remaining order.
            await _snapshot.PersistAsync(leagueId);
            return saved;
        }
    }
}
