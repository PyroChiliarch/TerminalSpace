using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

namespace ConsolespaceshipsServer
{
    class Server
    {
        TcpListener serverListener;
        Int32 port;

        public void Start()
        {


            //Variables
            serverListener = null;
            Int32 port = 13000;
            IPAddress localAddress = IPAddress.Parse("127.0.0.1");


            //Try to make the server
            try
            {
                //Make Server
                serverListener = new TcpListener(localAddress, port);

                //start server
                serverListener.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            

            
        }

        public void ReceiveMessagesLoop ()
        {
            
            
            try
            {
                Byte[] bytes = new Byte[256];
                String data = null;

                while (true)
                {
                    Console.WriteLine("Waiting for connection...");
                    TcpClient client = serverListener.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;

                    NetworkStream stream = client.GetStream();

                    int i;
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received {0}", data);

                        data = data.ToUpper();

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);
                    }

                    client.Close();
                }
            }

            catch (SocketException e)
            {
                Console.WriteLine("Socket Exception: {0}", e);
            }
            finally
            {
                serverListener.Stop();
            }
        }

    }
}
