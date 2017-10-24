using System;
using System.Collections.Generic;
using TrainInformation.Data_Structures;
using TrainInformation.Exceptions;

namespace TrainInformation.Logic
{
    internal class Graph
    {
        private static readonly int INFINITY = 100000;
        private static readonly char DUMMY_NODE_NAME = '1';
        private readonly int MAX_NUMBER_OF_NODES;
        private readonly AdjacencyList _adjacencyList;

        public Graph() : this(24){}
        public Graph(int maxNumberOfNodes)
        {
            MAX_NUMBER_OF_NODES = maxNumberOfNodes;
            _adjacencyList = new AdjacencyList(MAX_NUMBER_OF_NODES);
        }
        public List<char> GetNeighborsOf(char node)
        {
            var neighbors = _adjacencyList.GetNeighborsOf(node);

            if (neighbors.Count == 0)
            {
                throw new GraphException(GraphExceptionType.NoNeightborExists
                    , $"Node {node} does not have neighbors");
            }
            
            return _adjacencyList.GetNeighborsOf(node);
        }

        public void AddOneWayPath(char source, char destination, int length)
        {
            _adjacencyList.AddDirectedEdge(source, destination, length);
        }

        public int GetLengthOf(char source, char destination)
        {
            var length = _adjacencyList.GetWeightOf(source, destination);
            if (length < 0)
            {
                throw new GraphException(GraphExceptionType.NoRouteExists, "NO SUCH ROUTE");
            }

            return length;
        }

        public int GetLengthOfShortestPath(char source, char destination)
        {
            CheckIfPathCanExist(source, destination);

            var allNodes = GetAllNodes();
            var pathIsALoop = source == destination;
            
            var minPathLengthFromSource = new Dictionary<char, int>(MAX_NUMBER_OF_NODES);
            var previousNodeOnShortestPathFromSource = new Dictionary<char, char>(MAX_NUMBER_OF_NODES);
            var remainingPaths = new MinPriorityQueue<PathFromSource>(MAX_NUMBER_OF_NODES);
            var unknownNode = '-';

            if (pathIsALoop)
            {
                destination = DUMMY_NODE_NAME;
                allNodes.Add(destination);
            }

            foreach (var node in allNodes)
            {
                var initialPathLength = INFINITY;
                if (node == source)
                {
                    initialPathLength = 0;
                    remainingPaths.Enqueue(new PathFromSource(node, initialPathLength));
                }
                minPathLengthFromSource.Add(node, initialPathLength);
                previousNodeOnShortestPathFromSource.Add(node, unknownNode);
            }

            while (remainingPaths.Count != 0)
            {
                var currentPath = remainingPaths.Dequeue();
                var currentNode = currentPath.Stop;
                if (currentPath.Length > minPathLengthFromSource[currentNode] 
                    || (pathIsALoop && currentNode == destination))
                {
                    continue;
                }

                var neighbors = GetNeighborsOf(currentNode);
                foreach (var neighbor in neighbors)
                {
                    var currentNeighbor = neighbor;
                    var additionalPathLength = GetLengthOf(currentNode, currentNeighbor);

                    if (pathIsALoop && currentNeighbor == source)
                    {
                        currentNeighbor = destination;
                    }

                    if (minPathLengthFromSource[currentNode] + additionalPathLength >= minPathLengthFromSource[currentNeighbor]) continue;

                    minPathLengthFromSource[currentNeighbor] = minPathLengthFromSource[currentNode] + additionalPathLength;
                    previousNodeOnShortestPathFromSource[currentNeighbor] = currentNode;
                    remainingPaths.Enqueue(new PathFromSource(currentNeighbor, minPathLengthFromSource[currentNeighbor]));
                }

            }

            var lengthOfShortestPath = minPathLengthFromSource[destination];
            CheckIfPathLengthIsValid(lengthOfShortestPath);
            return lengthOfShortestPath;
        }

        public int GetNumberOfPathsWithMaxStops(char source, char destination, int limit)
        {
            return CountPathsWith(source
                , destination
                , (currentPathLength) => (currentPathLength > 0 && currentPathLength <= limit)
                , (currentPathLength) => (currentPathLength == limit)
                , (currentStop, neighbor) => 1);
        }

        public int GetNumberOfPathsWithExactStops(char source, char destination, int limit)
        {
            return CountPathsWith(source
                , destination
                , (currentPathLength) => (currentPathLength == limit)
                , (currentPathLength) => (currentPathLength == limit)
                , (currentStop, neighbor) => 1);
        }

        public int GetNumberOfPathsWithMaxLength(char source, char destination, int limit)
        {
            return CountPathsWith(source
                , destination
                , (currentPathLength) => (currentPathLength > 0 && currentPathLength < limit)
                , (currentPathLength) => (currentPathLength >= limit)
                , GetLengthOf);
        }

        private int CountPathsWith(char source, char destination, Predicate<int> pathLengthConditionIsMet, Predicate<int> stopSearchConditionIsMet, Func<char, char, int> getPathLength)
        {
            CheckIfPathCanExist(source, destination);

            var pathsFromSource = new Queue<PathFromSource>();
            var pathCount = 0;

            pathsFromSource.Enqueue(new PathFromSource(source, 0));

            while (pathsFromSource.Count != 0)
            {
                var currentPath = pathsFromSource.Dequeue();
                var currentStop = currentPath.Stop;
                var currentLength = currentPath.Length;

                if (currentStop == destination && pathLengthConditionIsMet(currentLength))
                {
                    pathCount++;
                }

                if (stopSearchConditionIsMet(currentLength))
                {
                    continue;
                }

                var neighbors = GetNeighborsOf(currentStop);
                foreach (var neighbor in neighbors)
                {
                    var additionalLength = getPathLength(currentStop, neighbor);
                    pathsFromSource.Enqueue(new PathFromSource(neighbor, currentLength + additionalLength));
                }
            }
            return pathCount;
        }

        private void CheckIfPathCanExist(char source, char destination)
        {
            var allNodes = GetAllNodes();
            if (!allNodes.Contains(source) || !allNodes.Contains(destination))
            {
                throw new GraphException(GraphExceptionType.NoRouteExists, "NO SUCH ROUTE");
            }
        }

        private void CheckIfPathLengthIsValid(int pathLength)
        {
            if (pathLength == INFINITY)
            {
                throw new GraphException(GraphExceptionType.NoRouteExists, "NO SUCH ROUTE");
            }
        }

        public List<char> GetAllNodes()
        {
            return _adjacencyList.GetAllVertices();
        }
    }
}