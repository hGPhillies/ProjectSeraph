using DnsClient.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; 
using ProjectSeraphBackend.Application.Interfaces;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("ProjectSeraphBackend.Tests")]

namespace ProjectSeraphBackend.Application.Services
{
    public class WebSocketService : IWebSocketService
    {
        private WebSocket? _webSocket;
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private readonly ILogger<WebSocketService> _logger;

        public WebSocketService(ILogger<WebSocketService> logger)
        {
            _logger = logger;
        }

        public async Task HandleConnection(HttpContext context)
        {
            _logger.LogInformation("Alarm connection handler called");

            if (!context.WebSockets.IsWebSocketRequest)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            WebSocket newWebSocket = null;

            try
            {
                newWebSocket = await context.WebSockets.AcceptWebSocketAsync();

                _lock.EnterWriteLock();
                try
                {
                    if (_webSocket?.State == WebSocketState.Open)
                    {
                        _logger.LogInformation("Closing old connection for new client...");
                        
                        _ = Task.Run(async () =>
                        {
                            try
                            {
                                await _webSocket.CloseAsync(
                                    WebSocketCloseStatus.NormalClosure, "Replaced", CancellationToken.None);
                            }
                            catch { /* Suppress errors during disposal */ }
                        });
                    }

                    _webSocket = newWebSocket;
                    _logger.LogInformation("New WebSocket connection accepted and stored!");
                }
                finally
                {
                    _lock.ExitWriteLock();
                }

                await KeepConnectionAlive(newWebSocket);

            }
            catch (WebSocketException ex)
            {
                _logger.LogError(ex, "WebSocket error during connection setup or background run.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during WebSocket handling.");
            }
            finally
            {
                
                _lock.EnterWriteLock();
                try
                {
                    if (_webSocket == newWebSocket)
                    {
                        _webSocket = null;
                        _logger.LogInformation("WebSocket disconnected and reference cleared.");
                    }
                    
                }
                finally
                {
                    _lock.ExitWriteLock();
                }

                newWebSocket?.Dispose();
            }
        }

        internal async Task KeepConnectionAlive(WebSocket socket)
        {
            var buffer = new byte[1024];

            try
            {
                while (socket.State == WebSocketState.Open)
                {
                    var result = await socket.ReceiveAsync(
                        new ArraySegment<byte>(buffer),
                        CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await socket.CloseAsync(
                            WebSocketCloseStatus.NormalClosure, "Client closed", CancellationToken.None);
                        break;
                    }
                }
            }
            catch (WebSocketException ex)
            {
                // Log warnings for normal disconnects or immediate failures
                _logger.LogWarning(ex, "WebSocket connection terminated due to error.");
            }
            // No finally block here, as HandleAlarmSocketConnection handles cleanup.
        }

        public async Task<bool> SendToClient(object message)
        {
            _lock.EnterReadLock();
            var socketToSend = _webSocket;
            _lock.ExitReadLock();

            _logger.LogInformation("Attempting to send message to client...");

            if (socketToSend?.State == WebSocketState.Open)
            {
                try
                {
                    var json = JsonSerializer.Serialize(message);
                    var bytes = Encoding.UTF8.GetBytes(json);                    

                    await socketToSend.SendAsync(
                        new ArraySegment<byte>(bytes),
                        WebSocketMessageType.Text,
                        true,
                        CancellationToken.None);

                    _logger.LogInformation("Message sent to WebSocket client");
                    return true;
                }
                catch (WebSocketException ex)
                {
                    _logger.LogError(ex, "Failed to send WebSocket message");
                    return false;
                }
            }

            _logger.LogWarning("No active WebSocket connection to send message");
            return false;
        }

        public bool IsConnected
        {
            get
            {
                _lock.EnterReadLock();
                try
                {
                    return _webSocket?.State == WebSocketState.Open;
                }
                finally
                {
                    _lock.ExitReadLock();
                }
            }
        }
    }
}