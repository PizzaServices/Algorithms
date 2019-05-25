using System;
using System.Collections.Generic;

namespace PizzaAlgo.Search
{
    public class RedBlackTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        private static readonly bool RED = true;
        private static readonly bool BLACK = false;

        private Node root;

        private class Node
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int N { get; set; }
            public bool Color { get; set; }

            public Node(TKey key, TValue value, int n, bool color)
            {
                Key = key;
                Value = value;
                N = n;
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
            if (key == null)
                throw new ArgumentException("argument to get() is null");
            return Get(root, key);
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
            if (!Contains(key)) return;

            if (!IsRed(root.Left) && !IsRed(root.Right))
                root.Color = RED;

            root = Delete(root, key);
            if (!IsEmpty()) root.Color = BLACK;
        }

        public int Size(TKey lo, TKey hi)
        {
            if (lo == null)
                throw new ArgumentException("first argument to size() is null");
            if (hi == null)
                throw new ArgumentException("second argument to size() is null");

            if (lo.CompareTo(hi) > 0)
                return 0;

            if (Contains(hi))
                return Rank(hi) - Rank(lo) + 1;
            else
                return Rank(hi) - Rank(lo);
        }

        public int Height()
        {
            return Height(root);
        }

        public TKey Min()
        {
            if (IsEmpty())
                return default;
            return Min(root).Key;
        }

        public TKey Max()
        {
            if (IsEmpty())
                return default;
            return Max(root).Key;
        }

        public TKey Floor(TKey key)
        {
            if (key == null)
                return default;
            if (IsEmpty())
                return default;

            Node x = Floor(root, key);

            if (x == null)
                return default;
            else
                return x.Key;
        }

        public TKey Ceiling(TKey key)
        {
            if (key == null)
                return default;

            if (IsEmpty())
                return default;

            Node x = Ceiling(root, key);

            if (x == null)
                return default;
            else
                return x.Key;
        }

        public IEnumerable<TKey> Keys()
        {
            if (IsEmpty())
                return new Queue<TKey>();

            return Keys(Min(), Max());
        }

        public IEnumerable<TKey> Keys(TKey lo, TKey hi)
        {
            if (lo == null)
                throw new ArgumentException("first argument to keys() is null");
            if (hi == null)
                throw new ArgumentException("second argument to keys() is null");

            Queue<TKey> queue = new Queue<TKey>();

            if (IsEmpty() || lo.CompareTo(hi) > 0)
                return queue;

            Keys(root, queue, lo, hi);
            return queue;
        }

        public TKey Select(int k)
        {
            if (k < 0 || k >= Size())
            {
                throw new ArgumentException("argument to select() is invalid: " + k);
            }
            Node x = Select(root, k);
            return x.Key;
        }

        public int Rank(TKey key)
        {
            if (key == null)
                throw new ArgumentException("argument to rank() is null");
            return Rank(key, root);
        }

        private Node Delete(Node h, TKey key)
        {

            if (key.CompareTo(h.Key) < 0)
            {
                if (!IsRed(h.Left) && !IsRed(h.Left.Left))
                    h = MoveRedLeft(h);
                h.Left = Delete(h.Left, key);
            }
            else
            {
                if (IsRed(h.Left))
                    h = RotateRight(h);

                if (key.CompareTo(h.Key) == 0 && (h.Right == null))
                    return null;

                if (!IsRed(h.Right) && !IsRed(h.Right.Left))
                    h = MoveRedRight(h);

                if (key.CompareTo(h.Key) == 0)
                {
                    Node x = Min(h.Right);
                    h.Key = x.Key;
                    h.Value = x.Value;
                    h.Right = DeleteMin(h.Right);
                }
                else
                    h.Right = Delete(h.Right, key);
            }
            return Balance(h);
        }

        private Node DeleteMax(Node h)
        {
            if (IsRed(h.Left))
                h = RotateRight(h);

            if (h.Right == null)
                return null;

            if (!IsRed(h.Right) && !IsRed(h.Right.Left))
                h = MoveRedRight(h);

            h.Right = DeleteMax(h.Right);

            return Balance(h);
        }

        private Node DeleteMin(Node h)
        {
            if (h.Left == null)
                return null;

            if (!IsRed(h.Left) && !IsRed(h.Left.Left))
                h = MoveRedLeft(h);

            h.Left = DeleteMin(h.Left);
            return Balance(h);
        }

