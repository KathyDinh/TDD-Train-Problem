using System;
using System.Collections.Generic;

namespace TrainInformation
{
    internal class AdjacencyList
    {
        private static readonly int MAX_NUMBER_OF_VERTICES = 5; //Town names are alphabet between A-E
        private Dictionary<char, List<DirectedEdge>> adjacencyList;

        public AdjacencyList()
        {
            adjacencyList = new Dictionary<char, List<DirectedEdge>>(MAX_NUMBER_OF_VERTICES);
        }

        public List<char> GetNeighborsOf(char vertex)
        {
            List<DirectedEdge> edgesFromVertex;
            if (!adjacencyList.TryGetValue(vertex, out edgesFromVertex))
            {
                throw new RailRoadSystemException(RailRoadSystemExceptionType.NoTownExists
                    ,$"Town {vertex} does not exist");
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
            if (!adjacencyList.TryGetValue(edge.StartVertex, out edgesFromStartVertex))
            {
                edgesFromStartVertex = new List<DirectedEdge>(MAX_NUMBER_OF_VERTICES);
                adjacencyList.Add(edge.StartVertex, edgesFromStartVertex);
            }

            edgesFromStartVertex.Add(edge);
        }


        public List<DirectedEdge> GetEdgesFrom(char startVertex)
        {
            List<DirectedEdge> edgesFromStartVertex;
            if (!adjacencyList.TryGetValue(startVertex, out edgesFromStartVertex))
            {
                throw new RailRoadSystemException(RailRoadSystemExceptionType.NoEdgeExists
                    , $"There is no edges starting from {startVertex}");
            }
            return edgesFromStartVertex;
        }
    }
}