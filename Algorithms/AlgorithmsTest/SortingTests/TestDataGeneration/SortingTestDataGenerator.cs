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

        private static IEnumerable<int> _numbers;

        public IEnumerable<int> UnsortedList { get; private set; }
        public IEnumerable<int> SortedList { get; private set; }

        public IEnumerable<int> CreateRandomNumbers(int count = 10000)
        {
            if (count > 10000)
                count = 10000;

            if (_numbers == null)
                _numbers = GetNumbers(_fileName, 10000);

            var list = _numbers.Take(count).ToList();
            UnsortedList = list;

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            SortedList = list.OrderBy(number => number).ToList();
            stopwatch.Stop();
            Console.WriteLine("C# Sorting-Algorithm: {0}", stopwatch.Elapsed);
            return list;
        }

        private static IEnumerable<int> GetNumbers(string fileName, int count)
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
                    while (!file.EndOfStream || count == 0)
                    {
                        string line = file.ReadLine() ?? string.Empty;
                        --count; 

                        if (int.TryParse(line, out var result))
                            yield return result;
                    }
                }
            }
        }
    }
}
