using System;

namespace TrainInformation
{
    class Program
    {
        static void Main(string[] args)
        {
            const string EXIT_CMD = "exit";
            while(true)
            {
                Console.WriteLine(
                    $"Please enter absolute path of the text file to continue or {EXIT_CMD} to close the application:");
                var userInput = Console.ReadLine();

                if (string.Compare(userInput
                    , EXIT_CMD
                    , StringComparison.CurrentCultureIgnoreCase) 
                    == 0)
                {
                    return;
                }
            }
        }
    }
}