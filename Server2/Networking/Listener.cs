using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

namespace Server2.Networking
{

    //The listener class once it has started listening for new connections will continuously 
    //raise the SocketAccepted Event everytime a new connection is made to the server
    //This will continue until it is told to stop
    class Listener
    {
        //A temporary socket that is used to store the most recent connection
        Socket s;


        //Is the object actively listening for new connections?
        public bool Listening
        {
            get;
            private set;
        }

        //What port are we using for connections?
        public int Port
        {
            get;
            private set;
        }

        //Constructor
        public Listener (int port)
        {
            Port = port;
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        //Start listening for connections
        //Returns true on success or already listening
        //False on error or failed to connect
        public bool Start ()
        {
            //Ignore if we are already listening
            if (Listening)
                return true;

            //Binds socket to Endpoint with specified server port
            try
            {
                s.Bind(new IPEndPoint(0, Port));
            }
            catch (SocketException e) when (e.ErrorCode == 10048)
            {

                Log.AddLogError(e);
                Log.AddLogString("Are you trying to run multiple servers?");
                return false;
            } catch (Exception e)
            {
                throw e;
            }
            
            


            //Simply sets the state of the socket
            s.Listen(0);
            
            //Asynchronous Operation
            //Will begin to listen for new connections
            //The callback function recalls this method so that a loop is made
            //This allows the Listener to continue accepting new connections
            s.BeginAccept(Callback_newConnection, null);

            //We are now continuously listening for connections
            Listening = true;

            return true;
        }

        //Stop listening for new connections
        public void Stop ()
        {
            //Ignore if we are already not listening
            if (!Listening)
                return;

            //Clean up the socket since it is no longer being used
            s.Close();
            s.Dispose();

            //Create a fresh socket for when we want to start listening again
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        //Our socket is activley listening for new connections
        //This will be called everytime it receives a new connection
        //The new connection is added to the list
        void Callback_newConnection(IAsyncResult asyncResult)
        {
            try
            {
                //Get the new connection
                Socket s = this.s.EndAccept(asyncResult);

                //Calls the event
                //A new socket has been connected
                SocketAccepted?.Invoke(s);

                //Recall this operation again
                //Forms a loop to continuously listen for connections
                this.s.BeginAccept(Callback_newConnection, null);
            }
            catch (Exception e)
            {
                Log.AddLogError(e);
            }
        }

        public delegate void SocketAcceptedHandler(Socket e);
        public event SocketAcceptedHandler SocketAccepted;

    }
}
