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
            return size(root);
        }

        public TValue Get(TKey key)
        {
            return get(root, key);
        }

        public void Put(TKey key, TValue value)
        {
            root = put(root, key, value);
        }

        public TKey Min()
        {
            return min(root).Key;
        }

        public TKey Max()
        {
            return max(root).Key;
        }

        public TKey Floor(TKey key)
        {
            Node x = floor(root, key);
            if (x == null)
                return default;
            return x.Key;
        }

        public TKey Ceiling(TKey key)
        {
            Node x = ceiling(root, key);
            if (x == null)
                return default;
            return x.Key;
        }

        public TKey Select(int k)
        {
            return select(root, k).Key;
        }

        public int Rank(TKey key)
        {
            return rank(key, root);
        }

        public void DeleteMin()
        {
            root = deleteMin(root);
        }

        public void DeleteMax()
        {
            root = deleteMax(root);
        }

        public void Delete(TKey key)
        {
            root = delete(root, key);
        }

        public IEnumerable<TKey> Keys()
        {
            return Keys(Min(), Max());
        }

        public IEnumerable<TKey> Keys(TKey lo, TKey hi)
        {
            Queue<TKey> queue = new Queue<TKey>();
            keys(root, queue, lo, hi);
            return queue;
        }

        private int size(Node x)
        {
            if (x == null)
                return 0;
            else
                return x.N;
        }

        private TValue get(Node x, TKey key)
        {
            if (x == null)
                return default;

            int cmp = key.CompareTo(x.Key);

            if (cmp < 0)
                return get(x.Left, key);
            else if (cmp > 0)
                return get(x.Right, key);
            else
                return x.Value;
        }

        private Node put(Node x, TKey key, TValue value)
        {
            if (x == null)
                return new Node(key, value, 1);

            int cmp = key.CompareTo(x.Key);
            if (cmp < 0)
                x.Left = put(x.Left, key, value);
            else if (cmp > 0)
                x.Right = put(x.Right, key, value);
            else
                x.Value = value;

            x.N = size(x.Left) + size(x.Right) + 1;
            return x;
        }

        private Node min(Node x)
        {
            if (x.Left == null)
                return x;
            return min(x.Left);
        }

        private Node max(Node x)
        {
            if (x.Right == null)
                return x;
            return max(x.Right);
        }

        private Node floor(Node x, TKey key)
        {
            if (x == null)
                return null;

            int cmp = key.CompareTo(x.Key);
            if (cmp == 0)
                return x;

            if (cmp < 0)
                return floor(x.Left, key);

            Node t = floor(x.Right, key);

            if (t != null)
                return t;
            else
                return x;
        }

        private Node ceiling(Node x, TKey key)
        {
            if (x == null)
                return null;

            int cmp = key.CompareTo(x.Key);
            if (cmp == 0)
                return x;

            if (cmp > 0)
                return floor(x.Right, key);

            Node t = floor(x.Left, key);

            if (t != null)
                return t;
            else
                return x;
        }

        private Node select(Node x, int k)
        {
            if (x == null)
                return null;

            int t = size(x.Left);

            if (t > k)
                return select(x.Left, k);
            else if (t < k)
                return select(x.Right, k - t - 1);
            else
                return x;
        }

        private int rank(TKey key, Node x)
        {
            if (x == null)
                return 0;

            int cmp = key.CompareTo(x.Key);

            if (cmp < 0)
                return rank(key, x.Left);
            else if (cmp > 0)
                return 1 + rank(key, x.Right);
            else
                return size(x.Left);
        }

        private Node deleteMin(Node x)
        {
            if (x.Left == null)
                return x.Right;

            x.Left = deleteMin(x.Left);
            x.N = size(x.Left) + size(x.Right) + 1;
            return x;
        }

        private Node deleteMax(Node x)
        {
            if (x.Right == null)
                return x.Right;

            x.Right = deleteMax(x.Right);
            x.N = size(x.Left) + size(x.Right) + 1;
            return x;
        }

        private Node delete(Node x, TKey key)
        {
            if (x == null)
                return null;

            int cmp = key.CompareTo(x.Key);

            if (cmp < 0)
                x.Left = delete(x.Left, key);
            else if (cmp > 0)
                x.Right = delete(x.Right, key);
            else
            {
                if (x.Right == null)
                    return x.Left;
                if (x.Left == null)
                    return x.Right;

                Node t = x;
                x = min(t.Right);
                x.Right = deleteMin(t.Right);
                x.Left = t.Left;
            }
            x.N = size(x.Left) + size(x.Right) + 1;
            return x;
        }

        private void keys(Node x, Queue<TKey> queue, TKey lo, TKey hi)
        {
            if (x == null)
                return;

            int cmplo = lo.CompareTo(x.Key);
            int cmphi = hi.CompareTo(x.Key);

            if (cmplo < 0)
                keys(x.Left, queue, lo, hi);
            if (cmplo <= 0 && cmphi >= 0)
                queue.Enqueue(x.Key);
            if (cmphi > 0)
                keys(x.Right, queue, lo, hi);
        }
    }
}
