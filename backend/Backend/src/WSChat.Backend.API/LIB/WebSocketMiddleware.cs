using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Polichat_Backend
{
    public class SocketMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SocketHandler _handler;

        public SocketMiddleware(RequestDelegate next, SocketHandler handler)
        {
            _next = next;
            _handler = handler;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
                return;

            var socket = await context.WebSockets.AcceptWebSocketAsync();
            await _handler.OnConnected(socket);
            await Receive(socket, async (result, buffer) =>
            {
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    string idSocket = _handler.Connections.GetId(socket);
                    await _handler.Receive(socket, result, buffer);
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _handler.OnDisconnected(socket);
                }
            });

            // Just Test
            //await _next(context);
        }

        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> messageToHandler)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                messageToHandler(result, buffer);
            }
        }
    }
}
