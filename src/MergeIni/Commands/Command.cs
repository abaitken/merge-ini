using MergeIni.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeIni.Commands
{
    internal abstract class Command
    {
        internal abstract int Execute(CommandLineOptions options);
    }
}
