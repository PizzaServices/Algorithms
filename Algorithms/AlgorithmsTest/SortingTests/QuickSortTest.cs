using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Algorithms.Sorting;
using AlgorithmsTest.SortingTests.TestDataGeneration;
using NUnit.Framework;

namespace AlgorithmsTest.SortingTests
{
    public class QuickSortTest
    {
        [TestCase(new[] { 3, 2, 1 }, new[] { 1, 2, 3 })]
        [TestCase(new[] { 2, 3, 1 }, new[] { 1, 2, 3 })]
        [TestCase(new[] { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 }, new[] { 0, 2, 3, 4, 8, 9, 12, 15, 16, 23, 25, 34, 42, 46, 55 })]
        public void TestQuickSort(int[] arrayToSort, int[] expected)
        {
            arrayToSort.QuickSort();
            Assert.That(expected, Is.EquivalentTo(arrayToSort));
        }

        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        public void TestRandomNumbersQuickSort(int count)
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
            list.QuickSort();
            watch.Stop();

            sorting = watch.Elapsed;

            Console.WriteLine("Time for reading the file: {0}", fileReading);
            Console.WriteLine("Time for sorting: {0}", sorting);

            Assert.That(testDataGenerator.SortedList, Is.EquivalentTo(list));
        }

        [TestCase(new[] { 3, 2, 1 }, new[] { 1, 2, 3 })]
        [TestCase(new[] { 2, 3, 1 }, new[] { 1, 2, 3 })]
        [TestCase(new[] { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 }, new[] { 0, 2, 3, 4, 8, 9, 12, 15, 16, 23, 25, 34, 42, 46, 55 })]
        public void TestThreeWaySort(int[] arrayToSort, int[] expected)
        {
            arrayToSort.ThreeWaySort();
            Assert.That(expected, Is.EquivalentTo(arrayToSort));
        }

        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        public void TestRandomNumbersThreeWaySort(int count)
        {
            var watch = new Stopwatch();

            var testDataGenerator = new SortingTestDataGenerator();
            IList<int> list = testDataGenerator.CreateRandomNumbers(count).ToList();

            watch.Start();
            list.ThreeWaySort();
            watch.Stop();

            var sorting = watch.Elapsed;

            Console.WriteLine("Time for sorting: {0}", sorting);

            Assert.That(testDataGenerator.SortedList, Is.EquivalentTo(list));
        }
    }
}
