using System;
using System.IO;
using System.Text;
using TrainInformation.Domain;

namespace TrainInformation
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                if (!TryGetFileName(out var fileName)) return;

                try
                {
                    PrintRailwaySystemInfoInFile(fileName);
                }
                catch (Exception ex)
                {
                    PrintError(ex);
                }
                finally
                {
                    Console.WriteLine();
                }
            }
        }

        private static void PrintRailwaySystemInfoInFile(string fileName)
        {
            var routes = GetTrainRouteInfoFromFile(fileName);
            var railroadSystem = new RailroadSystem();
            railroadSystem.BuildRoutesGraphWith(routes);

            Console.WriteLine($"Output #1: {railroadSystem.GetDistanceOfRouteWith('A', 'B', 'C')}");
            Console.WriteLine($"Output #2: {railroadSystem.GetDistanceOfRouteWith('A', 'D')}");
            Console.WriteLine($"Output #3: {railroadSystem.GetDistanceOfRouteWith('A', 'D', 'C')}");
            Console.WriteLine($"Output #4: {railroadSystem.GetDistanceOfRouteWith('A', 'E', 'B', 'C', 'D')}");
            Console.WriteLine($"Output #5: {railroadSystem.GetDistanceOfRouteWith('A', 'E', 'D')}");
            Console.WriteLine($"Output #6: {railroadSystem.GetNumberOfTripsWithMaxStops('C', 'C', 3)}");
            Console.WriteLine($"Output #7: {railroadSystem.GetNumberOfTripsWithExactStops('A', 'C', 4)}");
            Console.WriteLine($"Output #8: {railroadSystem.GetDistanceOfShortestRoute('A', 'C')}");
            Console.WriteLine($"Output #9: {railroadSystem.GetDistanceOfShortestRoute('B', 'B')}");
            Console.WriteLine($"Output #10: {railroadSystem.GetNumberOfTripsWithMaxDistance('C', 'C', 30)}");
        }

        private static void PrintError(Exception ex)
        {
            Console.WriteLine($"An error has occur: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
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

        public static string[] GetTrainRouteInfoFromFile(string fileName)
        {
            string[] routes = null;

            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                //assume that the route info is in first line and routes are separated by comma and space
                var routeInfoLine = streamReader.ReadLine();
                if (routeInfoLine != null)
                {
                    routes = routeInfoLine.Split(new[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);
                }
            }

            return routes;
        }
    }
}
