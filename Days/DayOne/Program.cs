using DayOne;

var textMapper = new TextMapper("TestData\\ExampleData.TXT");
Console.WriteLine(DistanceCalculator.CalculateDistance(textMapper.Lists));