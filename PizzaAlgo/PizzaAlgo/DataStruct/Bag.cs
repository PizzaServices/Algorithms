using System.Collections;
using System.Collections.Generic;

namespace PizzaAlgo.DataStruct
{
    public class Bag<Item> : IEnumerable<Item>
    {
        private Node first;
        private int count;

        private class Node
        {
            public Item Item { get; set; }
            public Node Next { get; set; }
        }

        public void Add(Item item)
        {
            var oldfirst = first;
            first = new Node();
            first.Item = item;
            first.Next = oldfirst;
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

        public IEnumerator<Item> GetEnumerator()
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
