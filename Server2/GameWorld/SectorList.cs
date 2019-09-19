using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Server2.GameWorld
{
    class SectorStorage
    {

        private readonly Dictionary<Vector3, Sector> sectorList = new Dictionary<Vector3, Sector>();

        public SectorStorage ()
        {

        }



        //Adds a sector, retaining its current position
        public bool AddSector(Sector sec)
        {

            if (sec.Pos != null)
            {
                AddSectorAtPos(sec.Pos, sec);
                return true;
            }

            return false;
        }


        //Adds a sector at specified position
        public bool AddSectorAtPos(Vector3 pos, Sector sec)
        {
            if (pos != null)
            {
                sec.Pos = pos;
                sectorList.Add(pos, sec);
                return true;
            }

            

            return false;
        }

    }
}
