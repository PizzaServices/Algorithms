namespace PizzaAlgo.Graphs.EdgeWeightedDirected
{
    public class DirectedEdge
    {
        public int StartVertex { get; }
        public int DestinationVertex { get; }
        public int Weight { get; }

        public DirectedEdge(int startVertex, int destinationVertex, int weight)
        {
            StartVertex = startVertex;
            DestinationVertex = destinationVertex;
            Weight = weight;
        }
    }
}
