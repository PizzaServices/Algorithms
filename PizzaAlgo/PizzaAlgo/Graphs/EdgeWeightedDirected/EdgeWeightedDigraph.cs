using System.Collections.Generic;
using PizzaAlgo.DataStructures;

namespace PizzaAlgo.Graphs.EdgeWeightedDirected
{
    public class EdgeWeightedDigraph
    {
        public int Vertices { get; }
        public int CountOfEdges { get; private set; }

        private Bag<DirectedEdge>[] adj;

        public EdgeWeightedDigraph(int vertices)
        {
            Vertices = vertices;
            CountOfEdges = 0;
            adj = new Bag<DirectedEdge>[vertices];

            for(int i = 0; i < vertices; i++)
                adj[i] = new Bag<DirectedEdge>();
        }

        public void AddEdge(DirectedEdge edge)
        {
            adj[edge.StartVertex].Add(edge);
            CountOfEdges++;
        }

        public IEnumerable<DirectedEdge> Adj(int vertex)
        {
            return adj[vertex];
        }

        public IEnumerable<DirectedEdge> Edges()
        {
            var bag = new Bag<DirectedEdge>();

            for (int v = 0; v < Vertices; v++)
                foreach (var edge in adj[v])
                    bag.Add(edge);

            return bag;
        }
    }
}
