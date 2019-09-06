using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Space
{
    interface IPilotable
    {
        //TODO Receive Control Events from Pilot(Character)
        Character Pilot { get; }

        bool AddPilot (Character newPilot);
        bool RemovePilot ();

    }
}
