using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSeraph_AdminClient.ViewModel
{
    public interface IWebSocketClientService : IDisposable
    {
        event Action<Model.AlarmMessage> AlarmReceived;
        event Action<bool, string> ConnectionChanged;
        bool IsConnected { get; }
        Task ConnectAsync();
        Task DisconnectAsync();
    }
}
