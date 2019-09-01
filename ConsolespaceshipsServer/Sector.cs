using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolespaceshipsServer
{
    public class Sector
    {
        private Dictionary<SpaceCoord, SpaceObject> spaceObjectList;



        public Sector ()
        {

            spaceObjectList = new Dictionary<SpaceCoord, SpaceObject>();
        }
        
        public string[] GetSpaceObjectList ()
        {
            List<string> list = new List<string>();
            foreach (KeyValuePair<SpaceCoord, SpaceObject> spaceObject in spaceObjectList)
            {
                list.Add(spaceObject.Value.Name);
            }
            return list.ToArray();
        }

        internal bool SpawnSpaceObject(SpaceObject newObject, SpaceCoord pos)
        {

            if (!spaceObjectList.ContainsKey(pos))
            {
                spaceObjectList.Add(pos, newObject);
                return true;
            } else
            {
                return false;
            }
            
            
        }

    }
}