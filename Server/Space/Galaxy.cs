using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Space
{
    static class Galaxy
    {
        //Represents a Galaxy
        //Stores, spawns sectors


        //All sectors exist, but are not spawned and stored until they are needed
        //Once they are spawned they are stored in sectorList
        //For this reason, methods that create of delete sectors are private
        static private Dictionary<SectorTransform, Sector> sectorList = new Dictionary<SectorTransform, Sector>();


        
        //Return Sector reference
        static public Sector GetSector (SectorTransform pos)
        {
            Sector sector;


            //spawn the sector if it dosn't exist
            if (sectorList.TryGetValue(pos, out sector))
            {
                return sector;
            }
            else
            {
                Sector newSector = SpawnSector(pos);
                return newSector;
            }
        }

        //spawn a sector
        static private Sector SpawnSector (SectorTransform pos)
        {
            Console.WriteLine("There are " + sectorList.Count + " sectors");
            Sector newSector = new Sector(pos);
            sectorList.Add(pos, newSector);
            Console.WriteLine("Spawned New Sector: " + pos.ToString());
            return newSector;
        }

    }
}
