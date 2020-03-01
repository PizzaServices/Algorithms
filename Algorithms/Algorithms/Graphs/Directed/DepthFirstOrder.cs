using System.Collections.Generic;
using Algorithms.Graphs.EdgeWeightedDirected;

namespace Algorithms.Graphs.Directed
{
    public class DepthFirstOrder
    {
        private readonly bool[] marked;         
        private readonly int[] pre;                
        private readonly int[] post;                
        private readonly Queue<int> preOrder;   
        private readonly Queue<int> postOrder;  
        private int preCounter;            
        private int postCounter;           

        public DepthFirstOrder(Digraph graph)
        {
            pre = new int[graph.Vertices];
            post = new int[graph.Vertices];
            postOrder = new Queue<int>();
            preOrder = new Queue<int>();
            marked = new bool[graph.Vertices];
            for (int v = 0; v < graph.Vertices; v++)
                if (!marked[v]) Dfs(graph, v);
        }

        public DepthFirstOrder(EdgeWeightedDigraph graph)
        {
            pre = new int[graph.Vertices];
            post = new int[graph.Vertices];
            postOrder = new Queue<int>();
            preOrder = new Queue<int>();
            marked = new bool[graph.Vertices];
            for (int v = 0; v < graph.Vertices; v++)
                if (!marked[v]) Dfs(graph, v);
        }

        private void Dfs(Digraph graph, int vertex)
        {
            marked[vertex] = true;
            pre[vertex] = preCounter++;
            preOrder.Enqueue(vertex);
            foreach (var w in graph.Adjacency(vertex))
            {
                if (!marked[w])
                {
                    Dfs(graph, w);
                }
            }
            postOrder.Enqueue(vertex);
            post[vertex] = postCounter++;
        }

        private void Dfs(EdgeWeightedDigraph graph, int vertex)
        {
            marked[vertex] = true;
            pre[vertex] = preCounter++;
            preOrder.Enqueue(vertex);
            foreach (var edge in graph.Adj(vertex))
            {
                int w = edge.DestinationVertex;
                if (!marked[w])
                {
                    Dfs(graph, w);
                }
            }
            postOrder.Enqueue(vertex);
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
            return postOrder;
        }

        public IEnumerable<int> Pre()
        {
            return preOrder;
        }

        public IEnumerable<int> ReversePost()
        {
            var reverse = new Stack<int>();
            foreach (int v in postOrder)
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
