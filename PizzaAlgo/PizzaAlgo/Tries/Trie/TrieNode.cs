using System;
using System.Collections.Generic;

namespace PizzaAlgo.Tries.Trie
{
    public class TrieNode<TValue> : TrieNodeBase<TValue>
    {
        private readonly Dictionary<char, TrieNode<TValue>> mChildren;
        private readonly Queue<TValue> mValues;

        protected TrieNode()
        {
            mChildren = new Dictionary<char, TrieNode<TValue>>();
            mValues = new Queue<TValue>();
        }

        protected override int KeyLength => 1;

        protected override IEnumerable<TrieNodeBase<TValue>> Children()
        {
            return mChildren.Values;
        }

        protected override IEnumerable<TValue> Values()
        {
            return mValues;
        }

        protected override TrieNodeBase<TValue> GetOrCreateChild(char key)
        {
            if (mChildren.TryGetValue(key, out var result)) return result;
            result = new TrieNode<TValue>();
            mChildren.Add(key, result);
            return result;
        }

        protected override TrieNodeBase<TValue> GetChildOrNull(string query, int position)
        {
            if (query == null) throw new ArgumentNullException("query");
            return
                mChildren.TryGetValue(query[position], out var childNode)
                    ? childNode
                    : null;
        }

        protected override void AddValue(TValue value)
        {
            mValues.Enqueue(value);
        }
    }
}

