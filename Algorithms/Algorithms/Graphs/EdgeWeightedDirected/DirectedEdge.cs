namespace Algorithms.Graphs.EdgeWeightedDirected
{
    public class DirectedEdge
    {
        public int StartVertex { get; }
        public int DestinationVertex { get; }
        public double Weight { get; }

        public DirectedEdge(int startVertex, int destinationVertex, double weight)
        {
            StartVertex = startVertex;
            DestinationVertex = destinationVertex;
            Weight = weight;
        }
    }
}
