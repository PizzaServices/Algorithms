using System.Collections.Generic;

namespace Algorithms.Searching
{
    public static class BinarySearchExtension
    {
        public static int BSearch<T>(this IList<T> collection, T key, IComparer<T> comparer = null)
        {
            comparer ??= Comparer<T>.Default;

            int lo = 0;
            int hi = collection.Count - 1;
            while(lo <= hi)
            {
                int mid = lo + (hi - lo) / 2;
                int cmp = comparer.Compare(key, collection[mid]);

                if (cmp < 0)//key < array[mid])
                    hi = mid - 1;
                else if (cmp > 0) //key > array[mid])
                    lo = mid + 1;
                else
                    return mid;
            }
            return -1;
        }
    }
}
