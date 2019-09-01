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
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //Handle player login
            Console.WriteLine("Enter Name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Password");
            string pass = Console.ReadLine();
            socket.Connect("127.0.0.1", 8);
            socket.Send(Encoding.Default.GetBytes("login " + name + " " + pass));

            //Keep sending messages
            while (true)
            {
                string msg = "";

                //Make sure the msg sent is not empty
                while (msg == "")
                {
                    msg = Console.ReadLine();
                }
                
                
                socket.Send(Encoding.Default.GetBytes(msg));

                /*
                byte[] buffer = new byte[8192];
                int msgLength = socket.Receive(buffer);

                if (msgLength < buffer.Length)
                {
                    Array.Resize<byte>(ref buffer, msgLength);
                }

                Console.WriteLine(Encoding.Default.GetString(buffer));
                */
            }

            socket.Close();
            socket.Dispose();
            
        }

        
    }
}
