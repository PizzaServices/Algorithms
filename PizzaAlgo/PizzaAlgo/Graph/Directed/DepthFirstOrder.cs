using System.Collections.Generic;
using PizzaAlgo.Graph.EdgeWeightedDirected;

namespace PizzaAlgo.Graph.Directed
{
    public class DepthFirstOrder
    {
        private bool[] marked;         
        private int[] pre;                
        private int[] post;                
        private Queue<int> preorder;   
        private Queue<int> postorder;  
        private int preCounter;            
        private int postCounter;           

        public DepthFirstOrder(Digraph graph)
        {
            pre = new int[graph.Vertices];
            post = new int[graph.Vertices];
            postorder = new Queue<int>();
            preorder = new Queue<int>();
            marked = new bool[graph.Vertices];
            for (int v = 0; v < graph.Vertices; v++)
                if (!marked[v]) Dfs(graph, v);
        }

        public DepthFirstOrder(EdgeWeightedDigraph graph)
        {
            pre = new int[graph.Vertices];
            post = new int[graph.Vertices];
            postorder = new Queue<int>();
            preorder = new Queue<int>();
            marked = new bool[graph.Vertices];
            for (int v = 0; v < graph.Vertices; v++)
                if (!marked[v]) Dfs(graph, v);
        }

        private void Dfs(Digraph graph, int vertex)
        {
            marked[vertex] = true;
            pre[vertex] = preCounter++;
            preorder.Enqueue(vertex);
            foreach (var w in graph.Adjacency(vertex))
            {
                if (!marked[w])
                {
                    Dfs(graph, w);
                }
            }
            postorder.Enqueue(vertex);
            post[vertex] = postCounter++;
        }

        private void Dfs(EdgeWeightedDigraph graph, int vertex)
        {
            marked[vertex] = true;
            pre[vertex] = preCounter++;
            preorder.Enqueue(vertex);
            foreach (var edge in graph.Adj(vertex))
            {
                int w = edge.DestinationVertex;
                if (!marked[w])
                {
                    Dfs(graph, w);
                }
            }
            postorder.Enqueue(vertex);
            post[vertex] = postCounter++;
        }

        public int Pre(int vertex)
        {
            return pre[vertex];
        }

        public int Post(int vertex)
        {
            return post[vertex];
        }


        public IEnumerable<int> Post()
        {
            return postorder;
        }

        public IEnumerable<int> Pre()
        {
            return preorder;
        }

        public IEnumerable<int> ReversePost()
        {
            Stack<int> reverse = new Stack<int>();
            foreach (int v in postorder)
                reverse.Push(v);
            return reverse;
        }

        private bool Check()
        {
            int r = 0;
            foreach (int v in Post())
            {
                if (Post(v) != r)
                {
                    return false;
                }
                r++;
            }

            r = 0;
            foreach (int v in Pre())
            {
                if (Pre(v) != r)
                {
                    return false;
                }
                r++;
            }

            return true;
        }
    }
}
