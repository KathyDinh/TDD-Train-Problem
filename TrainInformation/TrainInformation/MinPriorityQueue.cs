using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainInformation
{
    internal class MinPriorityQueue<T> where T : IComparable<T>
    {
        private List<T> backingStore;
        public MinPriorityQueue() : this(10) { }
        public MinPriorityQueue(int initialCapacity)
        {
            backingStore = new List<T>(initialCapacity);
        }

        public void Enqueue(T item)
        {
            if (Count == 0)
            {
                backingStore.Add(item);
                return;
            }

            for (var i = 0; i < Count; i++)
            {
                if (item.CompareTo(backingStore[i]) >= -1) continue;
                backingStore.Insert(i, item);
                return;
            }

            backingStore.Add(item);
        }

        public T Dequeue()
        {
            if (Count == 0)
            {
                return default(T);
            }
            var front = backingStore[0];
            backingStore.RemoveAt(0);

            return front;
        }

        public void Clear()
        {
            backingStore.Clear();
        }

        public int Count
        {
            get { return backingStore.Count; }
        }
    }
}
