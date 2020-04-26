using System.Collections.Generic;
using Algorithms.Containers;

namespace Algorithms.Graphs.EdgeWeighted
{
    public class EdgeWeightedGraph
    {
        public int Vertices { get; }
        public int Edges { get; private set; }

        private readonly Bag<Edge>[] adjacencyList;

        public EdgeWeightedGraph(int verticesCount)
        {
            Vertices = verticesCount;
            Edges = 0;
            adjacencyList = new Bag<Edge>[verticesCount];
            for (var i = 0; i < verticesCount; i++)
            {
                adjacencyList[verticesCount] = new Bag<Edge>();
            }
        }

        public void AddEdge(Edge edge)
        {
            int v = edge.Either();
            int w = edge.Other(v);

            adjacencyList[v].Add(edge);
            adjacencyList[w].Add(edge);
            Edges++;
        }

        public IEnumerable<Edge> Adj(int vertex)
        {
            return adjacencyList[vertex];
        }

        public IEnumerable<Edge> GetEdges()
        {
            var result = new Bag<Edge>();

            foreach (var bag in adjacencyList)
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
