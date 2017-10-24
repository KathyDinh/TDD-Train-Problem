using NUnit.Framework;
using TrainInformation;

namespace TrainInformation.Test
{
    [TestFixture]
    public class ProgramTest
    {
        [Test]
        public void GetTrainRouteInfoFromFile_ShouldReturnCorrectRouteInfo()
        {
            var fileName = TestContext.CurrentContext.TestDirectory + @"/sample_route_data.txt";
            var expected = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";

            var actual = Program.GetTrainRouteInfoFromFile(fileName);

            Assert.That(actual, Is.EquivalentTo(expected));
        }
    }
}
