using System;
using System.Net.Sockets;
using System.Text;

namespace ClientGUI
{
    internal class NetConnection
    {
        Socket socket;

        public NetConnection ()
        {
            
        }

        public void Connect (string ipAddress, int port)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipAddress, port);
            socket.BeginReceive(new byte[] { 0 }, 0, 0, 0, MsgReceivedCallback, null);
        }



        public void Login(string username, string password)
        {
            socket.Send(Encoding.Default.GetBytes("login " + " " + username + " " + password));
        }






        private void MsgReceivedCallback(IAsyncResult ar)
        {
            

                //Receive Message Array
                //Stop receiveing with the socket so it can be read
                socket.EndReceive(ar);
                //Make a storage for the new message
                byte[] buffer = new byte[8192];
                //Fill buffer with message
                int msgLength = socket.Receive(buffer, buffer.Length, 0);
                //Resize the buffer if necessary
                if (msgLength < buffer.Length)
                {
                    Array.Resize<byte>(ref buffer, msgLength);
                }
                //Place message in a var for use
                string incomingMsg = Encoding.Default.GetString(buffer);
                //Split commands up and fill a list based on EOF char
                string[] incomingMsgs = incomingMsg.Split(';');



                //Act upon each new message
                //Just incase we receive multiple at the same time
                foreach (string msg in incomingMsgs)
                {

                    MessageReceived(msg);
                }




                //Begin the thread again and listen for another msg from the connection
                socket.BeginReceive(new byte[] { 0 }, 0, 0, 0, MsgReceivedCallback, null);
            
            
        }




        public delegate void MessageReceivedHandler(string message);

        public event MessageReceivedHandler MessageReceived;
    }

    
}