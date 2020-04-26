using Algorithms.Containers;
using NUnit.Framework;

namespace AlgorithmsTest.ContainerTests
{
    [TestFixture]
    public class MinPQTest
    {
        [TestCase]
        public void ShouldReturnTrueIfMinPqIsEmpty()
        {
            // Arrange
            var minPQ = new MinPQ<int>();

            // Act
            var result = minPQ.IsEmpty();

            // Assert
            Assert.IsTrue(result);
        }

        [TestCase]
        public void ShouldReturnFalseIfMinPqIsNotEmpty()
        {
            // Arrange
            var minPQ = new MinPQ<int>();
            minPQ.Insert(5);

            // Act
            var result = minPQ.IsEmpty();

            // Assert
            Assert.IsFalse(result);
        }

        [TestCase(new[] { 0, 5, 6 }, 3)]
        [TestCase(new[] { 0, 5, 6, 7, 9, 10, 5 }, 7)]
        [TestCase(new[] { 0, 5, 6, 57, 69, 45, 25, 364, 1, 25 }, 10)]
        [TestCase(new[] { 0, 5, 6, 57, 93, 645, 21, 3, 48, 63, 15, 458, 13, 485, 36, 78, 85, 123, 648, 147 }, 20)]
        public void ShouldReturnSize(int[] values, int expectedCount)
        {
            // Arrange
            var minPQ = new MinPQ<int>(values);

            // Act
            var actualCount = minPQ.Size();

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestCase(new[] { 0, 5, 6 }, new[] { 0, 5, 6 })]
        [TestCase(new[] { 0, 5, 6, 7, 9, 10, 5 }, new[] { 0, 5, 5, 6, 7, 9, 10 })]
        [TestCase(new[] { 0, 5, 6, 57, 69, 45, 25, 364, 1, 25 }, new[] { 0, 1, 5, 6, 25, 25, 45, 57, 69, 364 })]
        [TestCase(new[] { 0, 5, 6, 57, 93, 645, 21, 3, 48, 63, 15, 458, 13, 485, 36, 78, 85, 123, 648, 147 },
                  new[] { 0, 3, 5, 6, 13, 15, 21, 36, 48, 57, 63, 78, 85, 93, 123, 147, 458, 485, 645, 648 })]
        public void ShouldReturnSmallestKeyInTheQueue(int[] values, int[] expectedOrder)
        {
            // Arrange
            var minPQ = new MinPQ<int>(values);

            foreach (var expected in expectedOrder)
            {
                // Act
                var actualMin = minPQ.Min();
                var actualMinDelete = minPQ.DelMin();

                // Assert
                Assert.AreEqual(actualMin, actualMinDelete);
                Assert.AreEqual(expected, actualMin);
            }
        }
    }
}
