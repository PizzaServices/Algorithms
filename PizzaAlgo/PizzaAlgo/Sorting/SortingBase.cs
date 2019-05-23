using System;

namespace PizzaAlgo.Sorting
{
    public abstract class SortingBase<T> where T : IComparable<T>
    {
        protected static bool less(T v, T w)
        {
            return v.CompareTo(w) < 0;
        }

        protected static void exch(T[] a, int i, int j)
        {
            var t = a[i];
            a[i] = a[j];
            a[j] = t;
        }

        protected static bool isSorted(T[] a)
        {
            for(int i = 1; i < a.Length; i++)
            {
                if (less(a[i], a[i - 1]))
                    return false;
            }

            return true;
        }
    }
}
