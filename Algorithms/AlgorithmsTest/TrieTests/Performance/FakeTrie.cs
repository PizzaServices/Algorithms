using System.Collections.Generic;
using Algorithms.Tries;

namespace PizzaAlgoTest.TrieTests.Performance
{
    public class FakeTrie<T> : ITrie<T>
    {
        private readonly Stack<KeyValuePair<string, T>> mStack;

        public FakeTrie()
        {
            mStack = new Stack<KeyValuePair<string, T>>();
        }

        public IEnumerable<T> Retrieve(string query)
        {
            foreach (var keyValuePair in mStack)
            {
                string key = keyValuePair.Key;
                T value = keyValuePair.Value;
                if (key.Contains(query)) yield return value;
            }
        }

        public void Add(string key, T value)
        {
            var keyValPair = new KeyValuePair<string, T>(key, value);
            mStack.Push(keyValPair);
        }
    }
}
