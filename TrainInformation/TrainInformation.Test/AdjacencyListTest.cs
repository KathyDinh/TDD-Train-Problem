using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TrainInformation.Test
{   
    [TestFixture]
    class AdjacencyListTest
    {
        [Test]
        public void AddDirectedEdge_ShouldAddEdgeToTheStartVertex()
        {
            var startVertex = 'A';
            var endVertex = 'C';
            var weight = 7;

            var target = new AdjacencyList();
            target.AddDirectedEdge(startVertex, endVertex, weight);

            var actual = target.GetEdgesFrom(startVertex);
            Assert.That(actual.Exists(anEdge => anEdge.StartVertex == startVertex && anEdge.EndVertex == endVertex && anEdge.Weight == weight));
        }

        [Test]
        public void GetEdgesFrom_ShoudldThrowExceptionIfThereIsNoEdge()
        {
            var startVertex = 'B';

            var target = new AdjacencyList();
            var exception = Assert.Throws<RailRoadSystemException>(() => target.GetEdgesFrom(startVertex));
            Assert.That(exception.exceptionType, Is.EqualTo(RailRoadSystemExceptionType.NoEdgeExists));
        }

        [Test]
        public void GetEdgesFrom_ShouldReturnAllEdgesStartingFromStartVertex()
        {
            var startVertex = 'C';
            var expected = new List<DirectedEdge>
            {
                new DirectedEdge(startVertex, 'D', 6)
                , new DirectedEdge(startVertex, 'E', 8)
                , new DirectedEdge(startVertex, 'A', 1)
            };

            var target = new AdjacencyList();
            foreach (var edge in expected)
            {
                target.AddDirectedEdge(edge);
            }

            var actual = target.GetEdgesFrom(startVertex);
            Assert.That(actual, Is.EquivalentTo(expected));
        }
    }
}
