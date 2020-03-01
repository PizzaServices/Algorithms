using System.Collections.Generic;

namespace Algorithms.Graphs.Undirected
{
    public class DepthFirstPaths
    {
        private readonly bool[] marked;
        private readonly int[] edgeTo;

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

        public IEnumerable<int> PathTo(int vertex)
        {
            if (!HasPathTo(vertex))
                return null;

            var path = new Stack<int>();

            for (int index = vertex; index != startVertex; index = edgeTo[index])
                path.Push(index);

            path.Push(startVertex);
            return path;
        }

        private void DepthFirstSearch(Graph graph, int vertex)
        {
            marked[vertex] = true;
            foreach(var w in graph.Adjacency(vertex))
            {
                if (marked[w]) 
                    continue;

                edgeTo[w] = vertex;
                DepthFirstSearch(graph, w);
            }
        }
    }
}
