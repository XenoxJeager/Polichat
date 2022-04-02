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
                while (true)
                    
                {
                    ws.OnMessage += Ws_OnMessage;
                    ws.Connect();
                    
                    var a = Console.ReadLine();
                    ws.Send(a);
                    
                }
              
            }
        }
        private static void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("" + e.Data);

            
        }
        
    }
}