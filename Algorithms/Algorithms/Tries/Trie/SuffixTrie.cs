using System.Collections.Generic;
using System.Linq;
using Algorithms.Tries.PatriciaTrie;

namespace Algorithms.Tries.Trie
{
    public class SuffixTrie<TValue> : ITrie<TValue>
    {
        private readonly Trie<TValue> mInnerTrie;
        private readonly int mMinSuffixLength;

        public SuffixTrie(int minSuffixLength)
            : this(new Trie<TValue>(), minSuffixLength)
        {
        }

        private SuffixTrie(Trie<TValue> innerTrie, int minSuffixLength)
        {
            mInnerTrie = innerTrie;
            mMinSuffixLength = minSuffixLength;
        }

        public IEnumerable<TValue> Retrieve(string query)
        {
            return
                mInnerTrie
                    .Retrieve(query)
                    .Distinct();
        }

        public void Add(string key, TValue value)
        {
            foreach (var suffix in GetAllSuffixes(mMinSuffixLength, key))
            {
                mInnerTrie.Add(suffix, value);
            }
        }

        private static IEnumerable<string> GetAllSuffixes(int minSuffixLength, string word)
        {
            for (var i = word.Length - minSuffixLength; i >= 0; i--)
            {
                var partition = new StringPartition(word, i);
                yield return partition.ToString();
            }
        }
    }
}
