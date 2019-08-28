using System;
using System.Collections.Generic;

namespace Algorithms.Sorting
{
    public static class ShellSorter
    {
        public static void ShellSort<T>(this IList<T> collection, IComparer<T> comparer = null)
        {
            comparer ??= Comparer<T>.Default;

            int n = collection.Count;
            int h = 1;
            while(h < n/3)
            {
                h = 3 * h + 1;
            }

            while(h >= 1)
            {
                for(int i = h; i < n; i++)
                {
                    for(int j = i; j >= h && SortingBase.Less((T)collection[j], (T)collection[j-h], comparer); j -= h)
                    {
                        SortingBase.Exch(collection, j, j - h);
                    }
                }
                h /= 3;
            }
        }
    }
}
