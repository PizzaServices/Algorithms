namespace PizzaAlgo.Graphs.Undirected
{
    public class Cycle
    {
        private bool[] marked;
        private bool hasCycle;

        public Cycle(Graph graph)
        {
            marked = new bool[graph.Vertices];
            for(int i = 0; i < graph.Vertices; i++)
            {
                if (!marked[i])
                    Dfs(graph, i, i);
            }
        }

        public bool HasCycle()
        {
            return hasCycle;
        }

        private void Dfs(Graph graph, int v, int u)
        {
            marked[v] = true;
            foreach(var w in graph.Adjacency(v))
            {
                if (!marked[w])
                    Dfs(graph, w, v);
                else if (w != u)
                    hasCycle = true;
            }
        }
    }
}
