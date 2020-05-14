# Edge weighted graphs

This namespace contains undirected weighted graphs and corresponding algorithms. These are explained in detail below.

## Edge

```csharp
// create a new edge 
var edge = new Edge(0, 1, 5.0d);

// get either endpoint of the edge
int either = edge.Either();

// get the endpoint of this edge that is different from the given vertex
int other = edge.Other(1);

// compare the weight of two edges. 
// -1 if weight of the other edge is higher
// 0 if the weight of the two edges is equal
// 1 if the weight of the other edge is lower
var other = new Edge(1, 2, 2.0d);
int compare = edge.CompareTo(other); // = 1
```

---

## Graph

```csharp
// create a new instance
var graph = new EdgeWeightedGraph(3);

// add edges
var edgeOne = new Edge(0, 1, 5.0d);
var edgeTwo = new Edge(1, 2, 2.0d);
graph.AddEdge(edgeOne);
graph.AddEdge(edgeTwo);

// get the adjacency for the vertex
IEnumerable<Edge> adjacency = graph.Adjacency(0);

// get all edges
IEnumerable<Edge> edges = graph.GetEdges();
```

---

## KruskalMST

```csharp
// create graph instance
var graph = new EdgeWeightedGraph(8);
graph.AddEdge(new Edge(4, 5, 0.35d));
graph.AddEdge(new Edge(4, 7, 0.37d));
graph.AddEdge(new Edge(5, 7, 0.28d));
graph.AddEdge(new Edge(0, 7, 0.16d));
graph.AddEdge(new Edge(1, 5, 0.32d));
graph.AddEdge(new Edge(0, 4, 0.38d));
graph.AddEdge(new Edge(2, 3, 0.17d));
graph.AddEdge(new Edge(1, 7, 0.19d));
graph.AddEdge(new Edge(0, 2, 0.26d));
graph.AddEdge(new Edge(1, 2, 0.36d));
graph.AddEdge(new Edge(1, 3, 0.29d));
graph.AddEdge(new Edge(2, 7, 0.34d));
graph.AddEdge(new Edge(6, 2, 0.40d));
graph.AddEdge(new Edge(3, 6, 0.52d));
graph.AddEdge(new Edge(6, 0, 0.58d));
graph.AddEdge(new Edge(6, 4, 0.93d));

// create new instance of the Kruskal algorithm
var kruskal = new KruskalMST(graph);

// get the edges of the minimum spanning tree (or forest)
var edges = kruskal.Edges();
// 0-7 0.16000
// 2-3 0.17000
// 1-7 0.19000
// 0-2 0.26000
// 5-7 0.28000
// 4-5 0.35000
// 6-2 0.40000
```

---

## (Lazy) Prim

```csharp
// for this example we use the same graph as in the example above. The result will surprisingly be the same.
// create a instance of the Prim algorithm
var prim = new PrimMst(graph);
// create a instance of a lazy version of the Prim algorithm
var lazy = new LazyPrimMst(graph);

// get edges
var edgesPrim = prim.Edges();
var edgesLazy = lazy.Edges();
```