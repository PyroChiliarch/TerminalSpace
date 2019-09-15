using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Space
{
    public class Sector
    {
        //WARNING
        //Overides Equals()

        readonly private Dictionary<uint, SpaceObject> spaceObjectList;
        public SectorTransform SectorTransform;

        //Every item spawned in sector is given an id
        //Starts at 1, 0 is undefined
        private uint idInSectorCounter = 1;




        //=============================================================================
        //Constructors
        //=============================================================================

        public Sector (SectorTransform newPos)
        {

            spaceObjectList = new Dictionary<uint, SpaceObject>();

            SectorTransform = newPos;
        }











        //=============================================================================
        //General Methods
        //=============================================================================

        internal SpaceObject[] GetSpaceObjectList ()
        {

            ;
            List<SpaceObject> list = new List<SpaceObject>();
            foreach (KeyValuePair<uint, SpaceObject> spaceObject in spaceObjectList)
            {
                list.Add(spaceObject.Value);
            }
            return list.ToArray();
        }


        //Get a space object fromthe sector via its id
        //Returns null if invalid
        internal SpaceObject GetSpaceObject (uint id)
        {
            if (spaceObjectList.ContainsKey(id))
            {
                return spaceObjectList[id];
            } 
            else
            {
                return null;
            }
            
        }

        internal SpaceObject GetSpaceObjectAtPos (Vector3 pos)
        {
            throw new NotImplementedException();
        }


        internal bool SpawnSpaceObject(SpaceObject newObject, Vector3 newPos)
        {
            


            //Spawn the object with correct values
            //Opposite of DespawnSpaceObject
            //TODO: Make a simple Collision check
            newObject.Transform = new Transform();
            newObject.Transform.Position = newPos;
            newObject.IdInSector = idInSectorCounter;
            newObject.Sector = this;
            SpaceObjectSpawnedEvent?.Invoke(this, newObject.Transform, newObject.IdInSector); //Fires the event
            newObject.DestroyEvent += SpaceObject_DestroyEvent;
            newObject.TransformUpdatedEvent += SpaceObject_TransformUpdatedEvent;
            
            
            spaceObjectList.Add(idInSectorCounter, newObject);
            idInSectorCounter += 1;

            

            
            //Update Clients
            foreach (KeyValuePair<uint, SpaceObject> keyValuePair in spaceObjectList)
            {
                if (keyValuePair.Value is Character character)
                {
                    character.Player.SendUpdateMsg("new:" + newObject.IdInSector + ":" + newObject.Transform.Position.ToString());
                }
            }

            //Update newly spawned client (if its a player)
            //Send every thing in the sector to it
            if (newObject is Character newChar)
            {
                //Spawn each of the existing objects
                foreach (KeyValuePair<uint, SpaceObject> keyValuePair in spaceObjectList)
                {
                    newChar.Player.SendUpdateMsg("new:" + keyValuePair.Value.IdInSector + ":" + keyValuePair.Value.Transform.Position.ToString());
                }
            }
            
            return true;

        }

        
        
        internal bool DespawnSpaceObject (uint id)
        {
            //Opposite of SpawnSpaceObject

            if (spaceObjectList.ContainsKey(id))
            {
                SpaceObject spaceObject = spaceObjectList[id];


                //Update Clients
                foreach (KeyValuePair<uint, SpaceObject> keyValuePair in spaceObjectList)
                {
                    if (keyValuePair.Value is Character character)
                    {
                        character.Player.SendUpdateMsg("rem:" + spaceObject.IdInSector);
                    }
                }


                if (spaceObject is Character thisCharacter)
                {
                    thisCharacter.Player.SendUpdateMsg("clr:");
                }

                //Call Events
                SpaceObjectDespawnedEvent?.Invoke(this, spaceObject.Transform, spaceObject.IdInSector);

                //Update references
                spaceObjectList.Remove(id);

                //Remove the space Object
                spaceObject.DestroyEvent -= SpaceObject_DestroyEvent;
                spaceObject.TransformUpdatedEvent -= SpaceObject_TransformUpdatedEvent;
                spaceObject.IdInSector = 0;
                spaceObject.Sector = null;
                spaceObject.Transform = null;
                


                return true;
            }

            return false;
        }


        //=============================================================================
        //Event Handlers
        //=============================================================================

        
        internal void SpaceObject_DestroyEvent (object item, EventArgs e)
        {
            //Despawns the spaceobject
            DespawnSpaceObject(((SpaceObject)item).IdInSector);
        }

        private void SpaceObject_TransformUpdatedEvent(SpaceObject caller, Transform transform)
        {
            //Fire event
            SpaceObjectPosUpdated?.Invoke(this, transform, caller.IdInSector);

            //Update Clients
            IEnumerable<Character> characters = spaceObjectList.OfType<Character>();
            //Update Clients
            foreach (KeyValuePair<uint,SpaceObject> entry in spaceObjectList)
            {
                if (entry.Value is Character character)
                character.Player.SendUpdateMsg("mov:" + caller.IdInSector + ":" + transform.Position.ToString());
            }

        }

        







        //=============================================================================
        //Overrides
        //=============================================================================



        public static bool operator ==(Sector c1, Sector c2)
        {
            if (ReferenceEquals(c1, c2)) return true;
            if (c1 is null) return false;
            return c1.Equals(c2);
        }

        public static bool operator !=(Sector c1, Sector c2)
        {
            return !(c1 == c2);
        }

        public bool Equals(Sector other)
        {
            if (other is null)
                return false;

            return (this.SectorTransform == other.SectorTransform);
        }


        public override bool Equals(object obj)
        {

            if (!(obj is Sector other))
                return false;
            return Equals(other);
        }

        public override string ToString()
        {
            return SectorTransform.ToString();
        }

        public override int GetHashCode()
        {
            var hashCode = -633811817;
            hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<uint, SpaceObject>>.Default.GetHashCode(spaceObjectList);
            hashCode = hashCode * -1521134295 + EqualityComparer<SectorTransform>.Default.GetHashCode(SectorTransform);
            return hashCode;
        }


        public delegate void SpaceObjectModified (Sector caller, Transform pos, uint idInSector);


        public event SpaceObjectModified SpaceObjectSpawnedEvent;
        public event SpaceObjectModified SpaceObjectDespawnedEvent;
        public event SpaceObjectModified SpaceObjectPosUpdated;
        
    }
}