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
            string title = "Terminal Space";

            String userCommand = null;
            String serverResponse = null;


            Console.Title = title;

            Client client = new Client(13000);
            client.Start();


            while (true)
            {
                //Receive user Command, all commands are changed to upper case
                userCommand = Console.ReadLine().ToUpper();

                if (userCommand == "QUIT")
                {
                    //Disconnect  from the server
                    serverResponse = client.SendCommand("DISCONNECT");
                    client.Stop();
                    break;
                } else if (userCommand == "WHERE AM I")
                {
                    //Find out where we are
                    serverResponse = client.SendCommand(userCommand);
                } else
                {
                    //Just send the message if we have no matching command
                    serverResponse = client.SendCommand(userCommand);
                }
                
                //Send Users Command
                serverResponse = serverResponse.ToUpper();

                Console.WriteLine("Received: {0}", serverResponse);
            }

            Console.WriteLine("\nPress Enter to quit");
            Console.Read();


        }

        
    }
}
