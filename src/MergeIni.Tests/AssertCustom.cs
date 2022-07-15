using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeIni.Tests
{
    internal static class AssertCustom
    {
        public static string Format(string value)
        {
            var result = value;

            result = result.Replace("\r", "\\r");
            result = result.Replace("\n", "\\n");

            return result;
        }

        public static void AreEqual(string expected, string actual)
        {
            var e = Format(expected);
            var a = Format(actual);

            AreEqualInternal(e, a);
        }

        private static string ReplaceNulls(object obj)
        {
            if (obj == null)
                return "(NULL)";

            var inputString = obj.ToString();

            if (inputString == null)
                return "(NULL)";

            return inputString;
        }

        private static void AreEqualInternal(string expected, string actual)
        {
            if (!actual.GetType().Equals(expected.GetType()))
                throw new AssertFailedException($@"AssertCustom.AreEqual failed. Types do not match.");
            
            if (object.Equals(expected, actual))
                return;


            if (actual == null || expected == null)
                throw new AssertFailedException($@"AssertCustom.AreEqual failed. Expected non-null result.");

            var differenceIndicator = new StringBuilder();
            for (int i = 0; i < Math.Max(expected.Length, actual.Length); i++)
            {
                if (i >= expected.Length)
                {
                    differenceIndicator.Append('#');
                    continue;
                }

                if(i >= actual.Length)
                {
                    differenceIndicator.Append(' ');
                    continue;
                }

                var e = expected[i];
                var a = actual[i];

                if(e != a)
                {
                    differenceIndicator.Append('^');
                    continue;
                }

                differenceIndicator.Append(' ');
            }


            throw new AssertFailedException($@"AssertCustom.AreEqual failed. Expected and actual do not match
Expected:  <{expected}>
Actual:    <{actual}>
Difference: {differenceIndicator}");
        }
    }
}
