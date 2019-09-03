using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolespaceshipsServer
{
    public class Sector
    {
        private Dictionary<Transform, SpaceObject> spaceObjectList;
        public SectorTransform SectorTransform;


        public Sector (SectorTransform newPos)
        {

            spaceObjectList = new Dictionary<Transform, SpaceObject>();

            SectorTransform = newPos;
        }
        
        public string[] GetSpaceObjectList ()
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

    }
}