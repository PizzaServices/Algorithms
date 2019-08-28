using Algorithms.DataStructures;

namespace Algorithms.Graphs.EdgeWeightedDirected
{
    public class DijkstraSP : ShortestPath
    {
        private IndexMinPQ<double> priorityQueue;

        public DijkstraSP(EdgeWeightedDigraph graph, int startVertex)
        {
            edgeTo = new DirectedEdge[graph.Vertices];
            distTo = new double[graph.Vertices];
            priorityQueue = new IndexMinPQ<double>(graph.Vertices);

            for (int v = 0; v < graph.Vertices; v++)
                distTo[v] = double.MaxValue;

            distTo[startVertex] = 0.0f;

            priorityQueue.Insert(startVertex, 0.0f);

            while (!priorityQueue.IsEmpty())
                Relax(graph, priorityQueue.DelMin());

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

                    if(priorityQueue.Contains(w))
                        priorityQueue.Change(w, distTo[w]);
                    else
                        priorityQueue.Insert(w, distTo[w]);
                }
            }
        }
    }
}
