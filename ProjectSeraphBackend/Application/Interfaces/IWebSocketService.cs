using MongoDB.Bson.IO;
using System.Net.WebSockets;

namespace ProjectSeraphBackend.Application.Interfaces
{
    public interface IWebSocketService
    {
        Task HandleConnection(HttpContext context);
        Task<bool> SendToClient(object message);
        bool IsConnected { get; }
    }
}
