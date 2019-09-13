using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Space
{
    class Ship : SpaceObject, IHealth, IPilotable, IHasRadar, IHasWarpDrive
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

        //Returns success
        public bool AddPilot (Character newPilot)
        {
            if (Pilot == null)
            {
                //Add Pilot
                Pilot = newPilot;
                Pilot.Parent = this;
                Pilot.Sector.DespawnSpaceObject(Pilot.IdInSector);

                //Add event delegates
                Pilot.PingRadarEvent += PingRadar;
                Pilot.WarpToEvent += WarpTo;

                //Send console logs
                Pilot.Player.SendInfoMsg("Welcome aboard the " + Name + " captain!");

                return true;
            }

            Pilot.Player.SendInfoMsg("Cannot board ship");
            return false;
        }


        //Returns Success
        public bool RemovePilot ()
        {
            if (Pilot != null)
            {
                //Remove Pilot
                Pilot.Transform = this.Transform;
                Pilot.Parent = null;
                Sector.SpawnSpaceObject(Pilot);
                Pilot = null;

                //Remove Event delegates
                Pilot.PingRadarEvent -= PingRadar;
                Pilot.WarpToEvent -= WarpTo;

                //Send Console logs
                Pilot.Player.SendInfoMsg("You have left the ship");
                
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

        






        //=============================================================================
        //Other Methods
        //=============================================================================




        public void PingRadar (Character character, string command)
        {
            SpaceObject[] objectList = Sector.GetSpaceObjectList();
            character.Player.SendInfoMsg("Sending from " + Name + ", ping in: " + Sector.ToString());
            character.Player.SendInfoMsg("Objects Found: " + objectList.Length.ToString());

            foreach (SpaceObject item in objectList)
            {
                if (item is IHealth)
                {
                    IHealth target = item as IHealth;
                    character.Player.SendInfoMsg(item.IdInSector + " - " + item.Transform.position.ToString() + " - " + item.Name + " - " + target.Health + "/" + target.MaxHealth);
                }
                else
                {
                    character.Player.SendInfoMsg(item.IdInSector + " - " + item.Name);
                }
            }
        }







        public void WarpTo(Character character, string action)
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
                Pilot.Player.SendInfoMsg("Invalid Warp Command");
                return;
            }

            //TODO Add Functions in galaxy/sector for warping
            //WarpTo Function Surrogate
            Sector.DespawnSpaceObject(this.IdInSector);
            Galaxy.GetSector(destination).SpawnSpaceObject(this);

        }





        public override string ToString()
        {
            return Name;
        }
    }
}
