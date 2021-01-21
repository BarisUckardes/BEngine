using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEngine.Core.ConsoleDebug
{
    public static class BConsoleLog
    {

        public static void DropLog(string log,LogType logType = LogType.Verbose)
        {
            ConsoleColor formerColor = Console.ForegroundColor;

            switch (logType) // selecting log type
            {
                 
                case LogType.Verbose:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Log: " + log);
                    Console.ForegroundColor = formerColor;
                    break;

                case LogType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Warning: " + log);
                    Console.ForegroundColor = formerColor;
                    break;

                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: " + log);
                    Console.ForegroundColor = formerColor;
                    break;
            }
        }
    }
}
