using System;

namespace Algorithms.DataStructures
{
    public class DisjointSet
    {
        private int[] parent; 
        private byte[] rank;
        private int count;

        public DisjointSet(int count)
        {
            if (count < 0)
                throw new ArgumentException();

            this.count = count;
            parent = new int[count];
            rank = new byte[count];

            for (int i = 0; i < count; i++)
            {
                parent[i] = i;
                rank[i] = 0;
            }
        }

        public int Find(int p)
        {
            Validate(p);
            while (p != parent[p])
            {
                parent[p] = parent[parent[p]];
                p = parent[p];
            }

            return p;
        }

        public int Count()
        {
            return count;
        }

        public bool Connected(int p, int q)
        {
            return Find(p) == Find(q);
        }

        public void Union(int p, int q)
        {
            int rootP = Find(p);
            int rootQ = Find(q);

            if (rootP == rootQ)
                return;

            if (rank[rootP] < rank[rootQ])
                parent[rootP] = rootQ;
            else if (rank[rootP] > rank[rootQ])
                parent[rootQ] = rootP;
            else
            {
                parent[rootQ] = rootP;
                rank[rootP]++;
            }

            count--;
        }

        private void Validate(int p)
        {
            int n = parent.Length;
            if (p < 0 || p >= n)
            {
                throw new ArgumentException("index " + p + " is not between 0 and " + (n - 1));
            }
        }
    }
}
