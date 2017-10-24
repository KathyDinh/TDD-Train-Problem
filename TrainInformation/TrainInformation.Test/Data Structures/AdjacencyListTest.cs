using System.Collections.Generic;
using NUnit.Framework;
using TrainInformation.Data_Structures;

namespace TrainInformation.Test.Data_Structures
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
        public void AddDirectedEdge_ShouldAddVerticesWithUppercase()
        {
            var startVertex = 'a';
            var endVertex = 'b';
            var weight = 4;

            var target = new AdjacencyList();
            target.AddDirectedEdge(startVertex, endVertex, weight);

            var actual = target.GetEdgesFrom(startVertex);
            Assert.That(actual.Exists(anEdge => anEdge.StartVertex == char.ToUpperInvariant(startVertex) && anEdge.EndVertex == char.ToUpperInvariant(endVertex) && anEdge.Weight == weight));
        }

        [Test]
        public void GetEdgesFrom_ShoudldReturnEmptyLisIfThereIsNoEdge()
        {
            var startVertex = 'B';

            var target = new AdjacencyList();
            var actual = target.GetEdgesFrom(startVertex);

            Assert.That(actual.Count, Is.EqualTo(0));
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
            for (var i = 0; i < actual.Count; i++)
            {
                Assert.That(actual[i].StartVertex, Is.EqualTo(expected[i].StartVertex));
                Assert.That(actual[i].EndVertex, Is.EqualTo(expected[i].EndVertex));
                Assert.That(actual[i].Weight, Is.EqualTo(expected[i].Weight));
            }
        }

        [Test]
        public void GetEdgesFrom_ShouldIgnoreCaseAndReturnAllEdgesFromTheStartVertex()
        {
            var startVertex = 'd';
            var expected = new List<DirectedEdge>
            {
                new DirectedEdge(startVertex, 'g', 4)
                , new DirectedEdge(startVertex, 'E', 2)
            };

            var target = new AdjacencyList();
            foreach (var edge in expected)
            {
                target.AddDirectedEdge(edge);
            }

            var actual = target.GetEdgesFrom(startVertex);
            Assert.That(actual.Count, Is.EqualTo(expected.Count));
        }
    }
}
