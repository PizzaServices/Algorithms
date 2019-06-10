using System.Collections.Generic;

namespace PizzaAlgo.Graph
{
    public class DepthFirstPaths
    {
        private bool[] marked;
        private int[] edgeTo;

        private readonly int startVertex;

        public DepthFirstPaths(Graph graph, int startVertex)
        {
            marked = new bool[graph.Vertices];
            edgeTo = new int[graph.Vertices];
            this.startVertex = startVertex;
        }

        public bool HasPathTo(int vertex)
        {
            return marked[vertex];
        }

        public IEnumerable<int> pathTo(int vertex)
        {
            if (!HasPathTo(vertex))
                return null;

            Stack<int> path = new Stack<int>();
            for (int i = vertex; i != startVertex; i = edgeTo[i])
                path.Push(i);

            path.Push(startVertex);
            return path;
        }

        private void DepthFirstSearch(Graph graph, int vertex)
        {
            marked[vertex] = true;
            foreach(var w in graph.Adjacency(vertex))
            {
                if(!marked[w])
                {
                    edgeTo[w] = vertex;
                    DepthFirstSearch(graph, w);
                }
            }
        }
    }
}
