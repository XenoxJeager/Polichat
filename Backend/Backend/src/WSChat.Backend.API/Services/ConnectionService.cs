using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Polichat_Backend.Resources;

namespace Polichat_Backend.LIB;

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
