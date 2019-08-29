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
        static void Main(string[] args)
        {
            Console.Title = "Terminal Space";

            Client client = new Client(13000);

            client.Start();

            while (true)
            {
                client.Loop(Console.ReadLine());
            }

            Console.WriteLine("\nPress Enter to continue");
            Console.Read();


        }
    }
}
