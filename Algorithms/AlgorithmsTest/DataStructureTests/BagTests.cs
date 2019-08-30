using System.Linq;
using Algorithms.DataStructures;
using NUnit.Framework;

namespace AlgorithmsTest.DataStructureTests
{
    [TestFixture]
    public class BagTests
    {
        [TestCase]
        public void ShouldReturnTrueIfBagIsEmpty()
        {
            // Arrange
            var bag = new Bag<int>();

            // Act
            var result = bag.IsEmpty();

            // Assert
            Assert.AreEqual(true, result);
        }

        [TestCase]
        public void ShouldReturnFalseIfBagIsNotEmpty()
        {
            // Arrange
            var bag = new Bag<int>();
            bag.Add(5);

            // Act
            var result = bag.IsEmpty();

            // Assert
            Assert.AreEqual(false, result);
        }

        [TestCase(new[] { 1, 2, 3 }, 3)]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, 5)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 10)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, 15)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 }, 20)]
        public void ShouldReturnSize(int[] valuesToAdd, int expected)
        {
            // Arrange 
            var bag = new Bag<int>();
            foreach(var value in valuesToAdd)
                bag.Add(value);

            // Act
            var result = bag.Size();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestCase(new[] { 1, 2, 3 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 })]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 })]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 })]
    public void ShouldIteratOverItems(int[] valuesToAdd)
        {
            // Arrange
            var bag = new Bag<int>();
            foreach (var value in valuesToAdd)
                bag.Add(value);

            // Act
            var result = bag.ToList();

            // Assert
            for(int i = 0; i < result.Count; ++i)
                Assert.AreEqual(valuesToAdd[i], result[result.Count - 1 - i]);
        }
    }
}
