using MergeIni.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MergeIni
{
    internal class IniReader : IDisposable
    {
        private readonly Stream _stream;
        private bool _disposedValue;

        public IniReader(Stream stream)
        {
            _stream = stream;
        }

        public IniReader(string filename)
            : this(new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
        }

        private static readonly Regex _sectionNameRegex = new Regex(@"^\s*\[(?<NAME>.+?)\]", RegexOptions.Compiled);
        private static readonly Regex _valueRegex = new Regex(@"^\s*(?<KEY>.+?)\s*=\s*(?<VALUE>.*?)$", RegexOptions.Compiled);

        public IniDocument Read()
        {
            var result = new IniDocument();

            using var reader = new StreamReader(_stream);

            while(!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var sectionNameMatch = _sectionNameRegex.Match(line);
                if(sectionNameMatch.Success)
                {
                    var sectionName = sectionNameMatch.Groups["NAME"].Value;
                    result.Sections.Add(new Section(sectionName));
                    continue;
                }

                var valueMatch = _valueRegex.Match(line);

                if(!valueMatch.Success)
                    throw new InvalidOperationException($"Failed to match key value");

                var lastSection = result.Sections.LastOrDefault();
                if(lastSection == null)
                    throw new InvalidOperationException($"Value without section");

                var key = valueMatch.Groups["KEY"].Value;
                var value = valueMatch.Groups["VALUE"].Value;
                lastSection.Values.Add(new KeyValuePair<string, string>(key, value));
            }

            return result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _stream.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
