using System.Collections.Generic;

namespace TrainInformation
{
    internal class Graph
    {
        private AdjacencyList adjacencyList;

        public Graph()
        {
            adjacencyList = new AdjacencyList();
        }
        public List<char> GetNeighborsOf(char town)
        {
            return adjacencyList.GetNeighborsOf(town);
        }

        public void AddOneWayRoute(char startTown, char endTown, int distance)
        {
            adjacencyList.AddDirectedEdge(startTown, endTown, distance);
        }
    }
}