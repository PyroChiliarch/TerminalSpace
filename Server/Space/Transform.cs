using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Space
{
    public class Transform
    {
        //WARNING
        //Overides Equals()


        Vector3 _position;
        public Vector3 Position
        {
            get
            {
                return _position;
            }

            set
            {
                _position = value;
                UpdatedEvent?.Invoke();
            }
        }








        public Transform ()
        {
            Position = new Vector3(0, 0, 0);
        }

        public Transform (float x, float y, float z)
        {
            Position = new Vector3(x, y, z);
        }

















        //=============================================================================
        //Overrides
        //=============================================================================

        public override string ToString()
        {
            return "" + Position.x + "," + Position.y + "," + Position.z;
        }

        public static bool operator ==(Transform c1, Transform c2)
        {
            if (ReferenceEquals(c1, c2)) return true;
            if (c1 is null) return false;
            return c1.Equals(c2);
        }

        public static bool operator !=(Transform c1, Transform c2)
        {
            return !(c1 == c2);
        }

        public bool Equals(Transform other)
        {
            if (other is null)
                return false;

            return (this.Position == other.Position);
        }


        public override bool Equals(object obj)
        {
            if (!(obj is Transform other))
                return false;
            return Equals(other);
        }



        public override int GetHashCode()
        {
            return 1206833562 + EqualityComparer<Vector3>.Default.GetHashCode(Position);
        }


        public delegate void UpdatedEventHandler ();

        public event  UpdatedEventHandler UpdatedEvent;
    }
}
