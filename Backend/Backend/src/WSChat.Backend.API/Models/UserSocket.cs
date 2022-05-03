using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UGen;

namespace Polichat_Backend.Resources;

public class UserSocket
{
    public string Id { get; } = UGenerator.Generate();

    public string Name =>
        Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(string.Join(" ", Id.Split("_")));

    public Room Room { get; }
    public WebSocket WebSocket { get; }
    
    public UserSocket(Room room, WebSocket webSocket)
    {
        Room = room;
        WebSocket = webSocket;
    }

    public async Task<(WebSocketReceiveResult Result, ArraySegment<byte> Text)> Receive()
    {
        var buffer = new byte[1500];
        var res = await WebSocket.ReceiveAsync(buffer, CancellationToken.None);
        return (res, buffer[..res.Count]);
    }

    public async Task Send(ArraySegment<byte> data)
    {
        if (WebSocket.State != WebSocketState.Open)
            return;
        
        await WebSocket.SendAsync(
            data, 
            WebSocketMessageType.Text, 
            WebSocketMessageFlags.EndOfMessage, 
            CancellationToken.None
            );
    }
}