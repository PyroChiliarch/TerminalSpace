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
        private uint idInSectorCounter = 0;




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

        internal SpaceObject GetSpaceObject (uint id)
        {
            return spaceObjectList[id];
        }



        internal bool SpawnSpaceObject(SpaceObject newObject)
        {
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
            return c1.Equals(c2);
        }

        public static bool operator !=(Sector c1, Sector c2)
        {
            return !c1.Equals(c2);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Sector))
            {
                return false;
            }

            var sec = (Sector)obj;
            return SectorTransform == sec.SectorTransform;
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