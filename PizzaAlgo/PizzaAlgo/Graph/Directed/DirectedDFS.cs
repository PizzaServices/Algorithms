using System.Collections.Generic;

namespace PizzaAlgo.Graph.Directed
{
    public class DirectedDFS
    {
        private bool[] marked;

        public DirectedDFS(Digraph digraph, int startVertex)
        {
            marked = new bool[digraph.Vertices];
            DepthFirstSearch(digraph, startVertex);
        }

        public DirectedDFS(Digraph digraph, IEnumerable<int> sources)
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
            foreach(var w in digraph.Adjacency(vertex))
            {
                if (!marked[w])
                    DepthFirstSearch(digraph, w);
            }
        }
    }
}
