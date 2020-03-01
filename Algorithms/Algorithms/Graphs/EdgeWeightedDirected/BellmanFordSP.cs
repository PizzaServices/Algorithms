using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Graphs.EdgeWeightedDirected
{
    public class BellmanFordSp : ShortestPath
    {
        private readonly bool[] onQueue;
        private readonly Queue<int> queue;
        private int cost;
        private IEnumerable<DirectedEdge> cycle;

        public BellmanFordSp(EdgeWeightedDigraph graph, int startVertex)
        {
            DistTo = new double[graph.Vertices];
            EdgeTo = new DirectedEdge[graph.Vertices];
            onQueue = new bool[graph.Vertices];
            queue = new Queue<int>();

            for (int v = 0; v < graph.Vertices; v++)
                DistTo[v] = double.MaxValue;

            DistTo[startVertex] = 0.0f;
            queue.Enqueue(startVertex);
            onQueue[startVertex] = true;

            while (!queue.Any() && HasNegativeCycle())
            {
                int v = queue.Dequeue();
                onQueue[v] = false;
                Relax(graph, v);
            }
        }

        public bool HasNegativeCycle()
        {
            return cycle != null;
        }

        public IEnumerable<DirectedEdge> NegativeCycle()
        {
            return cycle;
        }

        private void Relax(EdgeWeightedDigraph graph, int vertex)
        {
            foreach (var edge in graph.Adj(vertex))
            {
                int w = edge.DestinationVertex;
                if (DistTo[w] > DistTo[vertex] + edge.Weight)
                {
                    DistTo[w] = DistTo[vertex] + edge.Weight;
                    EdgeTo[w] = edge;

                    if (!onQueue[w])
                    {
                        queue.Enqueue(w);
                        onQueue[w] = true;
                    }
                }
                if(cost++ % graph.Vertices == 0)
                    FindNegativeCycle();
            }
        }

        private void FindNegativeCycle()
        {
            int vertex = EdgeTo.Length;
            var spt = new EdgeWeightedDigraph(vertex);

            for(int v = 0; v < vertex; v++)
                if(EdgeTo[v] != null)
                    spt.AddEdge(EdgeTo[v]);

            var cf = new EdgeWeightedCycleFinder(spt);

            cycle = cf.Cycle();
        }
    }
}
