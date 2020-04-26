using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algorithms.Containers;

namespace Algorithms.Graphs.Undirected
{
    /// <summary>
    /// Represent a undirected unweighted Graph.
    /// </summary>
    public class Graph
    {
        public int Vertices { get; } 
        public int Edges { get; private set; }

        private readonly Bag<int>[] adjacencyList;

        /// <summary>
        /// Create a new instance with the give number of vertices
        /// </summary>
        /// <param name="numberOfVertices">Number of vertices of the graph</param>
        public Graph(int numberOfVertices)
        {
            Vertices = numberOfVertices;
            Edges = 0;
            adjacencyList = new Bag<int>[numberOfVertices];
            for(int index = 0; index < numberOfVertices; index++)
            {
                adjacencyList[index] = new Bag<int>();
            }
        }

        /// <summary>
        /// Adds a new edge between the two vertices. Since this is a undirected graph the connection is made for both sides.
        /// </summary>
        /// <param name="tailVertex">The first vertex</param>
        /// <param name="headVertex">The second vertex</param>
        public void AddEdge(int tailVertex, int headVertex)
        {
            adjacencyList[tailVertex].Add(headVertex);
            adjacencyList[headVertex].Add(tailVertex);
            Edges++;
        }

        /// <summary>
        /// Get the adjacency for the given vertex
        /// </summary>
        /// <param name="vertex">For this vertex you will get the adjacency</param>
        /// <returns>A list of edges</returns>
        public IEnumerable<int> Adjacency(int vertex)
        {
            return adjacencyList[vertex];
        }

        /// <summary>
        /// Converts the graph in a string
        /// </summary>
        /// <returns>The graph represented as a string.</returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(Vertices + " vertices, " + Edges + " edges \n");
            for (int tailVertex = 0; tailVertex < Vertices; tailVertex++)
            {
                stringBuilder.Append(tailVertex + ": ");
                foreach (var headVertex in adjacencyList[tailVertex])
                {
                    stringBuilder.Append(headVertex + " ");
                }
                stringBuilder.Append("\n");
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Get the number of edges for the vertex
        /// </summary>
        /// <param name="vertex">You will get the number of edges for this vertex</param>
        /// <returns>The number of vertexes for the given vertex</returns>
        public int Degree(int vertex)
        {
            ValidateVertex(vertex);
            return adjacencyList[vertex].Size();
        }

        /// <summary>
        /// Returns the highest number of edges over all vertices.
        /// </summary>
        /// <returns>The highest number of edges</returns>
        public int MaxDegree()
        {
            int max = 0;
            for(int i = 0; i < Vertices; i++)
            {
                if (Degree(i) > max)
                    max = Degree(i);
            }

            return max;
        }

        /// <summary>
        /// Returns the average number of edges over all vertices.
        /// </summary>
        /// <returns>The average number of edges</returns>
        public double AvgDegree()
        {
            return 2.0d * Edges / Vertices;
        }

        /// <summary>
        /// Returns the number of self loops in the graph
        /// </summary>
        /// <returns>The count of self loops</returns>
        public int NumberOfSelfLoops()
        {
            int count = 0;
            for(int i = 0; i < Vertices; i++)
            {
                count += Adjacency(i).Count(edge => i == edge);
            }

            return count / 2;
        }

        private void ValidateVertex(int vertex)
        {
            if (vertex < 0 || vertex > Vertices)
                throw new ArgumentException();
        }
    }
}
