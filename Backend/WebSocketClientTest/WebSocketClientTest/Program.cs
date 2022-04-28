using WebSocketSharp;
using System;
using Newtonsoft.Json;
namespace WebSocketClientTest
{
    
    internal class Program
    {
        public static void Main(string[] args)

        {
            var id = Console.ReadLine();
            using (WebSocket ws = new WebSocket("ws://localhost:3000/ws?room_id="+id))
            {
                int x = 0;
                ws.OnMessage += Ws_OnMessage;
                ws.Connect();
                while (true)
                    
                {
                    
                    
                    var a = Console.ReadLine();
                    ws.Send(a);
                    x++;
                }
              
            }
        }
        private static void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("" + e.Data);

            
        }
        
    }
}