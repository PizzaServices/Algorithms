﻿using System.Linq;
using Algorithms.Trees.RangeTree;
using NUnit.Framework;

namespace AlgorithmsTest.TreeTests.RangeTreeTests
{
    [TestFixture]
    internal class TreeOfIntTests
    {
        [Test]
        public void BuildEmptyIntervalTree()
        {
            var emptyTree = new RangeTree<int, int>();
            Assert.Pass();
        }

        [Test]
        public void CreateEmptyIntervalTree()
        {
            var emptyTree = new RangeTree<int, int>();
            Assert.That(emptyTree, Is.Not.Null);
        }

        [Test]
        public void TestSeparateIntervals()
        {
            var tree = new RangeTree<int, int>();
            tree.Add(0, 10, 100);
            tree.Add(20, 30, 200);

            var result = tree.Query(5).ToList();
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo(100));
        }

        [Test]
        public void TwoIntersectingIntervals()
        {
            var tree = new RangeTree<int, int>();
            tree.Add(0, 10, 100);
            tree.Add(3, 30, 200);

            var result = tree.Query(5).ToList();
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0], Is.EqualTo(100));
            Assert.That(result[1], Is.EqualTo(200));
        }

        [Test]
        public void QueryOutOfSyncTree_ExpectObsoleteResults()
        {
            var tree = new RangeTree<int, int>();
            tree.Add(0, 10, 100);

            var result = tree.Query(5).ToList();
            Assert.That(result.Count, Is.EqualTo(1));

            tree.Add(3, 30, 200);

            result = tree.Query(5).ToList();
            Assert.That(result.Count, Is.EqualTo(2));
        }
    }
}
