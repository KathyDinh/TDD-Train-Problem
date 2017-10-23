using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TrainInformation
{
    internal class RailroadSystem
    {
        private static readonly string ROUTE_INFO_PATTERN = @"([A-E])([A-E])(\d+)";
        private static readonly int START_TOWN_GROUP_INDEX = 1;
        private static readonly int END_TOWN_GROUP_INDEX = 2;
        private static readonly int DISTANCE_GROUP_INDEX = 3;
        private static readonly int MAX_NUMBER_OF_TOWNS = 5; //Town names are alphabet between A-E
        private static readonly Regex ROUTE_INFO_REG_EX = new Regex(ROUTE_INFO_PATTERN);

        private Graph _routesGraph;
        public Graph RoutesGraph
        {
            get { return _routesGraph; }
            private set { _routesGraph = value; }
        }

        public RailroadSystem()
        {
        }

        public Graph GetRoutes()
        {
            return _routesGraph;
        }

        public void BuildRoutesGraphWith(string[] routesInfo)
        {
            RoutesGraph = new Graph(MAX_NUMBER_OF_TOWNS);
            foreach (var route in routesInfo)
            {
                var match = ROUTE_INFO_REG_EX.Match(route.ToUpper());

                if (!match.Success) continue;
                var startTown = Convert.ToChar(match.Groups[START_TOWN_GROUP_INDEX].Value);
                var endTown = Convert.ToChar(match.Groups[END_TOWN_GROUP_INDEX].Value);
                var distance = Convert.ToInt32(match.Groups[DISTANCE_GROUP_INDEX].Value);
                RoutesGraph.AddOneWayRoute(startTown, endTown, distance);
            }
        }

        public int GetDistanceOfRouteWith(char[] stops)
        {
            var totalDistance = 0;
            for (var i = 0; i < stops.Length - 1; i++)
            {
                totalDistance += RoutesGraph.GetDistanceOf(stops[i], stops[i + 1]);
            }
            return totalDistance;
        }

        public int GetDistanceOfShortestRoute(char startTown, char endTown)
        {
            if (startTown == endTown)
            {
                return RoutesGraph.GetDistanceOfShortestLoop(startTown);
            }
            return RoutesGraph.GetDistanceOfShortestRoute(startTown, endTown);
        }
    }
}