namespace PizzaAlgo.Graphs.Directed
{
    public class KosarajuSCC
    {
        private bool[] marked;
        private int[] id;
        private int count;

        public KosarajuSCC(Digraph digraph)
        {
            marked = new bool[digraph.Vertices];
            id = new int[digraph.Vertices];

            var order = new DepthFirstOrder(digraph.Reverse());
            foreach(var s in order.ReversePost())
            {
                if(!marked[s])
                {
                    Dfs(digraph, s);
                    count++;
                }
            }
        }

        public bool StronglyConnected(int v, int w)
        {
            return id[v] == id[w];
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
