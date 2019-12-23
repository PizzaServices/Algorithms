using System.Collections;
using System.Collections.Generic;

namespace Algorithms.DataStructures
{
    public class Bag<TItem> : IEnumerable<TItem>
    {
        private Node first;
        private int count;

        private class Node
        {
            public TItem Item { get; set; }
            public Node Next { get; set; }
        }

        public void Add(TItem item)
        {
            var oldFirst = first;
            first = new Node {Item = item, Next = oldFirst};
            count++;
        }

        public bool IsEmpty()
        {
            return first == null;
        }

        public int Size()
        {
            return count;
        }

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
