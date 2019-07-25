using System;

namespace PizzaAlgo.String
{
    public static class MSD
    {
        private static int r = 256;
        private static string[] aux;

        private static readonly int m = 15;

        public static void Sort(string[] array)
        {
            int arrayLength = array.Length;
            aux = new string[arrayLength];
            Sort(array, 0, arrayLength-1, 0, aux);
        }

        private static void Sort(string[] array, int lo, int hi, int d, string[] aux)
        {

            if (hi <= lo + m)
            {
                Insertion(array, lo, hi, d);
                return;
            }

            int[] count = new int[r + 2];
            for (int i = lo; i <= hi; i++)
            {
                int c = CharAt(array[i], d);
                count[c + 2]++;
            }

            for (int r = 0; r < r + 1; r++)
                count[r + 1] += count[r];

            for (int i = lo; i <= hi; i++)
            {
                int c = CharAt(array[i], d);
                aux[count[c + 1]++] = array[i];
            }

            for (int i = lo; i <= hi; i++)
                array[i] = aux[i - lo];


            for (int j = 0; j < r; j++)
                Sort(array, lo + count[j], lo + count[j + 1] - 1, d + 1, aux);
        }

        private static int CharAt(string s, int d)
        {
            if (d < s.Length)
                return s[d];
            else
                return -1;
        }


        private static void Insertion(string[] a, int lo, int hi, int d)
        {
            for (int i = lo; i <= hi; i++)
                for (int j = i; j > lo && Less(a[j], a[j - 1], d); j--)
                    Exch(a, j, j - 1);
        }

        private static void Exch(string[] a, int i, int j)
        {
            string temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }
        private static bool Less(string v, string w, int d)
        {
            for (int i = d; i < Math.Min(v.Length, w.Length); i++)
            {
                if (v[i] < w[i]) return true;
                if (v[i] > w[i]) return false;
            }
            return v.Length < w.Length;
        }
    }
}