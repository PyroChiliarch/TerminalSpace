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
        public static Dictionary<string, PlayerActionHandler> playerActionList;

        public Client remoteClient;

        public string name;

        //Constructor
        public Player(Socket newRemoteClient)
        {
            remoteClient = new Client(newRemoteClient);

            //Setup the list of player actions
            //And asign each of them an event
            playerActionList = new Dictionary<string, PlayerActionHandler>()
            {
                {"login", PlayerLoginEvent },
                {"echo", PlayerEchoEvent },
                {"yell", PlayerYellEvent }
            };

            //Subscribe to player actions that will affect the player themselves
            playerActionList["echo"] += PlayerEcho;
        }



        //Causes the player to do an action
        //Returns false if the action is invalid
        public bool DoAction (string action)
        {
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








        //=============================================================================
        //Player Action Methods
        //These need to be subscribed to the events in player action list to do anything
        //=============================================================================

        //Echos back to the player
        public void PlayerEcho (Player player, string action)
        {
            Console.WriteLine("Echo");
            
        }










        //=============================================================================
        //Player Action Events

        public delegate void PlayerActionHandler(Player player, string action);

        public event PlayerActionHandler PlayerLoginEvent;
        public event PlayerActionHandler PlayerEchoEvent;
        public event PlayerActionHandler PlayerYellEvent;
            
    }
}
