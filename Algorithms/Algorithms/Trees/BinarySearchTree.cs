using System;
using System.Collections.Generic;

namespace Algorithms.Trees
{
    public class BinarySearchTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        private Node root;
        private class Node
        {
            public TKey Key { get; }
            public TValue Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int SubTreeSize { get; set; }
            public Node(TKey key, TValue value, int subTreeSize)
            {
                Key = key;
                Value = value;
                SubTreeSize = subTreeSize;
            }
        }

        /// <summary>
        /// Does this symbol table contain the given key?
        /// </summary>
        /// <param name="key">the key</param>
        /// <returns>
        ///     <see langword="true" /> if this symbol table contains <paramref name="key"/> and <see langword="false" /> otherwise
        /// </returns>
        public bool Contains(TKey key)
        {
            if (key == null)
                return false;

            return Get(key) != null;
        }

        /// <summary>
        /// Returns the number of key-value pairs in this symbol table.
        /// </summary>
        /// <returns>the number of key-value pairs in this symbol table</returns>
        public int Size()
        {
            return Size(root);
        }

        /// <summary>
        /// Returns the value associated with the given key.
        /// </summary>
        /// <param name="key">the key</param>
        /// <returns>
        ///     the value associated with the given key if the key is in the symbol table
        ///     and <see langword="null" /> if the key is not in the symbol table
        /// </returns>
        public TValue Get(TKey key)
        {
            return Get(root, key);
        }

        /// <summary>
        /// Inserts the specified key-value pair into the symbol table, overwriting the old
        /// value with the new value if the symbol table already contains the specified key.
        /// </summary>
        /// <param name="key">the key</param>
        /// <param name="value"> the value</param>
        public void Insert(TKey key, TValue value)
        {
            root = Insert(root, key, value);
        }

        /// <summary>
        /// Returns the smallest key in the symbol table.
        /// </summary>
        /// <returns>the smallest key in the symbol table</returns>
        public TKey Min()
        {
            return Min(root).Key;
        }

        /// <summary>
        /// Returns the largest key in the symbol table.
        /// </summary>
        /// <returns>the largest key in the symbol table</returns>
        public TKey Max()
        {
            return Max(root).Key;
        }

        /// <summary>
        /// Returns the largest key in the symbol table less than or equal to <paramref name="key"/>.
        /// </summary>
        /// <param name="key">the key</param>
        /// <returns>the largest key in the symbol table less than or equal to <paramref name="key"/></returns>
        public TKey Floor(TKey key)
        {
            var node = Floor(root, key);
            return node == null ? default : node.Key;
        }

        /// <summary>
        /// Returns the smallest key in the symbol table greater than or equal to <paramref name="key"/>.
        /// </summary>
        /// <param name="key">the key</param>
        /// <returns>the smallest key in the symbol table greater than or equal to <paramref name="key"/></returns>
        public TKey Ceiling(TKey key)
        {
            var node = Ceiling(root, key);
            return node == null ? default : node.Key;
        }

        /// <summary>
        /// Return the key in the symbol table whose rank is <paramref name="rank"/>.
        /// This is the(k+1)st smallest key in the symbol table.
        /// </summary>
        /// <param name="rank">the order statistic</param>
        /// <returns>the key in the symbol table of rank <paramref name="rank"/></returns>
        public TKey Select(int rank)
        {
            if (rank < 0 || rank > Size())
                return default;

            return Select(root, rank).Key;
        }

        /// <summary>
        /// Return the number of keys in the symbol table strictly less than <paramref name="key"/>.
        /// </summary>
        /// <param name="key">the key</param>
        /// <returns>the number of keys in the symbol table strictly less than <paramref name="key"/></returns>
        public int Rank(TKey key)
        {
            return Rank(key, root);
        }

        /// <summary>
        /// Removes the smallest key and associated value from the symbol table.
        /// </summary>
        public void RemoveMin()
        {
            root = RemoveMin(root);
        }

        /// <summary>
        /// Removes the largest key and associated value from the symbol table.
        /// </summary>
        public void RemoveMax()
        {
            root = RemoveMax(root);
        }

        /// <summary>
        /// Removes the specified key and its associated value from this symbol table
        /// (if the key is in this symbol table).
        /// </summary>
        /// <param name="key">the key to remove</param>
        public void Remove(TKey key)
        {
            root = Remove(root, key);
        }

        /// <summary>
        /// Returns all keys in the symbol table.
        /// </summary>
        /// <returns>all keys in the symbol table</returns>
        public IEnumerable<TKey> Keys()
        {
            return Keys(Min(), Max());
        }

