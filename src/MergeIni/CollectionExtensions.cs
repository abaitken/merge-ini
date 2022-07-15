using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeIni
{
    internal static class CollectionExtensions
    {
        public static int IndexOf<T>(this IList<T> collection, Func<T, bool> predicate)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                var item = collection[i];
                if (predicate(item))
                    return i;
            }

            return -1;
        }
    }
}
