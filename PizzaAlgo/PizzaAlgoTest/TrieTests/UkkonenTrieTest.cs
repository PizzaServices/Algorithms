using NUnit.Framework;
using PizzaAlgo.Tries;
using PizzaAlgo.Tries.Ukkonen;

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
