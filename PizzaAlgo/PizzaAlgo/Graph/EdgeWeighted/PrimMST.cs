using System.Collections.Generic;
using PizzaAlgo.DataStruct;

namespace PizzaAlgo.Graph.EdgeWeighted
{
    public class PrimMST
    {
        private Edge[] edgeTo;
        private double[] distTo;
        private bool[] marked;
        private IndexMinPQ<double> prioryQueue;

        public PrimMST(EdgeWeightedGraph graph)
        {
            edgeTo = new Edge[graph.Vertecies];
            distTo = new double[graph.Vertecies];
            marked = new bool[graph.Vertecies];

            for (int v = 0; v < graph.Vertecies; v++)
                distTo[v] = double.MaxValue;

            prioryQueue = new IndexMinPQ<double>(graph.Vertecies);

            distTo[0] = 0.0f;
            prioryQueue.Insert(0,0.0f);

            while(!prioryQueue.IsEmpty())
                Visit(graph, prioryQueue.DelMin());
        }

        public IEnumerable<Edge> Edges()
        {
            Queue<Edge> mst = new Queue<Edge>();

            for (int v = 0; v < edgeTo.Length; v++)
            {
                var edge = edgeTo[v];
                if(edge != null)
                    mst.Enqueue(edge);
            }

            return mst;
        }

        private void Visit(EdgeWeightedGraph graph, int vertex)
        {
            marked[vertex] = true;
            foreach (var edge in graph.Adj(vertex))
            {
                int otherVertex = edge.Other(vertex);
                if(marked[otherVertex])
                    continue;

                if (edge.Weight < distTo[otherVertex])
                {
                    edgeTo[otherVertex] = edge;
                    distTo[otherVertex] = edge.Weight;

                    if(prioryQueue.Contains(otherVertex))
                        prioryQueue.Change(otherVertex, distTo[otherVertex]);
                    else
                        prioryQueue.Insert(otherVertex, distTo[otherVertex]);
                }
            }
        }
    }
}
