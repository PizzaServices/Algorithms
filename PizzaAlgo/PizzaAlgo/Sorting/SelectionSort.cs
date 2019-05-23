using System;

namespace PizzaAlgo.Sorting
{
    public class SelectionSort<T> : SortingBase<T> where T : IComparable<T>
    {
        public static void sort(T[] a)
        {
            int n = a.Length;
            for(int i = 0; i < n; i++)
            {
                int min = i;
                for(int j = i+1; j < n; j++)
                {
                    if (less(a[j], a[min]))
                        min = j;
                }
                exch(a, i, min);
            }
        }
    }
}
