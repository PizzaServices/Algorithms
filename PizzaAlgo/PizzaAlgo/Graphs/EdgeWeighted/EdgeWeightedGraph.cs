using System.Collections.Generic;
using PizzaAlgo.DataStructures;

namespace PizzaAlgo.Graphs.EdgeWeighted
{
    public class EdgeWeightedGraph
    {
        public int Vertecies { get; private set; }
        public int Edges { get; private set; }

        private Bag<Edge>[] adj;

        public EdgeWeightedGraph(int v)
        {
            Vertecies = v;
            Edges = 0;
            adj = new Bag<Edge>[v];
            for (var i = 0; i < v; i++)
            {
                adj[v] = new Bag<Edge>();
            }
        }

        public void AddEdge(Edge e)
        {
            int v = e.Either();
            int w = e.Other(v);

            adj[v].Add(e);
            adj[w].Add(e);
            Edges++;
        }

        public IEnumerable<Edge> Adj(int v)
        {
            return adj[v];
        }

        public IEnumerable<Edge> GetEdges()
        {
            var result = new Bag<Edge>();

            foreach (var bag in adj)
            {
                foreach (var edge in bag)
                {
                    result.Add(edge);
                }
            }

            return result;
        }
    }
}
