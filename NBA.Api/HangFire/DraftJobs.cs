// OBSOLETE — this file is intentionally empty and safe to delete.
//
// DraftJobs used to drive the draft pick timer via self-scheduling Hangfire jobs stored in
// Postgres. That mechanism was replaced by a Redis sorted-set delayed queue:
//   - scheduling:  NBA.Data.Redis.Operations.DraftRedisOperations (ScheduleDraftTimer / ClaimDueDraftTimer)
//   - processing:  NBA.Api.Draft.DraftTimerProcessor
//   - polling:     NBA.Api.HostedService.DraftTimerHostedService
//
// The deletion of this file was not permitted automatically; remove it from the project when
// convenient. Hangfire itself is still used for the recurring daily-games job.

//using ApplicationDefaults.Options;
//using Hangfire;
//using Microsoft.AspNetCore.SignalR;
//using Microsoft.Extensions.Options;
//using NBA.Api.SignalR.Clients;
//using NBA.Api.SignalR.Hubs;
//using NBA.Data.Context;
//using NBA.Data.Redis.Entities;
//using NBA.Data.Redis.Enumerations;
//using NBA.Service.League.Draft;

//namespace NBA.Api.HangFire
//{
//    public class DraftJobs(IHubContext<DraftHub, IDraftHubClient> hubContext, DraftManager draftManager,
//        IBackgroundJobClient backgroundJobClient, DraftService draftService, NbaFantasyRedis redis,
//        IOptions<DraftOptions> draftOptions)
//    {
//        private readonly IHubContext<DraftHub, IDraftHubClient> _hubContext = hubContext;
//        private readonly DraftManager _draftManager = draftManager;
//        private readonly IBackgroundJobClient _backgroundJobClient = backgroundJobClient;
//        private readonly DraftService _draftService = draftService;
//        private readonly DraftOptions _draftOptions = draftOptions.Value;
//        private readonly NbaFantasyRedis _redis = redis;
//        public async Task StartDraft(long leagueId)
//        {
//            var isDraftCompleted = await draftService.CheckDraftCompleted(leagueId);

//            var state = await _draftManager.GetDraftState(leagueId);

//            if (isDraftCompleted)
//            {
//                state!.DraftStatus = (int)DraftStatus.DraftCompleted;
//                await _hubContext.Clients.Group(leagueId.ToString()).UpdateDraftState(state!);
//                return;
//            }


//            state!.DraftStatus = (int)DraftStatus.DraftStarted;
//            state = await _draftManager.UpdaterDraftState(leagueId, state);

//            await DraftCycle(leagueId, false, state);
//        }
//        public async Task DraftCycle(long leagueId, bool nextPick, DraftState? state = null)
//        {
//            // Guard the advance-and-reschedule critical section so the timer firing and a
//            // simultaneous manual pick (or two Hangfire servers) can't both move the draft on.
//            var lockToken = await _redis.Draft.TryAcquireDraftCycleLock(leagueId, TimeSpan.FromSeconds(10));
//            if (lockToken is null)
//                return;

//            try
//            {
//                state = await _draftManager.ResetTimer(leagueId);

//                if (nextPick)
//                {
//                    state = await _draftManager.NextPick(state, leagueId);

//                    if (state!.DraftBoardTeams == null)
//                    {
//                        await _draftService.EndDraft(leagueId);
//                        return;
//                    }
//                }

//                await _hubContext.Clients.Group(leagueId.ToString()).UpdateDraftState(state!);

//                var jobId = _backgroundJobClient.Schedule<DraftJobs>(job => job.DraftCycle(leagueId, true), TimeSpan.FromSeconds(_draftOptions.DraftPickTime));
//                await _redis.Draft.SetDraftTimerJobId(leagueId, jobId);
//            }
//            finally
//            {
//                await _redis.Draft.ReleaseDraftCycleLock(leagueId, lockToken);
//            }
//        }
//    }
//}
