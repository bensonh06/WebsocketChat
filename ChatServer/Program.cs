using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ChatServer
{
    internal class Program
    {
        static async void Main(string[] args)
        {
            int port = 8080;

            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:" + port + "/");
            listener.Start();

            Console.WriteLine("Server listening on port " + port);

            while (true)
            {
                HttpListenerContext request = await listener.GetContextAsync();

                WebSocketContext connection = await request.AcceptWebSocketAsync(null);
                Console.WriteLine("new websocket connection");

                WebSocket ws = connection.WebSocket;

                byte[] buffer = new byte[1024];

                try
                {
                    WebSocketReceiveResult result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                        Console.WriteLine("websocket closed");
                    }
                    else
                    {
                        string clientMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        Console.WriteLine("received: " + clientMessage);

                        byte[] echoMessage = Encoding.UTF8.GetBytes("Server: " + clientMessage);
                        await ws.SendAsync(new ArraySegment<byte>(echoMessage, 0, echoMessage.Length), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                }
                catch
                {
                    Console.WriteLine("something went bad with websocket");
                    break;
                }
            }

        }


    }
}

