using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class PlayerAction
    {
        public readonly string Name;
        public readonly string Syntax;
        public readonly string Description;
        public Player.PlayerActionHandler ActionHandler;

        /// <summary>
        /// Use to compare against other player actions
        /// </summary>
        /// <param name="name"></param>
        public PlayerAction(string name)
        {

        }

        /// <summary>
        /// Use to create a full proper PlayerAction
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_syntax"></param>
        /// <param name="_description"></param>
        public PlayerAction (string _name, string _syntax, string _description, Player.PlayerActionHandler _actionHandler)
        {
            Name = _name;
            Syntax = _syntax;
            Description = _description;
            ActionHandler = _actionHandler;
        }



        




        public static bool operator ==(PlayerAction c1, PlayerAction c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(PlayerAction c1, PlayerAction c2)
        {
            return !c1.Equals(c2);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PlayerAction))
            {
                return false;
            }

            var other = (PlayerAction)obj;
            return Name == other.Name;
        }

        public override string ToString()
        {
            string str = Name;
            return str;
        }

        public override int GetHashCode()
        {
            return 363513814 + EqualityComparer<string>.Default.GetHashCode(Name);
        }
    }
}
