using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Space
{
    abstract class SpaceObject
    {

        public Guid ID;
        public uint IdInSector;

        public Transform Transform;
        public Sector Sector;
        
        //TODO Implement
        public SpaceObject Parent;

        public string Name
        {
            get;
            protected set;
        }









        //=============================================================================
        //Constructors
        //=============================================================================

        public SpaceObject()
        {
            ID = Guid.NewGuid();
            Name = "Undefined";
            Sector = null;
            Transform = null;
            IdInSector = 0;
        }


        //=============================================================================
        //Methods
        //=============================================================================


        //Removes an object from existence
        public virtual void Destroy ()
        {
            OnDestroy();
        }





        //=============================================================================
        //Events
        //=============================================================================


        //Called when object is destroyed
        //Called Last
        public virtual void OnDestroy()
        {
            this.DestroyEvent(this, EventArgs.Empty);
        }
        

        public event EventHandler DestroyEvent;
    }
}
