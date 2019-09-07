using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Space
{
    interface IHasRadar
    {


        //Character Event subscriber
        void PingRadar (Character character, string commands);

    }
}
