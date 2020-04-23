namespace Algorithms.Graphs.Undirected
{
    public class Cycle
    {
        private readonly bool[] marked;
        private bool hasCycle;

        public Cycle(Graph graph)
        {
            marked = new bool[graph.Vertices];
            for(int vertex = 0; vertex < graph.Vertices; vertex++)
            {
                if (!marked[vertex])
                    Dfs(graph, vertex, vertex);
            }
        }

        public bool HasCycle()
        {
            return hasCycle;
        }

        private void Dfs(Graph graph, int tailVertex, int headVertex)
        {
            marked[tailVertex] = true;
            foreach(var w in graph.Adjacency(tailVertex))
            {
                if (!marked[w])
                    Dfs(graph, w, tailVertex);
                else if (w != headVertex)
                    hasCycle = true;
            }
        }
    }
}
