using System;

namespace PizzaAlgo.Sorting
{
    public class QuickSort<T> : SortingBase<T> where T : IComparable<T>
    {
        public static void Sort(T[] a)
        {
            Sort(a, 0, a.Length - 1);
        }

        public static void ThreeWaySort(T[] a)
        {
            ThreeWaySort(a, 0, a.Length - 1);
        }

        private static void ThreeWaySort(T[] a, int lo, int hi)
        {
            if (hi <= lo)
                return;

            int lt = lo;
            int i = lo + 1;
            int gt = hi;

            var v = a[lo];
            while(i <= gt)
            {
                int cmp = a[i].CompareTo(v);
                if (cmp < 0)
                    exch(a, lt++, i++);
                else if (cmp > 0)
                    exch(a, i, gt--);
                else
                    i++;
            }
            Sort(a, lo, lt - 1);
            Sort(a, gt + 1, hi);
        }

        private static void Sort(T[] a, int lo, int hi)
        {
            if (hi <= lo)
                return;

            int j = Partition(a, lo, hi);
            Sort(a, lo, j - 1);
            Sort(a, j + 1, hi);
        }

        private static int Partition(T[] a, int lo, int hi)
        {
            int i = lo;
            int j = hi + 1;

            var v = a[lo];
            while(true)
            {
                while (less(a[++i], v))
                    if (j == hi)
                        break;

                while (less(v, a[--j]))
                    if (j == lo)
                        break;

                if (i >= j)
                    break;

                exch(a, i, j);
            }
            exch(a, lo, j);
            return j;
        }
    }
}
