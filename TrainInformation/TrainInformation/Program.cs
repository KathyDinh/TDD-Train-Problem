using System;
using System.IO;
using System.Text;

namespace TrainInformation
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                if (!TryGetFileName(out var fileName)) return;

                var routes = GetTrainRouteInfoFromFile(fileName);
            }
        }

        private static bool TryGetFileName(out string userInput)
        {
            const string EXIT_CMD = "exit";
            const string SAMPLE_DATA_FILE_NAME = "sample_route_data.txt";
            Console.WriteLine(
                $"Please enter absolute path of the text file to continue or {EXIT_CMD} to close the application:");
            userInput = Console.ReadLine();

            if (string.Compare(userInput
                    , EXIT_CMD
                    , StringComparison.CurrentCultureIgnoreCase)
                == 0)
            {
                return false;
            }

            if (string.IsNullOrEmpty(userInput.Trim()))
            {
                userInput = SAMPLE_DATA_FILE_NAME;
            }

            return true;
        }

        private static string[] GetTrainRouteInfoFromFile(string fileName)
        {
            string[] routes = null;

            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                //assume that the route info is in first line and routes are separated by comma and space
                var routeInfoLine = streamReader.ReadLine();
                if (routeInfoLine != null)
                {
                    routes = routeInfoLine.Split(new[] {",", " "}, StringSplitOptions.RemoveEmptyEntries);
                }
            }

            return routes;
        }
    }
}