using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithms.DataStructures
{
    /// <summary>
    /// This class represents an indexed priority queue of generic keys.
    /// It supports the usual insert and delete-the-minimum
    /// operations, along with delete and change-the-key
    /// methods. In order to let the client refer to keys on the priority queue,
    /// an integer between 0 and maxN - 1
    /// is associated with each key — the client uses this integer to specify
    /// which key to delete or change.
    /// It also supports methods for peeking at the minimum key,
    /// testing if the priority queue is empty, and iterating through
    /// the keys.
    /// </summary>
    /// <typeparam name="TKey">he generic type of key on this priority queue</typeparam>
    public class IndexMinPQ<TKey> : IEnumerable<int> where TKey : IComparable<TKey>
    {
        private readonly int maxCount;
        private int count;
        private readonly int[] queue;
        private readonly int[] inverseQueue;
        private readonly TKey[] keys;

        /// <summary>
        /// Initializes an empty indexed priority queue with indices between 0
        /// and maxN - 1.
        /// </summary>
        /// <param name="maxCount">the keys on this priority queue are index from 0 max-1</param>
        /// <exception cref="System.ArgumentException">Thrown when <paramref name="maxCount"/> is less than 0</exception>
        public IndexMinPQ(int maxCount)
        {
            if (maxCount < 0)
                throw new ArgumentException();

            this.maxCount = maxCount;
            count = 0;
            keys = new TKey[maxCount + 1];
            queue = new int[maxCount + 1];
            inverseQueue = new int[maxCount + 1];

            for (int i = 0; i <= maxCount; i++)
                inverseQueue[i] = -1;
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
        /// Is <paramref name="index"/> an index on this priority queue?
        /// </summary>
        /// <param name="index">an index</param>
        /// <returns>
        ///     <see langword="true" /> if <paramref name="index"/> is an index on this priority queue;
        ///     <see langword="false" /> otherwise
        /// </returns>
        public bool Contains(int index)
        {
            if (index < 0 || index >= maxCount)
                throw new ArgumentException();

            return inverseQueue[index] != -1;
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
        /// Associates key with <paramref name="index"/>.
        /// </summary>
        /// <param name="index">an index</param>
        /// <param name="key">the key to associate with index <paramref name="index"/></param>
        public void Insert(int index, TKey key)
        {
            if (index < 0 || index >= maxCount)
                throw new ArgumentException();
            if (Contains(index))
                throw new ArgumentException("index is already in the priority queue");

            count++;
            inverseQueue[index] = count;
            queue[count] = index;
            keys[index] = key;
            Swim(count);
        }

        /// <summary>
        /// Returns an index associated with a minimum key.
        /// </summary>
        /// <returns>an index associated with a minimum key</returns>
        public int MinIndex()
        {
            if (count == 0)
                throw new Exception("Priority queue underflow");

            return queue[1];
        }

        /// <summary>
        /// Returns a minimum key.
        /// </summary>
        /// <returns>a minimum key</returns>
        public TKey MinKey()
        {
            if (count == 0)
                throw new Exception("Priority queue underflow");

            return keys[queue[1]];
        }

        /// <summary>
        /// Removes a minimum key and returns its associated index.
        /// </summary>
        /// <returns>an index associated with a minimum key</returns>
        /// <exception cref="System.Exception">if this priority queue is empty</exception>
        public int DelMin()
        {
            if (count == 0)
                throw new Exception("Priority queue underflow");

            int min = queue[1];

            Exchange(1, count--);
            Sink(1);

            inverseQueue[min] = -1;
            keys[min] = default;
            queue[count + 1] = -1;

            return min;
        }

        /// <summary>
        /// Returns the key associated with index <paramref name="index"/>.
        /// </summary>
        /// <param name="index">the index of the key to return</param>
        /// <returns>the key associated with index <paramref name="index"/></returns>
        public TKey KeyOf(int index)
        {
            if (index < 0 || index >= maxCount)
                throw new ArgumentException();

            if (!Contains(index))
                throw new Exception("index is not in the priority queue");
            else
                return keys[index];
        }

        /// <summary>
        /// Change the key associated with index <paramref name="index"/> to the specified value.
        /// </summary>
        /// <param name="index">the index of the key to change</param>
        /// <param name="key">key change the key associated with <paramref name="index"/> to this key</param>
        /// <exception cref="System.ArgumentException">unless 0 less/equal i less maxN</exception>
        /// <exception cref="System.Exception">no key is associated with <paramref name="index"/></exception>
        public void ChangeKey(int index, TKey key)
        {
            if (index < 0 || index >= maxCount)
                throw new ArgumentException();

            if (!Contains(index))
                throw new Exception("index is not in the priority queue");

            keys[index] = key;
            Swim(inverseQueue[index]);
            Sink(inverseQueue[index]);
        }

        /// <summary>
        /// Decrease the key associated with <paramref name="index"/> to the specified value.
        /// </summary>
        /// <param name="index">the index of the key to decrease</param>
        /// <param name="key">decrease the key associated with <paramref name="index"/> to this key</param>
        /// <exception cref="System.ArgumentException">unless 0 less/equal i less maxN</exception>
        /// <exception cref="System.ArgumentException">if <paramref name="key"/> >= keyOf(index)</exception>
        /// <exception cref="System.Exception">no key is associated with <paramref name="index"/></exception>
        public void DecreaseKey(int index, TKey key)
        {
            if (index < 0 || index >= maxCount)
                throw new ArgumentException();

            if (!Contains(index))
                throw new Exception("index is not in the priority queue");

            if (keys[index].CompareTo(key) <= 0)
                throw new ArgumentException("Calling decreaseKey() with given argument would not strictly decrease the key");

            keys[index] = key;
            Swim(inverseQueue[index]);
        }

        /// <summary>
        /// Increase the key associated with <paramref name="index"/> to the specified value.
        /// </summary>
        /// <param name="index">the index of the key to increase</param>
        /// <param name="key">increase the key associated with <paramref name="index"/> to this key</param>
        /// <exception cref="System.ArgumentException">0 less/equal <paramref name="index"/> less maxN</exception>
        /// <exception cref="System.Exception">no key is associated with index <paramref name="index"/></exception>
        /// <exception cref="System.ArgumentException">if <paramref name="key"/> less/equal keyOf(<paramref name="index"/>)</exception>
        public void IncreaseKey(int index, TKey key)
        {
            if (index < 0 || index >= maxCount)
                throw new ArgumentException();

            if (!Contains(index))
                throw new Exception("index is not in the priority queue");

            if (keys[index].CompareTo(key) >= 0)
                throw new ArgumentException("Calling increaseKey() with given argument would not strictly increase the key");

            keys[index] = key;
            Sink(inverseQueue[index]);
        }

        /// <summary>
        /// Remove the key associated with <paramref name="index"/>.
        /// </summary>
        /// <param name="index">the index of the key to remove</param>
        /// <exception cref="System.ArgumentException"><paramref name="index"/> less/equal 0 less maxN</exception>
        /// <exception cref="System.Exception">no key is associated with <paramref name="index"/></exception>
        public void Delete(int index)
        {
            if (index < 0 || index >= maxCount)
                throw new ArgumentException();

            if (!Contains(index))
                throw new Exception("index is not in the priority queue");

            int inverseIndex = inverseQueue[index];

            Exchange(inverseIndex, count--);
            Swim(inverseIndex);
            Sink(inverseIndex);

            keys[index] = default;
            inverseQueue[index] = -1;
        }


        private bool Greater(int indexOne, int indexTwo)
        {
            return keys[queue[indexOne]].CompareTo(keys[queue[indexTwo]]) > 0;
        }

        private void Exchange(int indexOne, int indexTwo)
        {
            int swap = queue[indexOne];
            queue[indexOne] = queue[indexTwo];
            queue[indexTwo] = swap;
            inverseQueue[queue[indexOne]] = indexOne;
            inverseQueue[queue[indexTwo]] = indexTwo;
        }

        private void Swim(int k)
        {
            while (k > 1 && Greater(k / 2, k))
            {
                Exchange(k, k / 2);
                k /= 2;
            }
        }

        private void Sink(int k)
        {
            while (2 * k <= count)
            {
                int j = 2 * k;
                if (j < count && Greater(j, j + 1)) j++;
                if (!Greater(k, j)) break;
                Exchange(k, j);
                k = j;
            }
        }

        /// <summary>
        /// Returns an iterator that iterates over the keys on the priority queue in ascending order.
        /// </summary>
        /// <returns>an iterator that iterates over the keys in ascending order</returns>
        public IEnumerator<int> GetEnumerator()
        {
            return ((IEnumerable<int>) queue).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
