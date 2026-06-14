using ApplicationDefaults.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using NBA.Api.SignalR.Clients;
using NBA.Data.Context;
using NBA.Data.Redis.Entities;
using NBA.Service.League.Draft;
using NBA.Service.Player;

namespace NBA.Api.SignalR.Hubs
{
    [Authorize]
    public class DraftHub(DraftManager draftManager, NbaFantasyRedis redis,
        PlayerManager playerManager,
        DraftService draftService, IOptions<DraftOptions> draftOptions) : Hub<IDraftHubClient>
    {
        private readonly DraftManager _draftManager = draftManager;
        private readonly NbaFantasyRedis _redis = redis;
        private readonly DraftService _draftService = draftService;
        private readonly PlayerManager _playerManager = playerManager;
        private readonly DraftOptions _draftOptions = draftOptions.Value;

        private Task ArmPickDeadline(long leagueId) =>
            _redis.Draft.ScheduleDraftTimer(leagueId, DateTimeOffset.UtcNow.AddSeconds(_draftOptions.DraftPickTime));
        // 1. Send state to a user the moment they connect/refresh
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var leagueIdString = long.TryParse(httpContext?.Request.Query["leagueId"], out long leagueId);

            await _draftService.CheckDraftCompleted(leagueId);

            if (leagueIdString)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, leagueId.ToString());
            }

            var state = await _draftManager.GetDraftState(leagueId) ?? await _draftManager.CreateDraftState(leagueId);

            var draft = await _draftService.DraftOrder(leagueId);
            state.DraftBoardTeams = _draftService.PrepareDraftBoard(draft);

            state.DraftPlayers = await _playerManager.GetPlayersOnDraftBoard(leagueId);

            await _draftManager.UpdaterDraftState(leagueId, state);

            await Clients.Caller.UpdateDraftState(state!);
            await base.OnConnectedAsync();
        }
        // 2. Action called by the Commissioner refresh the timer for user that requested it
        public async Task<DraftState> ResetTimer(long leagueId)
        {
            var state = await _draftManager.ResetTimer(leagueId);

            await Clients.Group(leagueId.ToString()).UpdateDraftState(state);

            // Re-arming the deadline is a single ZADD that overwrites the league's existing score.
            await ArmPickDeadline(leagueId);

            return state;
        }


        public async Task DraftPlayer(long leagueId, long playerId, int pick) 
        {
            await _playerManager.AddDraftedPlayers(leagueId, playerId, pick);

            var state = await _draftManager.ResetTimer(leagueId);

            state = await _playerManager.AddTeamsDrafterPlayersToDraftState(state);

            await _draftManager.NextPick(state, leagueId);

            state.DraftPlayers = await _playerManager.GetPlayersOnDraftBoard(leagueId);

            await Clients.Group(leagueId.ToString()).UpdateDraftState(state);

            await ArmPickDeadline(leagueId);
        }


    }
}
