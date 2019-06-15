using System;
using System.Collections;
using System.Collections.Generic;

namespace PizzaAlgo.DataStruct
{
    public class IndexMinPQ<TKey> : IEnumerable<int> where TKey : IComparable<TKey>
    {
        private readonly int maxCount;
        private int count;
        private readonly int[] queue;
        private readonly int[] inversQueue;
        private readonly TKey[] keys;

        public IndexMinPQ(int maxCount)
        {
            if (maxCount < 0)
                throw new ArgumentException();

            this.maxCount = maxCount;
            count = 0;
            keys = new TKey[maxCount + 1];
            queue = new int[maxCount + 1];
            inversQueue = new int[maxCount + 1];

            for (int i = 0; i <= maxCount; i++)
                inversQueue[i] = -1;
        }

        public bool IsEmpty()
        {
            return count == 0;
        }

        public bool Contains(int i)
        {
            if (i < 0 || i >= maxCount)
                throw new ArgumentException();

            return inversQueue[i] != -1;
        }

        public int Size()
        {
            return count;
        }

        public void Insert(int i, TKey key)
        {
            if (i < 0 || i >= maxCount)
                throw new ArgumentException();
            if (Contains(i))
                throw new ArgumentException("index is already in the priority queue");

            count++;
            inversQueue[i] = count;
            queue[count] = i;
            keys[i] = key;
            Swim(count);
        }

        public int MinIndex()
        {
            if (count == 0)
                throw new Exception("Priority queue underflow");

            return queue[1];
        }

        public TKey MinKey()
        {
            if (count == 0)
                throw new Exception("Priority queue underflow");

            return keys[queue[1]];
        }

        public int DelMin()
        {
            if (count == 0)
                throw new Exception("Priority queue underflow");

            int min = queue[1];

            Exch(1, count--);
            Sink(1);

            inversQueue[min] = -1;
            keys[min] = default;
            queue[count + 1] = -1;

            return min;
        }

        public TKey KeyOf(int i)
        {
            if (i < 0 || i >= maxCount)
                throw new ArgumentException();

            if (!Contains(i))
                throw new Exception("index is not in the priority queue");
            else
                return keys[i];
        }

        public void ChangeKey(int i, TKey key)
        {
            if (i < 0 || i >= maxCount)
                throw new ArgumentException();

            if (!Contains(i))
                throw new Exception("index is not in the priority queue");

            keys[i] = key;
            Swim(inversQueue[i]);
            Sink(inversQueue[i]);
        }

        public void Change(int i, TKey key)
        {
            ChangeKey(i, key);
        }

        public void DecreaseKey(int i, TKey key)
        {
            if (i < 0 || i >= maxCount)
                throw new ArgumentException();

            if (!Contains(i))
                throw new Exception("index is not in the priority queue");

            if (keys[i].CompareTo(key) <= 0)
                throw new ArgumentException("Calling decreaseKey() with given argument would not strictly decrease the key");

            keys[i] = key;
            Swim(inversQueue[i]);
        }

        public void IncreaseKey(int i, TKey key)
        {
            if (i < 0 || i >= maxCount)
                throw new ArgumentException();

            if (!Contains(i))
                throw new Exception("index is not in the priority queue");

            if (keys[i].CompareTo(key) >= 0)
                throw new ArgumentException("Calling increaseKey() with given argument would not strictly increase the key");

            keys[i] = key;
            Sink(inversQueue[i]);
        }

        public void Delete(int i)
        {
            if (i < 0 || i >= maxCount)
                throw new ArgumentException();

            if (!Contains(i))
                throw new Exception("index is not in the priority queue");

            int index = inversQueue[i];

            Exch(index, count--);
            Swim(index);
            Sink(index);

            keys[i] = default;
            inversQueue[i] = -1;
        }

        private bool Greater(int i, int j)
        {
            return keys[queue[i]].CompareTo(keys[queue[j]]) > 0;
        }

        private void Exch(int i, int j)
        {
            int swap = queue[i];
            queue[i] = queue[j];
            queue[j] = swap;
            inversQueue[queue[i]] = i;
            inversQueue[queue[j]] = j;
        }

        private void Swim(int k)
        {
            while (k > 1 && Greater(k / 2, k))
            {
                Exch(k, k / 2);
                k = k / 2;
            }
        }

        private void Sink(int k)
        {
            while (2 * k <= count)
            {
                int j = 2 * k;
                if (j < count && Greater(j, j + 1)) j++;
                if (!Greater(k, j)) break;
                Exch(k, j);
                k = j;
            }
        }


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
