using System;
using System.Numerics;

namespace Server2.GameWorld
{

    

    internal class Sector
    {

        public Vector3 Pos;
        private GameObjectList gameObjectList = new GameObjectList();


        
        public void SpawnGameObject (GameObject obj, Transform newTrans)
        {
            gameObjectList.Spawn(obj, this, newTrans);
        }

        public void DespawnGameobject (Guid id)
        {
            gameObjectList.Despawn(id);
        }
    }


    


}