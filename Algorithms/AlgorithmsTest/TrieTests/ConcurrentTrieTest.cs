using System.Linq;
using NUnit.Framework;
using Algorithms.Tries;
using Algorithms.Tries.Trie;

namespace PizzaAlgoTest.TrieTests
{
    public class ConcurrentTrieTest : BaseTrieTest
    {
        protected override ITrie<int> CreateTrie()
        {
            return new ConcurrentTrie<int>();
        }

        [TestCase(1)]
        [TestCase(4)]
        [TestCase(13)]
        public void ExhaustiveParallelAdd(int degreeofParallelism)
        {
            ITrie<string> trie = new ConcurrentTrie<string>();
            LongPhrases40
                .AsParallel()
                .WithDegreeOfParallelism(degreeofParallelism)
                .ForAll(phrase => trie.Add(phrase, phrase));
        }
    }
}
