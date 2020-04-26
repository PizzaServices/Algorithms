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
var adjacency = graph.Adjacency(0);

// convert the graph to string
var stringGraph = graph.ToString();

// get the number of edges for the vertex
var degree = graph.Degree(1); // = 4

// get the highest number of edges over all vertices
var maxDegree = graph.MaxDegree(); // = 4

// get the average number of edges over all vertices
var avgDegree = graph.AvgDegree(); // = 2

// get the number of self loops inside the graph
var countSelfLoops = graph.NumberOfSelfLoops(); // = 1
```

### Tips:

* If you try to access a vertex that is not present you will get a `ArgumentException`
* A self loop describes the property when a vertex has a connection on itself, e.g. `graph.AddEdge(0,0)`

--- 

## Cycle

```csharp
// create a new instance, whe use the graph from the previous example
var cycle = new Cycle(graph);

// Check if the graph contains a cycle
bool hasCycle = cycle.HasCycle(); // = true because of graph.AddEdge(2,0);
```

### Tips:

* A self loop is not a cycle

---


