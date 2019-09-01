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


        //List of players
        Dictionary<string, Player> playerList;

        //The player object
        //TODO: Refactor
        //Player player;



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
            //TODO: Refactor Constructor
            //player = new Player();

            //Setup external Player commands
            //TODO: Refactor Event
            //Player.playerActionList["yell"] += Player_PlayerYellEvent;


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
            Player player = new Player(newConnection);

            //Setup remoteClient events
            player.remoteClient.ReceivedMsgEvent += new Client.ClientReceivedMsgHandler(client_ReceivedMsg);
            player.remoteClient.DisconnectedEvent += new Client.ClientDisconnectedHandler(client_Disconnected);

            //Setup Player events
            Player.playerActionList["yell"] += Player_PlayerYellEvent;


            //Add the new client to the list in the window
            Invoke((MethodInvoker)delegate
            {
               ListViewItem i = new ListViewItem();
               i.Text = player.remoteClient.EndPoint.ToString(); //End point = IP + Port
               i.SubItems.Add(player.remoteClient.ID); //GUID of client
               i.SubItems.Add("xx"); //Last Message
               i.SubItems.Add("xx"); //Last message Time
               i.Tag = player; //The object associated with the table entry
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
                   Player player = lstClients.Items[i].Tag as Player;

                   if (player.remoteClient.ID == sender.ID)
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
                    Player player = lstClients.Items[i].Tag as Player;

                    if (player.remoteClient.ID == sender.ID)
                    {
                        //Update the Table
                        lstClients.Items[i].SubItems[2].Text = incomingMsg;
                        lstClients.Items[i].SubItems[3].Text = DateTime.Now.ToString();

                        //Makes player do an action if the msg was a command
                        player.DoAction(incomingMsg);
                        break;
                    }
                }
            });

            
        }

        
    }
}
