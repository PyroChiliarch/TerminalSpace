using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolespaceshipsServer
{

    struct SectorCoord
    {
        public static bool operator ==(SectorCoord c1, SectorCoord c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(SectorCoord c1, SectorCoord c2)
        {
            return !c1.Equals(c2);
        }

        public int x;
        public int y;
        public int z;



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
