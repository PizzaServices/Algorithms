# Directed Graphs

This namespace contains directed unweighted graphs and corresponding algorithms. These are explained in detail below.

## Digraph

```csharp
// create a new instance with 5 vertices.
var graph = new Digraph(5);

// add edges but only from tail vertex to head vertex and not vice versa!
graph.AddEdge(0,1);
graph.AddEdge(1,2);
graph.AddEdge(2,0);
graph.AddEdge(3,4);
graph.AddEdge(4,0);

// get the adjacency for the vertex
IEnumerable<int> adjacency = graph.Adjacency(0);

// get the reverse of the digraph.
Digraph reverse = graph.Reverse();

// convert the graph to string
string stringGraph = graph.ToString();
```

* If you try to access a vertex that is not present you will get a `IndexOutOfRangeException`

---

## Cycle

```csharp
// create a new instance, whe use the graph from the previous example
var cycle = new DirectedCycle(graph);

// check if the graph contains a cycle
bool hasCycle = cycle.HasCycle(); // true because of graph.AddEdge(4,0);

// get the cycle
IEnumerable<int> cycleVertices = cycle.Cycle();
```

### Tips:

* A self loop is a cycle

---

## Directed depth first search

```csharp
// create a new instance, whe use the graph from the previous example
var dfs = new DirectedDfs(graph, 0);
// you could also specify a list of start vertices
dfs = new DirectedDfs(graph, new List{0,1,2});

bool marked = dfs.Marked(4); // true
```

### Tips:

* The implementation is using recursion

---

## Depth first order

```csharp
// setup
var graph = new Digraph(13);
graph.AddEdge(1,3);
graph.AddEdge(1,5);
graph.AddEdge(2,3);
graph.AddEdge(0,6);
graph.AddEdge(0,1);
graph.AddEdge(2,0);
graph.AddEdge(11,12);
graph.AddEdge(9,12);
graph.AddEdge(9,10);
graph.AddEdge(9,11);
graph.AddEdge(3,5);
graph.AddEdge(8,7);
graph.AddEdge(5,4);
graph.AddEdge(0,5);
graph.AddEdge(6,4);
graph.AddEdge(6,9);
graph.AddEdge(7,6);

// create a new instance
var dfOrder = new DepthFirstOrder(graph);

// get the preorder number of the vertex
int pre = dfOrder.Pre(2);

// get the postorder number of the vertex
int post = dfOrder.Post(2);

// get the vertices in postorder
IEnumerable<int> postOrder = dfOrder.Post();

// get the vertices in preorder
IEnumerable<int> preOrder = dfOrder.Pre();

// get the vertices in reverse postorder
IEnumerable<int> reversePostOrder = dfOrder.ReversePost();
```

### Tips:

* Digraph: 0,1,2,3,4,5,6,7,8,9,10,11,12
* Preorder: 0,5,4,1,6,9,11,12,10,2,3,7,8
* Postorder: 4,5,1,12,11,10,9,6,0,3,2,7,8
* Reverse postorder: 8,7,2,3,0,6,9,10,11,12,1,5,4

---

## Strongly connecte components with the Kosaraju-Sharir algorithm

```csharp
// create new instance whe use the graph from the previous example
var scc = new KosarajuSharirScc(graph);

// check if vertices `tail` and `head` in the same strong component
bool connected = scc.StronglyConnected(9,12);

// get the id of the component which contains the vertex
int id = scc.Id(9);

// get the count of components
int count = scc.Count();
```

### Tips:

* The id is zero based