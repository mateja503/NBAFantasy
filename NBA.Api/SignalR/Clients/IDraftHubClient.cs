using NBA.Data.Redis.Entities;
using NBA.Service.Draft;

namespace NBA.Api.SignalR.Clients
{
    public interface IDraftHubClient
    {
        Task UpdateDraftState(DraftState state);
    }
}
