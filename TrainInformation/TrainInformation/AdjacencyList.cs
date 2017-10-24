using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainInformation
{
    internal class AdjacencyList
    {
        private readonly int MAX_NUMBER_OF_VERTICES;
        private Dictionary<char, List<DirectedEdge>> edgesByStartVertex;

        public AdjacencyList() : this(0) { }
        public AdjacencyList(int maxNumberOfVertices)
        {
            MAX_NUMBER_OF_VERTICES = maxNumberOfVertices;
            edgesByStartVertex = new Dictionary<char, List<DirectedEdge>>(MAX_NUMBER_OF_VERTICES
                , new CaseInsensitiveCharComparer());//Dictionary is case-insensitive
        }

        public List<char> GetNeighborsOf(char vertex)
        {
            List<DirectedEdge> edgesFromVertex;
            if (!edgesByStartVertex.TryGetValue(vertex, out edgesFromVertex))
            {
                return new List<char>();  
            }

            List<char> neighbors = new List<char>();
            foreach (var edge in edgesFromVertex)
            {
                neighbors.Add(edge.EndVertex);
            }
            return neighbors;
        }

        public void AddDirectedEdge(char startVertex, char endVertex, int weight)
        {
            AddDirectedEdge(new DirectedEdge(startVertex, endVertex, weight));
        }

        public void AddDirectedEdge(DirectedEdge edge)
        {
            List<DirectedEdge> edgesFromStartVertex;
            if (!edgesByStartVertex.TryGetValue(edge.StartVertex, out edgesFromStartVertex))
            {
                edgesFromStartVertex = new List<DirectedEdge>(MAX_NUMBER_OF_VERTICES);
                edgesByStartVertex.Add(edge.StartVertex, edgesFromStartVertex);
            }

            edgesFromStartVertex.Add(edge);
        }


        public List<DirectedEdge> GetEdgesFrom(char startVertex)
        {
            List<DirectedEdge> edgesFromStartVertex;
            if (!edgesByStartVertex.TryGetValue(startVertex, out edgesFromStartVertex))
            {
                return new List<DirectedEdge>();
            }
            return edgesFromStartVertex;
        }

        public int GetWeightOf(char startVertex, char endVertex)
        {
            var edgesFromStartVertex = GetEdgesFrom(startVertex);
            foreach (var edge in edgesFromStartVertex)
            {
                if (edge.EndVertex == endVertex)
                {
                    return edge.Weight;
                }
            }

            return -1;
        }

        public List<char> GetAllVertices()
        {
            return edgesByStartVertex.Keys.ToList();
        }
    }
}