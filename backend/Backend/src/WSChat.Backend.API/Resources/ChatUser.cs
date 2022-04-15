using Polichat_Backend.Properties;
using UGen;
namespace Polichat_Backend
{
    public class ChatUser
    {
        public string Id = UGenerator.Generate();
      
        public Rooms Room = Rooms.Auth_Right;
    }
}