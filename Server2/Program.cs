using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creates the Server Window
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ServerWindow());
        }
    }
}
