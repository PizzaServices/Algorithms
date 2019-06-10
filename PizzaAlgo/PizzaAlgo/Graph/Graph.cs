using PizzaAlgo.DataStruct;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaAlgo.Graph
{
    public class Graph
    {
        public int Nodes { get; private set; } 
        public int Edges { get; private set; }

        private Bag<int>[] adj;

        public Graph(int numberOfNodes)
        {
            Nodes = numberOfNodes;
            Edges = 0;
            adj = new Bag<int>[numberOfNodes];
            for(int i = 0; i < numberOfNodes; i++)
            {
                adj[i] = new Bag<int>();
            }
        }

        public void AddEdge(int v, int w)
        {
            adj[v].Add(w);
            adj[w].Add(v);
            Edges++;
        }

        public IEnumerable<int> Adjacency(int node)
        {
            return adj[node];
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append(Nodes + " vertices, " + Edges + " edges \n");
            for (int v = 0; v < Nodes; v++)
            {
                s.Append(v + ": ");
                foreach (var w in adj[v])
                {
                    s.Append(w + " ");
                }
                s.Append("\n");
            }
            return s.ToString();
        }

        public int Degree(int node)
        {
            ValidateVertex(node);
            return adj[node].Size();
        }

        public int maxDegree()
        {
            int max = 0;
            for(int i = 0; i < Nodes; i++)
            {
                if (Degree(i) > max)
                    max = Degree(i);
            }

            return max;
        }

        public double AvgDegree()
        {
            return 2 * Edges / Nodes;
        }

        public int NumberOfSelfLoops()
        {
            int count = 0;
            for(int i = 0; i < Nodes; i++)
            {
                foreach(var edge in Adjacency(i))
                {
                    if (i == edge)
                        count++;
                }
            }

            return count / 2;
        }

        private void ValidateVertex(int node)
        {
            if (node < 0 || node > Nodes)
                throw new ArgumentException();
        }
    }
}
