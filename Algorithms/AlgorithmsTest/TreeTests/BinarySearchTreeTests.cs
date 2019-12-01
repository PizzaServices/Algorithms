using Algorithms.Trees;
using NUnit.Framework;

namespace AlgorithmsTest.TreeTests
{
    public class BinarySearchTreeTests
    {
        [Test]
        public void ShouldInsertAndFind()
        {
            // Arrange
            var tree = new BinarySearchTree<int,int>();

            // Act
            tree.Insert(5,5);
            tree.Insert(3,3);
            tree.Insert(7,7);
            tree.Insert(2,2);
            tree.Insert(1,1);
            tree.Insert(4,4);

            // Assert
            Assert.IsTrue(tree.Get(5) == 5);
            Assert.IsTrue(tree.Get(3) == 3);
            Assert.IsTrue(tree.Get(7) == 7);
            Assert.IsTrue(tree.Get(2) == 2);
            Assert.IsTrue(tree.Get(4) == 4);
            Assert.IsTrue(tree.Get(1) == 1);
        }

        [Test]
        public void ShouldRemove()
        {
            // Arrange
            var tree = new BinarySearchTree<int, int>();
            
            // Act
            tree.Insert(3,3);
            tree.Insert(1, 1);
            tree.Insert(4, 4);
            tree.Insert(2, 2);
            tree.Insert(5,5);

            tree.Remove(1);
            tree.Remove(2);
            tree.Remove(6);

            // Assert
            Assert.IsTrue(tree.Get(1) == default);
            Assert.IsTrue(tree.Get(2) == default);
            Assert.IsTrue(tree.Size() == 3);
            Assert.IsTrue(tree.Get(3) == 3);
            Assert.IsTrue(tree.Get(4) == 4);
            Assert.IsTrue(tree.Get(5) == 5);
        }

        [Test]
        public void ShouldRemoveMin()
        {
            // Arrange
            var tree = new BinarySearchTree<int, int>();

            // Act
            tree.Insert(8,8);
            tree.Insert(4,4);
            tree.Insert(6,6);
            tree.Insert(1,1);
            tree.Insert(9,9);

            tree.RemoveMin();
            tree.RemoveMin();

            // Assert
            Assert.IsTrue(tree.Get(1) == default);
            Assert.IsTrue(tree.Get(4) == default);
            Assert.IsTrue(tree.Get(8) == 8);
            Assert.IsTrue(tree.Get(6) == 6);
            Assert.IsTrue(tree.Get(9) == 9);
        }

        [Test]
        public void ShouldRemoveMax()
        {
            // Arrange
            var tree = new BinarySearchTree<int, int>();

            // Act
            tree.Insert(8, 8);
            tree.Insert(4, 4);
            tree.Insert(6, 6);
            tree.Insert(1, 1);
            tree.Insert(9, 9);

            tree.RemoveMax();
            tree.RemoveMax();

            // Assert
            Assert.IsTrue(tree.Get(9) == default);
            Assert.IsTrue(tree.Get(8) == default);
            Assert.IsTrue(tree.Get(1) == 1);
            Assert.IsTrue(tree.Get(4) == 4);
            Assert.IsTrue(tree.Get(6) == 6);
            
        }
    }
}
