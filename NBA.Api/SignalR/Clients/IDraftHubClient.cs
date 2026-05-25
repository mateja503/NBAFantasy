using NBA.Data.Redis.Entities;
using NBA.Service.League.Draft;

namespace NBA.Api.SignalR.Clients
{
    public interface IDraftHubClient
    {
        Task UpdateDraftState(DraftState state);
        Task TeamDraftBoard(DraftBoardTeams draftBoard);
    }
}
