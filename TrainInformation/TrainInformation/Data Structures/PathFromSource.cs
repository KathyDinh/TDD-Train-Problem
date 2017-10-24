using System;

namespace TrainInformation.Data_Structures
{
    internal class PathFromSource : IComparable<PathFromSource>
    {
        public PathFromSource(char stop, int length)
        {
            Stop = stop;
            Length = length;
        }

        public char Stop { get; set; }
        public int Length { get; set; }
        public int CompareTo(PathFromSource other)
        {
            return Stop - other.Stop;
        }
    }
}