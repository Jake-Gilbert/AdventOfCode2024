
using DayNine;

var sampleData = "2333133121414131402";
var challengeData = File.ReadAllText("ChallengeData.TXT");
var initialiser = new FileInitialiser(challengeData);
var sorted = Sorter.Sort(initialiser.memRep);
var partTwoSorted = Sorter.BlockSort(initialiser.memRep, initialiser.freeSpaceIndexes);
var checkSum = Calculator.CalculateCheckSum(sorted);
var partTwoChecksum = Calculator.CalculatePartTwoChecksum(partTwoSorted);
Console.WriteLine($"Part one: {checkSum}");
Console.WriteLine($"Part two: {partTwoChecksum}");