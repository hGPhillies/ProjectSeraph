using ProjectSeraphBackend.Application.Interfaces;
using ProjectSeraphBackend.Domain;

namespace ProjectSeraphBackend.FrameworksAndDrivers.Endpoints
{
    public static class AlarmWebSocketEndpoints
    {
        public static void MapAlarmWebSocket(this IEndpointRouteBuilder app)
        {
            app.MapGet("/ws/alarms", async (HttpContext context, IWebSocketService webSocketService) => 
            {
                await webSocketService.HandleConnection(context);
            });

            // Test Endpoint
            app.MapPost("/test/alarm", async (IWebSocketService webSocketService) =>
            {
                var testAlarm = new
                {
                    AlarmType = "Red",
                    DateTime = DateTime.UtcNow,
                    
                };

                var sent = await webSocketService.SendToClient(testAlarm);

                return sent ? Results.Ok(new {Success = true}) : Results.BadRequest(new {Success = false});
            });

            app.MapGet("/ws/status", (IWebSocketService webSocketService) =>
            {
                return Results.Ok(new
                {
                    Connected = webSocketService.IsConnected,
                    Timestamp = DateTime.UtcNow,
                    Message = webSocketService.IsConnected
                        ? "WebSocket client is connected"
                        : "No WebSocket client connected"
                });
            });
        }
    }
}
