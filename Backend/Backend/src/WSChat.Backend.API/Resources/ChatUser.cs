using System;
using Microsoft.Extensions.Primitives;
using Polichat_Backend.Properties;
using UGen;
namespace Polichat_Backend
{
    public class ChatUser
    {
        
        
        public string Id = UGenerator.Generate();
        public Rooms Room;
        public ChatUser(Rooms room)
        { 
            Room = room;
        }
    }
}