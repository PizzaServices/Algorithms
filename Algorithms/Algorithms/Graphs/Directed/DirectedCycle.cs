using System.Collections.Generic;

namespace Algorithms.Graphs.Directed
{
    public class DirectedCycle
    {
        private readonly bool[] marked;
        private readonly int[] edgeTo;
        private Stack<int> cycle;
        private readonly bool[] onStack;

        public DirectedCycle(Digraph digraph)
        {
            onStack = new bool[digraph.Vertices];
            edgeTo = new int[digraph.Vertices];
            marked = new bool[digraph.Vertices];

            for(int v = 0; v < digraph.Vertices; v++)
            {
                if (!marked[v])
                    Dfs(digraph, v);
            }
        }

        public bool HasCycle()
        {
            return cycle != null;
        }

        public IEnumerable<int> Cycle()
        {
            return cycle;
        }

        private void Dfs(Digraph digraph, int vertex)
        {
            onStack[vertex] = true;
            marked[vertex] = true;

            foreach (var headVertex in digraph.Adjacency(vertex))
            {
                if (HasCycle())
                    return;
                else if (!marked[headVertex])
                {
                    edgeTo[headVertex] = vertex;
                    Dfs(digraph, headVertex);
                }
                else if(onStack[headVertex])
                {
                    cycle = new Stack<int>();
                    for (int x = vertex; x != headVertex; x = edgeTo[x])
                        cycle.Push(x);

                    cycle.Push(headVertex);
                    cycle.Push(vertex);
                }
            }
            onStack[vertex] = false;
        }
    }
}
