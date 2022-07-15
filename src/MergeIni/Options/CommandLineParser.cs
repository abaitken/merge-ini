using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeIni.Options
{
    internal class CommandLineParser
    {
        public IEnumerable<CommandLineOption> Parse(string[] args)
        {
            if (args.Length == 0)
                yield break;

            var order = 0;

            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];

                string key;
                string? value = null;

                if (arg.StartsWith('/') || arg.StartsWith('-'))
                {
                    key = arg[1..];

                    i++;
                    if (i < args.Length)
                    {
                        var nextArg = args[i];
                        var nextArgIsSwitch = nextArg.StartsWith('/') || nextArg.StartsWith('-');

                        if (!nextArgIsSwitch)
                            value = nextArg;
                        else
                            i--;
                    }
                }
                else
                {
                    key = string.Empty;
                    value = arg;
                }

                yield return new CommandLineOption
                {
                    Key = key,
                    Order = order,
                    Value = value
                };
                order++;
            }

        }
    }
}
