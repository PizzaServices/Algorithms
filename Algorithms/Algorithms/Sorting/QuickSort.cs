using System.Collections.Generic;

namespace Algorithms.Sorting
{
    public static class QuickSorter
    {
        /// <summary>
        /// Sort the List
        /// </summary>
        /// <typeparam name="T">the type of the stored values</typeparam>
        /// <param name="collection">the collection to sort</param>
        /// <param name="comparer">the comparer for the values, if not specified the default comparer is used</param>
        public static void QuickSort<T>(this IList<T> collection, IComparer<T> comparer = null)
        {
            comparer ??= Comparer<T>.Default;
            Sort(collection, 0, collection.Count - 1, comparer);
        }

        /// <summary>
        /// Sort the List
        /// </summary>
        /// <typeparam name="T">the type of the stored values</typeparam>
        /// <param name="collection">the collection to sort</param>
        /// <param name="comparer">the comparer for the values, if not specified the default comparer is used</param>
        public static void ThreeWaySort<T>(this IList<T> collection, IComparer<T> comparer = null)
        {
            comparer ??= Comparer<T>.Default;
            ThreeWaySort(collection, 0, collection.Count - 1, comparer);
        }

        private static void ThreeWaySort<T>(IList<T> collection, int lo, int hi, IComparer<T> comparer)
        {
            if (hi <= lo)
                return;

            int lt = lo;
            int i = lo + 1;
            int gt = hi;

            var v = collection[lo];
            while(i <= gt)
            {
                int cmp = comparer.Compare(collection[i], v);
                if (cmp < 0)
                    SortingBase.Exch(collection, lt++, i++);
                else if (cmp > 0)
                    SortingBase.Exch(collection, i, gt--);
                else
                    i++;
            }
            Sort(collection, lo, lt - 1, comparer);
            Sort(collection, gt + 1, hi, comparer);
        }

        private static void Sort<T>(IList<T> collection, int lo, int hi, IComparer<T> comparer)
        {
            if (hi <= lo)
                return;

            int j = Partition(collection, lo, hi, comparer);
            Sort(collection, lo, j - 1, comparer);
            Sort(collection, j + 1, hi, comparer);
        }

        private static int Partition<T>(IList<T> collection, int lo, int hi, IComparer<T> comparer)
        {
            int i = lo;
            int j = hi + 1;

            var v = collection[lo];
            while(true)
            {
                while (SortingBase.Less(collection[++i], v, comparer))
                    if (i == hi)
                        break;

                while (SortingBase.Less(v, collection[--j], comparer))
                    if (j == lo)
                        break;

                if (i >= j)
                    break;

                SortingBase.Exch(collection, i, j);
            }
            SortingBase.Exch(collection, lo, j);
            return j;
        }
    }
}
