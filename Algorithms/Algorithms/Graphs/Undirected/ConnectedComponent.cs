namespace Algorithms.Graphs.Undirected
{
    public class ConnectedComponent
    {
        private bool[] marked;
        private int[] id;
        private int count;

        public ConnectedComponent(Graph graph)
        {
            marked = new bool[graph.Vertices];
            id = new int[graph.Vertices];

            for(int i = 0; i < graph.Vertices; i++)
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

        public int Id(int vertex)
        {
            return id[vertex];
        }

        public int Count()
        {
            return count;
        }

        private void Dfs(Graph graph, int vertex)
        {
            marked[vertex] = true;
            id[vertex] = count;
            foreach(var w in graph.Adjacency(vertex))
            {
                if (!marked[w])
                    Dfs(graph, w);
            }
        }
    }
}
