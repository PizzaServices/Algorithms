using System;

namespace Algorithms.Sorting
{
    public class InsertionSort<T> : SortingBase<T> where T : IComparable<T>
    {
        public static void sort(T[] a)
        {
            int n = a.Length;
            for(int i = 1; i < n; i++)
            {
                for(int j = i; j > 0 && less(a[j], a[j-1]); j--)
                {
                    exch(a, j, j - 1);
                }
            }
        }
    }
}
