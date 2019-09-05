using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Space
{
    interface IPilotable
    {
        //TODO Implement IPilotable
        Character Pilot { get; }

        bool AddPilot (Character newPilot);
        bool RemovePilot ();

    }
}
