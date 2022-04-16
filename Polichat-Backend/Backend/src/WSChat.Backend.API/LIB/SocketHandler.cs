using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Polichat_Backend
{
    public abstract class SocketHandler
    {
        public ConnectionService Connections { get; set; }

        public SocketHandler(ConnectionService connections)
        {
            Connections = connections;
        }

        public virtual async Task OnConnected(WebSocket socket)
        {
            await Task.Run(() =>
            {
                Connections.AddSocket(socket);
            });
        }

        public virtual async Task OnDisconnected(IDisposable socket)
        {
            await Connections.RemoveSocketAsync(Connections.GetId((WebSocket)socket));
        }

        public abstract Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
        //public abstract Task Receive(string idSocket, int length, byte[] buffer);

        public async Task Send(WebSocket socket, string message)
        {
            if (socket == null)
                return;
            if (socket.State != WebSocketState.Open)
                return;

            var data = Encoding.UTF8.GetBytes(message);
            await socket.SendAsync(new ArraySegment<byte>(data, 0, data.Length), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task SendMessage(string id, string message)
        {
            await Send(Connections.GetSocketById(id), message);
        }

        public async Task SendMessageToAll(string message,WebSocket s)
        {
            ChatUser sender = Connections.GetChat(s);
            
            foreach (var connection in Connections.GetAllConnections())
            {
                if (sender.Room == connection.Key.Room)
                {
                    await Send(connection.Value, message);
                    
                }
                
            }
        }

    }
}
