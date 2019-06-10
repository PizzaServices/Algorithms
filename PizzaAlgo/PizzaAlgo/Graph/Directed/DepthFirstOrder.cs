using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaAlgo.Graph.Directed
{
    public class DepthFirstOrder
    {
        private bool[] marked;

        private Queue<int> pre;
        private Queue<int> post;
        private Stack<int> reversePost;

        public DepthFirstOrder(Digraph digraph)
        {
            pre = new Queue<int>();
            post = new Queue<int>();
            reversePost = new Stack<int>();
            marked = new bool[digraph.Vertices];

            for(int v = 0; v < digraph.Vertices; v++)
            {
                if (!marked[v])
                    Dfs(digraph, v);
            }
        }

        public IEnumerable<int> Pre()
        {
            return pre;
        }

        public IEnumerable<int> Post()
        {
            return post;
        }

        public IEnumerable<int> ReversePost()
        {
            return reversePost;
        }

        private void Dfs(Digraph digraph, int vertex)
        {
            pre.Enqueue(vertex);

            marked[vertex] = true;
            foreach(var w in digraph.Adjacency(vertex))
            {
                if (!marked[w])
                    Dfs(digraph, w);
            }

            post.Enqueue(vertex);
            reversePost.Push(vertex);
        }
    }
}
