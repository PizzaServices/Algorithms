using System.Collections.Generic;

namespace PizzaAlgo.Graph.Directed
{
    public class Topological
    {
        private IEnumerable<int> order;

        public Topological(Digraph digraph)
        {
            var cycleFinder = new DirectedCycle(digraph);
            if(!cycleFinder.HasCycle())
            {
                var dfs = new DepthFirstOrder(digraph);
                order = dfs.ReversePost();
            }
        }

        public IEnumerable<int> Order()
        {
            return order;
        }

        public bool IsAcyclicallyDigraph()
        {
            return order != null;
        }
    }
}
