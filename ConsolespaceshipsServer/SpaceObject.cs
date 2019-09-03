using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolespaceshipsServer
{
    class SpaceObject
    {

        public Transform Transform;

        public string Name
        {
            get;
            private set;
        }

        public int Health
        {
            get;
            private set;
        }

        public int MaxHealth
        {
            get;
            private set;
        }




        public SpaceObject()
        {
            Name = "testItem";
            MaxHealth = 100;
            Health = MaxHealth;
            Transform = null;
        }

        public SpaceObject(string name)
        {
            Name = name;
            MaxHealth = 100;
            Health = MaxHealth;
        }
    }
}
