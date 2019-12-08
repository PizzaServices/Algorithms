using System;
using System.Linq;
using Algorithms.Trees;
using NUnit.Framework;

namespace AlgorithmsTest.TreeTests
{
    public class BinarySearchTreeTests
    {
        private class TestClass : IComparable<TestClass>
        {
            public int Value { get; }

            public TestClass(int value)
            {
                Value = value;
            }

            public int CompareTo(TestClass obj)
            {
                if (obj.Value > Value)
                    return -1;
                if (obj.Value < Value)
                    return 1;
                return 0;
            }
        }

        [Test]
        public void ShouldInsertAndFind()
        {
            // Arrange
            var tree = new BinarySearchTree<TestClass, int>();

            // Act
            tree.Insert(new TestClass(5), 5);
            tree.Insert(new TestClass(3), 3);
            tree.Insert(new TestClass(7), 7);
            tree.Insert(new TestClass(2), 2);
            tree.Insert(new TestClass(1), 1);
            tree.Insert(new TestClass(4), 4);
            tree.Insert(new TestClass(4), -4);
            tree.Insert(null, 0);

            // Assert
            Assert.IsTrue(tree.Get(new TestClass(5)) == 5);
            Assert.IsTrue(tree.Get(new TestClass(3)) == 3);
            Assert.IsTrue(tree.Get(new TestClass(7)) == 7);
            Assert.IsTrue(tree.Get(new TestClass(2)) == 2);
            Assert.IsTrue(tree.Get(new TestClass(4)) == -4);
            Assert.IsTrue(tree.Get(new TestClass(1)) == 1);
            Assert.IsTrue(tree.Get(null) == default);

            Assert.AreEqual(6, tree.Size());
        }

        [Test]
        public void ShouldRemove()
        {
            // Arrange
            var tree = new BinarySearchTree<TestClass, int>();

            // Act
            tree.Insert(new TestClass(7), 7);
            tree.Insert(new TestClass(3), 3);
            tree.Insert(new TestClass(1), 1);
            tree.Insert(new TestClass(4), 4);
            tree.Insert(new TestClass(2), 2);
            tree.Insert(new TestClass(5), 5);
            tree.Insert(new TestClass(8), 8);

            tree.Remove(new TestClass(1));
            tree.Remove(new TestClass(2));
            tree.Remove(new TestClass(6));
            tree.Remove(new TestClass(7));

            // Assert
            Assert.IsTrue(tree.Get(new TestClass(1)) == default);
            Assert.IsTrue(tree.Get(new TestClass(2)) == default);
            Assert.IsTrue(tree.Get(new TestClass(7)) == default);
            Assert.IsTrue(tree.Get(new TestClass(6)) == default);
            Assert.IsTrue(tree.Size() == 4);
            Assert.IsTrue(tree.Get(new TestClass(3)) == 3);
            Assert.IsTrue(tree.Get(new TestClass(4)) == 4);
            Assert.IsTrue(tree.Get(new TestClass(5)) == 5);
            Assert.IsTrue(tree.Get(new TestClass(8)) == 8);
        }

        [Test]
        public void ShouldRemoveMin()
        {
            // Arrange
            var tree = new BinarySearchTree<TestClass, int>();

            // Act
            tree.Insert(new TestClass(8), 8);
            tree.Insert(new TestClass(4), 4);
            tree.Insert(new TestClass(6), 6);
            tree.Insert(new TestClass(1), 1);
            tree.Insert(new TestClass(9), 9);

            tree.RemoveMin();
            tree.RemoveMin();

            // Assert
            Assert.IsTrue(tree.Get(new TestClass(1)) == default);
            Assert.IsTrue(tree.Get(new TestClass(4)) == default);
            Assert.IsTrue(tree.Get(new TestClass(8)) == 8);
            Assert.IsTrue(tree.Get(new TestClass(6)) == 6);
            Assert.IsTrue(tree.Get(new TestClass(9)) == 9);
        }

        [Test]
        public void ShouldRemoveMax()
        {
            // Arrange
            var tree = new BinarySearchTree<TestClass, int>();

            // Act
            tree.Insert(new TestClass(8), 8);
            tree.Insert(new TestClass(4), 4);
            tree.Insert(new TestClass(6), 6);
            tree.Insert(new TestClass(1), 1);
            tree.Insert(new TestClass(9), 9);

            tree.RemoveMax();
            tree.RemoveMax();

            // Assert
            Assert.IsTrue(tree.Get(new TestClass(9)) == default);
            Assert.IsTrue(tree.Get(new TestClass(8)) == default);
            Assert.IsTrue(tree.Get(new TestClass(1)) == 1);
            Assert.IsTrue(tree.Get(new TestClass(4)) == 4);
            Assert.IsTrue(tree.Get(new TestClass(6)) == 6);

        }

