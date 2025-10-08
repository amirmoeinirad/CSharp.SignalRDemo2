
using Microsoft.AspNetCore.SignalR;

namespace SignalRDemo
{
    public class NotificationHub : Hub
    {
        // Timer to send messages periodically
        private static Timer? _timer;

        // 'IHubContext' to access the hub context for sending messages to clients
        // Hub context  includes information about all connected clients.
        // Hub context keeps infor about the hub lifecycle when a method such as a timer is used outside the hub class.
        // Since the timer runs every 5 minutes apart from the Hub class, it needs to access the hub context to send messages to clients.
        // IHubContext provides necessary information for the timer.
        private static IHubContext<NotificationHub>? _hubContext;

        // Dependency Injection (DI) by constructor
        public NotificationHub(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        // Static constructor to initialize the timer
        // Static constructors are called once, when the class is first accessed.
        static NotificationHub()
        {
            // Start sending messages every 5 seconds
            // The server automatically sends/pushes messages to all clients every 5 seconds.
            _timer = new Timer(async _ => await SendMessageToClients(), null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        }

        // A custom method to send messages to all connected clients
        public static async Task SendMessageToClients()
        {
            if (_hubContext != null)
            {
                // 'ReceiveMessage' is the method name that will be triggered on the client side.
                // 'ReceiveMessage' can also be interpreted as the event name on the client side.
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", $"Message from Server. Server Time: {DateTime.Now:T}");
            }
        }
    }

}
