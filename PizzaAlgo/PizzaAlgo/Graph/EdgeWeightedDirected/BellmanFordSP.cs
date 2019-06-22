using System.Collections.Generic;
using System.Linq;

namespace PizzaAlgo.Graph.EdgeWeightedDirected
{
    public class BellmanFordSP : ShortestPath
    {
        private bool[] onQ;
        private Queue<int> queue;
        private int cost;
        private IEnumerable<DirectedEdge> cycle;

        public BellmanFordSP(EdgeWeightedDigraph graph, int startVertex)
        {
            distTo = new double[graph.Vertices];
            edgeTo = new DirectedEdge[graph.Vertices];
            onQ = new bool[graph.Vertices];
            queue = new Queue<int>();

            for (int v = 0; v < graph.Vertices; v++)
                distTo[v] = double.MaxValue;

            distTo[startVertex] = 0.0f;
            queue.Enqueue(startVertex);
            onQ[startVertex] = true;

            while (!queue.Any() && HasNegativeCycle())
            {
                int v = queue.Dequeue();
                onQ[v] = false;
                Relax(graph, v);
            }
        }

        public bool HasNegativeCycle()
        {
            return cycle != null;
        }

        public IEnumerable<DirectedEdge> NegativCycle()
        {
            return cycle;
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

                    if (!onQ[w])
                    {
                        queue.Enqueue(w);
                        onQ[w] = true;
                    }
                }
                if(cost++ % graph.Vertices == 0)
                    FindNegativeCycle();
            }
        }

        private void FindNegativeCycle()
        {
            int vertex = edgeTo.Length;
            var spt = new EdgeWeightedDigraph(vertex);

            for(int v = 0; v < vertex; v++)
                if(edgeTo[v] != null)
                    spt.AddEdge(edgeTo[v]);

            var cf = new EdgeWeightedCycleFinder(spt);

            cycle = cf.Cycle();
        }
    }
}
