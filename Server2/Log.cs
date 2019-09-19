using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server2
{
    static class Log
    {

        public static void AddLogString (String msg)
        {
            Console.WriteLine(DateTime.Now.ToString() + ": " + msg );

        }

        public static void AddLogError (Exception e)
        {
            Console.WriteLine(DateTime.Now.ToString() + ": Critical Error!");
            Console.WriteLine(e.StackTrace);
            Console.WriteLine(e.Message);
        }
    }
}
