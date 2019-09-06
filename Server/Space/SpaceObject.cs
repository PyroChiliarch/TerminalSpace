using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Space
{
    abstract class SpaceObject
    {

        //=============================================================================
        //Fields and Properties
        //=============================================================================

        public Guid ID;
        public uint IdInSector;

        //TODO Implement Parenting
        public SpaceObject Parent;

        public string Name { get; protected set; }

        private Sector _sector;
        public Sector Sector
        {
            get
            {
                if (_sector != null)
                {
                    return _sector;
                }
                else if (Parent != null)
                {
                    return Parent.Sector;
                }
                else
                {
                    return null;
                }
            }

            set
            {
                _sector = value;
            }
        }

        private Transform _transform;
        public Transform Transform 
        {
            get 
            {
                if (_transform != null)
                {
                    return _transform;
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
                this._transform = value;
            }
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
