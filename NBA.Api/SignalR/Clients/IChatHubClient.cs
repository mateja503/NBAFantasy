namespace NBA.Api.SignalR.Clients
{
    public interface IChatHubClient
    {
        Task ReceiveMessage(string user, string message);
    }
}
