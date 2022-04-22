using UGen;

namespace Polichat_Backend.Resources;

public class ChatUser
{
    public string Id = UGenerator.Generate();
    public Rooms Room;

    public ChatUser(Rooms room)
    {
        Room = room;
    }
}