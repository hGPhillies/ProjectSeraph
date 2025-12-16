using System;
using System.Net.WebSockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using ProjectSeraphBackend.Application.Services;
using Xunit;

namespace ProjectSepraphTest
{
    public class WebSocketServiceTest
    {
        // A test WebSocket implementation for testing purposes
        private class TestWebSocket : WebSocket
        {
            // Allows setting the current state for testing
            public WebSocketState CurrentState { get; set; } = WebSocketState.Closed;
            // Captures the last sent text message
            public string? LastSentText { get; private set; }
            // If true, SendAsync will throw a WebSocketException
            public bool ThrowOnSend { get; set; } = false;
           
            public override WebSocketState State => CurrentState;
            
            public override string? SubProtocol => null;

           
            public override WebSocketCloseStatus? CloseStatus => null;
            public override string? CloseStatusDescription => null;

            public override void Abort() { CurrentState = WebSocketState.Closed; }

            // CloseAsync method
            public override Task CloseAsync(WebSocketCloseStatus closeStatus, string? statusDescription, CancellationToken cancellationToken)
            {
                CurrentState = WebSocketState.Closed;
                return Task.CompletedTask;
            }

            // CloseOutputAsync method
            public override Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string? statusDescription, CancellationToken cancellationToken)
            {
                CurrentState = WebSocketState.Closed;
                return Task.CompletedTask;
            }
            // Dispose method
            public override void Dispose() { CurrentState = WebSocketState.Closed; }

            public override Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
            {
                throw new NotSupportedException();
            }

            // Simulates sending a message
            public override Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
            {
                if (ThrowOnSend)
                    throw new WebSocketException("Send failed (test)");

                LastSentText = Encoding.UTF8.GetString(buffer.Array ?? Array.Empty<byte>(), buffer.Offset, buffer.Count);
                return Task.CompletedTask;
            }
        }
        // Helper method to set the private _webSocket field via reflection
        private static void SetPrivateSocket(WebSocketService service, WebSocket? socket)
        {
            var field = typeof(WebSocketService).GetField("_webSocket", BindingFlags.Instance | BindingFlags.NonPublic);
            if (field == null) throw new InvalidOperationException("Could not find _webSocket field via reflection");
            field.SetValue(service, socket);
        }

        [Fact]
        // Test sending a message when there is no WebSocket connection
        public async Task SendToClient_ReturnsFalse_WhenNoConnection()
        {
            var logger = NullLogger<WebSocketService>.Instance;
            var service = new WebSocketService(logger);

            // Ensure no socket is set
            SetPrivateSocket(service, null);

            var result = await service.SendToClient(new { Text = "hello" });

            Assert.False(result);
        }

        
        [Fact]
        // Test sending a message when the WebSocket is connected
        public async Task SendToClient_SendsMessage_WhenConnected()
        {
            var logger = NullLogger<WebSocketService>.Instance;
            var service = new WebSocketService(logger);

            var testSocket = new TestWebSocket { CurrentState = WebSocketState.Open };
            SetPrivateSocket(service, testSocket);

            var payload = new { Text = "hello" };
            var result = await service.SendToClient(payload);

            Assert.True(result);
            Assert.NotNull(testSocket.LastSentText);

            // The service serializes with System.Text.Json; check serialized content
            Assert.Contains("\"Text\":\"hello\"", testSocket.LastSentText);
        }

        [Fact]
        // Test sending a message when the WebSocket send operation throws an exception
        public async Task SendToClient_ReturnsFalse_WhenSendThrowsWebSocketException()
        {
            var logger = NullLogger<WebSocketService>.Instance;
            var service = new WebSocketService(logger);

            var testSocket = new TestWebSocket { CurrentState = WebSocketState.Open, ThrowOnSend = true };
            SetPrivateSocket(service, testSocket);

            var result = await service.SendToClient(new { Text = "willfail" });

            Assert.False(result);
        }
    }
}
