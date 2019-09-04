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
