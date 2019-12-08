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

            return !Equals(Get(key), default(TValue));
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
            return key == null ? default : Get(root, key);
        }

        /// <summary>
        /// Inserts the specified key-value pair into the symbol table, overwriting the old
        /// value with the new value if the symbol table already contains the specified key.
        /// </summary>
        /// <param name="key">the key</param>
        /// <param name="value"> the value</param>
        public void Insert(TKey key, TValue value)
        {
            if (key == null)
                return;

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

            var result = Select(root, rank);
            return result != null ? result.Key : default;
        }

        /// <summary>
        /// Return the number of keys in the symbol table strictly less than <paramref name="key"/>.
        /// </summary>
        /// <param name="key">the key</param>
        /// <returns>the number of keys in the symbol table strictly less than <paramref name="key"/></returns>
        public int Rank(TKey key)
        {
            if (key == null)
                return -1;

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

        private static Node Floor(Node root, TKey key)
        {
            Node node = root;
            Node smallestNodeVisit = null;

            while (node != null)
            {
                var cmp = key.CompareTo(node.Key);

                if (cmp < 0)
                {
                    node = node.Left;
                    smallestNodeVisit = CompareGreater(node, smallestNodeVisit);
                }
                else if (cmp > 0)
                {
                    smallestNodeVisit = CompareGreater(node, smallestNodeVisit);
                    node = node.Right;
                }
                else
                    return node;
            }

            return smallestNodeVisit;
        }

        private static Node CompareGreater(Node one, Node two)
        {
            if (one == null)
                return two;

            if (two == null)
                return one;

            var cmp = one.Key.CompareTo(two.Key);
            return cmp > 0 ? one : two;
        }

        private static Node Ceiling(Node root, TKey key)
        {
            Node node = root;
            Node largestNodeVisit = null;

            while (node != null)
            {
                var cmp = key.CompareTo(node.Key);

                if (cmp < 0)
                {
                    largestNodeVisit = CompareSmaller(node, largestNodeVisit);
                    node = node.Left;
                }
                else if (cmp > 0)
                {
                    node = node.Right;
                    largestNodeVisit = CompareSmaller(node, largestNodeVisit);
                }
                else
                    return node;
            }

            return largestNodeVisit;
        }

        private static Node CompareSmaller(Node one, Node two)
        {
            if (one == null)
                return two;

            if (two == null)
                return one;

            var cmp = one.Key.CompareTo(two.Key);
            return cmp < 0 ? one : two;
        }

        private static Node Select(Node node, int rank)
        {
            while (true)
            {
                if (node == null)
                    return null;

                var leftSubTreeSize = Size(node.Left);

                if (leftSubTreeSize > rank)
                {
                    node = node.Left;
                }
                else if (leftSubTreeSize < rank)
                {
                    node = node.Right;
                    rank = rank - leftSubTreeSize - 1;
                }
                else
                    return node;
            }
        }

        private static int Rank(TKey key, Node root)
        {
            int rank = 0;
            var node = root;

            while (node != null)
            {
                var cmp = key.CompareTo(node.Key);
                if (cmp < 0)
                    node = node.Left;
                else if (cmp > 0)
                {
                    rank += 1 + Size(node.Left);
                    node = node.Right;
                }
                else
                    return rank + Size(node.Left);
            }

            return -1;
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

                var cmpLow = low.CompareTo(node.Key);
                var cmpHigh = high.CompareTo(node.Key);

                if (cmpLow < 0)
                    Keys(node.Left, queue, low, high);
                if (cmpLow <= 0 && cmpHigh >= 0)
                    queue.Enqueue(node.Key);
                if (cmpHigh > 0)
                {
                    node = node.Right;
                    continue;
                }

                break;
            }
        }
    }
}
