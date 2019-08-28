using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Algorithms.Sorting
{
    public static class MergeSorter
    {
        public static void SortTopDown<T>(this IList<T> collection, IComparer<T> comparer = null)
        {
            comparer ??= Comparer<T>.Default;
            var aux = new T[collection.Count];
            SortTopDown(collection, 0, collection.Count - 1, aux, comparer);
        }

        public static void SortBottomUp<T>(this IList<T> collection, IComparer<T> comparer = null)
        {
            comparer ??= Comparer<T>.Default;
            int n = collection.Count;
            var aux = new T[collection.Count];

            for (int sz = 1; sz < n; sz = sz + sz)
                for (int lo = 0; lo < n - sz; lo += sz + sz)
                    Merge(collection, lo, lo + sz - 1, Math.Min(lo + sz + sz - 1, n - 1), aux, comparer);
        }

        private static void Merge<T>(IList<T> collection, int lo, int mid, int hi, T[] aux, IComparer<T> comparer)
        {
            Debug.Assert(IsSorted(collection, lo, mid, comparer));
            Debug.Assert(IsSorted(collection, mid+1, hi, comparer));

            int i = lo;
            int j = mid + 1;

            for (int k = lo; k <= hi; k++)
                aux[k] = collection[k];

            for(int k = lo; k <= hi; k++)
            {
                if (i > mid)
                    collection[k] = aux[j++];
                else if (j > hi)
                    collection[k] = aux[i++];
                else if (SortingBase.Less(aux[j], aux[i], comparer))
                    collection[k] = aux[j++];
                else
                    collection[k] = aux[i++];
            }
        }

        private static void SortTopDown<T>(IList<T> collection, int lo, int hi, T[] aux, IComparer<T> comparer)
        {
            if (hi <= lo)
                return;

            int mid = lo + (hi - lo) / 2;
            SortTopDown(collection, lo, mid, aux, comparer);
            SortTopDown(collection, mid+1, hi, aux, comparer);
            Merge(collection, lo, mid, hi, aux, comparer);
        }

        private static bool IsSorted<T>(IList<T> collection, int lo, int hi, IComparer<T> comparer)
        {
            for (int i = lo + 1; i <= hi; i++)
                if (SortingBase.Less(collection[i], collection[i - 1], comparer)) return false;
            return true;
        }
    }
}
