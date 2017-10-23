using System;

namespace TrainInformation
{
    internal class RailRoadSystemException : Exception
    {
        public RailRoadSystemExceptionType exceptionType;
        public RailRoadSystemException(RailRoadSystemExceptionType exceptionType, string message): base(message)
        {
            this.exceptionType = exceptionType;
        }
    }

    internal enum RailRoadSystemExceptionType
    {
        NoEdgeExists,
        NoTownExists
    }
}