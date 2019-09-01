using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;

namespace ConsolespaceshipsServer
{
    public partial class ServerWindowForm : Form
    {
        //Listener for new connections
        Listener listener;

        
        //The player object
        Player player;



        //=============================================================================
        //Constructor
        //run when form is started
        //=============================================================================
        public ServerWindowForm()
        {
            //Initialise the Form
            InitializeComponent();

            //Setup new connection listener
            listener = new Listener(8);
            //Event for new connections
            listener.SocketAccepted += new Listener.SocketAcceptedHandler(listener_SocketAccepted);

            //Initialise our only player
            player = new Player();

            //Setup external Player commands
            Player.playerActionList["yell"] += Player_PlayerYellEvent;


            //Load event for the windows form
            Load += new EventHandler(ServerWindowForm_Load);
        }

        private void ServerWindowForm_Load(object sender, EventArgs e)
        {
            //Start the listener
            listener.Start();
        }






        //=============================================================================
        //Player Event Delegates
        //=============================================================================
        private void Player_PlayerYellEvent(string action)
        {
            Console.WriteLine("Player is Yelling!");
        }











        //=============================================================================
        //Listener event delegates
        //=============================================================================

        //Called when a new connection is made
        private void listener_SocketAccepted(Socket newConnection)
        {
            //Setup a new client with a the new connection(Socket)
            Client client = new Client(newConnection);
            client.ReceivedMsgEvent += new Client.ClientReceivedMsgHandler(client_ReceivedMsg);
            client.DisconnectedEvent += new Client.ClientDisconnectedHandler(client_Disconnected);

            //Add the new client to the list in the window
            Invoke((MethodInvoker)delegate
            {
               ListViewItem i = new ListViewItem();
               i.Text = client.EndPoint.ToString(); //End point = IP + Port
               i.SubItems.Add(client.ID); //GUID of client
               i.SubItems.Add("xx"); //Last Message
               i.SubItems.Add("xx"); //Last message Time
               i.Tag = client; //Not sure what it is, but its important
               lstClients.Items.Add(i);
            });
        }












        //=============================================================================
        //Client event delegates
        //=============================================================================
        
        //Called when a client disconnects
        //Removes the client from the list
        private void client_Disconnected(Client sender)
        {
            //Remove the client that disconnected from the client list
            Invoke((MethodInvoker)delegate
            {
               for (int i = 0; i < lstClients.Items.Count; i++)
               {
                   Client client = lstClients.Items[i].Tag as Client;

                   if (client.ID == sender.ID)
                   {
                       lstClients.Items.RemoveAt(i);
                       break;
                   }
               }
            });
        }


        //Called when the remote client sends a message
        //Updates the client info table
        //Makes the client/player do an action if the msg was a command
        private void client_ReceivedMsg(Client sender, byte[] data)
        {
            string incomingMsg = Encoding.Default.GetString(data);

            //Update the client table with the message that was received
            Invoke((MethodInvoker)delegate
            {
                for (int i = 0; i < lstClients.Items.Count; i++)
                {
                    Client client = lstClients.Items[i].Tag as Client;

                    if (client.ID == sender.ID)
                    {
                        lstClients.Items[i].SubItems[2].Text = incomingMsg;
                        lstClients.Items[i].SubItems[3].Text = DateTime.Now.ToString();
                        break;
                    }
                }
            });

            //Makes player do an action if the msg was a command
            player.DoAction(incomingMsg);
        }

        
    }
}
