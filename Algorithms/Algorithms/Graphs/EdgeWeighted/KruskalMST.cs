using System.Collections.Generic;
using Algorithms.DataStructures;

namespace Algorithms.Graphs.EdgeWeighted
{
    public class KruskalMST
    {
        private Queue<Edge> mst;

        public KruskalMST(EdgeWeightedGraph graph)
        {
            mst = new Queue<Edge>();

            var prioryQueue = new MinPQ<Edge>();
            foreach (var edge in graph.GetEdges())
                prioryQueue.Insert(edge);

            var ds = new DisjointSet(graph.Vertecies);

            while (!prioryQueue.IsEmpty() && mst.Count < graph.Vertecies - 1)
            {
                var edge = prioryQueue.DelMin();
                int vertex = edge.Either();
                int otherVertex = edge.Other(vertex);

                if(ds.Connected(vertex, otherVertex))
                    continue;

                ds.Union(vertex, otherVertex);
                mst.Enqueue(edge);
            }
        }

        public IEnumerable<Edge> Edges()
        {
            return mst;
        }
    }
}
