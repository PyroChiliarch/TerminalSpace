using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

namespace ConsolespaceshipsServer
{
    class Client
    {

        //the ID of the client
        public string ID
        {
            get;
            private set;
        }

        //IP and Port of client
        public IPEndPoint EndPoint
        {
            get;
            private set;
        }

        //The clients socket
        Socket socket;

        //Constructor
        public Client (Socket newConnection)
        {
            socket = newConnection;
            ID = Guid.NewGuid().ToString();
            EndPoint = (IPEndPoint)socket.RemoteEndPoint;

            //Starts a new thread that receives msgs from the client
            //the callback function loops itself to continuously receive messages
            socket.BeginReceive(new byte[] { 0 }, 0, 0, 0, MsgReceivedCallback, null);
        }

        //Called everytime the socket if this client receives a message
        void MsgReceivedCallback (IAsyncResult asyncResult)
        {
            try
            {
                socket.EndReceive(asyncResult);

                //Make a storage for the new message
                byte[] buffer = new byte[8192];

                //Fill buffer with message
                int msgLength = socket.Receive(buffer, buffer.Length, 0);

                //Resize the buffer if necessary
                if ( msgLength < buffer.Length)
                {
                    Array.Resize<byte>(ref buffer, msgLength);
                }

                //We received a msg, so trigger the event(Delegate) for the client
                if (ReceivedMsgEvent != null)
                {
                    ReceivedMsgEvent(this, buffer);
                }

                //Begin the thread again and listen for another msg from the connection
                socket.BeginReceive(new byte[] { 0 }, 0, 0, 0, MsgReceivedCallback, null);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

                //Call the Client has disconnected event so  it can be cleaned up properly and handled properly
                if (DisconnectedEvent != null)
                {
                    DisconnectedEvent(this);
                }
                
            }
        }

        //Disconnect the remote connection to the remote client
        //Dispose the socket afterwards
        public void Close()
        {
            socket.Close();
            socket.Dispose();
        }

        //Sends a string to the client
        public void Send(string msg)
        {
            char eofChar = ';';

            socket.Send(Encoding.Default.GetBytes(msg + eofChar));
        }

        //Triggers when the remote client sends a message
        public delegate void ClientReceivedMsgHandler(Client sender, byte[] data);

        //Triggers when the remote client disconnects
        public delegate void ClientDisconnectedHandler(Client sender);

        public event ClientReceivedMsgHandler ReceivedMsgEvent;
        public event ClientDisconnectedHandler DisconnectedEvent;

    }
}
