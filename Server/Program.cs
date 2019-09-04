using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

using System.Windows.Forms;

namespace Server
{
    class Program
    {

        //
        
        const string version = "v0.1.0";

        static void Main(string[] args)
        {
            //Setup Window
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ServerWindowForm());
        }
    }
}
