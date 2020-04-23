using System.Collections.Generic;
using Algorithms.DataStructures;

namespace Algorithms.Graphs.EdgeWeighted
{
    public class PrimMst
    {
        private readonly Edge[] edgeTo;
        private readonly double[] distTo;
        private readonly bool[] marked;
        private readonly IndexMinPQ<double> prioryQueue;

        public PrimMst(EdgeWeightedGraph graph)
        {
            edgeTo = new Edge[graph.Vertices];
            distTo = new double[graph.Vertices];
            marked = new bool[graph.Vertices];

            for (int vertex = 0; vertex < graph.Vertices; vertex++)
                distTo[vertex] = double.MaxValue;

            prioryQueue = new IndexMinPQ<double>(graph.Vertices);

            distTo[0] = 0.0f;
            prioryQueue.Insert(0,0.0f);

            while(!prioryQueue.IsEmpty())
                Visit(graph, prioryQueue.DelMin());
        }

        public IEnumerable<Edge> Edges()
        {
            var mst = new Queue<Edge>();

            foreach (var edge in edgeTo)
            {
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

                if (!(edge.Weight < distTo[otherVertex])) 
                    continue;

                edgeTo[otherVertex] = edge;
                distTo[otherVertex] = edge.Weight;

                if(prioryQueue.Contains(otherVertex))
                    prioryQueue.ChangeKey(otherVertex, distTo[otherVertex]);
                else
                    prioryQueue.Insert(otherVertex, distTo[otherVertex]);
            }
        }
    }
}
