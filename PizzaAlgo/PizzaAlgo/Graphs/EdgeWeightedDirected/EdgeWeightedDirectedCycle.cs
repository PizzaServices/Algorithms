using System.Collections.Generic;

namespace PizzaAlgo.Graphs.EdgeWeightedDirected
{
    public class EdgeWeightedDirectedCycle
    {
        private bool[] marked;             
        private DirectedEdge[] edgeTo;        
        private bool[] onStack;            
        public Stack<DirectedEdge> Cycle { get; private set; }    

        public EdgeWeightedDirectedCycle(EdgeWeightedDigraph graph)
        {
            marked = new bool[graph.Vertices];
            onStack = new bool[graph.Vertices];
            edgeTo = new DirectedEdge[graph.Vertices];
            for (int v = 0; v < graph.Vertices; v++)
                if (!marked[v]) Dfs(graph, v);

        }

        private void Dfs(EdgeWeightedDigraph graph, int vertex)
        {
            onStack[vertex] = true;
            marked[vertex] = true;
            foreach (DirectedEdge edge in graph.Adj(vertex))
            {
                int w = edge.DestinationVertex;

                if (Cycle != null)
                    return;
                else if (!marked[w])
                {
                    edgeTo[w] = edge;
                    Dfs(graph, w);
                }
                else if (onStack[w])
                {
                    Cycle = new Stack<DirectedEdge>();

                    DirectedEdge f = edge;
                    while (f.StartVertex != w)
                    {
                        Cycle.Push(f);
                        f = edgeTo[f.StartVertex];
                    }
                    Cycle.Push(f);

                    return;
                }
            }

            onStack[vertex] = false;
        }

        public bool HasCycle()
        {
            return Cycle != null;
        }

        private bool Check()
        {
            if (HasCycle())
            {
                DirectedEdge first = null, last = null;
                foreach (var edge in Cycle)
                {
                    if (first == null) first = edge;
                    if (last != null)
                    {
                        if (last.DestinationVertex != edge.StartVertex)
                        {
                            return false;
                        }
                    }
                    last = edge;
                }

                if (last.DestinationVertex != first.StartVertex)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
