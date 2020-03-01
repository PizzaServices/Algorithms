namespace Algorithms.Searching
{
    public class BinarySearch
    {
        /// <summary>
        /// Search for the key in the array 
        /// </summary>
        /// <param name="key">the key to search</param>
        /// <param name="a">the array</param>
        /// <returns>the position of the key otherwise -1</returns>
        public static int Rank(int key, int[] a)
        {
            int lo = 0;
            int hi = a.Length - 1;
            while(lo <= hi)
            {
                int mid = lo + (hi - lo) / 2;
                if (key < a[mid])
                    hi = mid - 1;
                else if (key > a[mid])
                    lo = mid + 1;
                else
                    return mid;
            }
            return -1;
        }
    }
}
