using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server2.GameWorld
{
    


    class GameObjectList
    {
        private readonly Dictionary<Guid, GameObject> gameObjectList = new Dictionary<Guid, GameObject>();


        public GameObjectList ()
        {

        }




        public bool Spawn(GameObject obj, Sector sec, Transform newTrans)
        {
            if (obj.IsLoaded == true)
            {
                obj.Transform = newTrans;
                obj.CurrentSector = sec;
                gameObjectList.Add(obj.GetID(), obj);
                return true;
            }

            return false;
        }

        public bool Despawn (Guid id)
        {
            if (gameObjectList.ContainsKey(id))
            {
                GameObject obj = gameObjectList[id];
                obj.Transform = null;
                obj.CurrentSector = null;
                gameObjectList.Remove(id);
                return true;
            }

            return false;
        }

    }
}
