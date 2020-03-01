using Algorithms.Graphs.Directed;

namespace Algorithms.Graphs.EdgeWeightedDirected
{
    public class AcyclicSp : ShortestPath
    {
        public AcyclicSp(EdgeWeightedDigraph graph, int startVertex)
        {
            EdgeTo = new DirectedEdge[graph.Vertices];
            DistTo = new double[graph.Vertices];

            for (int v = 0; v < graph.Vertices; v++)
                DistTo[v] = double.MaxValue;

            DistTo[startVertex] = 0.0f;

            var topological = new Topological(graph);

            foreach (var v in topological.Order())
                Relax(graph, v);

        }

        private void Relax(EdgeWeightedDigraph graph, int vertex)
        {
            foreach (var edge in graph.Adj(vertex))
            {
                int w = edge.DestinationVertex;

                if (!(DistTo[w] > DistTo[vertex] + edge.Weight)) 
                    continue;

                DistTo[w] = DistTo[vertex] + edge.Weight;
                EdgeTo[w] = edge;
            }
        }
    }
}
