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

        private Transform transform;
        public Transform Transform 
        {
            get 
            {
                if (transform != null)
                {
                    return transform;
                }
                else if (Parent != null)
                {
                    return Parent.Transform;
                }
                else
                {
                    return null;
                }
            }

            set 
            {
                this.transform = value;
            }
        }
        public Sector Sector;
        
        //TODO Implement Parenting
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
        //Property Accessors
        //=============================================================================

        public virtual Transform GetTransform ()
        {
            if (Transform != null)
            {
                return Transform;
            }
            else if (Parent != null)
            {
                return Parent.Transform;
            }
            else
            {
                throw new NullReferenceException("Tried to reference null transform\nHas the object been spawned?");
            }
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
