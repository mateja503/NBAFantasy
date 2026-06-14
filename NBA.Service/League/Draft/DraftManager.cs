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
        NbaFantasyRedis redis, DraftService draftService)
    {
        private readonly NbaFantasyContext _context = context;
        private readonly JsonSerializerOptions _jsonOptions = jsonOptions.Value.SerializerOptions;
        private readonly DraftOptions _draftOptions = draftOptions.Value;
        private readonly NbaFantasyRedis _redis = redis;
        private readonly DraftService _draftService = draftService;
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

            return currentState;
        }

        public async Task<DraftState> UpdaterDraftState(long leagueId, DraftState state) 
        {
            return await _redis.Draft.SetDraftState(leagueId, state);
        }

        public async Task<DraftState?> GetDraftState(long leagueId) 
        {
            return await _redis.Draft.GetCurrentDraftState(leagueId);
        }
        public async Task<DraftState> ResetTimer(long leagueId, int seconds = 60) 
        {
            var state = await _redis.Draft.GetCurrentDraftState(leagueId);
            seconds = _draftOptions.DraftPickTime;
            state?.PickEndTime = DateTime.UtcNow.AddSeconds(seconds);
            //state!.DraftStatus = (int)DraftStatus.Paused;
            await _redis.Draft.SetDraftState(leagueId, state!);
            return state!;
        }

        public async Task EndDraft(long leagueId)
        {
            // Remove any pending pick deadline from the timer sorted set.
            await _redis.Draft.CancelDraftTimer(leagueId);

            await _draftService.EndDraft(leagueId);

            _ = await _redis.Draft.DeleteStringDraftState(leagueId);
            await _redis.Draft.DeleteDraftTeams(leagueId);
        }

        public async Task<DraftState?> NextPick(DraftState state, long leagueId)
        {
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
            return await _redis.Draft.SetDraftState(leagueId, state);
        }
    }
}
