using Microsoft.AspNetCore.SignalR.Client;

class Program
{
    private static HubConnection _hubConnection;

    public static void Main()
    {
        while (true)
        {
            string msg = Console.ReadLine();
            if (msg == "connect")
            {
                Console.Write("Write your name: ");
                string name = Console.ReadLine();
                ConnectToChat(name);
                AddUserConnectionId(name);
            }
            else if(msg == "exit")
            {
                StopConnection();
            }
            else
            {

            }
        }
    }

    public static async Task AddUserConnectionId(string name)
    {
        _hubConnection?.InvokeAsync("AddUserConnectionId", name);
    }

    public async static Task ConnectToChat(string name)
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7083/chat-hub")
            .WithAutomaticReconnect()
            .Build();

        _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            Console.WriteLine($"{user}: {message}");
        });

        _hubConnection.On<string[]>("OnlineUsers", (online_users) =>
        {
            Console.WriteLine("Пользователи чата:");
            string users = string.Join(",", online_users);
            Console.WriteLine(users);
        });

        await _hubConnection.StartAsync();
    }

    public async static Task StopConnection()
    {
        await _hubConnection.StopAsync();
    }
}