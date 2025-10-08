
// Amir Moeini Rad
// October 2025

// Main Concept: Setting up a SignalR server in ASP.NET Core

// In this example, a message is sent from the server to all connected clients every 5 seconds.
// Since the index.html file is located in the wwwroot folder, it is served as the default file when accessing the root URL.
// In other words, when you navigate to http://localhost:5051 or https://localhost:7122, the index.html file is automatically served.

namespace SignalRDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("https://localhost:7122")
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                      .AllowCredentials());
            });

            // Registering the SignalR package.
            builder.Services.AddSignalR();

            // ------------------------------------

            var app = builder.Build();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseCors("AllowSpecificOrigin");

            app.UseRouting();

            // Enabling the NotificationHub class or the WebSocket
            // Maps SignalR hub endpoint to http(s)://www.domain.com/NotificationHub.
            app.MapHub<NotificationHub>("/NotificationHub");

            // Default endpoint for the first page.
            app.MapGet("/", () => "Hello World from the Backend!");

            app.Run();
        }
    }
}
