using System.Collections.Generic;

namespace Algorithms.Trees.RTree
{
    public interface ISpatialIndex<out T>
    {
        IReadOnlyList<T> Search();
        IReadOnlyList<T> Search(in Envelope boundingBox);
    }
}
