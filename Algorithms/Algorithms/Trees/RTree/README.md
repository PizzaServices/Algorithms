# R-Tree

## I copyed the code from this repo [RBush](https://github.com/viceroypenguin/RBush)!

RBush is a high-performance .NET library for 2D **spatial indexing** of points and rectangles.
It's based on an optimized **R-tree** data structure with **bulk insertion** support.

*Spatial index* is a special data structure for points and rectangles
that allows you to perform queries like "all items within this bounding box" very efficiently
(e.g. hundreds of times faster than looping over all items).
It's most commonly used in maps and data visualizations.

## Usage

### Creating a Tree

First, define the data item class to implement `ISpatialData`, which requires that
the class expose the `Envelope` property.  Then the class can be used as such:

```csharp
var tree = new RBush<Point>()
```

An optional argument (`maxEntries: `) to the constructor defines the maximum number 
of entries in a tree node. `9` (used by default) is a reasonable choice for most 
applications. Higher value means faster insertion and slower search, and vice versa.

```csharp
var tree = new RBush<Point>(maxEntries: 16)
```

### Adding Data

Insert an item:

```csharp
var item = new Point
{
    Envelope = new Envelope
    (
        minX = 0,
        minY = 0,
        maxX = 0,
        maxY = 0,
    ),
};
tree.Insert(item);
```

### Removing Data

Remove a previously inserted item:

```csharp
tree.Delete(item);
```

By default, RBush uses `object.Equals()` to select the item. If the item being 
passed in is not the same reference value, ensure that the class supports 
`object.Equals()` equality testing.

Remove all items:

```csharp
tree.Clear();
```

### Bulk-Inserting Data

Bulk-insert the given data into the tree:

```csharp
var points = new List<Point>();
tree.BulkLoad(points);
```

Bulk insertion is usually ~2-3 times faster than inserting items one by one.
After bulk loading (bulk insertion into an empty tree),
subsequent query performance is also ~20-30% better.

Note that when you do bulk insertion into an existing tree,
it bulk-loads the given data into a separate tree
and inserts the smaller tree into the larger tree.
This means that bulk insertion works very well for clustered data
(where items in one update are close to each other),
but makes query performance worse if the data is scattered.

### Search

```csharp
var result = tree.Search(
    new Envelope
    (
        minX: 40,
        minY: 20,
        maxX: 80,
        maxY: 70
    );
```

Returns an `IEnumerable<T>` of data items (points or rectangles) that the given bounding box intersects.

```csharp
var allItems = tree.Search();
```

Returns all items of the tree.

