using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PizzaAlgo.DataStruct
{
    public class MinPQ<TKey> : IEnumerable<TKey>
    {
        private TKey[] queue;
        private int count;
        private IComparer<TKey> comparer;

        public MinPQ() : this(1) { }

        public MinPQ(int initCapacity) : this(initCapacity, null) { }

        public MinPQ(IComparer<TKey> comparer) : this(1, comparer) { }

        public MinPQ(int initCapacity, IComparer<TKey> comparer)
        {
            this.comparer = comparer;
            queue = new TKey[initCapacity + 1];
            count = 0;
        }

        public MinPQ(IEnumerable<TKey> keys)
        {
            count = keys.Count();
            queue = new TKey[count + 1];

            for (int i = 0; i < count; i++)
                queue[i + 1] = keys.ElementAt(i);

            for(int k = count/2; k >= 1; k--)
                Sink(k);
        }

        public bool IsEmpty()
        {
            return count == 0;
        }

        public int Size()
        {
            return count;
        }

        public TKey Min()
        {
            return queue[1];
        }

        public void Insert(TKey newKey)
        {
            if(count == queue.Length - 1)
                Resize(2 * queue.Length);

            queue[++count] = newKey;
            Swim(count);
        }

        public TKey DelMin()
        {
            if (IsEmpty())
                return default;

            var min = queue[1];

            Exch(1, count--);
            Sink(1);

            queue[count + 1] = default;

            if((count > 0) && (count == (queue.Length - 1) / 4))
                Resize(queue.Length / 2);

            return min;
        }

        private void Resize(int capacity)
        {
            var tmp = new TKey[capacity];
            for (int i = 1; i <= count; i++)
            {
                tmp[i] = queue[i];
            }
            queue = tmp;
        }

        private void Swim(int k)
        {
            while (k > 1 && Greater(k / 2, k))
            {
                Exch(k, k/2);
                k = k / 2;
            }
        }

        private void Sink(int k)
        {
            while (2 * k <= count)
            {
                int j = 2 * k;
                if (j < count && Greater(j, j + 1))
                    j++;
                if (!Greater(k, j))
                    break;
                Exch(k, j);
                k = j;
            }
        }

        private bool Greater(int i, int j)
        {
            if (comparer == null)
                return ((IComparable) queue[i]).CompareTo(queue[j]) > 0;
            else
                return comparer.Compare(queue[i], queue[j]) > 0;
        }

        private void Exch(int i, int j)
        {
            var swap = queue[i];
            queue[i] = queue[j];
            queue[j] = swap;
        }

        private bool IsMinHeap()
        {
            return IsMinHeap(1);
        }

        private bool IsMinHeap(int k)
        {
            if (k > count)
                return true;
            int left = 2 * k;
            int right = 2 * k + 1;
            if (left <= count && Greater(k, left))
                return false;
            if (right <= count && Greater(k, right))
                return false;
            return IsMinHeap(left) && IsMinHeap(right);
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            return ((IEnumerable<TKey>) queue).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
