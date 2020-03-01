using System.Collections.Generic;

namespace Algorithms.Sorting
{
    public static class SelectionSorter
    {
        /// <summary>
        /// Sort the List
        /// </summary>
        /// <typeparam name="T">the type of the stored values</typeparam>
        /// <param name="collection">the collection to sort</param>
        /// <param name="comparer">the comparer for the values, if not specified the default comparer is used</param>
        public static void SelectionSort<T>(this IList<T> collection, IComparer<T> comparer = null)
        {
            comparer ??= Comparer<T>.Default;
            int n = collection.Count;
            for(int i = 0; i < n; i++)
            {
                int min = i;
                for(int j = i+1; j < n; j++)
                {
                    if (SortingBase.Less(collection[j], collection[min], comparer))
                        min = j;
                }
                SortingBase.Exch(collection, i, min);
            }
        }
    }
}
