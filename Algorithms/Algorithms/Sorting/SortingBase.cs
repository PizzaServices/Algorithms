using System.Collections.Generic;

namespace Algorithms.Sorting
{
    internal static class SortingBase
    {
        public static bool Less<T>(T v, T w, IComparer<T> comparer)
        {
            return comparer.Compare(v, w) < 0;
        }

        public static void Exch<T>(IList<T> collection, int i, int j)
        {
            var tmp = collection[i];
            collection[i] = collection[j];
            collection[j] = tmp;
        }

        public static bool IsSorted<T>(IList<T> collection, IComparer<T> comparer)
        {
            for(int i = 1; i < collection.Count; i++)
            {
                if (Less(collection[i], collection[i - 1], comparer))
                    return false;
            }

            return true;
        }
    }
}
