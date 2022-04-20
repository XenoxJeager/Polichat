using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Polichat_Backend.Resources;

namespace Polichat_Backend.LIB;

public class SocketMiddleware
{
    private readonly SocketHandler _handler;
    private readonly RequestDelegate _next;

    public SocketMiddleware(RequestDelegate next, SocketHandler handler)
    {
        _next = next;
        _handler = handler;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.WebSockets.IsWebSocketRequest)
            return;

        //id = 0 => Auth_Left
        //id = 1 => Auth_Right
        //id = 2 => Lib_Left
        //id = 3 => Lib_Right

        int.TryParse(context.Request.Query["room_id"], out var id);
        var room = Rooms.Error_Room;
        switch (id)
        {
            case 0:
                room = Rooms.Auth_Left;
                break;
            case 1:
                room = Rooms.Auth_Right;
                break;
            case 2:
                room = Rooms.Lib_Left;
                break;
            case 3:
                room = Rooms.Lib_Right;
                break;
        }

        var socket = await context.WebSockets.AcceptWebSocketAsync();
        await _handler.OnConnected(socket, room);
        await Receive(socket, async (result, buffer) =>
        {
            if (result.MessageType == WebSocketMessageType.Text)
            {
                var idSocket = _handler.Connections.GetId(socket);
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