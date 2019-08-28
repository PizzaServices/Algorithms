using System.Collections.Generic;

namespace Algorithms.Graphs.EdgeWeightedDirected
{
    public class EdgeWeightedCycleFinder
    {
        private readonly EdgeWeightedDigraph graph;
        private readonly bool[] onStack;
        private readonly DirectedEdge[] edgeTo;
        private readonly bool[] marked;

        private bool hasCycle;
        private Stack<DirectedEdge> cycle;

        public EdgeWeightedCycleFinder(EdgeWeightedDigraph graph)
        {
            this.graph = graph;
            onStack = new bool[graph.Vertices];
            edgeTo = new DirectedEdge[graph.Vertices];
            marked = new bool[graph.Vertices];
            for (int i = 0; i < graph.Vertices; ++i)
            {
                if (!marked[i] && !HasCycle())
                    Dfs(i);
            }
        }
        private void Dfs(int v)
        {
            marked[v] = true;
            onStack[v] = true;
            foreach (var e in graph.Adj(v))
            {
                if (hasCycle)
                    break;
                int w = e.DestinationVertex;
                if (onStack[w])
                {
                    hasCycle = true;
                    cycle = new Stack<DirectedEdge>();
                    int p = v;
                    cycle.Push(e);
                    while (p != w)
                    {
                        cycle.Push(edgeTo[p]);
                        p = edgeTo[p].StartVertex;
                    }
                }
                else
                {
                    if (!marked[w])
                    {
                        edgeTo[w] = e;
                        Dfs(w);
                    }
                }
            }
            onStack[v] = false;
        }
        public bool HasCycle()
        {
            return hasCycle;
        }
        public IEnumerable<DirectedEdge> Cycle()
        {
            return cycle;
        }
    }
}
