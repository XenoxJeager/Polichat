using UGen;
namespace Polichat_Backend
{
    public class ChatUser
    {
        private string id;
        public string Id
        {
            get
            {
                return id; 
                
            }
            set
            {
                id =  UGenerator.Generate();
            }
        }

        public string room = "AuthRíght";
    }
}