using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeIni.Tests
{
    internal static class StringExtensions
    {
        public static Stream ToStream(this string s)
        {
            using var ms = new MemoryStream();

            using var writer = new StreamWriter(ms);
            writer.Write(s);

            writer.Flush();
            var buffer = ms.ToArray();
            return new MemoryStream(buffer);
        }
    }
}
