# Containers

## Bag

> This class represents a bag (or multiset) of generic items. It supports insertion and iterating over the items in arbitrary order.

```csharp
// create a bag
var bag = new Bag<Foo>();

// add items to the bag
bag.Add(fooOne);
bag.Add(fooTwo);
bag.Add(fooThree);

// check if the Bag is empty
bool isEmpty = bag.IsEmpty();

// get the count of the bag
int size = bag.Size();

// iterate over the bag
foreach(var item in bag)
    Console.WriteLine(item.ToString());
```

### Tips:
* The bag holds the number of elements in a property. So the `IsEmpty()` and `Size()` function is a convenient call.

---

## DisjointSet

> This class represents a disjoint-sets data type (also known as union-find data structure ). It supports the classic union and find operations, along with a count operation that returns the total number of sets.

```csharp
// create new instance of the DisjointSet with n -1 elements
var set = new DisjointSet(5);

// connect elements together
set.Union(1,3);
set.Union(1,4);
set.Union(0,2);

// returns the "parent element" of the searched element
var find = set.Find(4); // = 1

// returns the number of sets
var count = set.Count(); // =2

// check if two elements are connected
var areConnected = set.Connected(3,4) // true
var areConnected = set.Connected(2,3) // false
```

### Tips:

* The DisjointSet holds the number of sets in a property.
* If you don´t provide `count > 0`, a `ArgumentException` is thrown
* If you use values that are not in the DisjointSet-Range a 'ArgumentException' is thrown

---

## Min Priority Queue

> This class represents a priority queue of generic keys.
It supports the usual insert and delete-the-minimum
operations, along with methods for peeking at the minimum key,
testing if the priority queue is empty, and iterating through
the keys.

```csharp
// create a empty queue
var queue = new MinPQ<string>();

// create a empty queue with the initial capacity
var queue = new MinPQ<string>(10);

// create a empty queue with the given comparer
var queue = new MinPQ<string>(comparer);

// create a empty queue with the given comparer and the initial capacity
var queue = new MinPQ<string>(10, comparer);

// create a queue based on the list of keys
var queue = new MinPQ<string>(keys);

// check if the queue is empty
bool isEmpty = queue.IsEmpty();

// get the size of the queue
int size = queue.Size();

// add new keys to the queue
queue.Insert("a");
queue.Insert("b");
queue.Insert("c");

// get the smallest key
string key = queue.Min(); // = a

// get and remove the smallest key
string key = queue.DelMin(); // = a
string key = queue.DelMin(); // = b
```

### Tips:
* A empty queue throws a `Exception` if you try to get or delete the min key
* When you create a instance with a list of key it takes proportional time to the number of keys, using sink-based heap construction

---

## Index Min Heap Priority Queue

> This class represents an indexed priority queue of generic keys.
It supports the usual insert and delete-the-minimum
operations, along with delete and change-the-key
methods. In order to let the client refer to keys on the priority queue,
an integer between 0 and maxN - 1
is associated with each key — the client uses this integer to specify
which key to delete or change.
It also supports methods for peeking at the minimum key,
testing if the priority queue is empty, and iterating through
the keys.

```csharp
// create a new Instance
var queue = new IndexMinPQ<string>(5);

// check if the queue is empty
bool isEmpty = queue.IsEmpty();

// Add index-key pairs
queue.Insert(1, "one");
queue.Insert(2, "two");
queue.Insert(3, "three");

// check if the index already assigned 
bool contains = queue.Contains(3);

// get the number of keys on this priority queue
int size = queue.Size();

// get the index associated with a minimum key
int index = queue.MinIndex();

// get the minimum key
string key = queue.MinKey();

// remove min key and get the index
int index = queue.DelMin();

// get the key of index
string key = queue.KeyOf(index);

// change key on index with new key
queue.ChangeKey(2, "newKey");

// decrease key on index
queue.DecreaseKey(2, "decrease");

// increase key on index
queue.IncreaseKey(2, "increase");
```

### Tips

* The IndexMinPQ throws always a `Exception` if you try to access indexes that are not in Range or you try to call operations that overflow or underflow the queue
* Use `Contains()`, `IsEmpty()` and `Size()` for each other operation to make sure you are able to perform the operation