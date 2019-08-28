using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Linq;

namespace AlgorithmsTest.SortingTests.TestDataGeneration
{
    public class SortingTestDataGenerator
    {
        private readonly string _fileName = "random-numbers.txt";

        public IEnumerable<int> UnsortedList { get; private set; }
        public IEnumerable<int> SortedList { get; private set; }

        public IEnumerable<int> CreateRandomNumbers(int count = 10000)
        {
            if (count > 10000)
                count = 10000;

            var list = GetNumbers(_fileName);
            UnsortedList = list;
            SortedList = list.OrderBy(number => number);
            return list;
        }

        private static IEnumerable<int> GetNumbers(string fileName)
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string directory = Path.GetDirectoryName(path) + "\\SortingTests\\TestDataGeneration\\";

            using (var stream = new FileStream(directory + fileName, FileMode.Open))
            {
                Debug.Assert(stream != null, "Could not find resource {0}", fileName);
                using (var file = new StreamReader(stream))
                {
                    while (!file.EndOfStream)
                    {
                        string line = file.ReadLine() ?? string.Empty;

                        if (int.TryParse(line, out var result))
                            yield return result;
                    }
                }
            }
        }
    }
}
