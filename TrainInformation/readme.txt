To build:
The application is a console application written in C#. 
Please build the code in Visual Studio version that support C# 6.0 as I have use some feature like string interpolation.

To run the application:
After build, do either of the following to start the app: 
1. Double click on the .exe file in bin/Debug or bin/Release folder
2. In Visual Studio, right-click on project TrainInformation > Set as StartUp Project > press button Start in toolbar.
Input the absolute path to your test file or exit to stop the app.
Leave the input empty if you want to see the result of the sample test data.

To vefify that sample test data result has been met:
Tests for question 1 to 10 can be found in RailRoadSystemTest class preceded with comments //Question X
You may want to change variable routeInfo in each test with a different test data file content.
 
Solution structure:
There are 2 projects TrainInformation and TrainInformation.Test in the solution.
The classes has been put in respective folder according to their category
Main classes are:
- Program: read file and accept input from user
- RailroadSystem: build graph and catch exception from composite class to give appropriate output.
- Graph: implement graph algorithms
- AdjacencyList: represent graph data structure
