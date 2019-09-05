using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Space
{
    class Transform
    {
        //WARNING
        //Overides Equals()



        public Vector3 position;








        public Transform ()
        {
            position = new Vector3(0, 0, 0);
        }

        public Transform (float x, float y, float z)
        {
            position = new Vector3(x, y, z);
        }

















        //=============================================================================
        //Overrides
        //=============================================================================

        public override string ToString()
        {
            return "" + position.x + "," + position.y + "," + position.z;
        }

        public static bool operator ==(Transform c1, Transform c2)
        {
            if (ReferenceEquals(c1, c2)) return true;
            if (ReferenceEquals(c1, null)) return false;
            return c1.Equals(c2);
        }

        public static bool operator !=(Transform c1, Transform c2)
        {
            return !(c1 == c2);
        }

        public bool Equals(Transform other)
        {
            if (ReferenceEquals(other, null))
                return false;

            return (this.position == other.position);
        }


        public override bool Equals(object obj)
        {

            Transform other = obj as Transform;
            if (ReferenceEquals(other, null))
                return false;
            return Equals(other);
        }



        public override int GetHashCode()
        {
            return 1206833562 + EqualityComparer<Vector3>.Default.GetHashCode(position);
        }
    }
}
