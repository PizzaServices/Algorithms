# Trees

## Binary Search Tree

### Usage:

```csharp
var tree = new BinarySearchTree<int, int>();

// add some key-value-pairs
tree.Insert(5, 5);
tree.Insert(3, 3);
tree.Insert(7, 7);

// search for a key
int value = tree.Get(3);

// delete a key
tree.Remove(3);

// delete the smallest key
tree.RemoveMin();

// delete the greates key
tree.RemoveMax();

// get all keys from the tree
IEnumerable<int> keys = tree.Keys();
// get all keys in the range (low and high are included)
IEnumerable<int> keys = tree.Keys(1,3);

// get floor and ceil
int floor = tree.Floor(3);
int ceil = tree.Ceiling(3);

// get the rank of a key in the tree
int rank = tree.Rank(7);

// get the key on rank 
int key = tree.Select(2);
```

### Tips:

* `null` cannot be used as a key.
* If a key-value pair is added and the key already exists, the previous value is replaced by the new value
* If a key does not exist, the default value of the value is always returned. With integers, for example, this means that the value `0` is returned
* The tree is designed in such a way that it does not trigger any exceptions