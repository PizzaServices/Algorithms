namespace Algorithms.Graphs.Undirected
{
    public class ConnectedComponent
    {
        private readonly bool[] marked;
        private readonly int[] id;
        private readonly int[] size;
        private readonly int count;

        public ConnectedComponent(Graph graph)
        {
            marked = new bool[graph.Vertices];
            id = new int[graph.Vertices];
            size = new int[graph.Vertices];

            for(int index = 0; index < graph.Vertices; index++)
            {
                if (marked[index]) 
                    continue;

                Dfs(graph, index);
                count++;
            }
        }

        public bool Connected(int tailVertex, int headVertex)
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

        public int Size(int vertex)
        {
            return size[id[vertex]];
        }

        private void Dfs(Graph graph, int vertex)
        {
            marked[vertex] = true;
            id[vertex] = count;
            size[count]++;
            foreach(var headVertex in graph.Adjacency(vertex))
            {
                if (!marked[headVertex])
                    Dfs(graph, headVertex);
            }
        }
    }
}
