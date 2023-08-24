using Microsoft.AspNetCore.SignalR;

namespace WebApplication3.Hubs
{
    public class ChatHub : Hub
    {
        private static Dictionary<string, string> _users = new Dictionary<string, string>();

        public override async Task OnConnectedAsync()
        {
            await SendMessage("Server", $"User connected!");
            await base.OnConnectedAsync();
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task AddUserConnectionId(string name)
        {
            _users.Add(name, Context.ConnectionId);
            var online_users = _users.OrderBy(x => x.Key).Select(x => x.Key).ToArray();
            await Clients.All.SendAsync("OnlineUsers", online_users);
        }
    }
}