        private Node MoveRedLeft(Node h)
        {
            FlipColors(h);
            if (IsRed(h.Right.Left))
            {
                h.Right = RotateRight(h.Right);
                h = RotateLeft(h);
                FlipColors(h);
            }
            return h;
        }

        private Node MoveRedRight(Node h)
        {

            FlipColors(h);
            if (IsRed(h.Left.Left))
            {
                h = RotateRight(h);
                FlipColors(h);
            }
            return h;
        }

        private Node Balance(Node h)
        {

            if (IsRed(h.Right))
                h = RotateLeft(h);

            if (IsRed(h.Left) && IsRed(h.Left.Left))
                h = RotateRight(h);

            if (IsRed(h.Left) && IsRed(h.Right))
                FlipColors(h);

            h.N = Size(h.Left) + Size(h.Right) + 1;
            return h;
        }

        private int Height(Node x)
        {
            if (x == null)
                return -1;

            return 1 + Math.Max(Height(x.Left), Height(x.Right));
        }

        private Node Min(Node x)
        {
            if (x.Left == null)
                return x;
            else
                return Min(x.Left);
        }

        private Node Max(Node x)
        {
            if (x.Right == null)
                return x;
            else
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
                return Ceiling(x.Right, key);

            Node t = Ceiling(x.Left, key);
            if (t != null)
                return t;
            else
                return x;
        }

        private Node Select(Node x, int k)
        {
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
                return 1 + Size(x.Left) + Rank(key, x.Right);
            else
                return Size(x.Left);
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

        private bool IsBST(Node x, TKey min, TKey max)
        {
            if (x == null)
                return true;

            if (min != null && x.Key.CompareTo(min) <= 0)
                return false;

            if (max != null && x.Key.CompareTo(max) >= 0)
                return false;

            return IsBST(x.Left, min, x.Key) && IsBST(x.Right, x.Key, max);
        }

        private bool IsSizeConsistent(Node x)
        {
            if (x == null)
                return true;

            if (x.N != Size(x.Left) + Size(x.Right) + 1)
                return false;

            return IsSizeConsistent(x.Left) && IsSizeConsistent(x.Right);
        }

        private bool Is23(Node x)
        {
            if (x == null)
                return true;

            if (IsRed(x.Right))
                return false;

            if (x != root && IsRed(x) && IsRed(x.Left))
                return false;

            return Is23(x.Left) && Is23(x.Right);
        }


        private bool IsBalanced(Node x, int black)
        {
            if (x == null)
                return black == 0;

            if (!IsRed(x))
                black--;

            return IsBalanced(x.Left, black) && IsBalanced(x.Right, black);
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
            while (x != null)
            {
                int cmp = key.CompareTo(x.Key);
                if (cmp < 0)
                    x = x.Left;
                else if (cmp > 0)
                    x = x.Right;
                else
                    return x.Value;
            }
            return default;
        }

        private Node Put(Node x, TKey key, TValue value)
        {
            if (x == null)
                return new Node(key, value, 1, RED);

            int cmp = key.CompareTo(x.Key);

            if (cmp < 0)
                x.Left = Put(x.Left, key, value);
            else if (cmp > 0)
                x.Right = Put(x.Right, key, value);
            else
                x.Value = value;

            if (IsRed(x.Right) && !IsRed(x.Left))
                x = RotateLeft(x);
            if (IsRed(x.Left) && IsRed(x.Left.Left))
                x = RotateRight(x);
            if (IsRed(x.Left) && IsRed(x.Right))
                FlipColors(x);

            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        private bool IsRed(Node x)
        {
            if (x == null)
                return false;
            return x.Color = RED;
        }

        private Node RotateLeft(Node x)
        {
            Node t = x.Right;
            x.Right = t.Left;
            t.Left = x;
            t.Color = x.Color;
            x.Color = RED;
            t.N = x.N;
            x.N = 1 + Size(x.Left) + Size(x.Right);
            return t;
        }

        private Node RotateRight(Node x)
        {
            Node t = x.Left;
            x.Left = t.Right;
            t.Right = x;
            t.Color = x.Color;
            x.Color = RED;
            t.N = x.N;
            x.N = 1 + Size(x.Left) + Size(x.Right);
            return t;
        }

        private void FlipColors(Node x)
        {
            x.Color = RED;
            x.Left.Color = BLACK;
            x.Right.Color = BLACK;
        }
    }
}
