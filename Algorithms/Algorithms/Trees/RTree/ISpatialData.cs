namespace Algorithms.Trees.RTree
{
    public interface ISpatialData
    {
        ref readonly Envelope Envelope { get; }
    }
}
