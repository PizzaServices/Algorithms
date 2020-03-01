using System.Collections.Generic;
using System.Text;
using Algorithms.DataStructures;

namespace Algorithms.Graphs.Directed
{
    public class Digraph
    {
        public int Vertices { get; }
        public int Edges { get; private set; }

        private readonly Bag<int>[] adjacencyList;

        public Digraph(int numberOfVertices)
        {
            Vertices = numberOfVertices;
            Edges = 0;
            adjacencyList = new Bag<int>[numberOfVertices];
            for (int i = 0; i < numberOfVertices; i++)
                adjacencyList[i] = new Bag<int>();
        }

        public void AddEdge(int tailVertex, int headVertex)
        {
            adjacencyList[tailVertex].Add(headVertex);
            Edges++;
        }

        public IEnumerable<int> Adjacency(int vertex)
        {
            return adjacencyList[vertex];
        }

        public Digraph Reverse()
        {
            var reverse = new Digraph(Vertices);
            for(int tailVertex = 0; tailVertex < Vertices; tailVertex++)
            {
                foreach (var headVertex in adjacencyList[tailVertex])
                    reverse.AddEdge(headVertex, tailVertex);
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
                foreach (var w in adjacencyList[v])
                {
                    s.Append(w + " ");
                }
                s.Append("\n");
            }
            return s.ToString();
        }
    }
}
