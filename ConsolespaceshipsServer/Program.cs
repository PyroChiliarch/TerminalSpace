using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

namespace ConsolespaceshipsServer
{
    class Program
    {
        static Listener listener;
        static List<Socket> sockets;
        const string version = "v1.0";

        static void Main(string[] args)
        {
            //Setup Window
            Console.Title = "Terminal Space : Server : " + version;
            
            //Initialise Vars
            //Setup list of sockets for each connection
            sockets = new List<Socket>();

            //Setup Listener
            listener = new Listener(8); //Call constructor
            listener.SocketAccepted += Listener_SocketAccepted; //Set Delegate(Event), Run everytime a new socket connection is made
            listener.Start(); //Start listening for new connections

            //Give the server something to do so  it dosn't exit
            Console.ReadLine();
            
        }

        //This method is called evertime the "SocketAccepted" event is called in the listener(new connections listener1)
        private static void Listener_SocketAccepted(Socket e)
        {
            //Called everytime there is a socket connected by the listener
            Console.WriteLine("New Connection: {0}\n{1}\n=============", e.RemoteEndPoint, DateTime.Now);
            //Add the new socket to the list of client connections
            sockets.Add(e);
        }
    }
}
