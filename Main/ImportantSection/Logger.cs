using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ImportantSection
{
    public class Logger
    {
        public static void writeLine(object b)
        {
            Console.WriteLine(b);
        }
        public static void info(object b)
        {
#if !DEBUG
            Console.WriteLine(b);
#endif
        }
        public static void debug(object b)
        {
#if DEBUG
            Console.WriteLine(b);
#endif
        }
        public static void log(object info, object debug)
        {
#if DEBUG
            Console.WriteLine(debug);
#else
            Console.WriteLine(info);
#endif
        }
    }
}
