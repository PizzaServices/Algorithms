using Algorithms.DataStructures;

namespace Algorithms.Graphs.EdgeWeightedDirected
{
    public class DijkstraSp : ShortestPath
    {
        private readonly IndexMinPQ<double> priorityQueue;

        public DijkstraSp(EdgeWeightedDigraph graph, int startVertex)
        {
            EdgeTo = new DirectedEdge[graph.Vertices];
            DistTo = new double[graph.Vertices];
            priorityQueue = new IndexMinPQ<double>(graph.Vertices);

            for (int v = 0; v < graph.Vertices; v++)
                DistTo[v] = double.MaxValue;

            DistTo[startVertex] = 0.0f;

            priorityQueue.Insert(startVertex, 0.0f);

            while (!priorityQueue.IsEmpty())
                Relax(graph, priorityQueue.DelMin());

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

                if(priorityQueue.Contains(w))
                    priorityQueue.ChangeKey(w, DistTo[w]);
                else
                    priorityQueue.Insert(w, DistTo[w]);
            }
        }
    }
}
