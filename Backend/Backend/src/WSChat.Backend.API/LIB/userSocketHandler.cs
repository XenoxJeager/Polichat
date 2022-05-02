using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Polichat_Backend.Resources;

namespace Polichat_Backend.LIB;

public class UserSocketHandler
{
    private List<UserSocket> UserSockets { get; } = new();

    public async Task RegisterSocket(Room room, WebSocket webSocket)
    {
        var userSocket = new UserSocket(room, webSocket);
        await HandleUserSocket(userSocket);
    }

    private async Task HandleUserSocket(UserSocket userSocket)
    {
        UserSockets.Add(userSocket);
        await Task.Delay(1000);
        await BroadcastIndiscriminate(userSocket.Room, GetMessage("admin", $"{userSocket.Name} joined the chat!"));
        
        while (userSocket.WebSocket.State == WebSocketState.Open)
        {
            var (result, text) = await userSocket.Receive();
            
            if (result.MessageType == WebSocketMessageType.Close)
                break;
            
            var str = Encoding.UTF8.GetString(text);

            if (string.IsNullOrEmpty(str))
                continue;

            await BroadcastDiscriminate(userSocket, $"{userSocket.Name}: {str}");
        }

        UserSockets.Remove(userSocket);
        await BroadcastIndiscriminate(userSocket.Room, GetMessage("admin", $"{userSocket.Name} left the chat!"));
    }

    private async Task BroadcastIndiscriminate(Room room, ArraySegment<byte> text)
    {
        foreach (var socket in GetInRoom(room))
            await socket.Send(text);
    }
    
    private async Task BroadcastDiscriminate(UserSocket author, string text)
    {
        foreach (var socket in GetInRoom(author.Room))
        {
            var type = socket.Id == author.Id ? "local" : "remote";
            await socket.Send(GetMessage(type, text));
        }
    }

    private IEnumerable<UserSocket> GetInRoom(Room room) => 
        UserSockets.Where(socket => socket.Room == room);
    
    private static ArraySegment<byte> GetMessage(string type, string text) => 
        Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(
            new { Type = type, Text = text },
            new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            }));
}