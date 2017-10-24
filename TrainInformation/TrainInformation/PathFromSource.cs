using System;

namespace TrainInformation
{
    internal class PathFromSource : IComparable<PathFromSource>
    {
        public PathFromSource(char stop, int distance)
        {
            Stop = stop;
            Distance = distance;
        }

        public char Stop { get; set; }
        public int Distance { get; set; }
        public int CompareTo(PathFromSource other)
        {
            return Stop - other.Stop;
        }
    }
}