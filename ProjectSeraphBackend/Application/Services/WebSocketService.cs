using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; 
using ProjectSeraphBackend.Application.Interfaces;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace ProjectSeraphBackend.Application.Services
{
    public class WebSocketService : IWebSocketService
    {
        private WebSocket? _webSocket;
        private readonly ILogger<WebSocketService> _logger;

        public WebSocketService(ILogger<WebSocketService> logger)
        {
            _logger = logger;
        }

        public async Task HandleConnection(HttpContext context)
        {
            _logger.LogInformation("📡 HandleConnection called");

            if (context.WebSockets.IsWebSocketRequest)
            {
                _logger.LogInformation("✅ It's a WebSocket request!");

                if (_webSocket?.State == WebSocketState.Open)
                {
                    _logger.LogInformation("⚠️ Closing existing connection...");
                    await _webSocket.CloseAsync(
                        WebSocketCloseStatus.NormalClosure,
                        "New connection established",
                        CancellationToken.None);
                }

                _webSocket = await context.WebSockets.AcceptWebSocketAsync();
                _logger.LogInformation("🎉 WebSocket connection accepted!");

                try
                {
                    await KeepConnectionAlive();
                }
                catch (WebSocketException ex)
                {
                    _logger.LogWarning(ex, "❌ Error in WebSocket connection");
                }
                finally
                {
                    _webSocket = null;
                    _logger.LogInformation("👋 WebSocket disconnected");
                }
            }
            else
            {
                _logger.LogWarning("❌ Not a WebSocket request");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        private async Task KeepConnectionAlive()
        {
            if (_webSocket == null) return;

            var buffer = new byte[1024];

            while (_webSocket.State == WebSocketState.Open)
            {
                var result = await _webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer),
                    CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _webSocket.CloseAsync(
                        WebSocketCloseStatus.NormalClosure,
                        "Client closed",
                        CancellationToken.None);

                    _logger.LogInformation("WebSocket connection closed by client.");
                    break;
                }
            }
        }

        public async Task<bool> SendToClient(object message)
        {
            _logger.LogInformation("📨 Attempting to send message to client...");

            if (_webSocket?.State == WebSocketState.Open)
            {
                try
                {
                    var json = JsonSerializer.Serialize(message);
                    var bytes = Encoding.UTF8.GetBytes(json);

                    await _webSocket.SendAsync(
                        new ArraySegment<byte>(bytes),
                        WebSocketMessageType.Text,
                        true,
                        CancellationToken.None);

                    _logger.LogInformation("✅ Message sent to WebSocket client");
                    return true;
                }
                catch (WebSocketException ex)
                {
                    _logger.LogError(ex, "❌ Failed to send WebSocket message");
                    return false;
                }
            }

            _logger.LogWarning("⚠️ No active WebSocket connection to send message");
            return false;
        }

        public bool IsConnected => _webSocket?.State == WebSocketState.Open;
    }
}