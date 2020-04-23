using System.Collections.Generic;
using Algorithms.DataStructures;

namespace Algorithms.Graphs.EdgeWeighted
{
    public class LazyPrimMst
    {
        private readonly bool[] marked;
        private readonly Queue<Edge> mst;
        private readonly MinPQ<Edge> prioryQueue;

        public LazyPrimMst(EdgeWeightedGraph graph)
        {
            prioryQueue = new MinPQ<Edge>();
            marked = new bool[graph.Vertices];
            mst = new Queue<Edge>();

            Visit(graph, 0);

            while (!prioryQueue.IsEmpty())
            {
                var edge = prioryQueue.DelMin();
                int vertex = edge.Either();
                int otherVertex = edge.Other(vertex);

                if(marked[vertex] && marked[otherVertex])
                    continue;

                mst.Enqueue(edge);
                if(!marked[vertex])
                    Visit(graph, vertex);
                if(!marked[otherVertex])
                    Visit(graph, otherVertex);
            }
        }

        public IEnumerable<Edge> Edges()
        {
            return mst;
        }

        private void Visit(EdgeWeightedGraph graph, int vertex)
        {
            marked[vertex] = true;
            foreach(var edge in graph.Adj(vertex))
            {
                if(!marked[edge.Other(vertex)])
                    prioryQueue.Insert(edge);
            }
        }
    }
}
