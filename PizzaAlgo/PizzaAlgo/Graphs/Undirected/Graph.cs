using PizzaAlgo.DataStructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaAlgo.Graphs.Undirected
{
    public class Graph
    {
        public int Vertices { get; private set; } 
        public int Edges { get; private set; }

        private Bag<int>[] adj;

        public Graph(int numberOfVertices)
        {
            Vertices = numberOfVertices;
            Edges = 0;
            adj = new Bag<int>[numberOfVertices];
            for(int i = 0; i < numberOfVertices; i++)
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

        public IEnumerable<int> Adjacency(int vertex)
        {
            return adj[vertex];
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append(Vertices + " vertices, " + Edges + " edges \n");
            for (int v = 0; v < Vertices; v++)
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

        public int Degree(int vertex)
        {
            ValidateVertex(vertex);
            return adj[vertex].Size();
        }

        public int maxDegree()
        {
            int max = 0;
            for(int i = 0; i < Vertices; i++)
            {
                if (Degree(i) > max)
                    max = Degree(i);
            }

            return max;
        }

        public double AvgDegree()
        {
            return 2 * Edges / Vertices;
        }

        public int NumberOfSelfLoops()
        {
            int count = 0;
            for(int i = 0; i < Vertices; i++)
            {
                foreach(var edge in Adjacency(i))
                {
                    if (i == edge)
                        count++;
                }
            }

            return count / 2;
        }

        private void ValidateVertex(int vertex)
        {
            if (vertex < 0 || vertex > Vertices)
                throw new ArgumentException();
        }
    }
}
