using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;

namespace TrainInformation
{
    internal class Graph
    {
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
            var allTowns = GetAllTowns().ToList();
            if (!allTowns.Contains(startTown) || !allTowns.Contains(endTown))
            {
                throw new RailRoadSystemException(RailRoadSystemExceptionType.NoRouteExists, "NO SUCH ROUTE");
            }

            var route_distance = new Dictionary<char, int>(MAX_NUMBER_OF_TOWNS);
            var previous = new Dictionary<char, char>(MAX_NUMBER_OF_TOWNS);
            var remainingTowns = new List<char>(MAX_NUMBER_OF_TOWNS);
            var unknownTown = '-';

            var infinity = 100000;
            foreach (var town in allTowns)
            {
                previous.Add(town, unknownTown);
                if (town == startTown)
                {
                    remainingTowns.Insert(0, town);
                    route_distance.Add(town, 0);
                    continue;
                }   
                remainingTowns.Add(town);
                route_distance.Add(town, infinity);
            }

            while (remainingTowns.Count != 0)
            {
                var currentTown = remainingTowns[0];
                remainingTowns.RemoveAt(0);

                var neighbors = GetNeighborsOf(currentTown);
                foreach (var neighbor in neighbors)
                {
                    var currentDistance = GetDistanceOf(currentTown, neighbor);

                    if (route_distance[currentTown] + currentDistance <
                        route_distance[neighbor])
                    {
                        route_distance[neighbor] = route_distance[currentTown] + currentDistance;
                        previous[neighbor] = currentTown;

                        remainingTowns.Remove(neighbor);
                        for(var i = 0; i < remainingTowns.Count; i++) 
                        {
                            if (route_distance[neighbor] < remainingTowns[i])
                            {
                                remainingTowns.Insert(i, neighbor);
                                break;
                            }
                        }
                    }
                }

            }

            if (route_distance[endTown] == infinity)
            {
                throw new RailRoadSystemException(RailRoadSystemExceptionType.NoRouteExists, "NO SUCH ROUTE");
            }
            return route_distance[endTown];
        }

        public List<char> GetAllTowns()
        {
            return adjacencyList.GetAllVertices();
        }

        public int GetDistanceOfShortestLoop(char startTown)
        {
            var allTowns = GetAllTowns();
            if (!allTowns.Contains(startTown))
            {
                throw new RailRoadSystemException(RailRoadSystemExceptionType.NoRouteExists, "NO SUCH ROUTE");
            }

            var endTown = '1';
            var route_distance = new Dictionary<char, int>(MAX_NUMBER_OF_TOWNS);
            var previous = new Dictionary<char, char>(MAX_NUMBER_OF_TOWNS);
            var remainingTowns = new List<char>(MAX_NUMBER_OF_TOWNS);
            var unknownTown = '-';
            var infinity = 100000;


            allTowns.Add(endTown);
            foreach (var town in allTowns)
            {
                previous.Add(town, unknownTown);
                if (town == startTown)
                {
                    remainingTowns.Insert(0, town);
                    route_distance.Add(town, 0);
                    continue;
                }
                remainingTowns.Add(town);
                route_distance.Add(town, infinity);
            }

            while (remainingTowns.Count != 0)
            {
                var currentTown = remainingTowns[0];
                remainingTowns.RemoveAt(0);

                if (currentTown == endTown)
                {
                    continue;
                }

                var neighbors = GetNeighborsOf(currentTown);
                foreach (var neighbor in neighbors)
                {
                    var currentDistance = GetDistanceOf(currentTown, neighbor);
                    var currentNeighbor = neighbor == startTown? endTown: neighbor;
                    
                    if (route_distance[currentTown] + currentDistance <
                        route_distance[currentNeighbor])
                    {
                        route_distance[currentNeighbor] = route_distance[currentTown] + currentDistance;
                        previous[currentNeighbor] = currentTown;

                        remainingTowns.Remove(currentNeighbor);
                        for (var i = 0; i < remainingTowns.Count; i++)
                        {
                            if (route_distance[currentNeighbor] < remainingTowns[i])
                            {
                                remainingTowns.Insert(i, currentNeighbor);
                                break;
                            }
                        }
                    }
                }

            }

            if (route_distance[endTown] == infinity)
            {
                throw new RailRoadSystemException(RailRoadSystemExceptionType.NoRouteExists, "NO SUCH ROUTE");
            }
            return route_distance[endTown];
        }

        public int GetNumberOfTripsWithMaxStops(char startTown, char endTown, int maxStops)
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
                    queue.Enqueue(neighbor);
                    stopCount.Add(new KeyValuePair<char, int>(neighbor, currentStopCount + 1));
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
}