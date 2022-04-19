
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Polichat_Backend.Properties;


namespace Polichat_Backend
{

   
    
        public class SocketMessageHandler:SocketHandler
        {
            public SocketMessageHandler(ConnectionService connections) : base(connections)
            {
            }

            public override async Task OnConnected(WebSocket socket,Rooms r)
            {
                await base.OnConnected(socket,r);
                var sid = Connections.GetId(socket);
                await SendMessageToAll($"{sid} just joined the {Connections.GetRoomID(socket)} Chatroom remain civil !!",socket);
            }

            public override async Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
            {
                var idSocket = Connections.GetId(socket);
                var message = $"{idSocket} said:{Encoding.UTF8.GetString(buffer, 0, result.Count)}";
                await SendMessageToAll(message,socket);
            }
        }
    }
