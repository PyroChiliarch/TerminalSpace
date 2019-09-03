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

        public Transform ()
        {
            position = new Vector3()
            {
                x = 0,
                y = 0,
                z = 0
            };
        }
    }
}
