using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.Write("Choose a username: ");

            string username = Console.ReadLine();

            Console.Title = "Chatting as " + username;

            while (true)
            {
                Console.Write(username + ": ");
                string msg = Console.ReadLine();

                //send msg to websocket
            }
        }
    }
}
