using System.Collections.Generic;

namespace Algorithms.Graphs.Undirected
{
    public class BreadthFirstPaths
    {
        private readonly bool[] marked;
        private readonly int[] edgeTo;

        private readonly int startVertex;

        public BreadthFirstPaths(Graph graph, int startVertex)
        {
            marked = new bool[graph.Vertices];
            edgeTo = new int[graph.Vertices];
            this.startVertex = startVertex;
            BreadthFirstSearch(graph, startVertex);
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

        private void BreadthFirstSearch(Graph graph, int vertex)
        {
            var queue = new Queue<int>();
            marked[vertex] = true;
            queue.Enqueue(vertex);

            while(queue.Count != 0)
            {
                int tailVertex = queue.Dequeue();

                foreach(var headVertex in graph.Adjacency(tailVertex))
                {
                    if (marked[headVertex]) 
                        continue;

                    edgeTo[headVertex] = tailVertex;
                    marked[headVertex] = true;
                    queue.Enqueue(headVertex);
                }
            }
        }
    }
}
