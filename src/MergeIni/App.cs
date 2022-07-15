using MergeIni.Commands;
using MergeIni.Options;

internal class App
{
    public App()
    {
    }

    internal int Run(string[] args)
    {
        var values = new CommandLineParser().Parse(args);
        var options = new CommandLineOptions(values);

        var command = CreateCommand(options);
        return command.Execute(options);
    }

    private Command CreateCommand(CommandLineOptions options)
    {
        if (options.DisplayHelp)
            return new DisplayHelpCommand();
        return new MergeFilesCommand();
    }
}