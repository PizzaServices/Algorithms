using NUnit.Framework;
using Algorithms.Tries;
using Algorithms.Tries.Ukkonen;

namespace PizzaAlgoTest.TrieTests
{
    [TestFixture]
    public class UkkonenTrieTest : SuffixTrieTest
    {
        protected override ITrie<int> CreateTrie()
        {
            return new UkkonenTrie<int>(0);
        }
    }
}
