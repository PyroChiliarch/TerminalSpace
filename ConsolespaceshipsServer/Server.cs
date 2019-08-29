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
                String incomingCommand = null;

                while (true)
                {
                    Console.WriteLine("Waiting for connection...");
                    TcpClient client = serverListener.AcceptTcpClient();
                    Console.WriteLine("Connected!");


                    NetworkStream stream = client.GetStream();


                    //Continue to loop client while the clients stream is active
                    int i;
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        //Reset Variables
                        incomingCommand = null;


                        //While reading stream
                        //Write incoming data to the console
                        incomingCommand = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        incomingCommand = incomingCommand.ToUpper();
                        Console.WriteLine("Received {0}", incomingCommand);

                        //Take action on client commands
                        if (incomingCommand == "WHERE AM I")
                        {
                            

                            //Tell client where they are
                            string returnMsg = "YOU ARE HERE";
                            //Print response to console
                            Console.WriteLine(returnMsg);
                            //Send response
                            byte[] returnData = System.Text.Encoding.ASCII.GetBytes(returnMsg);
                            stream.Write(returnData, 0, returnData.Length);
                        } else
                        {
                            //If unknown command
                            //Let client know
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes("Unknown Command: " + incomingCommand);
                            stream.Write(msg, 0, msg.Length);
                            Console.WriteLine("Error: Unknown Command");
                        }




                        
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
