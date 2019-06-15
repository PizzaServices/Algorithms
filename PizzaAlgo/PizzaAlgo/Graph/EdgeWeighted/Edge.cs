using System;

namespace PizzaAlgo.Graph.EdgeWeighted
{
    public class Edge : IComparable<Edge>
    {
        public int VertexOne { get; private set; }
        public int VertexTwo { get; private set; }
        public double Weight { get; private set; }

        public Edge(int v, int w, double weight)
        {
            VertexOne = v;
            VertexTwo = w;
            Weight = weight;
        }

        public int Either()
        {
            return VertexOne;
        }

        public int Other(int vertex)
        {
            if (vertex == VertexOne)
                return VertexTwo;
            if (vertex == VertexTwo)
                return VertexOne;
            throw new ArgumentException("Inconsistent edge");
        }

        public override string ToString()
        {
            return $"{VertexOne}-{VertexTwo} {Weight:F2}";
        }

        public int CompareTo(Edge that)
        {
            if (Weight < that.Weight)
                return -1;
            if (Weight > that.Weight)
                return 1;
            return 0;
        }
    }
}
