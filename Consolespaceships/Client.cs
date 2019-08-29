using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

namespace Consolespaceships
{
    class Client
    {
        TcpClient tcpClient;
        Int32 port;
        NetworkStream stream;

        public Client (Int32 _port)
        {
            tcpClient = null;
            stream = null;
            port = _port;
        }

        public void Start()
        {
            Console.WriteLine("Starting Client");
            
            try
            {
                //Try to connect to the host
                tcpClient = new TcpClient("localhost", port);
                stream = tcpClient.GetStream();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            Console.WriteLine("Client Started");
        }

        public void Loop(string message)
        {
            

            try
            {
                //Prepare Message
                message = Console.ReadLine();
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                //Send Message
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Sent: {0}", message);


                //Receive response
                data = new Byte[256];
                string responseData = String.Empty;
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);


            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            

            
        }

        public void Stop ()
        {
            stream.Close();
            tcpClient.Close();
        }

    }
}
