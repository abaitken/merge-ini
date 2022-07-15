using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeIni
{
    internal static class ConsoleHelper
    {
        public static void WriteHeader()
        {
            Console.WriteLine("merge-ini by Alex Boyne-Aitken");
        }

        internal static void WriteError(string text)
        {
            Console.WriteLine($@"ERROR: {text}");
        }

        internal static void WriteHelpHint()
        {
            Console.WriteLine($@"Use 'merge-ini -h' to display help information.");
        }
    }
}