        /// <summary>
        /// Returns all keys in the symbol table in the given range.
        /// </summary>
        /// <param name="low">minimum endpoint</param>
        /// <param name="high">maximum endpoint</param>
        /// <returns>
        ///     all keys in the symbol table between <paramref name="low"/> (inclusive) and <paramref name="high"/> (inclusive)
        /// </returns>
        public IEnumerable<TKey> Keys(TKey low, TKey high)
        {
            var queue = new Queue<TKey>();
            Keys(root, queue, low, high);
            return queue;
        }

        private static int Size(Node node)
        {
            return node?.SubTreeSize ?? 0;
        }

        private static TValue Get(Node node, TKey key)
        {
            while (true)
            {
                if (node == null) return default;

                var cmp = key.CompareTo(node.Key);

                if (cmp < 0)
                {
                    node = node.Left;
                }
                else if (cmp > 0)
                {
                    node = node.Right;
                }
                else
                    return node.Value;
            }
        }

        private static Node Insert(Node node, TKey key, TValue value)
        {
            if (node == null)
                return new Node(key, value, 1);

            var cmp = key.CompareTo(node.Key);
            if (cmp < 0)
                node.Left = Insert(node.Left, key, value);
            else if (cmp > 0)
                node.Right = Insert(node.Right, key, value);
            else
                node.Value = value;

            node.SubTreeSize = Size(node.Left) + Size(node.Right) + 1;
            return node;
        }

        private static Node Min(Node node)
        {
            while (true)
            {
                if (node.Left == null) return node;
                node = node.Left;
            }
        }

        private static Node Max(Node node)
        {
            while (true)
            {
                if (node.Right == null) return node;
                node = node.Right;
            }
        }

        private static Node Floor(Node node, TKey key)
        {
            while (true)
            {
                if (node == null) return null;

                var cmp = key.CompareTo(node.Key);
                if (cmp == 0) return node;

                if (cmp < 0)
                {
                    node = node.Left;
                    continue;
                }

                var t = Floor(node.Right, key);

                return t ?? node;
            }
        }

        private static Node Ceiling(Node node, TKey key)
        {
            if (node == null)
                return null;

            var cmp = key.CompareTo(node.Key);
            if (cmp == 0)
                return node;

            if (cmp > 0)
                return Floor(node.Right, key);

            var t = Floor(node.Left, key);

            return t ?? node;
        }

        private static Node Select(Node node, int rank)
        {
            while (true)
            {
                if (node == null) return null;

                var t = Size(node.Left);

                if (t > rank)
                {
                    node = node.Left;
                }
                else if (t < rank)
                {
                    node = node.Right;
                    rank = rank - t - 1;
                }
                else
                    return node;
            }
        }

        private static int Rank(TKey key, Node node)
        {
            while (true)
            {
                if (node == null) return 0;

                var cmp = key.CompareTo(node.Key);

                if (cmp < 0)
                {
                    node = node.Left;
                }
                else if (cmp > 0)
                    return 1 + Rank(key, node.Right);
                else
                    return Size(node.Left);
            }
        }

        private static Node RemoveMin(Node node)
        {
            if (node.Left == null)
                return node.Right;

            node.Left = RemoveMin(node.Left);
            node.SubTreeSize = Size(node.Left) + Size(node.Right) + 1;
            return node;
        }

        private static Node RemoveMax(Node node)
        {
            if (node.Right == null)
                return node.Left;

            node.Right = RemoveMax(node.Right);
            node.SubTreeSize = Size(node.Left) + Size(node.Right) + 1;
            return node;
        }

        private static Node Remove(Node node, TKey key)
        {
            if (node == null)
                return null;

            var cmp = key.CompareTo(node.Key);

            if (cmp < 0)
                node.Left = Remove(node.Left, key);
            else if (cmp > 0)
                node.Right = Remove(node.Right, key);
            else
            {
                if (node.Right == null)
                    return node.Left;
                if (node.Left == null)
                    return node.Right;

                var t = node;
                node = Min(t.Right);
                node.Right = RemoveMin(t.Right);
                node.Left = t.Left;
            }
            node.SubTreeSize = Size(node.Left) + Size(node.Right) + 1;
            return node;
        }

        private static void Keys(Node node, Queue<TKey> queue, TKey low, TKey high)
        {
            while (true)
            {
                if (node == null) return;

                var compareLow = low.CompareTo(node.Key);
                var compareHigh = high.CompareTo(node.Key);

                if (compareLow < 0) Keys(node.Left, queue, low, high);
                if (compareLow <= 0 && compareHigh >= 0) queue.Enqueue(node.Key);
                if (compareHigh > 0)
                {
                    node = node.Right;
                    continue;
                }

                break;
            }
        }
    }
}
