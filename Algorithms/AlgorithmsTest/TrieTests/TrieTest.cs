using System;
using System.Linq;
using NUnit.Framework;
using Algorithms.Tries;
using Algorithms.Tries.Trie;

namespace AlgorithmsTest.TrieTests
{
    public class TrieTest : BaseTrieTest
    {
        protected override ITrie<int> CreateTrie()
        {
            return new Trie<int>();
        }

        [Test]
        [Explicit]
        public void ExhaustiveParallelAddFails()
        {
            Assert.Throws<AggregateException>(() => ExhaustiveParallelAddThrowAggregateException());
        }

        private void ExhaustiveParallelAddThrowAggregateException()
        {
            while (true)
            {
                ITrie<int> trie = CreateTrie();
                LongPhrases40
                    .AsParallel()
                    .ForAll(phrase => trie.Add(phrase, phrase.GetHashCode()));
            }
        }
    }
}
