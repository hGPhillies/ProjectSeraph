using ProjectSeraph_AdminClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectSeraph_AdminClient.ViewModel
{
    public class WebSocketClientService : IWebSocketClientService, IDisposable
    {
        private ClientWebSocket _webSocket;
        private CancellationTokenSource _cts;
        private readonly string _webSocketUrl;

        public event Action<AlarmMessage> AlarmReceived;    
        public event Action<bool, string> ConnectionChanged;

        public bool IsConnected => _webSocket?.State == WebSocketState.Open;

        public WebSocketClientService(string webSocketUrl)
        {
            _webSocketUrl = webSocketUrl;
        }

        public async Task ConnectAsync()
        {
            try
            {
                _cts = new CancellationTokenSource();
                _webSocket = new ClientWebSocket();

                if(_webSocketUrl.StartsWith("wss", StringComparison.OrdinalIgnoreCase))
                {
                    // Ignore security checks for development
                    _webSocket.Options.RemoteCertificateValidationCallback = 
                        (sender, certificate, chain, sslPolicyErrors) => true;
                }

                ConnectionChanged?.Invoke(false, "Connecting...");

                await _webSocket.ConnectAsync(new Uri(_webSocketUrl), _cts.Token);

                if (_webSocket?.State == WebSocketState.Open) 
                {
                    ConnectionChanged?.Invoke(true, "Connected");
                    _ = Task.Run(() => ListenForAlarmsAsync(_cts.Token));
                }
                else
                {
                    ConnectionChanged?.Invoke(false, $"Connection Failed: State is {_webSocket.State}");
                }

            }
            catch (Exception ex)
            {
                ConnectionChanged?.Invoke(false, $"Connection failed: {ex.Message}");
                Console.WriteLine($"Websocket Connection Exception: {ex.Message}");
            }
        }

        private async Task ListenForAlarmsAsync(CancellationToken cancellationToken)
        {
            var buffer = new byte[1024];

            try
            {
                while (IsConnected && !cancellationToken.IsCancellationRequested)
                {
                    var result = await _webSocket.ReceiveAsync(
                        new ArraySegment<byte>(buffer), cancellationToken);

                    if(result.MessageType == WebSocketMessageType.Close || _webSocket.State != WebSocketState.Open)
                    {
                        break;
                    }

                    var messageJson = Encoding.UTF8.GetString(buffer, 0 , result.Count);
                    ProcessMessage(messageJson);
                }
            }
            catch(OperationCanceledException)
            {
                // Normal Shutdown
            }
            catch(WebSocketException)
            {
                ConnectionChanged?.Invoke(false, "Connection Lost");
            }
            catch(Exception ex)
            {
                ConnectionChanged?.Invoke(false, $"Error: {ex.Message}");
            }
            finally
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ConnectionChanged?.Invoke(false, "Disconnected");
                });

                await DisconnectAsync();
            }
        }

        private void ProcessMessage(string json)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    var alarmMessage = JsonSerializer.Deserialize<AlarmMessage>(json);

                    // All websocket messages are red alarms
                    if (alarmMessage != null)
                    {
                        AlarmReceived?.Invoke(alarmMessage);
                    }
                    else
                    {
                        Console.WriteLine($"Invalid alarm message: {json}");
                    }
                }
                catch (JsonException ex)
                {
                    // Not Json or wrong format
                    Console.WriteLine($"Failed to parse: {ex.Message}");
                    Console.WriteLine($"Received: {json}");

                }
            });
        }

        public async Task DisconnectAsync()
        {
            if(_webSocket?.State == WebSocketState.Open)
            {
                await _webSocket.CloseAsync(
                    WebSocketCloseStatus.NormalClosure,
                    "Client disconnecting",
                    CancellationToken.None);
            }

            _cts?.Cancel();
            ConnectionChanged?.Invoke(false, "Disconnected");
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _webSocket?.Dispose();
            _cts?.Dispose();
        }
    }
}
