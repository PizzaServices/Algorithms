using System.Collections.Generic;

namespace Algorithms.Sorting
{
    public static class InsertionSorter
    {
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
