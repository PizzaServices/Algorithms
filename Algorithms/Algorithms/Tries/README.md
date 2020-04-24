# Tries

## I copyed the code from this repo [trienet](https://github.com/gmamaladze/trienet)!

## Tries

There is a family of data structures reffered as Trie. In this post I want to focus on a c# implementations and usage of Trie data structures. If you want to find out more about the theory behind the data structure itself Google will be probably your best friend. In fact most of popular books on data structures and algorithms describe tries (see.: Advanced Data Structures by Peter Brass)

## Implementation

```csharp
public interface ITrie
{
  IEnumerable Retrieve(string query);
  void Add(string key, TValue value);
}
```

Class|Description  
-----|-------------
`Trie` | the simple trie, allows only prefix search, like `.Where(s => s.StartsWith(searchString))`
`SuffixTrie` | allows also infix search, like `.Where(s => s.Contains(searchString))`
`PatriciaTrie` | compressed trie, more compact, a bit more efficient during look-up, but a quite slower durig build-up.
`SuffixPatriciaTrie` | the same as PatriciaTrie, also enabling infix search.
`ParallelTrie` | very primitively implemented parallel data structure which allows adding data and retriving results from different threads simultaneusly.
