using System.Collections.Generic;
using System.Text;
using Algorithms.DataStructures;

namespace Algorithms.Graphs.Directed
{
    public class Digraph
    {
        public int Vertices { get; private set; }
        public int Edges { get; private set; }

        private Bag<int>[] adj;

        public Digraph(int numberOfVertices)
        {
            Vertices = numberOfVertices;
            Edges = 0;
            adj = new Bag<int>[numberOfVertices];
            for (int i = 0; i < numberOfVertices; i++)
                adj[i] = new Bag<int>();
        }

        public void AddEdge(int v, int w)
        {
            adj[v].Add(w);
            Edges++;
        }

        public IEnumerable<int> Adjacency(int vertex)
        {
            return adj[vertex];
        }

        public Digraph Reverse()
        {
            var reverse = new Digraph(Vertices);
            for(int v = 0; v < Vertices; v++)
            {
                foreach (var w in adj[v])
                    reverse.AddEdge(w, v);
            }

            return reverse;
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
    }
}
