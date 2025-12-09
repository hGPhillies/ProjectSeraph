using DnsClient.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; 
using ProjectSeraphBackend.Application.Interfaces;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;

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
            _logger.LogInformation("📡 Alarm connection handler called");

            if (!context.WebSockets.IsWebSocketRequest)
            {
                // ... (400 Bad Request logic remains)
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            WebSocket newWebSocket = null;

            try
            {
                // 1. Accept the connection (This can throw if the handshake fails)
                newWebSocket = await context.WebSockets.AcceptWebSocketAsync();

                // 2. Safely replace or close the existing connection (Write Lock)
                _lock.EnterWriteLock();
                try
                {
                    if (_webSocket?.State == WebSocketState.Open)
                    {
                        _logger.LogInformation("⚠️ Closing old connection for new client...");
                        // Non-blocking close attempt on the old socket
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
                    _logger.LogInformation("🎉 New WebSocket connection accepted and stored!");
                }
                finally
                {
                    _lock.ExitWriteLock();
                }

                // 3. BLOCKING CALL: Await the connection loop. 
                await KeepConnectionAlive(newWebSocket);

                // Execution only reaches here if KeepConnectionAlive completes (disconnect/close)
            }
            catch (WebSocketException ex)
            {
                // Catch connection errors that happen during handshake or immediately after acceptance
                _logger.LogError(ex, "❌ WebSocket error during connection setup or background run.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Unexpected error during WebSocket handling.");
            }
            finally
            {
                // 4. CRITICAL CLEANUP: Safely clear the reference regardless of how the connection terminated.
                // We only clear it if the socket we were managing is the current socket.
                _lock.EnterWriteLock();
                try
                {
                    if (_webSocket == newWebSocket)
                    {
                        _webSocket = null;
                        _logger.LogInformation("👋 WebSocket disconnected and reference cleared.");
                    }
                    // If newWebSocket is null here, it means AcceptWebSocketAsync failed, 
                    // and we didn't store anything, which is safe.
                }
                finally
                {
                    _lock.ExitWriteLock();
                }

                // Ensure the WebSocket object itself is disposed if it was successfully created
                newWebSocket?.Dispose();
            }
        }

        private async Task KeepConnectionAlive(WebSocket socket)
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
                _logger.LogWarning(ex, "❌ WebSocket connection terminated due to error.");
            }
            // No finally block here, as HandleAlarmSocketConnection handles cleanup.
        }

        public async Task<bool> SendToClient(object message)
        {
            _lock.EnterReadLock();
            var socketToSend = _webSocket;
            _lock.ExitReadLock();

            _logger.LogInformation("📨 Attempting to send message to client...");

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