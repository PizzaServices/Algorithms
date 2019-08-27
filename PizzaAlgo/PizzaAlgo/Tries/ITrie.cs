using System.Collections.Generic;

namespace PizzaAlgo.Tries
{
    public interface ITrie<TValue>
    {
        IEnumerable<TValue> Retrieve(string query);
        void Add(string key, TValue value);
    }
}
