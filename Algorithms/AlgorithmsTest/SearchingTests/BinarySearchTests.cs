using Algorithms.Searching;
using NUnit.Framework;

namespace AlgorithmsTest.SearchingTests
{
    public class BinarySearchTests
    {
        [TestCase(1, new int[0], -1)]
        [TestCase(1, new[] {1,2,3}, 0)]
        [TestCase(3, new[] {1,2,3}, 2)]
        [TestCase(2, new[] {1,2,3}, 1)]
        [TestCase(2, new[] {1,2,3,4}, 1)]
        [TestCase(3, new[] {1,2,3,4}, 2)]
        [TestCase(3, new[] {3,1,4,2}, -1)]        
        public void TestInt(int key, int[] array, int expected)
        {   
            var actual = array.BSearch(key);
            Assert.AreEqual(expected, actual);
        }

        [TestCase('E', new[] { 'A', 'B', 'C', 'D', 'E', 'F' }, 4)]
        public void TestChar(char key, char[] array, int expected)
        {
            var actual = array.BSearch(key);
            Assert.AreEqual(expected, actual);
        }
    }
}
