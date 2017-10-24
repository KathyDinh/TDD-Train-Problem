using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TrainInformation.Data_Structures;

namespace TrainInformation.Test.Data_Structures
{
    [TestFixture]
    class MinPriorityQueueTest
    {
        [Test]
        public void Enqueue_ShouldEnsureTheAscendingOrder()
        {
            var items = new List<char> {'K', 'L', 'A', 'Z'};

            var target = new MinPriorityQueue<char>();
            items.ForEach(anItem => target.Enqueue(anItem));

            items.Sort();
            foreach (var item in items)
            {
                Assert.That(target.Dequeue(), Is.EqualTo(item));
            }
        }

        [Test]
        public void Dequeue_ShouldRemoveTheSmallestItem()
        {
            var items = new List<char> { 'T', 'Z', 'B' };

            var target = new MinPriorityQueue<char>();
            items.ForEach(anItem => target.Enqueue(anItem));
            items.Sort();

            Assert.That(target.Dequeue(), Is.EqualTo(items[0]));
        }
    }
}
