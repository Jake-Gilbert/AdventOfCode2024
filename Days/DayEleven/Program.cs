using DayEleven;

var stones = File.ReadAllText("ChallengeData.txt");
var l = stones.Split(" ").Select(x => long.Parse(x)).ToList();
var startingDict = new Dictionary<long, long>();
foreach (var s in l)
{
    if (!startingDict.TryAdd(s, 1))
    {
        startingDict[s]++;
    }
}
var processedFinal = PlutonianEvaluator.Evaluate(startingDict, 75);
Console.WriteLine(processedFinal.StoneCount);
