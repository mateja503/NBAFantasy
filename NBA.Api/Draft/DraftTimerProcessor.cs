using ApplicationDefaults.Options;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using NBA.Api.SignalR.Clients;
using NBA.Api.SignalR.Hubs;
using NBA.Data.Context;
using NBA.Data.Redis.Enumerations;
using NBA.Service.League.Draft;

namespace NBA.Api.Draft
{
    // The draft pick "tick", decoupled from any scheduler. Previously this lived in a Hangfire job
    // that re-scheduled itself in Postgres on every pick. It now schedules the next deadline into a
    // Redis sorted set instead; DraftTimerHostedService fires AdvanceAsync when a deadline is due.
    // Called from three places: draft start, the timer poller, and (indirectly) the hub.
    public class DraftTimerProcessor(
        IHubContext<DraftHub, IDraftHubClient> hubContext,
        DraftManager draftManager,
        DraftService draftService,
        NbaFantasyRedis redis,
        IOptions<DraftOptions> draftOptions)
    {
        private readonly IHubContext<DraftHub, IDraftHubClient> _hubContext = hubContext;
        private readonly DraftManager _draftManager = draftManager;
        private readonly DraftService _draftService = draftService;
        private readonly NbaFantasyRedis _redis = redis;
        private readonly DraftOptions _draftOptions = draftOptions.Value;

        public async Task StartDraftAsync(long leagueId)
        {
            var isDraftCompleted = await _draftService.CheckDraftCompleted(leagueId);

            // Draft state is normally created when the first client connects to the hub, but
            // start-draft can be called before that — so create it here if it doesn't exist yet
            // instead of dereferencing a null state.
            var state = await _draftManager.GetDraftState(leagueId)
                ?? await _draftManager.CreateDraftState(leagueId);

            if (isDraftCompleted)
            {
                state.DraftStatus = (int)DraftStatus.DraftCompleted;
                await _hubContext.Clients.Group(leagueId.ToString()).UpdateDraftState(state);
                return;
            }

            // Ensure the draft order (and the derived board) exist before the first auto-advance,
            // which reads them — otherwise NextPick would hit a null teams collection.
            var draft = await _draftService.DraftOrder(leagueId);
            state.DraftBoardTeams = _draftService.PrepareDraftBoard(draft);
            state.DraftStatus = (int)DraftStatus.DraftStarted;
            await _draftManager.UpdaterDraftState(leagueId, state);

            await AdvanceAsync(leagueId, nextPick: false);
        }

        // Advances the draft one tick: reset the clock, optionally move to the next pick, broadcast
        // the new state, and arm the next deadline. The per-league lock guards against the timer
        // firing at the same instant as a manual pick.
        public async Task AdvanceAsync(long leagueId, bool nextPick)
        {
            var lockToken = await _redis.Draft.TryAcquireDraftCycleLock(leagueId, TimeSpan.FromSeconds(10));
            if (lockToken is null)
                return;

            try
            {
                var state = await _draftManager.ResetTimer(leagueId);

                if (nextPick)
                {
                    state = await _draftManager.NextPick(state, leagueId);

                    if (state!.DraftBoardTeams == null)
                    {
                        await _draftService.EndDraft(leagueId);
                        await _redis.Draft.CancelDraftTimer(leagueId);
                        return;
                    }
                }

                await _hubContext.Clients.Group(leagueId.ToString()).UpdateDraftState(state!);

                await _draftManager.ArmNextDeadlineAsync(leagueId);
            }
            finally
            {
                await _redis.Draft.ReleaseDraftCycleLock(leagueId, lockToken);
            }
        }

        // Arms (or re-arms) the pick deadline. Shared by the processor and the hub so the timer is
        // scheduled the same way no matter who advanced the draft. Clamp to >= 1s so a misconfigured
        // DraftPickTime (<= 0) can't make the deadline immediately due and spin the timer poller.
        //public Task ArmNextDeadlineAsync(long leagueId)
        //{
        //    var seconds = Math.Max(1, _draftOptions.DraftPickTime);
        //    return _redis.Draft.ScheduleDraftTimer(leagueId, DateTimeOffset.UtcNow.AddSeconds(seconds));
        //}
    }
}
