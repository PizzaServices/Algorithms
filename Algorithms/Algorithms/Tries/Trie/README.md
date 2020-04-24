# Tries

## Trie

```csharp
// create instance
var trie = new Trie<int>();

// add values
trie.Add("hello", 1);
trie.Add("world", 2);
trie.Add("hell", 3);

// prefix search
var result = trie.Retrieve("hel");
```

### Tips:

* The Trie implementation can be used for prefix searches like `.Where(s => s.StartsWith(searchString))`
* This implementation is a little bit slower than the `PatriciaTrie` in search performance

## SuffixTrie

```csharp
// create instance
var trie = new SuffixTrie<int>();

// add values
trie.Add("hello", 1);
trie.Add("world", 2);
trie.Add("hell", 3);

// suffix search
var result = trie.Retrieve("el");
```

### Tips:

* Basic trie for suffix search like `.Where(s => s.Contains(searchString))`

## ConcurrentTrie

```csharp
// create instance
var trie = new ConcurrentTrie<int>();

// add values from thread one
trie.Add("hello", 1);
trie.Add("world", 2);
trie.Add("hell", 3);

// add values from thread two
trie.Add("or", 4);
trie.Add("maybe", 5);
trie.Add("not", 6);

// prefix search
var result = trie.Retrieve("may");
```

### Tips:

* This trie is thread safe
* You can use this trie for prefix search