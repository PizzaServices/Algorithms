using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algorithms.Containers;

namespace Algorithms.Graphs.Undirected
{
    public class Graph
    {
        public int Vertices { get; } 
        public int Edges { get; private set; }

        private readonly Bag<int>[] adjacencyList;

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

        public void AddEdge(int tailVertex, int headVertex)
        {
            adjacencyList[tailVertex].Add(headVertex);
            adjacencyList[headVertex].Add(tailVertex);
            Edges++;
        }

        public IEnumerable<int> Adjacency(int vertex)
        {
            return adjacencyList[vertex];
        }

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

        public int Degree(int vertex)
        {
            ValidateVertex(vertex);
            return adjacencyList[vertex].Size();
        }

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

        public double AvgDegree()
        {
            return 2 * Edges / Vertices;
        }

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
