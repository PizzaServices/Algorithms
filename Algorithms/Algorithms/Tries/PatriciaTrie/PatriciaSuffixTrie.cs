using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Tries.PatriciaTrie
{
    public class PatriciaSuffixTrie<TValue> : ITrie<TValue>
    {
        private readonly PatriciaTrie<TValue> mInnerTrie;

        protected int MinQueryLength { get; }

        public PatriciaSuffixTrie(int minQueryLength)
            : this(minQueryLength, new PatriciaTrie<TValue>())
        {
        }

        internal PatriciaSuffixTrie(int minQueryLength, PatriciaTrie<TValue> innerTrie)
        {
            MinQueryLength = minQueryLength;
            mInnerTrie = innerTrie;
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
            var allSuffixes = GetAllSuffixes(MinQueryLength, key);
            foreach (var currentSuffix in allSuffixes)
            {
                mInnerTrie.Add(currentSuffix, value);
            }
        }

        private static IEnumerable<StringPartition> GetAllSuffixes(int minSuffixLength, string word)
        {
            for (var i = word.Length - minSuffixLength; i >= 0; i--)
            {
                yield return new StringPartition(word, i);
            }
        }
    }
}
