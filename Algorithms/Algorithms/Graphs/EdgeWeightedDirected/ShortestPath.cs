using System.Collections.Generic;

namespace Algorithms.Graphs.EdgeWeightedDirected
{
    public abstract class ShortestPath
    {
        protected DirectedEdge[] EdgeTo;
        protected double[] DistTo;

        public double GetDistanceTo(int vertex)
        {
            return DistTo[vertex];
        }

        public bool HasPathTo(int vertex)
        {
            return DistTo[vertex] < double.MaxValue;
        }

        public IEnumerable<DirectedEdge> PathTo(int vertex)
        {
            if (!HasPathTo(vertex))
                return null;

            var path = new Stack<DirectedEdge>();
            for(var edge = EdgeTo[vertex]; edge != null; edge = EdgeTo[edge.StartVertex])
                path.Push(edge);

            return path;
        }
    }
}
