using System.Collections.Generic;

namespace Algorithms.Graphs.Undirected
{
    public class SymbolGraph
    {
        private Dictionary<string, int> st;
        private string[] keys;
        private Graph graph;

        public SymbolGraph(IEnumerable<string> strings, string separator)
        {
            st = new Dictionary<string, int>();
            foreach (var str in strings)
            {
                var strParts = str.Split(separator);
                for (int i = 0; i < strParts.Length; i++)
                {
                    if (!st.ContainsKey(strParts[i]))
                        st.Add(strParts[i], st.Count);
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

        public bool Contains(string s)
        {
            return st.ContainsKey(s);
        }

        public int Index(string s)
        {
            if (st.ContainsKey(s))
                return st[s];
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
