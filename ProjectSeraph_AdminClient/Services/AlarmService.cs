using ProjectSeraph_AdminClient.Model;
using ProjectSeraph_AdminClient.View;
using ProjectSeraph_AdminClient.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectSeraph_AdminClient.Services
{
    public class AlarmService : IDisposable
    {
        private readonly IWebSocketClientService _webSocketClientService;

        public AlarmService(IWebSocketClientService webSocketClientService)
        {
            _webSocketClientService = webSocketClientService 
                ?? throw new ArgumentNullException(nameof(webSocketClientService));

            _webSocketClientService.AlarmReceived += OnAlarmReceived;
            _webSocketClientService.ConnectionChanged += OnConnectionChanged;

            StartConnection();
        }     
        
        private async void StartConnection()
        {
            try
            {
                await _webSocketClientService.ConnectAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"FATAL Alarm connection error: {e.Message}");
            }
        }

        private void OnAlarmReceived(AlarmMessage alarmMessage)
        {
            var alarm = new Alarm
            {
                CitizenId = alarmMessage.CitizenId,
                CitizenName = alarmMessage.CitizenName,
                Timestamp = alarmMessage.Timestamp
            };
            
            
            var alarmPopUpWindow = new AlarmPopUpWindow(alarm);
            alarmPopUpWindow.Show();
            
        }

        private void OnConnectionChanged(bool isConnected, string message)
        {
            // Handle connection status changes if needed
            Console.WriteLine($"Alarm Service: {message}");
        }

        public bool IsConnected => _webSocketClientService.IsConnected;

        private void ShowAlarmWindow(Alarm alarm)
        {
            var alarmPopUpWindow = new AlarmPopUpWindow(alarm); 
            alarmPopUpWindow.Show();
        }

        public void TestAlarm()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var testAlarm = new Alarm
                {
                    CitizenId = "999",
                    CitizenName = "Test Citizen",
                    Timestamp = DateTime.Now
                };
                
                ShowAlarmWindow(testAlarm);
            });
        }

        public void Dispose()
        {
            _webSocketClientService?.Dispose();
        }
        
    }
}
