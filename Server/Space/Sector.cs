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


        internal bool SpawnSpaceObject(SpaceObject newObject)
        {
            //Error checking
            if (newObject.Transform == null || newObject.Transform.position == null)
            {
                throw new NullReferenceException("Cannot spawn object will null Transform/Position");
            }

            //Opposite of DespawnSpaceObject
            //TODO: Make a simple Collision check
            newObject.DestroyEvent += SpaceObject_DestroyEvent;
            newObject.IdInSector = idInSectorCounter;
            newObject.Sector = this;
            spaceObjectList.Add(idInSectorCounter, newObject);
            idInSectorCounter += 1;
            return true;

        }

        internal bool DespawnSpaceObject (uint id)
        {
            //Opposite of SpawnSpaceObject

            if (spaceObjectList.ContainsKey(id))
            {
                SpaceObject spaceObject = spaceObjectList[id];
                spaceObject.DestroyEvent -= SpaceObject_DestroyEvent;
                spaceObject.IdInSector = 0;
                spaceObject.Sector = null;
                spaceObject.Transform = null;
                spaceObjectList.Remove(id);

                return true;
            }

            return false;
        }


        //=============================================================================
        //Event Handlers
        //=============================================================================

        internal void SpaceObject_DestroyEvent (object item, EventArgs e)
        {
            DespawnSpaceObject(((SpaceObject)item).IdInSector);
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
    }
}