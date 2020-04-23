using System.Collections.Generic;

namespace Algorithms.Sorting
{
    public static class InsertionSorter
    {
        /// <summary>
        /// Sort the List
        /// </summary>
        /// <typeparam name="T">the type of the stored values</typeparam>
        /// <param name="collection">the collection to sort</param>
        /// <param name="comparer">the comparer for the values, if not specified the default comparer is used</param>
        public static void InsertionSort<T>(this IList<T> collection, IComparer<T> comparer = null)
        {
            comparer ??= Comparer<T>.Default;

            int n = collection.Count;
            for(int i = 1; i < n; i++)
            {
                for(int j = i; j > 0 && SortingBase.Less((T)collection[j], (T)collection[j-1], comparer); j--)
                {
                    SortingBase.Exch(collection, j, j - 1);
                }
            }
        }
    }
}
