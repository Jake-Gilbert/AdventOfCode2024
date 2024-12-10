using DayEight;

var grid = File.ReadAllLines("TestData/ExampleData.TXT");
var search = new AntennaSearch(grid);
var antinodeCount = search.CalculateFreuqencyPositions();
Console.WriteLine(antinodeCount);
