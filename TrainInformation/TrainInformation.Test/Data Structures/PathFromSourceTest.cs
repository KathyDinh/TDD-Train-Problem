using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;
using TrainInformation.Data_Structures;

namespace TrainInformation.Test.Data_Structures
{
    [TestFixture]
    class PathFromSourceTest
    {
        [Test]
        public void CompareTo_ShouldReturnNegativeIfItsLengthIsSmaller()
        {
            var otherPath = new PathFromSource('A', 9);

            var target = new PathFromSource('G', 1);
            var actual = target.CompareTo(otherPath);

            Assert.That(actual, Is.Negative);
        }

        [Test]
        public void CompareTo_ShouldReturnZeroIfItsLengthIsEqual()
        {
            var otherPath = new PathFromSource('K', 10);

            var target = new PathFromSource('N', 10);
            var actual = target.CompareTo(otherPath);

            Assert.That(actual, Is.Zero);
        }

        [Test]
        public void CompareTo_ShouldReturnPositiveIfItsLengthIsGreater()
        {
            var otherPath = new PathFromSource('I', 67);

            var target = new PathFromSource('D', 200);
            var actual = target.CompareTo(otherPath);

            Assert.That(actual, Is.Positive);
        }
    }
}
