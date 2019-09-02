using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

namespace ConsolespaceshipsServer
{
    class Player
    {

        //List of commands,
        //Commands are stored with an event that will be called when the action is made
        //If you want a function to be called when the player does an action,
        //Subscribe it to the event listed in this list.
        public Dictionary<string, PlayerActionHandler> playerActionList;

        public Client remoteClient;

        public SectorCoord CurrentSector
        {
            get;
            private set;
        }

        public string name;

        //Constructor
        public Player(Socket newRemoteClient, SectorCoord spawnSector)
        {
            remoteClient = new Client(newRemoteClient);
            CurrentSector = spawnSector;

            //Setup the list of player actions
            //And asign each of them an event
            playerActionList = new Dictionary<string, PlayerActionHandler>()
            {
                {"login", PlayerLoginEvent },
                {"echo", PlayerEchoEvent },
                {"yell", PlayerYellEvent },
                {"help", PlayerHelpEvent },
                {"whereami", PlayerWhereamiEvent },
                {"broadcast", PlayerBroadcastEvent },
                {"radar", PlayerRadarEvent },
                {"warpto", PlayerWarptoEvent }
            };

            //Subscribe to player actions that will affect the player themselves
            playerActionList["echo"] += PlayerActionEcho;
            playerActionList["help"] += PlayerActionHelp;
            playerActionList["whereami"] += PlayerActionWhereami;
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

                playerActionList[command[0]].Invoke(this, action);
                return true;
            }
            else
            {
                return false;
            }
        }


        //Warps the player to another sector
        //Returns Success
        public bool WarpTo(SectorCoord sector)
        {
            if (CurrentSector != sector)
            {
                CurrentSector = sector;
                return true;
            }

            return false;
        }







        //=============================================================================
        //Methods that affect the client


        public void SendInfoMsg(string msg)
        {
            remoteClient.Send("INFO: " + msg);
        }

        public void SendSysMsg(string msg)
        {
            remoteClient.Send("SYS :" + msg);
        }






        //=============================================================================
        //Player Action Methods, (Event Subscribers)
        //These need to be subscribed to the events in player action list to do anything
        //=============================================================================

        //Echos back to the player
        private void PlayerActionEcho (Player player, string action)
        {
            Console.WriteLine("Echo");
            remoteClient.Send("Echo");
            
        }

        private void PlayerActionHelp (Player player, string action)
        {
            string commandList = ("" +
                "Help: Show this list\n" +
                "Echo: Get a response from the server\n" +
                "Yell: Scream into space");
            remoteClient.Send(commandList);
        }

        private void PlayerActionWhereami (Player player, string action)
        {
            player.remoteClient.Send("You are in sector: " + CurrentSector.ToString());
        }
        










        //=============================================================================
        //Player Action Events
        //=============================================================================

        public delegate void PlayerActionHandler(Player player, string action);

        public event PlayerActionHandler PlayerLoginEvent;
        public event PlayerActionHandler PlayerEchoEvent;
        public event PlayerActionHandler PlayerYellEvent;
        public event PlayerActionHandler PlayerHelpEvent;
        public event PlayerActionHandler PlayerWhereamiEvent;
        public event PlayerActionHandler PlayerBroadcastEvent;
        public event PlayerActionHandler PlayerRadarEvent;
        public event PlayerActionHandler PlayerWarptoEvent;
            
    }
}
