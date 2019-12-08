using System.Collections;
using System.Collections.Generic;

namespace Algorithms.DataStructures
{
    /// <summary>
    /// This class represents a bag (or multiset) of generic items.It supports insertion and iterating over the items in arbitrary order.
    /// </summary>
    /// <typeparam name="TItem">the type of values to store</typeparam>
    public class Bag<TItem> : IEnumerable<TItem>
    {
        private Node first;
        private int count;

        private class Node
        {
            public TItem Item { get; set; }
            public Node Next { get; set; }
        }

        /// <summary>
        /// Adds the item to this bag.
        /// </summary>
        /// <param name="item">the item to add to this bag</param>
        public void Add(TItem item)
        {
            var oldFirst = first;
            first = new Node {Item = item, Next = oldFirst};
            count++;
        }

        /// <summary>
        /// Returns <see langword="true" /> if this bag is empty.
        /// </summary>
        /// <returns><see langword="true" /> if this bag is empty <see langword="false" /> otherwise</returns>
        public bool IsEmpty()
        {
            return first == null;
        }

        /// <summary>
        /// Returns the number of items in this bag.
        /// </summary>
        /// <returns>the number of items in this bag</returns>
        public int Size()
        {
            return count;
        }

        /// <summary>
        /// Returns an iterator that iterates over the items in this bag in arbitrary order.
        /// </summary>
        /// <returns>an iterator that iterates over the items in this bag in arbitrary order</returns>
        public IEnumerator<TItem> GetEnumerator()
        {
            var current = first;
            while(current != null)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
