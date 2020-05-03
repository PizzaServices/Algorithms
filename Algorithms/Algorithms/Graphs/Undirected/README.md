# Undirected Graphs

This namespace contains undirected unweighted graphs and corresponding algorithms. These are explained in detail below.

## Graph

```csharp
// create a new instance with 5 vertices.
var graph = new Graph(5);

// add edges
graph.AddEdge(0,1);
graph.AddEdge(1,2);
graph.AddEdge(2,0);
graph.AddEdge(3,4);
graph.AddEdge(0,0);

// get the adjacency for the vertex
IEnumerable<int> adjacency = graph.Adjacency(0);

// convert the graph to string
string stringGraph = graph.ToString();

// get the number of edges for the vertex
int degree = graph.Degree(1); // = 4

// get the highest number of edges over all vertices
int maxDegree = graph.MaxDegree(); // = 4

// get the average number of edges over all vertices
double avgDegree = graph.AvgDegree(); // = 2

// get the number of self loops inside the graph
int countSelfLoops = graph.NumberOfSelfLoops(); // = 1
```

### Tips:

* If you try to access a vertex that is not present you will get a `ArgumentException`
* A self loop describes the property when a vertex has a connection on itself, e.g. `graph.AddEdge(0,0)`

--- 

## Cycle

```csharp
// create a new instance, whe use the graph from the previous example
var cycle = new Cycle(graph);

// check if the graph contains a cycle
bool hasCycle = cycle.HasCycle(); // = true because of graph.AddEdge(2,0);
```

### Tips:

* A self loop is not a cycle

---

## Connected components

```csharp
// create a new instance, whe use the graph from the previous example
var cc = new ConnectedComponent(graph);

// check if two vertices are connected
bool areConnected = cc.Connected(2,0);
bool areNotConnected = cc.Connected(4,1);

// get the count of components
int count = cc.Count(); // 2

// get the size of the component which contains the vertex
int sizeOfComponent = cc.Size(1); // 3

// get the id of the component which contains the vertex
int id = cc.Id(4); // 1
```
### Tips:
* The id is zero based
* The `Count()` of components is made up of the number of unconnected sub graphs.
* The `Size(int vertex)` method gives you the count of vertices in the particular component

--- 

## Breadth first search

```csharp
// create a new instance, whe use the graph from the previous example
var bfs = new BreadthFirstPaths(graph, 0);

// check if the the start vertex has a path to the given vertex
bool hasPathTo = bfs.HasPathTo(2); // true
hasPathTo = bfs.HasPathTo(3); // false

// get the path from the start vertex to the given vertex
IEnumerable<int> path = bfs.PathTo(2);
```

### Tips:

* The implementation is not using recursion

---

### Depth first search

```csharp
// create a new instance, whe use the graph from the previous example
var dfs = new DepthFirstPaths(graph, 0);

// check if the the start vertex has a path to the given vertex
bool hasPathTo = dfs.HasPathTo(2); // true
hasPathTo = dfs.HasPathTo(3); // false

// get the path from the start vertex to the given vertex
IEnumerable<int> path = dfs.PathTo(2);
```
### Tips:

* The implementation is using recursion