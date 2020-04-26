using System;

namespace Algorithms.Containers
{
    /// <summary>
    /// This class represents a disjoint-sets data type
    /// (also known as union-find data structure).
    /// It supports the classic union and find operations,
    /// along with a count operation that returns the total number of sets.
    /// </summary>
    public class DisjointSet
    {
        private readonly int[] parent; 
        private readonly byte[] rank;
        private int count;

        /// <summary>
        /// Initializes an empty union-find data structure with n elements 0 through n-1
        /// </summary>
        /// <param name="count">the number of elements</param>
        public DisjointSet(int count)
        {
            if (count < 0)
                throw new ArgumentException();

            this.count = count;
            parent = new int[count];
            rank = new byte[count];

            for (var index = 0; index < count; index++)
            {
                parent[index] = index;
                rank[index] = 0;
            }
        }

        /// <summary>
        /// Returns the canonical element of the set containing element <paramref name="element"/>.
        /// </summary>
        /// <param name="element">an element</param>
        /// <returns>the canonical element of the set containing element <paramref name="element"/></returns>
        public int Find(int element)
        {
            Validate(element);
            while (element != parent[element])
            {
                parent[element] = parent[parent[element]];
                element = parent[element];
            }

            return element;
        }

        /// <summary>
        /// Returns the number of sets.
        /// </summary>
        /// <returns>the number of sets (between 1 and n)</returns>
        public int Count()
        {
            return count;
        }

        /// <summary>
        /// Returns <see langword="true" /> if the two elements are in the same set.
        /// </summary>
        /// <param name="firstElement">one element</param>
        /// <param name="secondElement">the other element</param>
        /// <returns>
        ///     <see langword="true" /> if <paramref name="firstElement"/> and <paramref name="secondElement"/> are in the same set; <see langword="false" /> otherwise
        /// </returns>
        public bool Connected(int firstElement, int secondElement)
        {
            return Find(firstElement) == Find(secondElement);
        }

        /// <summary>
        /// Merges the set containing element <paramref name="firstElement"/> with the
        /// the set containing element <paramref name="secondElement"/>.
        /// </summary>
        /// <param name="firstElement">one element</param>
        /// <param name="secondElement">the other element</param>
        public void Union(int firstElement, int secondElement)
        {
            int rootOne = Find(firstElement);
            int rootTwo = Find(secondElement);

            if (rootOne == rootTwo)
                return;

            if (rank[rootOne] < rank[rootTwo])
                parent[rootOne] = rootTwo;
            else if (rank[rootOne] > rank[rootTwo])
                parent[rootTwo] = rootOne;
            else
            {
                parent[rootTwo] = rootOne;
                rank[rootOne]++;
            }

            count--;
        }

        private void Validate(int index)
        {
            int length = parent.Length;
            if (index < 0 || index >= length)
            {
                throw new ArgumentException("index " + index + " is not between 0 and " + (length - 1));
            }
        }
    }
}
