using System.Linq;
using NUnit.Framework;
using Algorithms.Tries;
using Algorithms.Tries.PatriciaTrie;

namespace PizzaAlgoTest.TrieTests
{
    [TestFixture]
    public class PatriciaTrieTest : BaseTrieTest
    {
        protected override ITrie<int> CreateTrie()
        {
            return new PatriciaTrie<int>();
        }

        [Test]
        public void TestNotExactMatched()
        {
            ITrie<int> trie = new PatriciaTrie<int>();
            trie.Add("aaabbb", 1);
            trie.Add("aaaccc", 2);

            var actual = trie.Retrieve("aab");
            CollectionAssert.AreEquivalent(Enumerable.Empty<int>(), actual);

        }
    }
}
