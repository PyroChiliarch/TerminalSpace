using System.Net.Sockets;

namespace Server2.Networking
{
    internal class NetworkController
    {
        Listener listener = new Listener(23068);

        public NetworkController ()
        {
            listener.Start();
            listener.SocketAccepted += listener_SocketAccepted;
        }


        private void listener_SocketAccepted (Socket socket)
        {

        }


    }
}