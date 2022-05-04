using System.Collections.Generic;
using System.Net.WebSockets;
using Polichat_Backend.Models;

namespace Polichat_Backend.Services;

public class ConnectionService
{
    public List<UserSocket> ActiveConnections { get; } = new();

    public UserSocket RegisterSocket(Room room, WebSocket webSocket)
    {
        var userSocket = new UserSocket(room, webSocket);
        ActiveConnections.Add(userSocket);
        return userSocket;
    }
}
