using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;
using TrainInformation.Exceptions;
using TrainInformation.Logic;

namespace TrainInformation.Test.Domain
{
    [TestFixture]
    class GraphTest
    {
        [Test]
        public void GetLengthOfShortestPath_ShouldThrowExceptionIfPathCannotExist()
        {
            var source = 'G';
            var destination = 'K';

            var target = new Graph();

            var exception = Assert.Throws<GraphException>(() => target.GetLengthOfShortestPath(source, destination));
            Assert.That(exception.ExceptionType, Is.EqualTo(GraphExceptionType.NoRouteExists));
        }

        [Test]
        public void GetNumberOfPathsWithExactStops_ShouldThrowExceptionIfPathCannotExist()
        {
            var source = 'J';
            var destination = 'U';

            var target = new Graph();

            var exception = Assert.Throws<GraphException>(() => target.GetLengthOfShortestPath(source, destination));
            Assert.That(exception.ExceptionType, Is.EqualTo(GraphExceptionType.NoRouteExists));
        }

        [Test]
        public void GetNumberOfPathsWithMaxStops_ShouldThrowExceptionIfPathCannotExist()
        {
            var source = 'K';
            var destination = 'H';
            var limit = 7;

            var target = new Graph();

            var exception = Assert.Throws<GraphException>(() => target.GetNumberOfPathsWithMaxStops(source, destination, limit));
            Assert.That(exception.ExceptionType, Is.EqualTo(GraphExceptionType.NoRouteExists));
        }

        [Test]
        public void GetNumberOfPathsWithMaxLength_ShouldThrowExceptionIfPathCannotExist()
        {
            var source = 'L';
            var destination = 'S';
            var limit = 9;

            var target = new Graph();

            var exception = Assert.Throws<GraphException>(() => target.GetNumberOfPathsWithMaxLength(source, destination, limit));
            Assert.That(exception.ExceptionType, Is.EqualTo(GraphExceptionType.NoRouteExists));
        }


    }
}
