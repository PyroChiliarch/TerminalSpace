using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolespaceshipsServer
{
    class Transform
    {
        public Vector3 position;
        public SectorCoord sector;

        public Transform ()
        {
            position = new Vector3()
            {
                x = 0,
                y = 0,
                z = 0
            };

            sector = new SectorCoord()
            {
                x = 0,
                y = 0,
                z = 0
            };
        }
    }
}
