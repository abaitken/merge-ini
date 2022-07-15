using MergeIni.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeIni.Options
{
    internal class CommandLineOptions
    {
        private readonly List<CommandLineOption> _options;

        public CommandLineOptions(IEnumerable<CommandLineOption> options)
        {
            _options = options.ToList();
        }

        public bool DisplayHelp
        {
            get => _options.Count == 0 || ContainsKey("h") || ContainsKey("?") || ContainsKey("help");
        }

        public string? OutputFile
        {
            get
            {
                if (!TryGetValue("o", out var result))
                    return null;

                return result.Value;
            }
        }

        public IEnumerable<MergeItem> MergeItems
        {
            get
            {
                var sortedOptions = _options.OrderBy(i => i.Order).Select(i => i);
                foreach (var item in sortedOptions)
                {
                    if(item.Key.Equals("m"))
                    {
                        yield return new MergeItem
                        {
                            File = item.Value ?? string.Empty,
                            Type = MergeSource.DirectMerge
                        };
                    }

                    if (item.Key.Equals("l"))
                    {
                        yield return new MergeItem
                        {
                            File = item.Value ?? string.Empty,
                            Type = MergeSource.FileList
                        };
                    }
                }
            }
        }

        private bool TryGetValue(string keyname, out CommandLineOption result)
        {
            var index = _options.IndexOf(i => i.Key.Equals(keyname));
            if (index < 0)
            {
                result = default;
                return false;
            }

            result = _options[index];
            return true;
        }

        private bool ContainsKey(string keyname)
        {
            return _options.Any(i => i.Key.Equals(keyname));
        }
    }
}
