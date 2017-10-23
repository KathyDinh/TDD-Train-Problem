﻿using System;
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
        public void GetDistanceOfRoute_ShouldThrowExceptionIfNoRouteExists()
        {
            var routeInfo = new[]
            {
                "AB5", "BC4", "CD8", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7"
            };

            var target = new RailroadSystem();
            target.BuildRoutesGraphWith(routeInfo);

            var stops = new [] {'A', 'E', 'D'};

            var exception = Assert.Throws<RailRoadSystemException>(() => target.GetDistanceOfRouteWith(stops));
            Assert.That(exception.exceptionType, Is.EqualTo(RailRoadSystemExceptionType.NoRouteExists));
            Assert.That(exception.Message, Is.EqualTo("NO SUCH ROUTE"));
        }
    }
}
