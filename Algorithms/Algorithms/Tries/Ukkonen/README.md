# Ukkonen´s algorithm

## Trie

```csharp
// create instance
var trie = new UkkonenTrie<int>();

// add values
trie.Add("hello", 1);
trie.Add("world", 2);
trie.Add("hell", 3);

// suffix search
var result = trie.Retrieve("el");
```

### Tips:

* The Trie implementation can be used for suffix searches like `.Where(s => s.Contains(searchString))`
* This implementation should be the fastes implementation for suffix search in my library.