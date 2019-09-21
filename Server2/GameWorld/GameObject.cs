using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace Server2.GameWorld
{
    class GameObject
    {

        //Has the object been initialised?
        public bool IsLoaded {get; private set; } = false;

        private Guid id = Guid.NewGuid();

        public Sector CurrentSector;
        public Transform Transform;

        public GameObject ()
        {
            id = Guid.Empty;
        }

        //Needs to be initialised so that the Guid is not incorrectly set
        public bool Initialise ()
        {
            if (IsLoaded == false)
            {
                IsLoaded = true;
                id = Guid.NewGuid();
                return true;
            }


            return false;


        }

        public Guid GetID ()
        {
            return id;
        }




        public bool SetID (Guid newID)
        {
            if (IsLoaded = false && newID != null)
            {
                id = newID;
                return true;
            }

            return false;
        }

    }
}
