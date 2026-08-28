using ApplicationDefaults.Exceptions;
using ApplicationDefaults.Options;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NBA.Data.Context;
using NBA.Data.Redis.Dtos;
using NBA.Data.Redis.Entities;
using NBA.Data.Redis.Enumerations;

namespace NBA.Service.League.Draft
{
    public class DraftManager(NbaFantasyContext context,
        IOptions<DraftOptions> draftOptions,
        NbaFantasyRedis redis, DraftService draftService, DraftSnapshotService snapshot)
    {
        private readonly NbaFantasyContext _context = context;
        private readonly DraftOptions _draftOptions = draftOptions.Value;
        private readonly NbaFantasyRedis _redis = redis;
        private readonly DraftService _draftService = draftService;
        private readonly DraftSnapshotService _snapshot = snapshot;

        public async Task<DraftState> CreateDraftState(long leagueId)
        {
            var leagueName = await _context.GetAllLeagues().Where(u => u.Leagueid == leagueId).Select(u => u.Name).SingleOrDefaultAsync();

            var state = new DraftState
            {
                LeagueName = leagueName ?? "NO LEAGUE",
                PickEndTime = DateTime.UtcNow,
                DraftStatus = (int)DraftStatus.Initial,
                DraftBoardTeams = new DraftBoardTeams { CurrentRound = 1 },
            };
            await _redis.League(leagueId).Draft.SetState(state);
            await _snapshot.PersistAsync(leagueId);

            return state;
        }

        public async Task<DraftState> UpdaterDraftState(long leagueId, DraftState state)
        {
            var saved = await _redis.League(leagueId).Draft.SetState(state);
            await _snapshot.PersistAsync(leagueId);
            return saved;
        }

        public async Task<DraftState?> GetDraftState(long leagueId)
        {
            // Restore from the Postgres snapshot first if Redis lost the draft.
            await _snapshot.EnsureRehydratedAsync(leagueId);
            return await _redis.League(leagueId).Draft.GetState();
        }
        public async Task<DraftState> ResetTimer(long leagueId)
        {
            // Recover from the durable snapshot if Redis lost the state, then fail loudly rather than
            // serializing a null state back into Redis (which previously stored the literal "null").
            await _snapshot.EnsureRehydratedAsync(leagueId);

            var draft = _redis.League(leagueId).Draft;

            var state = await draft.GetState()
                ?? throw new NBAException($"No active draft state for league {leagueId}", ErrorCodes.DataBaseRecordNotFound);

            // Keep the displayed deadline aligned with the (clamped) timer deadline.
            state.PickEndTime = DateTime.UtcNow.AddSeconds(Math.Max(1, _draftOptions.DraftPickTime));
            await draft.SetState(state);
            return state;
        }

        public async Task EndDraft(long leagueId)
        {
            var league = _redis.League(leagueId);

            // Remove any pending pick deadline from the timer sorted set.
            await league.Draft.CancelTimer();

            // Writes the drafted rosters into Postgres — it reads DraftedPlayersPerTeam off draft:state,
            // so it has to run before the Redis clean-up below.
            await _draftService.EndDraft(leagueId);

            // The draft board, the available player pool and the per-team roster sets are draft-time
            // scratch data; once the rosters are in Teamplayer there is nothing left to draft from.
            var teamIds = await _context.GetAllTeams()
                .Where(t => t.Leagueid == leagueId)
                .Select(t => t.Teamid)
                .ToListAsync();

            await league.Players.DeleteDraftPlayers(teamIds);

            _ = await league.Draft.DeleteState();
            await league.Draft.DeleteTeams();
            await _snapshot.DeleteAsync(leagueId);
        }

        // Strips a state down to the single shape clients get once a draft is over: status DraftEnded,
        // no board (so no team is left sitting on the clock), no available players, no drafted rosters,
        // and a deadline in the past so the displayed clock settles on 00:00.
        //
        // Called from NextPick when the draft order runs out — the last pick of the last round — and
        // from BuildEndedState, which serves both the end-draft endpoint and a client reconnecting to
        // an already-finished draft. Between them those cover every way a draft ends.
        public static void MarkEnded(DraftState state)
        {
            state.DraftStatus = (int)DraftStatus.DraftEnded;
            state.DraftBoardTeams = null;
            state.DraftPlayers = null;
            //if this is set the player don't get written in postgres 
            //state.DraftedPlayersPerTeam = null;
            state.PickEndTime = DateTime.UtcNow;

        }

        // Builds the payload announcing a finished draft, reusing the live state when there still is one
        // so the league name and drafted rosters survive. Deliberately writes nothing back to Redis or
        // the snapshot table — EndDraft removes those keys and a finished draft must not resurrect them.
        public async Task<DraftState> BuildEndedState(long leagueId)
        {
            var state = await _redis.League(leagueId).Draft.GetState();

            if (state is null)
            {
                var leagueName = await _context.GetAllLeagues().Where(u => u.Leagueid == leagueId)
                    .Select(u => u.Name).SingleOrDefaultAsync();

                state = new DraftState { LeagueName = leagueName ?? "NO LEAGUE" };
            }

            MarkEnded(state);
            return state;
        }

        public async Task<DraftState?> NextPick(DraftState state, long leagueId)
        {
            // Make sure Redis holds the order before we read/mutate it (recover from snapshot on miss),
            // otherwise a Redis flush could drop the draft order mid-advance.
            await _snapshot.EnsureRehydratedAsync(leagueId);

            var draft = _redis.League(leagueId).Draft;

            // Rehydration above is the last chance to get the order back; without it there is nothing to
            // advance, so fail loudly rather than dereferencing null.
            var draftTeams = await draft.GetTeams()
                ?? throw new NBAException($"No draft order for league {leagueId}", ErrorCodes.DataBaseRecordNotFound);

            TeamDraftBoard? teamToPick = null;
            var currentRound = draftTeams.Keys.FirstOrDefault();

            while (teamToPick is null)
            {
                if (draftTeams.TryGetValue(currentRound, out var teams))
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
                    // No round left to pull a team from — the order is exhausted.
                    break;
                }
            }

            await draft.SetTeams(draftTeams);

            // An emptied order means that was the last pick of the last round. Flag it here rather than a
            // tick later, and let the caller run EndDraft once the advanced state has been persisted —
            // ending it from inside this method deleted the Redis keys that the writes below recreate.
            // Note this is the reliable signal: PrepareDraftBoard also returns null for an Offline draft
            // (round key 0), which is not the same thing.
            if (draftTeams.Count == 0)
                MarkEnded(state);
            else
                state.DraftBoardTeams = _draftService.PrepareDraftBoard(draftTeams);

            var saved = await draft.SetState(state);

            // Checkpoint the advanced state + remaining order.
            await _snapshot.PersistAsync(leagueId);
            return saved;
        }

        public async Task<DraftState> AddTeamsDrafterPlayersToDraftState(DraftState state)
        {
            var teamId = state.DraftBoardTeams!.onTheClockTeam!.TeamId;
            var teamsDraftedPlayers = await _redis.Player.GetTeamsDraftedPlayers(teamId);

            if (teamsDraftedPlayers is not null)
            {
                state.DraftedPlayersPerTeam ??= new Dictionary<long, List<PlayerShortDto>>();
                // The roster comes off the player cache as entities; the draft state holds the client shape.
                state.DraftedPlayersPerTeam[teamId] = teamsDraftedPlayers.ToPlayerShortDtos();
            }

            return state;
        }

        // Arms (or re-arms) the pick deadline. Shared by the processor and the hub so the timer is
        // scheduled the same way no matter who advanced the draft. Clamp to >= 1s so a misconfigured
        // DraftPickTime (<= 0) can't make the deadline immediately due and spin the timer poller.
        public Task ArmNextDeadlineAsync(long leagueId)
        {
            var seconds = Math.Max(1, _draftOptions.DraftPickTime);
            return _redis.League(leagueId).Draft.ScheduleTimer(DateTimeOffset.UtcNow.AddSeconds(seconds));
        }
    }
}
