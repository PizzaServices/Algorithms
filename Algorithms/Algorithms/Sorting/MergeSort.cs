using System;

namespace Algorithms.Sorting
{
    public class MergeSort<T> : SortingBase<T> where T : IComparable<T>
    {
        private static T[] aux;

        public static void SortTopDown(T[] a)
        {
            aux = new T[a.Length];
            SortTopDown(a, 0, a.Length - 1);
        }

        public static void SortBottomUp(T[] a)
        {
            int n = a.Length;
            aux = new T[a.Length];

            for (int sz = 1; sz < n; sz = sz + sz)
                for (int lo = 0; lo < n - sz; lo += sz + sz)
                    Merge(a, lo, lo + sz - 1, Math.Min(lo + sz + sz - 1, n - 1));
        }

        public static void Merge(T[] a, int lo, int mid, int hi)
        {
            int i = lo;
            int j = mid + 1;

            for (int k = lo; k <= hi; k++)
                aux[k] = a[k];

            for(int k = lo; k <= hi; k++)
            {
                if (i > mid)
                    a[k] = aux[j++];
                else if (j > hi)
                    a[k] = aux[j++];
                else if (less(aux[j], aux[i]))
                    a[k] = aux[j++];
                else
                    a[k] = aux[i++];
            }
        }

        private static void SortTopDown(T[] a, int lo, int hi)
        {
            if (hi <= lo)
                return;

            int mid = lo + (hi - lo) / 2;
            SortTopDown(a, lo, mid);
            SortTopDown(a, mid + 1, hi);
            Merge(a, lo, mid, hi);
        }
    }
}
