using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Server2.Networking;

namespace Server2.GameWorld
{

    

    class Instance
    {
        private NetworkService networkService;

        private readonly SectorStorage sectorList = new SectorStorage();

        //Sectors that this  instance is responsible for
        private List<Vector3> ownedSectors; 


        public Instance (NetworkService netServ, List<Vector3> givenSectors )
        {
            networkService = netServ;
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

        public bool TakeSector (Vector3 secPos)
        {
            if (ownedSectors.Contains(secPos))
            {
                ownedSectors.Remove(secPos);
                return true;
            }

            return false;
        }

    }
}
