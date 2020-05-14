# Directed edge weighted graphs

This namespace contains directed weighted graphs and corresponding algorithms. These are explained in detail below.

## DirectedEdge

```csharp
// create new instance
var edge = new DirectedEdge(0, 1, 5.0d);
```

---

## Graph

```csharp
// create new instance
var graph = new EdgeWeightedDigraph(3);

// add edges
graph.AddEdge(new DirectedEdge(0, 1, 5.0d));
graph.AddEdge(new DirectedEdge(1, 2, 3.0d));

// get the adjacency for the vertex
IEnumerable<DirectedEdge> adjacency = graph.Adjacency(0);

// get all edges
IEnumerable<DirectedEdge> edges = graph.Edges();
```

---

## EdgeWeightedDirectedCycle

```csharp
// we will use the graph from the example above.
graph.AddEdge(new DirectedEdge(2,0, 6.0d));
// create new instance 
var cycle = EdgeWeightedDirectedCycle(graph);

bool hasCycle = cycle.HasCycle(); // true;
```

### Tips:

* The implementation uses a recursive dfs implementation

---

## Shortest path

```csharp
var graph = new EdgeWeightedDigraph(8);

graph.AddEdge(new DirectedEdge(5, 4, 0.35));
graph.AddEdge(new DirectedEdge(4, 7, 0.37));
graph.AddEdge(new DirectedEdge(5, 7, 0.28));
graph.AddEdge(new DirectedEdge(5, 1, 0.32));
graph.AddEdge(new DirectedEdge(4, 0, 0.38));
graph.AddEdge(new DirectedEdge(0, 2, 0.26));
graph.AddEdge(new DirectedEdge(3, 7, 0.39));
graph.AddEdge(new DirectedEdge(1, 3, 0.29));
graph.AddEdge(new DirectedEdge(7, 2, 0.34));
graph.AddEdge(new DirectedEdge(6, 2, 0.40));
graph.AddEdge(new DirectedEdge(3, 6, 0.52));
graph.AddEdge(new DirectedEdge(6, 0, 0.58));
graph.AddEdge(new DirectedEdge(6, 4, 0.93));

// create instances for shortes path finding
var acyclic = new AcyclicSp(graph, 5);
var bellmanFord = new BellmanFordSp(graph, 5);
var dijkstra = new DijkstraSp(graph, 5);

// check if path exist. Every of the above classes has the same methods.
// So you can exchange them
bool result = dijkstra.HasPathTo(6); // true

// get the distance to a edge 
double distance = dijkstra.GetDistanceTo(0); // 0.73

// get the path to a vertex
IEnumerable<DirectedEdge> path = dijkstra.PathTo(6);
```

### Tips:

* all results for the above example:

   * 5 to 0 (0.73)  5->4  0.35   4->0  0.38   
   *  5 to 1 (0.32)  5->1  0.32   
   *  5 to 2 (0.62)  5->7  0.28   7->2  0.34   
   *  5 to 3 (0.61)  5->1  0.32   1->3  0.29   
   *  5 to 4 (0.35)  5->4  0.35   
   *  5 to 5 (0.00)  
   *  5 to 6 (1.13)  5->1  0.32   1->3  0.29   3->6  0.52   
   *  5 to 7 (0.28)  5->7  0.28