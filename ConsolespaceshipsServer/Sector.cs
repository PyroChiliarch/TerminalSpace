using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolespaceshipsServer
{
    public class Sector
    {
        private Dictionary<Vector3, SpaceObject> spaceObjectList;



        public Sector ()
        {

            spaceObjectList = new Dictionary<Vector3, SpaceObject>();
        }
        
        public string[] GetSpaceObjectList ()
        {
            List<string> list = new List<string>();
            foreach (KeyValuePair<Vector3, SpaceObject> spaceObject in spaceObjectList)
            {
                list.Add(spaceObject.Value.Name);
            }
            return list.ToArray();
        }

        internal bool SpawnSpaceObject(SpaceObject newObject, Vector3 pos)
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