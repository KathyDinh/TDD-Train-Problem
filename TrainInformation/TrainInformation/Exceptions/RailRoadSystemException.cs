namespace TrainInformation.Exceptions
{
    internal class RailRoadSystemException : System.Exception
    {
        public RailRoadSystemExceptionType exceptionType;
        public RailRoadSystemException(RailRoadSystemExceptionType exceptionType, string message): base(message)
        {
            this.exceptionType = exceptionType;
        }
    }

    internal enum RailRoadSystemExceptionType
    {
        NoRouteExists,
        NoNeightborExists
    }
}