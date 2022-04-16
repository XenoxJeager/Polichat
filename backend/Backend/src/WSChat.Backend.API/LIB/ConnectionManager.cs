using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using UGen;

namespace Polichat_Backend
{
    public class ConnectionService
    {
        private ConcurrentDictionary<ChatUser, WebSocket> _connections = new ConcurrentDictionary<ChatUser, WebSocket>();

        public ConcurrentDictionary<ChatUser, WebSocket> GetAllConnections()
        {
            return _connections;
        }

        // private string GenerateConnectionId()
        // {
        //     return UGen.UGenerator.Generate();
        // }

        public void AddSocket(WebSocket socket)
        {
            _connections.TryAdd(new ChatUser(), socket);
        }

        public async Task RemoveSocketAsync(string id)
        {
            _connections.TryRemove(GetIdofUser(id), out var socket);
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "socket connection closed", CancellationToken.None);
        }

        public WebSocket GetSocketById(string id)
        {
            return _connections.FirstOrDefault(x => x.Key.Id == id).Value;
        }

        public string GetRoomID(IDisposable socket)
        {
            return _connections.FirstOrDefault(x => x.Value == (WebSocket)socket).Key.Room.ToString();
        }

        public ChatUser GetIdofUser(string id)
        {
            return _connections.FirstOrDefault(x => x.Key.Id == id).Key;
        }
        public string GetId(IDisposable socket)
        {
            return _connections.FirstOrDefault(x => x.Value == (WebSocket)socket).Key.Id;
        }

        public ChatUser GetChat(IDisposable socket)
        {
            return _connections.FirstOrDefault(x => x.Value == (WebSocket)socket).Key;
        }
    }
}
