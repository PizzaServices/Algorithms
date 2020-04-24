# PatriciaTrie

## Prefix

```csharp
// create instance
var trie = new PatriciaTrie<int>();

// add values
trie.Add("hello", 1);
trie.Add("world", 2);
trie.Add("hell", 3);

// prefix search
var result = trie.Retrieve("hel");
```

### Tips:

* The Trie implementation can be used for prefix searches like `.Where(s => s.StartsWith(searchString))`
* This implementation is a little bit faster in look-up but a quite slower during build-up

## Suffix

```csharp
// create instance
var trie = new SuffixPatriciaTrie<int>();

// add values
trie.Add("hello", 1);
trie.Add("world", 2);
trie.Add("hell", 3);

// suffix search
var result = trie.Retrieve("el");
```

### Tips:

* This patricia trie can be used for suffix search like `.Where(s => s.Contains(searchString))`
* It´s also a little bit faster during look-up but a quite slower during build-up