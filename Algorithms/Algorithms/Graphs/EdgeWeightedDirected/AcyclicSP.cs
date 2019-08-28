using Algorithms.Graphs.Directed;

namespace Algorithms.Graphs.EdgeWeightedDirected
{
    public class AcyclicSP : ShortestPath
    {
        public AcyclicSP(EdgeWeightedDigraph graph, int startVertex)
        {
            edgeTo = new DirectedEdge[graph.Vertices];
            distTo = new double[graph.Vertices];

            for (int v = 0; v < graph.Vertices; v++)
                distTo[v] = double.MaxValue;

            distTo[startVertex] = 0.0f;

            var topological = new Topological(graph);

            foreach (var v in topological.Order())
                Relax(graph, v);

        }

        private void Relax(EdgeWeightedDigraph graph, int vertex)
        {
            foreach (var edge in graph.Adj(vertex))
            {
                int w = edge.DestinationVertex;
                if (distTo[w] > distTo[vertex] + edge.Weight)
                {
                    distTo[w] = distTo[vertex] + edge.Weight;
                    edgeTo[w] = edge;
                }
            }
        }
    }
}
