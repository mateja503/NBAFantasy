using Microsoft.AspNetCore.SignalR;
using NBA.Api.SignalR.Clients;

namespace NBA.Api.SignalR.Hubs
{
    public class ChatHub : Hub<IChatHubClient>
    {
        public async Task SendMessage(string user, string message) 
        {
            await Clients.All.ReceiveMessage(user, message);
        }
    }
}
