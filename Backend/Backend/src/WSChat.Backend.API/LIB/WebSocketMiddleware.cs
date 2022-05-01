using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Polichat_Backend.Resources;
using static System.Int32;

namespace Polichat_Backend.LIB;

public class WebSocketMiddleware : IMiddleware
{
    private readonly UserSocketHandler _handler;

    public WebSocketMiddleware(UserSocketHandler handler)
    {
        _handler = handler;

    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (!context.WebSockets.IsWebSocketRequest)
            return;
        
        var socket = await context.WebSockets.AcceptWebSocketAsync();

        TryParse(context.Request.Query["room_id"], out var id);
        Room room = (Room) id;
        
        _handler.RegisterSocket(room, socket);
    }
}
