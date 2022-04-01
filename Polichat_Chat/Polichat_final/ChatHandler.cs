
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;



namespace Polichat_final
{

   
    
        public class WebSocketMessageHandler:WebSocketHandler
        {
            public WebSocketMessageHandler(ConnectionService connections) : base(connections)
            {
            }

            public override async Task OnConnected(WebSocket socket)
            {
                await base.OnConnected(socket);
                var sid = Connections.GetId(socket);
                await SendMessageToAll($"{sid} just joined one of the Great Chats of Polichat!!");
            }

            public override async Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
            {
                var idSocket = Connections.GetId(socket);
                var message = $"{idSocket} said:{Encoding.UTF8.GetString(buffer, 0, result.Count)}";
                await SendMessageToAll(message);
            }
        }
    }
