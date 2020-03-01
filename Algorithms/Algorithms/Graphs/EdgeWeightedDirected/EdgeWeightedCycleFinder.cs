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
            for (int index = 0; index < graph.Vertices; ++index)
            {
                if (!marked[index] && !HasCycle())
                    Dfs(index);
            }
        }
        private void Dfs(int vertex)
        {
            marked[vertex] = true;
            onStack[vertex] = true;
            foreach (var e in graph.Adj(vertex))
            {
                if (hasCycle)
                    break;

                int destinationVertex = e.DestinationVertex;
                if (onStack[destinationVertex])
                {
                    hasCycle = true;
                    cycle = new Stack<DirectedEdge>();
                    int tmpVertex = vertex;
                    cycle.Push(e);
                    while (tmpVertex != destinationVertex)
                    {
                        cycle.Push(edgeTo[tmpVertex]);
                        tmpVertex = edgeTo[tmpVertex].StartVertex;
                    }
                }
                else
                {
                    if (marked[destinationVertex]) 
                        continue;

                    edgeTo[destinationVertex] = e;
                    Dfs(destinationVertex);
                }
            }
            onStack[vertex] = false;
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
