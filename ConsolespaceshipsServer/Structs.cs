using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolespaceshipsServer
{
    //TODO: Remove
    



    struct Vector3
    {
        public float x;
        public float y;
        public float z;

        public Vector3 (float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        

    }

    public struct Vector3Int
    {
        public int x;
        public int y;
        public int z;


        public Vector3Int (int _x, int _y, int _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        public static bool operator ==(Vector3Int c1, Vector3Int c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(Vector3Int c1, Vector3Int c2)
        {
            return c1.Equals(c2);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector3Int))
            {
                return false;
            }

            var vec = (Vector3Int)obj;
            return x == vec.x &&
                   y == vec.y &&
                   z == vec.z;
        }


        public override string ToString()
        {
            string str = "" + x + "," + y + "," + z;
            return str;
        }

        public override int GetHashCode()
        {
            var hashCode = 373119288;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            hashCode = hashCode * -1521134295 + z.GetHashCode();
            return hashCode;
        }
    }
}
