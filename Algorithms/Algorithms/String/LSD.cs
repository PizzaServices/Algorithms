namespace Algorithms.String
{
    public static class Lsd
    {
        public static string[] Sort(string[] array, int stringLength)
        {
            int arrayLength = array.Length;
            const int r = 256;
            string[] aux = new string[arrayLength];

            for (int d = stringLength - 1; d >= 0; d--)
            {
                int[] count = new int[r + 1];
                for (int i = 0; i < arrayLength; i++)
                    count[array[i][d] + 1]++;

                for (int j = 0; j < r; j++)
                    count[j + 1] += count[j];

                for (int i = 0; i < arrayLength; i++)
                    aux[count[array[i][d]]++] = array[i];

                for (int i = 0; i < arrayLength; i++)
                    array[i] = aux[i];
            }

            return array;
        }
    }
}