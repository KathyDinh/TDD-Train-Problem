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

            var actual = target.BuildRoutesGraph(routeInfo);
            
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
            target.BuildRoutesGraph(routeInfo);
            var actual = target.RoutesGraph;

            Assert.That(actual.GetNeighborsOf('A'), Is.EquivalentTo(new [] { 'B', 'D', 'E'}));
        }
    }
}
