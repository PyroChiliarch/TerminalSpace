using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolespaceshipsServer
{
    class Player
    {
        //List of commands
        public static Dictionary<string, PlayerActionHandler> commandList;

        public Player()
        {


            commandList = new Dictionary<string, PlayerActionHandler>()
            {
                {"echo", PlayerActionEcho },
                {"yell", PlayerYellEvent }
            };
        }


        public void RunCommand (string action)
        {

            //Check to see if it is a command that should be actioned
            if (commandList.ContainsKey(action))
            {

                commandList[action].Invoke(action);

            }
            else
            {
                //No action
            }
        }

        public void PlayerActionEcho (string action)
        {
            Console.WriteLine("Echo Echo!");
        }

        

        public delegate void PlayerActionHandler(string action);

        public event PlayerActionHandler PlayerYellEvent;
            
    }
}
