using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

namespace Consolespaceships
{
    class Program
    {
        const string version = "v1.0";
        static void Main(string[] args)
        {
            Console.Title = "Terminal Space : Client : " + version;

            //Connect to server
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Connect("127.0.0.1", 8);
            
            //Keep sending messages
            while (true)
            {
                string msg = Console.ReadLine();
                s.Send(Encoding.Default.GetBytes(msg));
            }
            
            s.Close();
            s.Dispose();
            
        }

        
    }
}
