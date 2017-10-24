using System;
using System.Collections.Generic;
using TrainInformation.Data_Structures;
using TrainInformation.Exceptions;

namespace TrainInformation.Logic
{
    internal class Graph
    {
        private static readonly int INFINITY = 100000;
        private static readonly char DUMMY_TOWN_NAME = '1';
        private readonly int MAX_NUMBER_OF_TOWNS;
        private readonly AdjacencyList adjacencyList;

        public Graph(int maxNumberOfTowns)
        {
            MAX_NUMBER_OF_TOWNS = maxNumberOfTowns;
            adjacencyList = new AdjacencyList(MAX_NUMBER_OF_TOWNS);
        }
        public List<char> GetNeighborsOf(char town)
        {
            var neighbors = adjacencyList.GetNeighborsOf(town);

            if (neighbors.Count == 0)
            {
                throw new RailRoadSystemException(RailRoadSystemExceptionType.NoNeightborExists
                    , $"Town {town} does not have neighbors");
            }
            
            return adjacencyList.GetNeighborsOf(town);
        }

        public void AddOneWayRoute(char startTown, char endTown, int distance)
        {
            adjacencyList.AddDirectedEdge(startTown, endTown, distance);
        }

        public int GetDistanceOf(char startTown, char endTown)
        {
            var distance = adjacencyList.GetWeightOf(startTown, endTown);
            if (distance < 0)
            {
                throw new RailRoadSystemException(RailRoadSystemExceptionType.NoRouteExists, "NO SUCH ROUTE");
            }

            return distance;
        }

        public int GetDistanceOfShortestRoute(char startTown, char endTown)
        {
            var allTowns = GetAllTowns();
            CheckIfTownsAreValid(startTown, endTown, allTowns);

            var routeIsALoop = startTown == endTown;
            
            var route_distance = new Dictionary<char, int>(MAX_NUMBER_OF_TOWNS);
            var previous = new Dictionary<char, char>(MAX_NUMBER_OF_TOWNS);
            var remainingPaths = new MinPriorityQueue<PathFromSource>(MAX_NUMBER_OF_TOWNS);
            var unknownTown = '-';

            if (routeIsALoop)
            {
                endTown = DUMMY_TOWN_NAME;
                allTowns.Add(endTown);
            }

            foreach (var town in allTowns)
            {
                var initialDistance = INFINITY;
                if (town == startTown)
                {
                    initialDistance = 0;
                    remainingPaths.Enqueue(new PathFromSource(town, initialDistance));
                }
                route_distance.Add(town, initialDistance);
                previous.Add(town, unknownTown);
            }

            while (remainingPaths.Count != 0)
            {
                var currentPath = remainingPaths.Dequeue();
                var currentTown = currentPath.Stop;
                if (currentPath.Length > route_distance[currentTown] 
                    || (routeIsALoop && currentTown == endTown))
                {
                    continue;
                }

                var neighbors = GetNeighborsOf(currentTown);
                foreach (var neighbor in neighbors)
                {
                    var currentNeighbor = neighbor;
                    var currentDistance = GetDistanceOf(currentTown, currentNeighbor);

                    if (routeIsALoop && currentNeighbor == startTown)
                    {
                        currentNeighbor = endTown;
                    }

                    if (route_distance[currentTown] + currentDistance >= route_distance[currentNeighbor]) continue;

                    route_distance[currentNeighbor] = route_distance[currentTown] + currentDistance;
                    previous[currentNeighbor] = currentTown;
                    remainingPaths.Enqueue(new PathFromSource(currentNeighbor, route_distance[currentNeighbor]));
                }

            }

            var distanceOfShortestRoute = route_distance[endTown];
            CheckIfDistanceValueIsValid(distanceOfShortestRoute);
            return distanceOfShortestRoute;
        }

        private static void CheckIfTownsAreValid(char startTown, char endTown, List<char> allTowns)
        {
            if (!allTowns.Contains(startTown) || !allTowns.Contains(endTown))
            {
                throw new RailRoadSystemException(RailRoadSystemExceptionType.NoRouteExists, "NO SUCH ROUTE");
            }
        }

        private static void CheckIfDistanceValueIsValid(int distance)
        {
            if (distance == INFINITY)
            {
                throw new RailRoadSystemException(RailRoadSystemExceptionType.NoRouteExists, "NO SUCH ROUTE");
            }
        }
      
        public List<char> GetAllTowns()
        {
            return adjacencyList.GetAllVertices();
        }
        public int GetNumberOfTripsWithMaxStops(char startTown, char endTown, int maxStops)
        {
            return CountPathsWith(startTown
                , endTown
                , (currentPathLength) => (currentPathLength > 0 && currentPathLength <= maxStops)
                , (currentPathLength) => (currentPathLength == maxStops)
                , (currentStop, neighbor) => 1);
        }

        public int GetNumberOfTripsWithExactStops(char startTown, char endTown, int exactStops)
        {
            return CountPathsWith(startTown
                , endTown
                , (currentPathLength) => (currentPathLength == exactStops)
                , (currentPathLength) => (currentPathLength == exactStops)
                , (currentStop, neighbor) => 1);
        }

        public int GetNumberOfTripsWithMaxDistance(char startTown, char endTown, int maxDistance)
        {
            return CountPathsWith(startTown
                , endTown
                , (currentPathLength) => (currentPathLength > 0 && currentPathLength < maxDistance)
                , (currentPathLength) => (currentPathLength >= maxDistance)
                , GetDistanceOf);
        }

        private int CountPathsWith(char source, char destination, Predicate<int> pathLengthConditionIsMet, Predicate<int> stopSearchConditionIsMet, Func<char, char, int> getPathLength)
        {
            var pathsFromSource = new Queue<PathFromSource>();
            var tripCount = 0;

            pathsFromSource.Enqueue(new PathFromSource(source, 0));

            while (pathsFromSource.Count != 0)
            {
                var currentPath = pathsFromSource.Dequeue();
                var currentStop = currentPath.Stop;
                var currentLength = currentPath.Length;

                if (currentStop == destination && pathLengthConditionIsMet(currentLength))
                {
                    tripCount++;
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
            return tripCount;
        }
    }
}