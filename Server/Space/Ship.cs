using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Space
{
    class Ship : SpaceObject, IHealth, IPilotable
    {

        //=============================================================================
        //IPilotable

        public Character Pilot { get; private set; }






        //=============================================================================
        //IHealth

        public int Health { get; private set;}
        public int MaxHealth { get; }



        public Ship ()
        {
            //Inheritance SpaceObject
            ID = Guid.NewGuid();
            Name = "Unnamed Ship";
            Sector = null;
            Transform = null;
            IdInSector = 0;

            //Interface IHealth
            MaxHealth = 500;
            Health = MaxHealth;
        }

        //=============================================================================
        //Interface IPilotable Methods
        //=============================================================================

        public bool AddPilot (Character newPilot)
        {
            if (Pilot == null)
            {
                Pilot = newPilot;
                Pilot.Parent = this;
                Pilot.Sector.DespawnSpaceObject(Pilot.IdInSector);
                Pilot.Player.SendInfoMsg("Welcome aboard the " + Name + " captain!");
                return true;
            }

            Pilot.Player.SendInfoMsg("Cannot board ship");
            return false;
        }

        public bool RemovePilot ()
        {
            if (Pilot != null)
            {
                Pilot.Transform = this.Transform;
                Pilot.Parent = null;
                Sector.SpawnSpaceObject(Pilot);
                Pilot.Player.SendInfoMsg("You have left the ship");
                Pilot = null;
                return true;
            }

            
            return false; 

        }





        //=============================================================================
        //Interface IHealth Methods
        //=============================================================================

        public void AffectHealth(int delta)
        {
            Health += delta;

            //Max sure health dosn't go too high
            if (Health > MaxHealth)
                Health = MaxHealth;

            //Kill if health too low
            if (Health <= 0)
                Destroy();
        }

        
    }
}
