using System.Collections.Generic;
using PizzaAlgo.DataStructures;

namespace PizzaAlgo.Graphs.EdgeWeighted
{
    public class LazyPrimMST
    {
        private bool[] marked;
        private Queue<Edge> mst;
        private MinPQ<Edge> prioryQueue;

        public LazyPrimMST(EdgeWeightedGraph graph)
        {
            prioryQueue = new MinPQ<Edge>();
            marked = new bool[graph.Vertecies];
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
