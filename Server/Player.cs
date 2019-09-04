using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

using Server.Space;
using Server.Networking;

namespace Server
{
    class Player
    {
        //Responsible for Storing the players session
        //Note: the players character is different from their session


        //List of commands,
        //Commands are stored with an event that will be called when the action is made
        //If you want a function to be called when the player does an action,
        //Subscribe it to the event listed in this list.
        public Dictionary<string, PlayerAction> playerActionList;

        public Client RemoteClient {
            get;
            private set;
        }

        public string name;

        //Players postion
        public Transform Transform;

        

        public Guid PlayerID
        {
            get;
            private set;
        }









        //=============================================================================
        //Constructor
        //=============================================================================

        public Player(Socket newRemoteClient, Sector sector, Transform pos, Guid newPlayerID)
        {
            Transform = new Transform();
            RemoteClient = new Client(newRemoteClient);
            Transform = pos;
            PlayerID = newPlayerID;


            //Setup the list of player actions
            //And asign each of them an event
            playerActionList = new Dictionary<string, PlayerAction>()
            {
                {"login",
                    new PlayerAction(
                        "login",
                        "login <username> <password>",
                        "Login to the game",
                        PlayerLoginEvent) },

                {"echo",
                    new PlayerAction(
                        "echo", 
                        "echo",
                        "Receive an echo from the server",
                        PlayerEchoEvent) },

                {"yell",
                    new PlayerAction(
                        "yell",
                        "yell",
                        "Yell into space",
                        PlayerYellEvent) },

                {"help",
                    new PlayerAction(
                        "help",
                        "help",
                        "List every command",
                        PlayerHelpEvent) },

                {"whereami",
                    new PlayerAction(
                        "whereami",
                        "whereami",
                        "Tells you your location",
                        PlayerWhereamiEvent) },

                {"broadcast",
                    new PlayerAction(
                        "broadcast",
                        "broadcast <message>",
                        "Sends a message to everyone in the sector",
                        PlayerBroadcastEvent) },

                {"radar",
                    new PlayerAction(
                        "radar",
                        "radar",
                        "Searches for Objects in the sector",
                        PlayerRadarEvent) },

                {"warpto",
                    new PlayerAction(
                        "warpto",
                        "warpto <x> <y> <z>",
                        "Warp to another sector",
                        PlayerWarptoEvent) },

                {"create",
                    new PlayerAction(
                        "create",
                        "create <name> <x,y,z>",
                        "Creates a spaceObject at specified  location",
                        PlayerCreateEvent) },

                {"damage",
                    new PlayerAction(
                        "damage",
                        "damage <id>",
                        "Damages and object of <id>",
                        PlayerDamageEvent)}
            };

            //Subscribe to player actions that will affect the player themselves
            playerActionList["echo"].ActionHandler += PlayerActionEcho;
            playerActionList["help"].ActionHandler += PlayerActionHelp;
            
        }












        //=============================================================================
        //=============================================================================
        //Public Methods
        //=============================================================================
        //=============================================================================




        //=============================================================================
        //Methods that affect the player Character

        //Causes the player to do an action
        //Returns false if the action is invalid
        public bool DoAction (string action)
        {
            if (action == "")
            {
                return false;
            }

            string[] command = action.Split(new char[] { ' ' });

            


            //Check to see if it is a command that should be actioned
            if (playerActionList.ContainsKey(command[0]))
            {

                playerActionList[command[0]].ActionHandler.Invoke(this, action);
                return true;
            }
            else
            {
                return false;
            }
        }


        







        //=============================================================================
        //Methods that affect the client


        public void SendInfoMsg(string msg)
        {
            RemoteClient.Send("INFO:" + msg);
        }

        public void SendSysMsg(string msg)
        {
            RemoteClient.Send("SYS:" + msg);
        }





        //=============================================================================
        //Player Action Methods, (Event Subscribers)
        //These need to be subscribed to the events in player action list to do anything
        //=============================================================================

        //Echos back to the player
        private void PlayerActionEcho (Player player, string action)
        {
            Console.WriteLine("Echo");
            SendSysMsg("Echo");
            
        }


        //Lists commands
        private void PlayerActionHelp (Player player, string action)
        {
            foreach (KeyValuePair<string, PlayerAction> command in playerActionList)
            {
                SendInfoMsg("------------------");
                SendInfoMsg(command.Value.Name);
                SendInfoMsg(command.Value.Syntax);
                SendInfoMsg(command.Value.Description);
            }
        }

        
        










        //=============================================================================
        //Player Action Events
        //=============================================================================
        //These are all events for when the player sends a command

        public delegate void PlayerActionHandler(Player player, string action);

        public event PlayerActionHandler PlayerLoginEvent;
        public event PlayerActionHandler PlayerEchoEvent;
        public event PlayerActionHandler PlayerYellEvent;
        public event PlayerActionHandler PlayerHelpEvent;
        public event PlayerActionHandler PlayerWhereamiEvent;
        public event PlayerActionHandler PlayerBroadcastEvent;
        public event PlayerActionHandler PlayerRadarEvent;
        public event PlayerActionHandler PlayerWarptoEvent;
        public event PlayerActionHandler PlayerCreateEvent;
        public event PlayerActionHandler PlayerDamageEvent;
            
    }
}
