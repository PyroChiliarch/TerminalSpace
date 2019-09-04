using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Space
{
    abstract class SpaceObject
    {

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
