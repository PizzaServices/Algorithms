using System.Collections.Generic;
using Algorithms.DataStructures;

namespace Algorithms.Graphs.EdgeWeightedDirected
{
    public class EdgeWeightedDigraph
    {
        public int Vertices { get; }
        public int CountOfEdges { get; private set; }

        private readonly Bag<DirectedEdge>[] adjacencyList;

        public EdgeWeightedDigraph(int verticesCount)
        {
            Vertices = verticesCount;
            CountOfEdges = 0;
            adjacencyList = new Bag<DirectedEdge>[verticesCount];

            for(int i = 0; i < verticesCount; i++)
                adjacencyList[i] = new Bag<DirectedEdge>();
        }

        public void AddEdge(DirectedEdge edge)
        {
            adjacencyList[edge.StartVertex].Add(edge);
            CountOfEdges++;
        }

        public IEnumerable<DirectedEdge> Adj(int vertex)
        {
            return adjacencyList[vertex];
        }

        public IEnumerable<DirectedEdge> Edges()
        {
            var bag = new Bag<DirectedEdge>();

            for (int vertex = 0; vertex < Vertices; vertex++)
                foreach (var edge in adjacencyList[vertex])
                    bag.Add(edge);

            return bag;
        }
    }
}
