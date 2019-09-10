using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;


namespace ClientGUI
{
    class Program
    {



        static void Main(string[] args)
        {
            
            double updateSec = 30;
            double frameSec = 60;
            int winHeight = 600;
            int winWidth = 800;

            //Disposible
            TerminalSpaceWindow window = new TerminalSpaceWindow(winWidth, winHeight);
            window.Run(updateSec, frameSec);
            
            window.Dispose();

        }
    }
}
