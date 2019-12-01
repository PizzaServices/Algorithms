using System;
using System.Collections.Generic;

namespace Algorithms.Trees
{
    public class BinarySearchTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        private Node root;
        private class Node
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int N { get; set; }
            public Node(TKey key, TValue value, int n)
            {
                Key = key;
                Value = value;
                N = n;
            }
        }

        public int Size()
        {
            return Size(root);
        }

        public TValue Get(TKey key)
        {
            return Get(root, key);
        }

        public void Insert(TKey key, TValue value)
        {
            root = Insert(root, key, value);
        }

        public TKey Min()
        {
            return Min(root).Key;
        }

        public TKey Max()
        {
            return Max(root).Key;
        }

        public TKey Floor(TKey key)
        {
            Node x = Floor(root, key);
            if (x == null)
                return default;
            return x.Key;
        }

        public TKey Ceiling(TKey key)
        {
            Node x = Ceiling(root, key);
            if (x == null)
                return default;
            return x.Key;
        }

        public TKey Select(int k)
        {
            return Select(root, k).Key;
        }

        public int Rank(TKey key)
        {
            return Rank(key, root);
        }

        public void RemoveMin()
        {
            root = RemoveMin(root);
        }

        public void RemoveMax()
        {
            root = RemoveMax(root);
        }

        public void Remove(TKey key)
        {
            root = Remove(root, key);
        }

        public IEnumerable<TKey> Keys()
        {
            return Keys(Min(), Max());
        }

        public IEnumerable<TKey> Keys(TKey lo, TKey hi)
        {
            Queue<TKey> queue = new Queue<TKey>();
            Keys(root, queue, lo, hi);
            return queue;
        }

        private int Size(Node x)
        {
            if (x == null)
                return 0;
            else
                return x.N;
        }

        private TValue Get(Node x, TKey key)
        {
            if (x == null)
                return default;

            int cmp = key.CompareTo(x.Key);

            if (cmp < 0)
                return Get(x.Left, key);
            else if (cmp > 0)
                return Get(x.Right, key);
            else
                return x.Value;
        }

        private Node Insert(Node x, TKey key, TValue value)
        {
            if (x == null)
                return new Node(key, value, 1);

            int cmp = key.CompareTo(x.Key);
            if (cmp < 0)
                x.Left = Insert(x.Left, key, value);
            else if (cmp > 0)
                x.Right = Insert(x.Right, key, value);
            else
                x.Value = value;

            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        private Node Min(Node x)
        {
            if (x.Left == null)
                return x;
            return Min(x.Left);
        }

        private Node Max(Node x)
        {
            if (x.Right == null)
                return x;
            return Max(x.Right);
        }

        private Node Floor(Node x, TKey key)
        {
            if (x == null)
                return null;

            int cmp = key.CompareTo(x.Key);
            if (cmp == 0)
                return x;

            if (cmp < 0)
                return Floor(x.Left, key);

            Node t = Floor(x.Right, key);

            if (t != null)
                return t;
            else
                return x;
        }

        private Node Ceiling(Node x, TKey key)
        {
            if (x == null)
                return null;

            int cmp = key.CompareTo(x.Key);
            if (cmp == 0)
                return x;

            if (cmp > 0)
                return Floor(x.Right, key);

            Node t = Floor(x.Left, key);

            if (t != null)
                return t;
            else
                return x;
        }

        private Node Select(Node x, int k)
        {
            if (x == null)
                return null;

            int t = Size(x.Left);

            if (t > k)
                return Select(x.Left, k);
            else if (t < k)
                return Select(x.Right, k - t - 1);
            else
                return x;
        }

        private int Rank(TKey key, Node x)
        {
            if (x == null)
                return 0;

            int cmp = key.CompareTo(x.Key);

            if (cmp < 0)
                return Rank(key, x.Left);
            else if (cmp > 0)
                return 1 + Rank(key, x.Right);
            else
                return Size(x.Left);
        }

        private Node RemoveMin(Node x)
        {
            if (x.Left == null)
                return x.Right;

            x.Left = RemoveMin(x.Left);
            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        private Node RemoveMax(Node x)
        {
            if (x.Right == null)
                return x.Left;

            x.Right = RemoveMax(x.Right);
            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        private Node Remove(Node x, TKey key)
        {
            if (x == null)
                return null;

            int cmp = key.CompareTo(x.Key);

            if (cmp < 0)
                x.Left = Remove(x.Left, key);
            else if (cmp > 0)
                x.Right = Remove(x.Right, key);
            else
            {
                if (x.Right == null)
                    return x.Left;
                if (x.Left == null)
                    return x.Right;

                Node t = x;
                x = Min(t.Right);
                x.Right = RemoveMin(t.Right);
                x.Left = t.Left;
            }
            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        private void Keys(Node x, Queue<TKey> queue, TKey lo, TKey hi)
        {
            if (x == null)
                return;

            int cmplo = lo.CompareTo(x.Key);
            int cmphi = hi.CompareTo(x.Key);

            if (cmplo < 0)
                Keys(x.Left, queue, lo, hi);
            if (cmplo <= 0 && cmphi >= 0)
                queue.Enqueue(x.Key);
            if (cmphi > 0)
                Keys(x.Right, queue, lo, hi);
        }
    }
}
