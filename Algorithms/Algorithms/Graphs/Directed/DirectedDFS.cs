using System.Collections.Generic;

namespace Algorithms.Graphs.Directed
{
    public class DirectedDfs
    {
        private readonly bool[] marked;

        public DirectedDfs(Digraph digraph, int startVertex)
        {
            marked = new bool[digraph.Vertices];
            DepthFirstSearch(digraph, startVertex);
        }

        public DirectedDfs(Digraph digraph, IEnumerable<int> sources)
        {
            marked = new bool[digraph.Vertices];
            foreach(var s in sources)
            {
                if (!marked[s])
                    DepthFirstSearch(digraph, s);
            }
        }

        public bool Marked(int vertex)
        {
            return marked[vertex];
        }

        private void DepthFirstSearch(Digraph digraph, int vertex)
        {
            marked[vertex] = true;
            foreach(var headVertex in digraph.Adjacency(vertex))
            {
                if (!marked[headVertex])
                    DepthFirstSearch(digraph, headVertex);
            }
        }
    }
}
