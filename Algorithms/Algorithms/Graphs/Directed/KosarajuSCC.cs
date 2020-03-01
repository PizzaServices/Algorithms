namespace Algorithms.Graphs.Directed
{
    public class KosarajuSharirScc
    {
        private readonly bool[] marked;
        private readonly int[] id;
        private readonly int count;

        public KosarajuSharirScc(Digraph digraph)
        {
            marked = new bool[digraph.Vertices];
            id = new int[digraph.Vertices];

            var order = new DepthFirstOrder(digraph.Reverse());
            foreach(var s in order.ReversePost())
            {
                if (marked[s]) 
                    continue;

                Dfs(digraph, s);
                count++;
            }
        }

        public bool StronglyConnected(int tailVertex, int headVertex)
        {
            return id[tailVertex] == id[headVertex];
        }

        public int Id(int vertex)
        {
            return id[vertex];
        }

        public int Count()
        {
            return count;
        }

        private void Dfs(Digraph digraph, int vertex)
        {
            marked[vertex] = true;
            id[vertex] = count;
            
            foreach(var w in digraph.Adjacency(vertex))
            {
                if (!marked[w])
                    Dfs(digraph, w);
            }
        }
    }
}
