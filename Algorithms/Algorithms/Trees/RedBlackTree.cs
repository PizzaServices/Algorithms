using System;
using System.Collections.Generic;

namespace Algorithms.Trees
{
    public class RedBlackTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        private const bool RED = true;
        private const bool BLACK = false;

        private Node root;

        private class Node
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int SubTreeSize { get; set; }
            public bool Color { get; set; }

            public Node(TKey key, TValue value, int subTreeSize, bool color)
            {
                Key = key;
                Value = value;
                SubTreeSize = subTreeSize;
                Color = color;
            }
        }

        public bool IsEmpty()
        {
            return root == null;
        }

        public int Size()
        {
            return Size(root);
        }

        public void Put(TKey key, TValue value)
        {
            root = Put(root, key, value);
            root.Color = BLACK;
        }

        public TValue Get(TKey key)
        {
            return key == null ? default : Get(root, key);
        }


        public bool Contains(TKey key)
        {
            return Get(key) != null;
        }

        public void DeleteMin()
        {
            if (IsEmpty())
                return;

            if (!IsRed(root.Left) && !IsRed(root.Right))
                root.Color = RED;

            root = DeleteMin(root);
            if (!IsEmpty()) root.Color = BLACK;
        }

        public void DeleteMax()
        {
            if (IsEmpty())
                return;

            if (!IsRed(root.Left) && !IsRed(root.Right))
                root.Color = RED;

            root = DeleteMax(root);
            if (!IsEmpty()) root.Color = BLACK;
        }

        public void Delete(TKey key)
        {
            if (key == null)
                return;

            if (!Contains(key)) 
                return;

            if (!IsRed(root.Left) && !IsRed(root.Right))
                root.Color = RED;

            root = Delete(root, key);

            if (!IsEmpty()) 
                root.Color = BLACK;
        }

        public int Size(TKey low, TKey high)
        {
            if (low == null)
                throw new ArgumentException("first argument to size() is null");
            if (high == null)
                throw new ArgumentException("second argument to size() is null");

            if (low.CompareTo(high) > 0)
                return 0;

            if (Contains(high))
                return Rank(high) - Rank(low) + 1;
            else
                return Rank(high) - Rank(low);
        }

        public int Height()
        {
            return Height(root);
        }

        public TKey Min()
        {
            return IsEmpty() ? default : Min(root).Key;
        }

        public TKey Max()
        {
            return IsEmpty() ? default : Max(root).Key;
        }

        public TKey Floor(TKey key)
        {
            if (key == null)
                return default;
            if (IsEmpty())
                return default;

            var x = Floor(root, key);

            return x == null ? default : x.Key;
        }

        public TKey Ceiling(TKey key)
        {
            if (key == null)
                return default;

            if (IsEmpty())
                return default;

            var x = Ceiling(root, key);

            return x == null ? default : x.Key;
        }

        public IEnumerable<TKey> Keys()
        {
            return IsEmpty() ? new Queue<TKey>() : Keys(Min(), Max());
        }

        public IEnumerable<TKey> Keys(TKey low, TKey high)
        {
            if (low == null)
                throw new ArgumentException("first argument to keys() is null");
            if (high == null)
                throw new ArgumentException("second argument to keys() is null");

            var queue = new Queue<TKey>();

            if (IsEmpty() || low.CompareTo(high) > 0)
                return queue;

            Keys(root, queue, low, high);
            return queue;
        }

        public TKey Select(int rank)
        {
            if (rank < 0 || rank >= Size())
            {
                throw new ArgumentException("argument to select() is invalid: " + rank);
            }
            var x = Select(root, rank);
            return x.Key;
        }

        public int Rank(TKey key)
        {
            if (key == null)
                throw new ArgumentException("argument to rank() is null");
            return Rank(key, root);
        }

        private static Node Delete(Node node, TKey key)
        {

            if (key.CompareTo(node.Key) < 0)
            {
                if (!IsRed(node.Left) && !IsRed(node.Left.Left))
                    node = MoveRedLeft(node);
                node.Left = Delete(node.Left, key);
            }
            else
            {
                if (IsRed(node.Left))
                    node = RotateRight(node);

                if (key.CompareTo(node.Key) == 0 && (node.Right == null))
                    return null;

                if (!IsRed(node.Right) && !IsRed(node.Right.Left))
                    node = MoveRedRight(node);

                if (key.CompareTo(node.Key) == 0)
                {
                    Node tmpNode = Min(node.Right);
                    node.Key = tmpNode.Key;
                    node.Value = tmpNode.Value;
                    node.Right = DeleteMin(node.Right);
                }
                else
                    node.Right = Delete(node.Right, key);
            }
            return Balance(node);
        }

        private static Node DeleteMax(Node node)
        {
            if (IsRed(node.Left))
                node = RotateRight(node);

            if (node.Right == null)
                return null;

            if (!IsRed(node.Right) && !IsRed(node.Right.Left))
                node = MoveRedRight(node);

            node.Right = DeleteMax(node.Right);

            return Balance(node);
        }

        private static Node DeleteMin(Node node)
        {
            if (node.Left == null)
                return null;

            if (!IsRed(node.Left) && !IsRed(node.Left.Left))
                node = MoveRedLeft(node);

            node.Left = DeleteMin(node.Left);
            return Balance(node);
        }

        private static Node MoveRedLeft(Node node)
        {
            FlipColors(node);

            if (!IsRed(node.Right.Left)) 
                return node;

            node.Right = RotateRight(node.Right);
            node = RotateLeft(node);
            FlipColors(node);
            return node;
        }

        private static Node MoveRedRight(Node node)
        {

            FlipColors(node);

            if (!IsRed(node.Left.Left)) 
                return node;

            node = RotateRight(node);
            FlipColors(node);
            return node;
        }

        private static Node Balance(Node node)
        {

            if (IsRed(node.Right))
                node = RotateLeft(node);

            if (IsRed(node.Left) && IsRed(node.Left.Left))
                node = RotateRight(node);

            if (IsRed(node.Left) && IsRed(node.Right))
                FlipColors(node);

            node.SubTreeSize = Size(node.Left) + Size(node.Right) + 1;
            return node;
        }

        private static int Height(Node node)
        {
            if (node == null)
                return -1;

            return 1 + Math.Max(Height(node.Left), Height(node.Right));
        }

        private static Node Min(Node node)
        {
            while (true)
            {
                if (node.Left == null) 
                    return node;

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

                int cmp = key.CompareTo(node.Key);

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
            while (true)
            {
                if (node == null) return null;

                int cmp = key.CompareTo(node.Key);
                if (cmp == 0) return node;
                if (cmp > 0)
                {
                    node = node.Right;
                    continue;
                }

                Node tmpNode = Ceiling(node.Left, key);
                return tmpNode ?? node;
            }
        }

        private static Node Select(Node node, int rank)
        {
            while (true)
            {
                int size = Size(node.Left);
                if (size > rank)
                {
                    node = node.Left;
                }
                else if (size < rank)
                {
                    node = node.Right;
                    rank = rank - size - 1;
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
                    return 1 + Size(node.Left) + Rank(key, node.Right);
                else
                    return Size(node.Left);
            }
        }

        private static void Keys(Node x, Queue<TKey> queue, TKey low, TKey high)
        {
            while (true)
            {
                if (x == null) return;

                int cmpLow = low.CompareTo(x.Key);
                int cmpHigh = high.CompareTo(x.Key);

                if (cmpLow < 0) Keys(x.Left, queue, low, high);
                if (cmpLow <= 0 && cmpHigh >= 0) queue.Enqueue(x.Key);
                if (cmpHigh > 0)
                {
                    x = x.Right;
                    continue;
                }

                break;
            }
        }

        private bool IsBst(Node node, TKey min, TKey max)
        {
            if (node == null)
                return true;

            if (min != null && node.Key.CompareTo(min) <= 0)
                return false;

            if (max != null && node.Key.CompareTo(max) >= 0)
                return false;

            return IsBst(node.Left, min, node.Key) && IsBst(node.Right, node.Key, max);
        }

        private bool IsSizeConsistent(Node node)
        {
            if (node == null)
                return true;

            if (node.SubTreeSize != Size(node.Left) + Size(node.Right) + 1)
                return false;

            return IsSizeConsistent(node.Left) && IsSizeConsistent(node.Right);
        }

        private bool Is23(Node node)
        {
            if (node == null)
                return true;

            if (IsRed(node.Right))
                return false;

            if (node != root && IsRed(node) && IsRed(node.Left))
                return false;

            return Is23(node.Left) && Is23(node.Right);
        }


        private bool IsBalanced(Node node, int black)
        {
            if (node == null)
                return black == 0;

            if (!IsRed(node))
                black--;

            return IsBalanced(node.Left, black) && IsBalanced(node.Right, black);
        }

        private static int Size(Node node)
        {
            return node?.SubTreeSize ?? 0;
        }

        private static TValue Get(Node node, TKey key)
        {
            while (node != null)
            {
                var cmp = key.CompareTo(node.Key);
                if (cmp < 0)
                    node = node.Left;
                else if (cmp > 0)
                    node = node.Right;
                else
                    return node.Value;
            }
            return default;
        }

        private static Node Put(Node node, TKey key, TValue value)
        {
            if (node == null)
                return new Node(key, value, 1, RED);

            var cmp = key.CompareTo(node.Key);

            if (cmp < 0)
                node.Left = Put(node.Left, key, value);
            else if (cmp > 0)
                node.Right = Put(node.Right, key, value);
            else
                node.Value = value;

            if (IsRed(node.Right) && !IsRed(node.Left))
                node = RotateLeft(node);
            if (IsRed(node.Left) && IsRed(node.Left.Left))
                node = RotateRight(node);
            if (IsRed(node.Left) && IsRed(node.Right))
                FlipColors(node);

            node.SubTreeSize = Size(node.Left) + Size(node.Right) + 1;
            return node;
        }

        private static bool IsRed(Node node)
        {
            if (node == null)
                return false;
            return node.Color = RED;
        }

        private static Node RotateLeft(Node node)
        {
            Node tmpNode = node.Right;
            node.Right = tmpNode.Left;
            tmpNode.Left = node;
            tmpNode.Color = node.Color;
            node.Color = RED;
            tmpNode.SubTreeSize = node.SubTreeSize;
            node.SubTreeSize = 1 + Size(node.Left) + Size(node.Right);
            return tmpNode;
        }

        private static Node RotateRight(Node node)
        {
            Node tmpNode = node.Left;
            node.Left = tmpNode.Right;
            tmpNode.Right = node;
            tmpNode.Color = node.Color;
            node.Color = RED;
            tmpNode.SubTreeSize = node.SubTreeSize;
            node.SubTreeSize = 1 + Size(node.Left) + Size(node.Right);
            return tmpNode;
        }

        private static void FlipColors(Node node)
        {
            node.Color = RED;
            node.Left.Color = BLACK;
            node.Right.Color = BLACK;
        }
    }
}
