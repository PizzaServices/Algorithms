using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.DataStructures
{
    /// <summary>
    /// This class represents a priority queue of generic keys.
    /// It supports the usual insert and delete-the-minimum
    /// operations, along with methods for peeking at the minimum key,
    /// testing if the priority queue is empty, and iterating through
    /// the keys.
    /// </summary>
    /// <typeparam name="TKey">the generic type of key on this priority queue</typeparam>
    public class MinPQ<TKey> : IEnumerable<TKey>
    {
        private TKey[] queue;
        private int count;
        private readonly IComparer<TKey> comparer;

        /// <summary>
        /// Initializes an empty priority queue.
        /// </summary>
        public MinPQ() : this(1) { }

        /// <summary>
        /// Initializes an empty priority queue with the given initial capacity.
        /// </summary>
        /// <param name="initCapacity">the initial capacity of this priority queue</param>
        public MinPQ(int initCapacity) : this(initCapacity, null) { }

        /// <summary>
        /// Initializes an empty priority queue using the given comparer.
        /// </summary>
        /// <param name="comparer">the order in which to compare the keys</param>
        public MinPQ(IComparer<TKey> comparer) : this(1, comparer) { }

        /// <summary>
        /// Initializes an empty priority queue with the given initial capacity,
        /// using the given comparer.
        /// </summary>
        /// <param name="initCapacity">the initial capacity of this priority queue</param>
        /// <param name="comparer">the order in which to compare the keys</param>
        public MinPQ(int initCapacity, IComparer<TKey> comparer = null)
        {
            this.comparer = comparer ?? Comparer<TKey>.Default;
            queue = new TKey[initCapacity + 1];
            count = 0;
        }

        /// <summary>
        /// Initializes a priority queue from the array of keys.
        /// Takes time proportional to the number of keys, using sink-based heap construction.
        /// </summary>
        /// <param name="keys">the array of keys</param>
        public MinPQ(IEnumerable<TKey> keys)
        {
            count = keys.Count();
            queue = new TKey[count + 1];

            for (int i = 0; i < count; i++)
                queue[i + 1] = keys.ElementAt(i);

            for(int k = count/2; k >= 1; k--)
                Sink(k);
        }

        /// <summary>
        /// Returns <see langword="true" /> if this priority queue is empty.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> if this priority queue is empty;
        ///     <see langword="false" /> otherwise
        /// </returns>
        public bool IsEmpty()
        {
            return count == 0;
        }

        /// <summary>
        /// Returns the number of keys on this priority queue.
        /// </summary>
        /// <returns>the number of keys on this priority queue</returns>
        public int Size()
        {
            return count;
        }

        /// <summary>
        /// Returns a smallest key on this priority queue.
        /// </summary>
        /// <returns>a smallest key on this priority queue</returns>
        /// <exception cref="System.IndexOutOfRangeException">if this priority queue is empty</exception>
        public TKey Min()
        {
            if(IsEmpty())
                throw new IndexOutOfRangeException("Priority queue underflow");
            return queue[1];
        }

        /// <summary>
        /// Adds a new key to this priority queue.
        /// </summary>
        /// <param name="newKey">the key to add to this priority queue</param>
        public void Insert(TKey newKey)
        {
            if(count == queue.Length - 1)
                Resize(2 * queue.Length);

            queue[++count] = newKey;
            Swim(count);
        }

        /// <summary>
        /// Removes and returns a smallest key on this priority queue.
        /// </summary>
        /// <returns>smallest key on this priority queue</returns>
        /// <exception cref="System.IndexOutOfRangeException">if this priority queue is empty</exception>
        public TKey DelMin()
        {
            if (IsEmpty())
                throw new IndexOutOfRangeException("Priority queue underflow");

            var min = queue[1];

            Exchange(1, count--);
            Sink(1);

            queue[count + 1] = default;

            if((count > 0) && (count == (queue.Length - 1) / 4))
                Resize(queue.Length / 2);

            return min;
        }

        // helper function to double the size of the heap array
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
                Exchange(k, k/2);
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
                Exchange(k, j);
                k = j;
            }
        }

        private bool Greater(int i, int j)
        {
            if (comparer == null)
                return ((IComparable) queue[i]).CompareTo(queue[j]) > 0;

            return comparer.Compare(queue[i], queue[j]) > 0;
        }

        private void Exchange(int i, int j)
        {
            var swap = queue[i];
            queue[i] = queue[j];
            queue[j] = swap;
        }

        /// <summary>
        /// Returns an iterator that iterates over the keys on this priority queue
        /// in ascending order.
        /// </summary>
        /// <returns>an iterator that iterates over the keys in ascending order</returns>
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
