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

using Server.Space;
using Server.Networking;

namespace Server
{
    public partial class ServerWindowForm : Form
    {

        //Listener for new connections
        readonly Listener listener;


        //List of logged in players
        readonly Dictionary<Guid, Player> PlayerList;


        //=============================================================================
        //Fields with default values
        //Where players spawn
        readonly SectorTransform spawnSector = new SectorTransform(0, 0, 0);



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
            listener.SocketAccepted += new Listener.SocketAcceptedHandler(Listener_SocketAccepted);

            //List of players
            PlayerList = new Dictionary<Guid, Player>();


            Galaxy.GetSector(spawnSector).SpawnSpaceObject(new Ship(), new Vector3(0, 1, 4));



            //Load event for the windows form
            Load += new EventHandler(ServerWindowForm_Load);
        }

        private void ServerWindowForm_Load(object sender, EventArgs e)
        {
            //Start the listener
            listener.Start();
        }










        //=============================================================================
        //Listener event delegates
        //=============================================================================

        //Called when a new connection is made
        //Setups player objects
        private void Listener_SocketAccepted(Socket newConnection)
        {
            //Setup a new client with a the new connection(Socket)
            Player player = new Player(newConnection, new Transform(), Guid.NewGuid());
            
            
            //Setup remoteClient events
            player.RemoteClient.ReceivedMsgEvent += new Client.ClientReceivedMsgHandler(Client_ReceivedMsg);
            player.RemoteClient.DisconnectedEvent += new Client.ClientDisconnectedHandler(Client_Disconnected);

            //Setup Player events
            player.playerActionList["login"].ActionHandler += Player_PlayerLoginEvent;
            
            

            //Add the new client to the list in the window
            Invoke((MethodInvoker)delegate
            {
                ListViewItem i = new ListViewItem();
                i.Text = player.RemoteClient.EndPoint.ToString(); //End point = IP + Port
                i.SubItems.Add(player.RemoteClient.ID); //GUID of client
                i.SubItems.Add("xx"); //Last Message
                i.SubItems.Add("xx"); //Last message Time
                i.Tag = player; //The object associated with the table entry
                lstClients.Items.Add(i);
            });
        }

        




















        //=============================================================================
        //Player Event Delegates
        //=============================================================================
        

        //Spawns the player and adds them to the active player list
        private void Player_PlayerLoginEvent(Player player, string action)
        {
            string[] command = action.Split(new char[] { ' ' });

            //Add player to active player list
            player.name = command[1];
            PlayerList.Add(player.PlayerID, player);
            Console.WriteLine("Player Logged in: " + player.name);
            player.SendSysMsg("You have logged in as " + player.name);



            //Spawn player character
            Character character = new Character(player);
            Galaxy.GetSector(spawnSector).SpawnSpaceObject(character, new Vector3(0, -1, 4));


            player.SendInfoMsg("You are entering sector " + character.Sector.ToString());
        }

























        //=============================================================================
        //Client event delegates
        //=============================================================================

        //Called when a client disconnects
        //Removes the client from the list
        private void Client_Disconnected(Client sender)
        {
            
            //Remove the client that disconnected from the client list
            Invoke((MethodInvoker)delegate
            {
               for (int i = 0; i < lstClients.Items.Count; i++)
               {
                   Player player = lstClients.Items[i].Tag as Player;

                   if (player.RemoteClient.ID == sender.ID)
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
        private void Client_ReceivedMsg(Client sender, byte[] data)
        {
            string incomingMsg = Encoding.Default.GetString(data);

            //Update the client table with the message that was received
            Invoke((MethodInvoker)delegate
            {
                for (int i = 0; i < lstClients.Items.Count; i++)
                {
                    Player player = lstClients.Items[i].Tag as Player;

                    if (player.RemoteClient.ID == sender.ID)
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
