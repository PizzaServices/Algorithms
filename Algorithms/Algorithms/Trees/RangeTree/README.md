# Range Tree

## I copyed the code from this repo [RangeTree](https://github.com/mbuchetics/RangeTree)!

## A generic interval tree

A generic implementation of a centered interval tree in C#. 

From [Wikipedia](http://en.wikipedia.org/wiki/Interval_tree):
> In computer science, an interval tree is an ordered tree data structure to hold intervals. Specifically, it allows one to efficiently find all intervals that overlap with any given interval or point. It is often used for windowing queries, for instance, to find all roads on a computerized map inside a rectangular viewport, or to find all visible elements inside a three-dimensional scene.

Based on the Java implementation found here: http://www.sanfoundry.com/java-program-implement-interval-tree/

Queries require `O(log n + m)` time, with `n` being the total number of intervals and `m` being the number of reported results. Construction requires `O(n log n)` time, and storage requires `O(n)` space.

## Simple Interface

```csharp
public interface IRangeTree<TKey, TValue> 
    : IEnumerable<RangeValuePair<TKey, TValue>>
{
    IEnumerable<TValue> Values { get; }
    int Count { get; }

    IEnumerable<TValue> Query(TKey value);
    IEnumerable<TValue> Query(TKey from, TKey to);

    void Add(TKey from, TKey to, TValue value);
    void Remove(TValue item);
    void Remove(IEnumerable<TValue> items);
    void Clear();
}
```
    
## Usage ###

```csharp
var tree = new RangeTree<int, string>()
{
    { 0, 10, "1" },
    { 20, 30, "2" },
    { 15, 17, "3" },
    { 25, 35, "4" },
};

// Alternatively, use the Add method, for example:
// tree.Add(0, 10, "1");

var results1 = tree.Query(5);     // 1 item: [0 - 10]
var results2 = tree.Query(10);    // 1 item: [0 - 10]
var results3 = tree.Query(29);    // 2 items: [20 - 30], [25 - 35]
var results4 = tree.Query(5, 15); // 2 items: [0 - 10], [15 - 17]
```
    
The solution file contains a more examples and tests, that show how to use RangeTree with other data types.
    
## Implementation Details ##

In this implementation, whenever you add or remove items from the tree, the tree goes "out of sync" internally, which means that the items are stored, but the tree-index is not updated yet. Upon the next query, the tree structure is automatically rebuild. Subsequent queries will use the cached index and be much faster. The creation of the tree-index requires `O(n log n)` time. Therefore, it is best suited for trees that do not change often or small trees, where the creation time is negligible.
