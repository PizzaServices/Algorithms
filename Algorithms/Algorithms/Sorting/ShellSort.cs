using System;

namespace Algorithms.Sorting
{
    public class ShellSort<T> : SortingBase<T> where T : IComparable<T>
    {
        public static void sort(T[] a)
        {
            int n = a.Length;
            int h = 1;
            while(h < n/3)
            {
                h = 3 * h + 1;
            }

            while(h >= 1)
            {
                for(int i = h; i < n; i++)
                {
                    for(int j = i; j >= h && less(a[j], a[j-h]); j -= h)
                    {
                        exch(a, j, j - h);
                    }
                }
                h /= 3;
            }
        }
    }
}
