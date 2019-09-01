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
        static Socket socket;
        static List<string> incomingMsgBuffer;


        static void Main(string[] args)
        {
            Console.Title = "Terminal Space : Client : " + version;

            incomingMsgBuffer = new List<string>();


            //Initialise Socket
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //Handle player login
            Console.WriteLine("Enter Name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Password");
            string pass = Console.ReadLine();

            //Connect and send login details
            socket.Connect("127.0.0.1", 8);
            socket.Send(Encoding.Default.GetBytes("login " + name + " " + pass));

            socket.BeginReceive(new byte[] { 0 }, 0, 0, 0, MsgReceivedCallback, null);

            //Get player input
            while (true)
            {
                string msg = "";

                //Empty string will display incoming messages
                while (msg == "")
                {

                    foreach (string inMsg in incomingMsgBuffer)
                    {
                        Console.WriteLine(inMsg);
                    }
                    incomingMsgBuffer.Clear();



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

        private static void MsgReceivedCallback(IAsyncResult asyncResult)
        {
            try
            {
                socket.EndReceive(asyncResult);

                //Make a storage for the new message
                byte[] buffer = new byte[8192];

                //Fill buffer with message
                int msgLength = socket.Receive(buffer, buffer.Length, 0);

                //Resize the buffer if necessary
                if (msgLength < buffer.Length)
                {
                    Array.Resize<byte>(ref buffer, msgLength);
                }

                string incomingMsg = Encoding.Default.GetString(buffer);
                incomingMsgBuffer.Add(incomingMsg);

                //Begin the thread again and listen for another msg from the connection
                socket.BeginReceive(new byte[] { 0 }, 0, 0, 0, MsgReceivedCallback, null);
            } catch
            {
                throw new NotImplementedException();
            }
        }
        

    }
}
