using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PizzaAlgo.Tries.Trie;

namespace PizzaAlgo.Tries.PatriciaTrie
{
    public class PatriciaTrieNode<TValue> : TrieNodeBase<TValue>
    {
        private Dictionary<char, PatriciaTrieNode<TValue>> mChildren;
        private StringPartition mKey;
        private Queue<TValue> mValues;

        protected PatriciaTrieNode(StringPartition key, TValue value)
            : this(key, new Queue<TValue>(new[] { value }), new Dictionary<char, PatriciaTrieNode<TValue>>())
        {
        }

        protected PatriciaTrieNode(StringPartition key, Queue<TValue> values,
            Dictionary<char, PatriciaTrieNode<TValue>> children)
        {
            mValues = values;
            mKey = key;
            mChildren = children;
        }

        protected override int KeyLength => mKey.Length;

        protected override IEnumerable<TValue> Values()
        {
            return mValues;
        }

        protected override IEnumerable<TrieNodeBase<TValue>> Children()
        {
            return mChildren.Values;
        }


        protected override void AddValue(TValue value)
        {
            mValues.Enqueue(value);
        }

        internal virtual void Add(StringPartition keyRest, TValue value)
        {
            var zipResult = mKey.ZipWith(keyRest);

            switch (zipResult.MatchKind)
            {
                case MatchKind.ExactMatch:
                    AddValue(value);
                    break;
                case MatchKind.IsContained:
                    GetOrCreateChild(zipResult.OtherRest, value);
                    break;
                case MatchKind.Contains:
                    SplitOne(zipResult, value);
                    break;
                case MatchKind.Partial:
                    SplitTwo(zipResult, value);
                    break;
            }
        }


        private void SplitOne(ZipResult zipResult, TValue value)
        {
            var leftChild = new PatriciaTrieNode<TValue>(zipResult.ThisRest, mValues, mChildren);

            mChildren = new Dictionary<char, PatriciaTrieNode<TValue>>();
            mValues = new Queue<TValue>();
            AddValue(value);
            mKey = zipResult.CommonHead;

            mChildren.Add(zipResult.ThisRest[0], leftChild);
        }

        private void SplitTwo(ZipResult zipResult, TValue value)
        {
            var leftChild = new PatriciaTrieNode<TValue>(zipResult.ThisRest, mValues, mChildren);
            var rightChild = new PatriciaTrieNode<TValue>(zipResult.OtherRest, value);

            mChildren = new Dictionary<char, PatriciaTrieNode<TValue>>();
            mValues = new Queue<TValue>();
            mKey = zipResult.CommonHead;

            char leftKey = zipResult.ThisRest[0];
            mChildren.Add(leftKey, leftChild);
            char rightKey = zipResult.OtherRest[0];
            mChildren.Add(rightKey, rightChild);
        }

        protected void GetOrCreateChild(StringPartition key, TValue value)
        {
            if (!mChildren.TryGetValue(key[0], out var child))
            {
                child = new PatriciaTrieNode<TValue>(key, value);
                mChildren.Add(key[0], child);
            }
            else
            {
                child.Add(key, value);
            }
        }

        protected override TrieNodeBase<TValue> GetOrCreateChild(char key)
        {
            throw new NotSupportedException("Use alternative signature instead.");
        }

        protected override TrieNodeBase<TValue> GetChildOrNull(string query, int position)
        {
            if (query == null) throw new ArgumentNullException("query");
            if (!mChildren.TryGetValue(query[position], out var child)) return null;
            var queryPartition = new StringPartition(query, position, child.mKey.Length);
            return child.mKey.StartsWith(queryPartition) ? child : null;
        }

        public string Traversal()
        {
            var result = new StringBuilder();
            result.Append(mKey);

            var subtreeResult = string.Join(" ; ", mChildren.Values.Select(node => node.Traversal()).ToArray());
            if (subtreeResult.Length == 0) return result.ToString();
            result.Append("[");
            result.Append(subtreeResult);
            result.Append("]");

            return result.ToString();
        }

        public override string ToString()
        {
            return $"Key: {mKey}, Values: {Values().Count()} Children:{string.Join(";", mChildren.Keys)}, ";
        }
    }
}
