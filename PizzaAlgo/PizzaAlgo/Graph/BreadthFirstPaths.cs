using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaAlgo.Graph
{
    public class BreadthFirstPaths
    {
        private bool[] marked;
        private int[] edgeTo;

        private readonly int startNode;

        public BreadthFirstPaths(Graph graph, int startNode)
        {
            marked = new bool[graph.Nodes];
            edgeTo = new int[graph.Nodes];
            this.startNode = startNode;
            BreadthFirstSearch(graph, startNode);
        }

        public bool HasPathTo(int node)
        {
            return marked[node];
        }

        public IEnumerable<int> PathTo(int node)
        {
            if (!HasPathTo(node))
                return null;

            Stack<int> path = new Stack<int>();
            for (int i = node; i != startNode; i = edgeTo[i])
                path.Push(i);

            path.Push(startNode);
            return path;
        }

        private void BreadthFirstSearch(Graph graph, int startNode)
        {
            Queue<int> queue = new Queue<int>();
            marked[startNode] = true;
            queue.Enqueue(startNode);

            while(queue.Count != 0)
            {
                int v = queue.Dequeue();

                foreach(var w in graph.Adjacency(v))
                {
                    if(!marked[w])
                    {
                        edgeTo[w] = v;
                        marked[w] = true;
                        queue.Enqueue(w);
                    }
                }
            }
        }
    }
}
