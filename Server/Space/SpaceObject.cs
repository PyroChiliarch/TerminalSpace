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

        public string Name { get; protected set; }

        //TODO Implement Parenting
        public SpaceObject Parent;



        //=============================================================================
        //Parent Dependant Properties
        //=============================================================================
        //The following properties will return the parents Property if it is null
        //Useful if the object has been despawn but is still in sector
        //Eg He boarded a ship, despawned from the sector, but is still in that sector
        //Use GetRaw***() if you need the actual value
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

            internal set
            {
                if (_transform != null)
                {
                    _transform.UpdatedEvent -= OnTransformUpdated;
                }
                _transform = value;
                if (_transform != null)
                {
                    _transform.UpdatedEvent += OnTransformUpdated;
                }

                TransformUpdatedEvent?.Invoke(this, Transform);
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
        //Property Getters
        //=============================================================================

        //Gets Sector independant of parent
        //See _sector Implementation for details
        public Sector GetRawSector ()
        {
            return _sector;
        }

        //Gets Transform independant of parent
        //See _transform Implementation for details
        public Transform GetRawTransform ()
        {
            return _transform;
        }




        public void MoveTo (Vector3 newPos)
        {
            Transform.Position = newPos;
            TransformUpdatedEvent?.Invoke(this, Transform);
        }




        //=============================================================================
        //Methods
        //=============================================================================


        //Violently Removes an object from existence
        //DO NOT USE if you want to keep the object for use later, use Sector.Despawn() instead
        //DO USE if this object was destroyed violently by explosions rockets lasers or darth mauls saber
        public virtual void Destroy ()
        {
            OnDestroy();
        }


        




        //=============================================================================
        //Events
        //=============================================================================

        //Called when Transform is updated
        public void OnTransformUpdated ()
        {
            TransformUpdatedEvent?.Invoke(this, Transform);
        }

        //Called when object is destroyed
        //Called Last
        public virtual void OnDestroy()
        {
            this.DestroyEvent(this, EventArgs.Empty);
        }
        

        public delegate void TransformUpdatedHandler (SpaceObject caller, Transform transform);

        public event EventHandler DestroyEvent;
        public event TransformUpdatedHandler TransformUpdatedEvent;
    }
}
