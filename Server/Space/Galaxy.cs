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
        readonly static private Dictionary<SectorTransform, Sector> sectorList = new Dictionary<SectorTransform, Sector>();


        
        //Return Sector reference
        static public Sector GetSector (SectorTransform pos)
        {


            //spawn the sector if it dosn't exist
            if (sectorList.TryGetValue(pos, out Sector sector))
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
            
            //Create the sector
            Sector newSector = new Sector(pos);
            //Store the sector
            sectorList.Add(pos, newSector);

            //Fire the event
            SectorSpawnedEvent(newSector);

            Console.WriteLine("There are " + sectorList.Count + " sectors");
            Console.WriteLine("Spawned New Sector: " + pos.ToString());
            return newSector;
        }


        public delegate void SectorSpawnedHandler (Sector sector);

        static public event SectorSpawnedHandler SectorSpawnedEvent;
    }
}
