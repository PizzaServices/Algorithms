using System.Collections.Generic;

namespace Algorithms.Graphs.Undirected
{
    public class SymbolGraph
    {
        private readonly Dictionary<string, int> st;
        private readonly string[] keys;
        private readonly Graph graph;

        public SymbolGraph(IEnumerable<string> strings, string separator)
        {
            st = new Dictionary<string, int>();
            foreach (var str in strings)
            {
                var strParts = str.Split(separator);
                foreach (var part in strParts)
                {
                    if (!st.ContainsKey(part))
                        st.Add(part, st.Count);
                }
            }

            keys = new string[st.Count];
            foreach (var name in st.Keys)
            {
                keys[st[name]] = name;
            }

            graph = new Graph(st.Count);

            foreach (var str in strings)
            {
                var strParts = str.Split(separator);
                int vertex = st[strParts[0]];
                for (int i = 1; i < strParts.Length; i++)
                {
                    graph.AddEdge(vertex, st[strParts[i]]);
                }
            }
        }

        public bool Contains(string str)
        {
            return st.ContainsKey(str);
        }

        public int Index(string str)
        {
            if (st.ContainsKey(str))
                return st[str];
            else
                return -1;
        }

        public string Name(int vertex)
        {
            return keys[vertex];
        }

        public Graph GetGraph()
        {
            return graph;
        }
    }
}
