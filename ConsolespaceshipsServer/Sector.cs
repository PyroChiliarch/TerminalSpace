using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolespaceshipsServer
{
    public class Sector
    {
        //WARNING
        //Overides Equals()


        private Dictionary<Transform, SpaceObject> spaceObjectList;
        public SectorTransform SectorTransform;


        //=============================================================================
        //Constructors
        //=============================================================================

        public Sector (SectorTransform newPos)
        {

            spaceObjectList = new Dictionary<Transform, SpaceObject>();

            SectorTransform = newPos;
        }











        //=============================================================================
        //General Methods
        //=============================================================================

        internal string[] GetSpaceObjectList ()
        {
            List<string> list = new List<string>();
            foreach (KeyValuePair<Transform, SpaceObject> spaceObject in spaceObjectList)
            {
                list.Add(spaceObject.Value.Name);
            }
            return list.ToArray();
        }


        internal bool SpawnSpaceObject(SpaceObject newObject, Transform pos)
        {
            Console.WriteLine("Attempting to create Object at: " + pos.ToString());
            Console.WriteLine("Location is clear? " + !spaceObjectList.ContainsKey(pos));
            

            if (!spaceObjectList.ContainsKey(pos))
            {
                newObject.Transform = pos;
                spaceObjectList.Add(pos, newObject);
                return true;
            } else
            {
                return false;
            }
            
            
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
            return c1.Equals(c2);
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
            hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<Transform, SpaceObject>>.Default.GetHashCode(spaceObjectList);
            hashCode = hashCode * -1521134295 + EqualityComparer<SectorTransform>.Default.GetHashCode(SectorTransform);
            return hashCode;
        }
    }
}