using System.Collections.Generic;

namespace PizzaAlgo.Graph
{
    public class BreadthFirstPaths
    {
        private bool[] marked;
        private int[] edgeTo;

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

            Stack<int> path = new Stack<int>();
            for (int i = vertex; i != startVertex; i = edgeTo[i])
                path.Push(i);

            path.Push(startVertex);
            return path;
        }

        private void BreadthFirstSearch(Graph graph, int startVertex)
        {
            Queue<int> queue = new Queue<int>();
            marked[startVertex] = true;
            queue.Enqueue(startVertex);

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
