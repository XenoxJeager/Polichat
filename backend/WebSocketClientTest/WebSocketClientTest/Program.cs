using WebSocketSharp;
using System;
using Newtonsoft.Json;
namespace WebSocketClientTest
{
    
    internal class Program
    {
        public static void Main(string[] args)
        {
            using (WebSocket ws = new WebSocket("ws://localhost:28556/ws"))
            {
                int x = 0;
                while (x !=2)
                    
                {
                    ws.OnMessage += Ws_OnMessage;
                    ws.Connect();
                    
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