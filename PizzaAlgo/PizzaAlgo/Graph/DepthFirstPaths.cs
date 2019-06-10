using System.Collections.Generic;

namespace PizzaAlgo.Graph
{
    public class DepthFirstPaths
    {
        private bool[] marked;
        private int[] edgeTo;

        private readonly int startNode;

        public DepthFirstPaths(Graph graph, int startNode)
        {
            marked = new bool[graph.Nodes];
            edgeTo = new int[graph.Nodes];
            this.startNode = startNode;
        }

        public bool HasPathTo(int node)
        {
            return marked[node];
        }

        public IEnumerable<int> pathTo(int node)
        {
            if (!HasPathTo(node))
                return null;

            Stack<int> path = new Stack<int>();
            for (int i = node; i != startNode; i = edgeTo[i])
                path.Push(i);

            path.Push(startNode);
            return path;
        }

        private void DepthFirstSearch(Graph graph, int node)
        {
            marked[node] = true;
            foreach(var w in graph.Adjacency(node))
            {
                if(!marked[w])
                {
                    edgeTo[w] = node;
                    DepthFirstSearch(graph, w);
                }
            }
        }
    }
}
