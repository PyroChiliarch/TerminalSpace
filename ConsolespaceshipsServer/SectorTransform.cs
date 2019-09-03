using System.Collections.Generic;

namespace ConsolespaceshipsServer
{
    public class SectorTransform
    {
        //WARNING
        //Overides Equals()


        //represents a sectors position

        public Vector3Int position;






        //=============================================================================
        //Constructors
        //=============================================================================

        public SectorTransform ()
        {
            position.x = 0;
            position.y = 0;
            position.z = 0;
        }

        public SectorTransform (int x, int y, int z)
        {
            position.x = x;
            position.y = y;
            position.z = z;
        }














        //=============================================================================
        //Overrides
        //=============================================================================


        public override string ToString()
        {
            return position.ToString();
        }

        public static bool operator ==(SectorTransform c1, SectorTransform c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(SectorTransform c1, SectorTransform c2)
        {
            return c1.Equals(c2);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SectorTransform))
            {
                return false;
            }

            var vec = (SectorTransform)obj;
            return position == vec.position;
        }

        public override int GetHashCode()
        {
            return 1825389287 + EqualityComparer<Vector3Int>.Default.GetHashCode(position);
        }
    }
}