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
        public List<char> GetNeighborsOf(char vertex)
        {
            return adjacencyList.GetNeighborsOf(vertex);
        }
    }
}