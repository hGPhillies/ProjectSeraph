using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using ProjectSeraphBackend.Application.Services;
using System;
using System.Net.WebSockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
            public bool ThrowOnReceive { get; set; } = false;
            public Queue<WebSocketReceiveResult> ReceiveResults { get; } = new();
            public bool WasClosed { get; private set; } = false;
            public override WebSocketState State => CurrentState;
            public override string? SubProtocol => null;
            public override WebSocketCloseStatus? CloseStatus => null;
            public override string? CloseStatusDescription => null;
            public override void Abort() { CurrentState = WebSocketState.Closed; }
            // CloseAsync method
            public override Task CloseAsync(WebSocketCloseStatus closeStatus, string? statusDescription, CancellationToken cancellationToken)
            {  
                CurrentState = WebSocketState.Closed;
                WasClosed = true;
                return Task.CompletedTask;
            }

            // CloseOutputAsync method
            public override Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string? statusDescription, CancellationToken cancellationToken)
            {
                WasClosed = true;
                CurrentState = WebSocketState.Closed;
                return Task.CompletedTask;
            }
            // Dispose method
            public override void Dispose() { CurrentState = WebSocketState.Closed; }

            public override Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
            {
                if (ThrowOnReceive)
                    throw new WebSocketException("Receive failed (test)");
                if (ReceiveResults.Count == 0)
                    return Task.FromResult(new WebSocketReceiveResult(0, WebSocketMessageType.Close, true));

                return Task.FromResult(ReceiveResults.Dequeue());
                //throw new NotSupportedException();
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

        [Fact]
        // Test that KeepConnectionAlive processes messages while the socket is open
        public async Task KeepConnectionAlive_ProcessesMessages_WhileOpen()
        {
            var logger = new TestLogger<WebSocketService>();
            var service = new WebSocketService(logger);
            var testSocket = new TestWebSocket();

            // Setup a close message to end the loop
            testSocket.ReceiveResults.Enqueue(new WebSocketReceiveResult(
                0, WebSocketMessageType.Close, true, WebSocketCloseStatus.NormalClosure, null));

            await service.KeepConnectionAlive(testSocket);

            Assert.False(testSocket.WasClosed); // Service should NOT close on client close
        }

        [Fact]
         // Test that KeepConnectionAlive exits when the socket is not open
        public async Task KeepConnectionAlive_ExitsLoop_WhenSocketNotOpen()
        {
            // Arrange
            var logger = new TestLogger<WebSocketService>();
            var service = new WebSocketService(logger);
            var testSocket = new TestWebSocket { CurrentState = WebSocketState.Closed };

            //Act, Should exit immediately without processing
            await service.KeepConnectionAlive(testSocket);

            // Assert no receive attempts were made
            Assert.Empty(testSocket.ReceiveResults); // Should not try to receive
        }

        [Fact]
        // Test that KeepConnectionAlive closes the socket on receiving a close message
        public async Task KeepConnectionAlive_ClosesSocket_OnClientCloseMessage()
        {
            // Arrange
            var logger = new TestLogger<WebSocketService>();
            var service = new WebSocketService(logger);
            var testSocket = new TestWebSocket
            {
                CurrentState = WebSocketState.Open
            };

            //Setup a close message
            testSocket.ReceiveResults.Enqueue(new WebSocketReceiveResult(
                0, WebSocketMessageType.Close, true, WebSocketCloseStatus.NormalClosure, null));
            // Act, Call the method
            await service.KeepConnectionAlive(testSocket);

            // Assert, Verify CloseAsync was called
            Assert.True(testSocket.WasClosed);
        }

        [Fact]
        // Test that KeepConnectionAlive logs a warning on WebSocketException
        public async Task KeepConnectionAlive_LogsWarning_OnWebSocketException()
        {
            // Arrange  
            var logger = new TestLogger<WebSocketService>();
            var service = new WebSocketService(logger);
            var testSocket = new TestWebSocket
            {
                CurrentState = WebSocketState.Open,
                ThrowOnReceive = true
            };
            // Act, Setup a receive that will throw
            await service.KeepConnectionAlive(testSocket);
            // Assert that a warning was logged
            Assert.Contains(logger.LogEntries,
                e => e.LogLevel == LogLevel.Warning &&
                     e.Message.Contains("WebSocket connection terminated"));
        }

 
    }

    // Helper test logger to capture log entries, needed because WebSocketService uses logging 
    public class TestLogger<T> : ILogger<T>
    {
        public List<LogEntry> LogEntries { get; } = new();
        public IDisposable BeginScope<TState>(TState state) => null;
        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
            Exception exception, Func<TState, Exception, string> formatter)
        {
            LogEntries.Add(new LogEntry(logLevel, formatter(state, exception), exception));
        }

        public record LogEntry(LogLevel LogLevel, string Message, Exception Exception);
    }
}

