using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;

namespace TrainInformation
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
                if (currentPath.Distance > route_distance[currentTown] 
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
            var stopCountFromSource = new Queue<PathFromSource>();
            var tripCount = 0;
            
            stopCountFromSource.Enqueue(new PathFromSource(startTown, 0));

            while (stopCountFromSource.Count != 0)
            {
                var currentPath = stopCountFromSource.Dequeue();
                var currentTown = currentPath.Stop;
                var currentStopCount = currentPath.Distance;

                if (currentTown == endTown && currentStopCount > 0)
                {
                    tripCount++;
                }

                if (currentStopCount == maxStops)
                {
                    continue;
                }

                foreach (var neighbor in GetNeighborsOf(currentTown))
                {
                    stopCountFromSource.Enqueue(new PathFromSource(neighbor, currentStopCount + 1));
                }
            }
            return tripCount;
        }

        public int GetNumberOfTripsWithExactStops(char startTown, char endTown, int exactStops)
        {
            var queue = new Queue<char>();
            var stopCount = new List<KeyValuePair<char, int>>();
            var tripCount = 0;

            queue.Enqueue(startTown);
            stopCount.Add(new KeyValuePair<char, int>(startTown, 0));

            while (queue.Count != 0)
            {
                var currentTown = queue.Dequeue();
                var currentStopCountPair = stopCount.Find((pair) => pair.Key == currentTown);
                stopCount.Remove(currentStopCountPair);
                var currentStopCount = currentStopCountPair.Value;

                if (currentTown == endTown && currentStopCount == exactStops)
                {
                    tripCount++;
                }

                if (currentStopCount == exactStops)
                {
                    continue;
                }

                foreach (var neighbor in GetNeighborsOf(currentTown))
                {
                    queue.Enqueue(neighbor);
                    stopCount.Add(new KeyValuePair<char, int>(neighbor, currentStopCount + 1));
                }
            }
            return tripCount;
        }

        public int GetNumberOfTripsWithMaxDistance(char startTown, char endTown, int maxDistance)
        {
            var queue = new Queue<char>();
            var totalDistance = new List<KeyValuePair<char, int>>();
            var tripCount = 0;

            queue.Enqueue(startTown);
            totalDistance.Add(new KeyValuePair<char, int>(startTown, 0));

            while (queue.Count != 0)
            {
                var currentTown = queue.Dequeue();

                var currentTotalDistancePair = totalDistance.Find((pair) => pair.Key == currentTown);
                totalDistance.Remove(currentTotalDistancePair);
                var currentTotalDistance = currentTotalDistancePair.Value;

                if (currentTown == endTown && currentTotalDistance > 0 && currentTotalDistance < maxDistance)
                {
                    tripCount++;
                }

                if (currentTotalDistance >= maxDistance)
                {
                    continue;
                }

                foreach (var neighbor in GetNeighborsOf(currentTown))
                {
                    var currentDistance = GetDistanceOf(currentTown, neighbor);
                    queue.Enqueue(neighbor);
                    totalDistance.Add(new KeyValuePair<char, int>(neighbor, currentTotalDistance + currentDistance));
                }
            }
            return tripCount;
        }
    }

    internal class StopCountFromSource : IComparable<StopCountFromSource>
    {
        public char Stop { get; set; }
        public int StopCount { get; set; }
        public int CompareTo(StopCountFromSource other)
        {
            return StopCount - other.StopCount;
        }
    }

    internal class PathFromSource : IComparable<PathFromSource>
    {
        public PathFromSource(char stop, int distance)
        {
            Stop = stop;
            Distance = distance;
        }

        public char Stop { get; set; }
        public int Distance { get; set; }
        public int CompareTo(PathFromSource other)
        {
            return Stop - other.Stop;
        }
    }
}