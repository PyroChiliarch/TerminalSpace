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








        //=============================================================================
        //Constructors
        //=============================================================================

        public PlayerAction (string _name, string _syntax, string _description, Player.PlayerActionHandler _actionHandler)
        {
            Name = _name;
            Syntax = _syntax;
            Description = _description;
            ActionHandler = _actionHandler;
        }








        //=============================================================================
        //Overrides
        //=============================================================================

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