        [Test]
        public void ShouldGetKeys()
        {
            // Arrange
            var tree = new BinarySearchTree<TestClass, int>();
            tree.Insert(new TestClass(4), 4);
            tree.Insert(new TestClass(2), 2);
            tree.Insert(new TestClass(3), 3);
            tree.Insert(new TestClass(1), 1);
            tree.Insert(new TestClass(5), 5);

            // Act
            var allKeys = tree.Keys();
            var rangKeys = tree.Keys(new TestClass(1), new TestClass(3));

            // Assert
            Assert.AreEqual(allKeys.Count(), 5);
            Assert.AreEqual(rangKeys.Count(), 3);

            for (var key = 1; key < allKeys.Count(); key++)
                Assert.AreEqual(key, allKeys.ElementAt(key - 1).Value);

            for (var key = 1; key < rangKeys.Count(); key++)
                Assert.AreEqual(key, rangKeys.ElementAt(key - 1).Value);
        }

        [Test]
        public void ShouldReturnTrueIfTheTreeContainsTheKeyOtherwiseFalse()
        {
            // Arrange
            var tree = new BinarySearchTree<TestClass, int>();
            tree.Insert(new TestClass(4), 4);
            tree.Insert(new TestClass(2), 2);
            tree.Insert(new TestClass(3), 3);
            tree.Insert(new TestClass(1), 1);
            tree.Insert(new TestClass(5), 5);

            // Act
            var containsKey = tree.Contains(new TestClass(3));
            var notPresent = tree.Contains(new TestClass(7));
            var nullKey = tree.Contains(null);

            // Assert
            Assert.IsTrue(containsKey);
            Assert.IsFalse(notPresent);
            Assert.IsFalse(nullKey);
        }

        [Test]
        public void ShouldReturnFloor()
        {
            // Arrange
            var tree = new BinarySearchTree<TestClass, int>();
            tree.Insert(new TestClass(6), 6);
            tree.Insert(new TestClass(4), 4);
            tree.Insert(new TestClass(3), 3);
            tree.Insert(new TestClass(1), 1);
            tree.Insert(new TestClass(7), 7);

            // Act
            var less = tree.Floor(new TestClass(5));
            var equal = tree.Floor(new TestClass(3));

            // Assert
            Assert.AreEqual(4, less.Value);
            Assert.AreEqual(3, equal.Value);
        }

        [Test]
        public void ShouldReturnCeiling()
        {
            // Arrange
            var tree = new BinarySearchTree<TestClass, int>();
            tree.Insert(new TestClass(6), 6);
            tree.Insert(new TestClass(4), 4);
            tree.Insert(new TestClass(3), 3);
            tree.Insert(new TestClass(1), 1);
            tree.Insert(new TestClass(7), 7);

            // Act
            var less = tree.Ceiling(new TestClass(5));
            var equal = tree.Ceiling(new TestClass(3));

            // Assert
            Assert.AreEqual(6, less.Value);
            Assert.AreEqual(3, equal.Value);
        }

        [Test]
        public void ShouldReturnRank()
        {
            // Arrange
            var tree = new BinarySearchTree<TestClass, int>();
            tree.Insert(new TestClass(6), 6);
            tree.Insert(new TestClass(4), 4);
            tree.Insert(new TestClass(3), 3);
            tree.Insert(new TestClass(1), 1);
            tree.Insert(new TestClass(7), 7);

            // Act
            var withMaxValue = tree.Rank(new TestClass(7));
            var withRoot = tree.Rank(new TestClass(6));
            var withLeftSubTree = tree.Rank(new TestClass(4));
            var withNull = tree.Rank(null);
            var emptyTree = new BinarySearchTree<TestClass, int>().Rank(new TestClass(8));

            // Assert
            Assert.AreEqual(4, withMaxValue);
            Assert.AreEqual(3, withRoot);
            Assert.AreEqual(-1, withNull);
            Assert.AreEqual(-1, emptyTree);
        }

        [Test]
        public void ShouldReturnKeyOnRank()
        {
            // Arrange
            var tree = new BinarySearchTree<TestClass, int>();
            tree.Insert(new TestClass(6), 6);
            tree.Insert(new TestClass(4), 4);
            tree.Insert(new TestClass(3), 3);
            tree.Insert(new TestClass(1), 1);
            tree.Insert(new TestClass(7), 7);

            // Act
            var withValue = tree.Select(1);
            var minimum = tree.Select(0);
            var maximum = tree.Select(4);
            var withNull = tree.Select(-5);
            var emptyTree = new BinarySearchTree<TestClass, int>().Select(0);

            // Assert
            Assert.AreEqual(3, withValue.Value);
            Assert.AreEqual(1, minimum.Value);
            Assert.AreEqual(7, maximum.Value);
            Assert.IsNull(withNull);
            Assert.IsNull(emptyTree);
        }
    }
}

