using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeIni.Model
{
    internal class Section
    {
        public Section(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public List<KeyValuePair<string, string>> Values { get; } = new List<KeyValuePair<string, string>>();
    }
}
