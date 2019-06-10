namespace PizzaAlgo.Graph
{
    public class ConnectedComponent
    {
        private bool[] marked;
        private int[] id;
        private int count;

        public ConnectedComponent(Graph graph)
        {
            marked = new bool[graph.Nodes];
            id = new int[graph.Nodes];

            for(int i = 0; i < graph.Nodes; i++)
            {
                if(!marked[i])
                {
                    Dfs(graph, i);
                    count++;
                }
            }
        }

        public bool Connected(int v, int w)
        {
            return id[v] == id[w];
        }

        public int Id(int node)
        {
            return id[node];
        }

        public int Count()
        {
            return count;
        }

        private void Dfs(Graph graph, int node)
        {
            marked[node] = true;
            id[node] = count;
            foreach(var w in graph.Adjacency(node))
            {
                if (!marked[w])
                    Dfs(graph, w);
            }
        }
    }
}
