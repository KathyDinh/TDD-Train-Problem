How to run the application:
The application is a console application written in C#. 
Please build the code in Visual Studio version that support C# 6.0 as I have use some feature like string interpolation.
Double click on the .exe file in bin/Debug or bin/Release folder to start the app or press Start in Visual Studio.
Input the absolute path to your test file or exit to stop the app.
Leave the input empty if you want to see the result of the sample test data.

Solution structure:
There are 2 projects TrainInformation and TrainInformation.Test in the solution.
Tests for question 1 to 10 can be found in RailRoadSystemTest class.
There are 4 main classes: Program, RailroadSystem, Graph and AdjacencyList.
Program: read file and accept input from user
RailroadSystem: build graph and catch exception from composite class to give appropriate output.
Graph: implement graph algorithms
AdjacencyList: represent graph data structure

Note:
As I was short on time, I could not manage to polish the code as wanted.
I could only ensure the correctness of the solution.
My apology for that.
There are some area need to enhance like 
Dikjstra's algorithm should use either Priority Queue or Min Heap to improve efficiency.
