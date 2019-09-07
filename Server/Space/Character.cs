using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Space
{
    class Character : SpaceObject, IHealth
    {
        //A players ingame representation

        public Player Player;

        public int Health { get; private set; }
        public int MaxHealth { get; }

        public Character ()
        {
            //Inheritance SpaceObject
            ID = Guid.NewGuid();
            Name = "Undefined Name";
            Sector = null;
            Transform = null;
            IdInSector = 0;

            //IHealth
            MaxHealth = 100;
            Health = MaxHealth;

            Console.WriteLine("Warning: Created Character without assigning player! ID: " + ID.ToString());
        }

        public Character (Player newPlayer)
        {
            //Inheritance
            ID = Guid.NewGuid();
            Name = newPlayer.name;
            
            //IHealth
            MaxHealth = 100;
            Health = MaxHealth;

            //Object Specific
            Player = newPlayer;

            //TODO Make this smaller/Refactor
            //Move all commands from client to character
            newPlayer.playerActionList["yell"].ActionHandler += Player_YellEvent;
            newPlayer.playerActionList["broadcast"].ActionHandler += Player_BroadcastEvent;
            newPlayer.playerActionList["echo"].ActionHandler += Player_PlayerActionEcho;
            newPlayer.playerActionList["help"].ActionHandler += Player_PlayerActionHelp;
            newPlayer.playerActionList["radar"].ActionHandler += Player_RadarEvent;
            newPlayer.playerActionList["warpto"].ActionHandler += Player_WarptoEvent;
            newPlayer.playerActionList["create"].ActionHandler += Player_CreateEvent;
            newPlayer.playerActionList["damage"].ActionHandler += Player_DamageEvent;
            newPlayer.playerActionList["whereami"].ActionHandler += Player_WhereamiEvent;
            newPlayer.playerActionList["board"].ActionHandler += Player_BoardEvent;
            newPlayer.playerActionList["depart"].ActionHandler += Player_DepartEvent;

        }

        













        //=============================================================================
        //Player Action Event handlers
        //=============================================================================

        private void Player_YellEvent(Player player, string action)
        {
            Console.WriteLine(player.name + " is Yelling!");
            player.SendInfoMsg("In space, no one can hear you scream!");
        }

        private void Player_BroadcastEvent(Player player, string action)
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
            IEnumerable<Character> characters = Sector.GetSpaceObjectList().OfType<Character>();
            foreach (Character otherCharacter in characters)
            {
                if (Sector == otherCharacter.Sector
                && player.PlayerID != otherCharacter.Player.PlayerID)
                {
                    otherCharacter.Player.SendInfoMsg(broadcastMsg);

                }
                
            }
        }


        private void Player_CreateEvent(Player player, string action)
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

            Asteroid asteroid = new Asteroid(name, 100);
            asteroid.Transform = pos;

            bool result = Galaxy.GetSector(Sector.SectorTransform).SpawnSpaceObject(asteroid);

            if (result)
            {
                player.SendInfoMsg("Object Created at " + pos.ToString());
            }
            else
            {
                player.SendInfoMsg("Creation Failed at " + pos.ToString());
            }
        }



        private void Player_DamageEvent(Player player, string action)
        {
            string[] command = action.Split(' ');

            string id = command[1];
            string amount = command[2];

            SpaceObject target = Sector.GetSpaceObject(uint.Parse(id));

            if (target is IHealth)
            {
                ((IHealth)target).AffectHealth(int.Parse(amount) * -1);
                player.SendInfoMsg(target.Name + " was damaged");
                return;
            }

            player.SendInfoMsg("Object Cannot be Damaged!");
            return;
        }

        private void Player_WhereamiEvent(Player player, string action)
        {
            if (Parent != null)
            {
                Player.SendInfoMsg("You are in sector " + Sector.ToString() + " aboard " + Parent.ToString() );
                return;
            }

            Player.SendInfoMsg("You are in sector " + Sector.ToString());
        }


        private void Player_BoardEvent(Player player, string action)
        {
            string[] command = action.Split(' ');

            

            if (Parent == null)
            {
                if (Sector.GetSpaceObject(uint.Parse(command[1])) is IPilotable ship)
                {
                    if (!ship.AddPilot(this))
                    {
                        player.SendInfoMsg("You cannot board this object at this time");
                    }
                }
                else
                {
                    player.SendInfoMsg("This object is not pilotable");
                }
            } 
            else
            {
                player.SendInfoMsg("You ne to 'depart' before you can board another vessel");
            }
            
        }

        private void Player_DepartEvent(Player player, string action)
        {

            if (Parent is Ship ship)
            {
                ship.RemovePilot();
            }
        }

        private void Player_PlayerActionEcho(Player player, string action)
        {
            Console.WriteLine("Echo");
            player.SendSysMsg("Echo");

        }


        //Lists commands
        private void Player_PlayerActionHelp(Player player, string action)
        {
            foreach (KeyValuePair<string, PlayerAction> command in player.playerActionList)
            {
                player.SendInfoMsg("------------------");
                player.SendInfoMsg(command.Value.Name);
                player.SendInfoMsg(command.Value.Syntax);
                player.SendInfoMsg(command.Value.Description);
            }
        }


        //=============================================================================
        //Player action event handlers
        //That Will try to call in the parent first



        private void Player_RadarEvent(Player player, string action)
        {
            //Use Event subscribers first if possible
            if (PingRadarEvent != null)
            {
                PingRadarEvent(this, action);
                return;
            }


            SpaceObject[] objectList = Sector.GetSpaceObjectList();
            player.SendInfoMsg("Sending radar ping in: " + Sector.ToString());
            player.SendInfoMsg("Objects Found: " + objectList.Length.ToString());

            foreach (SpaceObject item in objectList)
            {
                if (item is IHealth)
                {
                    IHealth target = item as IHealth;
                    player.SendInfoMsg(item.IdInSector + " - " + item.Name + " - " + target.Health + "/" + target.MaxHealth);
                }
                else
                {
                    player.SendInfoMsg(item.IdInSector + " - " + item.Name);
                }
            }



        }

        private void Player_WarptoEvent(Player player, string action)
        {
            //Try to call the event first
            if (WarpToEvent != null)
            {
                WarpToEvent(this, action);
                return;
            }

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

            //TODO Add Functions in galaxy/sector for warping
            //WarpTo Function Surrogate
            Sector.DespawnSpaceObject(this.IdInSector);
            Galaxy.GetSector(destination).SpawnSpaceObject(this);


            player.SendInfoMsg("Arrived at " + destination.ToString());
        }

















        //=============================================================================
        //Interface IHealth Mthods
        //=============================================================================

        //Heal or damage
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


        public delegate void CharacterActionHandler (Character character, string action);

        public event CharacterActionHandler PingRadarEvent;
        public event CharacterActionHandler WarpToEvent;

    }
}
