using System;
using System.Collections.Generic;

namespace Algorithms.Sorting
{
    public static class ShellSorter
    {
        /// <summary>
        /// Sort the List
        /// </summary>
        /// <typeparam name="T">the type of the stored values</typeparam>
        /// <param name="collection">the collection to sort</param>
        /// <param name="comparer">the comparer for the values, if not specified the default comparer is used</param>
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
