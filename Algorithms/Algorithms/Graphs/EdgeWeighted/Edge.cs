using System;

namespace Algorithms.Graphs.EdgeWeighted
{
    public class Edge : IComparable<Edge>
    {
        public int TrailVertex { get; }
        public int HeadVertex { get; }
        public double Weight { get; }

        public Edge(int trailVertex, int headVertex, double weight)
        {
            TrailVertex = trailVertex;
            HeadVertex = headVertex;
            Weight = weight;
        }

        public int Either()
        {
            return TrailVertex;
        }

        public int Other(int vertex)
        {
            if (vertex == TrailVertex)
                return HeadVertex;
            if (vertex == HeadVertex)
                return TrailVertex;
            throw new ArgumentException("Inconsistent edge");
        }

        public override string ToString()
        {
            return $"{TrailVertex}-{HeadVertex} {Weight:F2}";
        }

        public int CompareTo(Edge that)
        {
            if (Weight < that.Weight)
                return -1;

            return Weight > that.Weight ? 1 : 0;
        }
    }
}
