using System.Collections.Generic;

namespace PizzaAlgo.Graphs.EdgeWeightedDirected
{
    public abstract class ShortestPath
    {
        protected DirectedEdge[] edgeTo;
        protected double[] distTo;

        public double DistTo(int vertex)
        {
            return distTo[vertex];
        }

        public bool HasPathTo(int vertex)
        {
            return distTo[vertex] < double.MaxValue;
        }

        public IEnumerable<DirectedEdge> PathTo(int vertex)
        {
            if (!HasPathTo(vertex))
                return null;

            var path = new Stack<DirectedEdge>();
            for(var edge = edgeTo[vertex]; edge != null; edge = edgeTo[edge.StartVertex])
                path.Push(edge);

            return path;
        }
    }
}
