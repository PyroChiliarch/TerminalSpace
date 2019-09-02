using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolespaceshipsServer
{

    struct SectorCoord
    {

        public int x;
        public int y;
        public int z;







        //=============================================================================
        //Logic Operators
        //=============================================================================

        public static bool operator ==(SectorCoord c1, SectorCoord c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(SectorCoord c1, SectorCoord c2)
        {
            return !c1.Equals(c2);
        }






        //=============================================================================
        //Overides
        //=============================================================================
        //Return the Coordinates in string format
        public override string ToString()
        {
            string str = "" + x + "," + y + "," + z;
            return str;
        }

        //Auto generated
        //Do we really need to define this?
        public override bool Equals(object obj)
        {
            if (!(obj is SectorCoord))
            {
                return false;
            }

            var coord = (SectorCoord)obj;
            return x == coord.x &&
                   y == coord.y &&
                   z == coord.z;
        }

        //Auto Generated
        //Do we really need to define this?
        public override int GetHashCode()
        {
            var hashCode = 373119288;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            hashCode = hashCode * -1521134295 + z.GetHashCode();
            return hashCode;
        }

        
    }


    struct SpaceCoord
    {
        public float x;
        public float y;
        public float z;

        
    }
    
}
