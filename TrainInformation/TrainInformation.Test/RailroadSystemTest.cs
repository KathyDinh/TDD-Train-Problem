using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TrainInformation.Test
{
    [TestFixture]
    class RailroadSystemTest
    {
        [Test]
        public void BuildRouteGraph_ShouldBuildAGraph()
        {
            var routeInfo = new string[0];
            var target = new RailroadSystem();

            target.BuildRoutesGraphWith(routeInfo);
            var actual = target.RoutesGraph;

            Assert.That(actual, Is.InstanceOf<Graph>());
        }

        [Test]
        public void BuildRouteGraph_ShouldBuildCorrectGraph()
        {
            var routeInfo = new[]
            {
                "AB5", "BC4", "CD8", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7"
            };
          
            var target = new RailroadSystem();
            target.BuildRoutesGraphWith(routeInfo);
            var actual = target.RoutesGraph;

            Assert.That(actual.GetNeighborsOf('A'), Is.EquivalentTo(new [] { 'B', 'D', 'E'}));
        }

        //Question 1-4
        [Test]
        public void GetDistanceOfRoute_ShouldReturnCorrectValue()
        {
            var routeInfo = new[]
            {
                "AB5", "BC4", "CD8", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7"
            };

            var target = new RailroadSystem();
            target.BuildRoutesGraphWith(routeInfo);

            var stops = new []
            {
                new [] {'A', 'B', 'C'}
                , new [] {'A', 'D' }
                , new [] {'A', 'D', 'C' }
                , new [] {'A', 'E', 'B', 'C', 'D'}
            };

            var expected = new []
            {
                9, 5, 13, 22
            };

            for (var i = 0; i < stops.Length; i++)
            {
                var actual = target.GetDistanceOfRouteWith(stops[i]);
                Assert.That(actual, Is.EqualTo(expected[i]));
            }
        }

        //Question 5
        [Test]
        public void GetDistanceOfRoute_ShouldReturnMessageIfNoRouteExists()
        {
            var routeInfo = new[]
            {
                "AB5", "BC4", "CD8", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7"
            };

            var target = new RailroadSystem();
            target.BuildRoutesGraphWith(routeInfo);

            var stops = new [] {'A', 'E', 'D'};

            var actual =  target.GetDistanceOfRouteWith(stops);
            Assert.That(actual, Is.EqualTo("NO SUCH ROUTE"));
        }

        //Question 6
        [Test]
        public void GetNumberOfTripsWithMaxStops_ShouldReturCorrectValue()
        {
            var routeInfo = new[]
            {
                "AB5", "BC4", "CD8", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7"
            };

            var target = new RailroadSystem();
            target.BuildRoutesGraphWith(routeInfo);

            var startTown = 'C';
            var endTown = 'C';
            var maxStops = 3;
            var expected = 2;

            var actual = target.GetNumberOfTripsWithMaxStops(startTown, endTown, maxStops);
            Assert.That(actual, Is.EqualTo(expected));
        }

        //Question 7
        [Test]
        public void GetNumberOfTripsWithExactStops_ShouldReturCorrectValue()
        {
            var routeInfo = new[]
            {
                "AB5", "BC4", "CD8", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7"
            };

            var target = new RailroadSystem();
            target.BuildRoutesGraphWith(routeInfo);

            var startTown = 'A';
            var endTown = 'C';
            var exactStops = 4;
            var expected = 3;

            var actual = target.GetNumberOfTripsWithExactStops(startTown, endTown, exactStops);
            Assert.That(actual, Is.EqualTo(expected));
        }

        //Question 8
        [Test]
        public void GetDistanceOfShortestRoute_IfStartTownAndEndTownAreDifferentShouldReturnCorrectValue()
        {
            var routeInfo = new[]
            {
                "AB5", "BC4", "CD8", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7"
            };

            var target = new RailroadSystem();
            target.BuildRoutesGraphWith(routeInfo);

            var startTown = 'A';
            var endTown = 'C';
            var expected = 9;

            var actual = target.GetDistanceOfShortestRoute(startTown, endTown);

            Assert.That(actual, Is.EqualTo(expected));
        }

        //Question 9
        [Test]
        public void GetDistanceOfShortestRoute_IfStartTownAndEndTownAreSameShouldReturnCorrectValue()
        {
            var routeInfo = new[]
            {
                "AB5", "BC4", "CD8", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7"
            };

            var target = new RailroadSystem();
            target.BuildRoutesGraphWith(routeInfo);

            var startTown = 'B';
            var endTown = 'B';
            var expected = 9;

            var actual = target.GetDistanceOfShortestRoute(startTown, endTown);

            Assert.That(actual, Is.EqualTo(expected));
        }

        //Question 10
        [Test]
        public void GetNumberOfTripsWithMaxDistance_ShouldReturnCorrectValue()
        {
            var routeInfo = new[]
            {
                "AB5", "BC4", "CD8", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7"
            };

            var target = new RailroadSystem();
            target.BuildRoutesGraphWith(routeInfo);

            var startTown = 'C';
            var endTown = 'C';
            var maxDistance = 30;
            var expected = 7;

            var actual = target.GetNumberOfTripsWithMaxDistance(startTown, endTown, maxDistance);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
