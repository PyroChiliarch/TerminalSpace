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
        //Structs
        


        //Listener for new connections
        Listener listener;


        //List of logged in players
        //TODO : Change Key to an object/Reference
        Dictionary<string, Player> playerList;

        //Galaxy
        Galaxy galaxy;
        




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
            playerList = new Dictionary<string, Player>();

            //The Galaxy
            galaxy = new Galaxy();

            //List of sectors
            //Spawn test sector
            //Spawn test object
            

            SectorTransform spawnSector = new SectorTransform(0, 0, 0);
            Transform newPos = new Transform();
            SpaceObject newObject = new SpaceObject();
            //sectorList.Add(spawnSector, new Sector(spawnSector));
            galaxy.GetSector(spawnSector).SpawnSpaceObject(newObject, newPos);
            galaxy.GetSector(spawnSector).SpawnSpaceObject(newObject, newPos);



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
            Player player = new Player(newConnection, null, new Transform());

            //Setup remoteClient events
            player.remoteClient.ReceivedMsgEvent += new Client.ClientReceivedMsgHandler(Client_ReceivedMsg);
            player.remoteClient.DisconnectedEvent += new Client.ClientDisconnectedHandler(Client_Disconnected);

            //Setup Player events
            player.playerActionList["yell"] += Player_PlayerYellEvent;
            player.playerActionList["login"] += Player_PlayerLoginEvent;
            player.playerActionList["broadcast"] += Player_PlayerBroadcastEvent;
            player.playerActionList["radar"] += Player_PlayerRadarEvent;
            player.playerActionList["warpto"] += Player_PlayerWarptoEvent;
            player.playerActionList["create"] += Player_PlayerCreateEvent;

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
        //Player Event Delegates
        //=============================================================================
        private void Player_PlayerYellEvent(Player player, string action)
        {
            Console.WriteLine(player.name + " is Yelling!");
            player.SendInfoMsg("In space, no one can hear you scream!");
        }

        //Spawns the player and adds them to the active player list
        private void Player_PlayerLoginEvent(Player player, string action)
        {
            string[] command = action.Split(new char[] { ' ' });

            //Add player to active player list
            player.name = command[1];
            playerList.Add(player.name, player);
            Console.WriteLine("Player Logged in: " + player.name);
            player.SendSysMsg("You have logged in as " + player.name);

            //Set the players sector
            SectorTransform sectorPos = new SectorTransform(0, 0, 0);
            player.Sector = galaxy.GetSector(sectorPos);

            player.SendInfoMsg("You are entering sector " + sectorPos.ToString());
        }

        private void Player_PlayerBroadcastEvent(Player player, string action)
        {
            string[] command = action.Split(new char[] { ' ' });

            
            string msg = "";
            for (int i = 1; i < command.Length; i++)
            {
                msg += command[i];
                msg += " ";
            }

            string broadcastMsg = player.name + "-" + msg;


            //Sends the broadcast message to every player in the sector that is not itself
            foreach (KeyValuePair<string, Player> otherPlayer in playerList)
            {
                if (player.Sector == otherPlayer.Value.Sector
                    && player != otherPlayer.Value)
                {
                    otherPlayer.Value.SendInfoMsg(broadcastMsg);
                }
            }
        }

        private void Player_PlayerRadarEvent(Player player, string action)
        {

            string[] objectList = player.Sector.GetSpaceObjectList();
            player.SendInfoMsg("Sending radar ping in: " + player.Sector.ToString());
            player.SendInfoMsg("Objects Found: " + objectList.Length.ToString());
            foreach (string item in objectList)
            {
                player.SendInfoMsg(item);
            }
        }

        private void Player_PlayerWarptoEvent(Player player, string action)
        {
            string[] command = action.Split(' ');

            //Generate sector coord from command
            //Catch errors
            SectorTransform destination;
            try
            {
                destination = new SectorTransform
                (
                    int.Parse(command[1]), //x
                    int.Parse(command[2]), //y
                    int.Parse(command[3])  //z
                );
            }
            catch
            {
                Console.WriteLine("Invalid Warp Command: " + action);
                player.SendInfoMsg("Invalid Warp Command");
                return;
            }


            player.WarpTo(galaxy.GetSector(destination));

            player.SendInfoMsg("Arrived at " + destination.ToString());
        }

        private void Player_PlayerCreateEvent(Player player, string action)
        {
            string[] command = action.Split(' ');

            //first arg
            string name = command[1];

            //Second arg
            Transform pos = new Transform();
            string[] parts = command[2].Split(',');
            pos.position.x = float.Parse(parts[0]);
            pos.position.y = float.Parse(parts[1]);
            pos.position.z = float.Parse(parts[2]);

            bool result = galaxy.GetSector(player.Sector.SectorTransform).SpawnSpaceObject(new SpaceObject(name), pos);

            if (result)
            {
                player.SendInfoMsg("Object Created at " + pos.ToString());
            }
            else
            {
                player.SendInfoMsg("Creation Failed at " + pos.ToString());
            }



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
        private void Client_ReceivedMsg(Client sender, byte[] data)
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
