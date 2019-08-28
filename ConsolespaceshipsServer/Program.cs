using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

namespace ConsolespaceshipsServer
{
    class Program
    {
        

        static void Main(string[] args)
        {
            //Variables
            string title = "Terminal Space Server";
            Server server = new Server();

            
            
            

            //Set window settings
            Console.Title = title;

            //Setup Sever
            server.Start();
            server.ReceiveMessagesLoop();


            

            Console.WriteLine("\nHit Enter to continue...");
            Console.Read();

        }


    }
}
