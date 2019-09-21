using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;


namespace Server2.GameWorld
{

    

    class Instance
    {

        private readonly SectorStorage sectorList = new SectorStorage();

        //Sectors that this  instance is responsible for
        private List<Vector3> ownedSectors; 


        public Instance (List<Vector3> givenSectors )
        {
            ownedSectors = givenSectors;
        }

        public bool GiveSector (Vector3 secPos)
        {
            if (!ownedSectors.Contains(secPos) )
            {
                ownedSectors.Add(secPos);
                return true;
            }

            return false;
        }


    }
}
