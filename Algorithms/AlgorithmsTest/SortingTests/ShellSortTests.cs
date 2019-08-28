using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Algorithms.Sorting;
using AlgorithmsTest.SortingTests.TestDataGeneration;
using NUnit.Framework;

namespace AlgorithmsTest.SortingTests
{
    public class ShellSortTests
    {
        [TestCase(new[] { 3, 2, 1 }, new[] { 1, 2, 3 })]
        [TestCase(new[] { 2, 3, 1 }, new[] { 1, 2, 3 })]
        [TestCase(new[] { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 }, new[] { 0, 2, 3, 4, 8, 9, 12, 15, 16, 23, 25, 34, 42, 46, 55 })]
        public void Test(int[] arrayToSort, int[] expected)
        {
            arrayToSort.ShellSort();
            Assert.That(expected, Is.EquivalentTo(arrayToSort));
        }

        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        public void TestRandomNumbers(int count)
        {
            TimeSpan fileReading;
            TimeSpan sorting;
            Stopwatch watch = new Stopwatch();

            watch.Start();
            var testDataGenerator = new SortingTestDataGenerator();
            IList<int> list = testDataGenerator.CreateRandomNumbers(count).ToList();
            watch.Stop();

            fileReading = watch.Elapsed;
            watch.Reset();

            watch.Start();
            list.ShellSort();
            watch.Stop();

            sorting = watch.Elapsed;

            Console.WriteLine("Time for reading the file: {0}", fileReading);
            Console.WriteLine("Time for sorting: {0}", sorting);

            Assert.That(testDataGenerator.SortedList, Is.EquivalentTo(list));
        }
    }
}
