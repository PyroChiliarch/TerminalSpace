using System.Collections.Generic;
using System.Net.Sockets;

namespace Server2.Networking
{
    internal class NetworkService
    {
        Listener listener = new Listener(23068);
        List<Socket> socketList = new List<Socket>();


        public NetworkService ()
        {
            listener.Start();
            listener.SocketAccepted += listener_SocketAccepted;
        }


        private void listener_SocketAccepted (Socket socket)
        {
            socketList.Add(socket);
        }



        

    }
}