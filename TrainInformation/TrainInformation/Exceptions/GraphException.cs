namespace TrainInformation.Exceptions
{
    internal class GraphException : System.Exception
    {
        public GraphExceptionType ExceptionType;
        public GraphException(GraphExceptionType exceptionType, string message): base(message)
        {
            ExceptionType = exceptionType;
        }
    }

    internal enum GraphExceptionType
    {
        NoRouteExists,
        NoNeightborExists
    }
}