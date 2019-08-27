namespace PizzaAlgo.Graphs.Undirected
{
    public class TwoColor
    {
        private bool[] marked;
        private bool[] color;
        private bool isTwoColorable = true;

        public TwoColor(Graph graph)
        {
            marked = new bool[graph.Vertices];
            color = new bool[graph.Vertices];

            for(int i = 0; i < graph.Vertices; i++)
            {
                if (!marked[i])
                    Dfs(graph, i);
            }
        }

        public bool IsBipartite()
        {
            return isTwoColorable;
        }

        private void Dfs(Graph graph, int vertex)
        {
            marked[vertex] = true;
            foreach (var w in graph.Adjacency(vertex))
            {
                if (!marked[w])
                {
                    color[w] = !color[vertex];
                    Dfs(graph, w);
                }
                else if (color[w] == color[vertex])
                    isTwoColorable = false;
            }
        }
    }
}
