using MergeIni.Options;

namespace MergeIni.Commands
{
    class DisplayHelpCommand : Command
    {
        internal override int Execute(CommandLineOptions options)
        {
            ConsoleHelper.WriteHeader();

            Console.WriteLine(@"
 Merges ini files together.
 Setting precedence is determines by merge file order. Last setting will win.

 -h                 Display this help
 -o <file-path>     Output file
 -m <file-path>     Merge file
 -l <file-path>     Merge file list
");

            return ExitCodes.OK;
        }
    }
}
