using DayThirteen;
using System.Text.RegularExpressions;
var fileArr = File.ReadAllText("TestData\\ChallengeData.txt").Split("\r\n\r\n");
var cumulativeTokenCost = 0L;
foreach (var line in fileArr)
{
    var axRegex = @"Button A:.*?X\+(\d+)";
    var axReg = new Regex(axRegex);
    var ayRegex = @"Button A:.*?Y\+(\d+)";
    var ayReg = new Regex(ayRegex);
    var bxRegex = @"Button B:.*?X\+(\d+)";
    var bxReg = new Regex(bxRegex);
    var byRegex = @"Button B:.*?Y\+(\d+)";
    var byReg = new Regex(byRegex);
    var pxRegex = @"Prize:.*?X\=(\d+)";
    var pxReg = new Regex(pxRegex);
    var pyRegex = @"Prize:.*?Y\=(\d+)";
    var pyReg = new Regex(pyRegex);
    var ax = int.Parse(axReg.Match(line).Groups[1].Value);
    var ay = int.Parse(ayReg.Match(line).Groups[1].Value);
    var bx = int.Parse(bxReg.Match(line).Groups[1].Value);
    var by = int.Parse(byReg.Match(line).Groups[1].Value);
    var px = int.Parse(pxReg.Match(line).Groups[1].Value) + 10000000000000;
    var py = int.Parse(pyReg.Match(line).Groups[1].Value) + 10000000000000;

    var solver = new EquationSolver(ax, ay, bx, by, px, py);
    if (solver.CalculateFewestPresses(out var aPresses, out var bPresses))
    {
        var tokenCostA = aPresses * 3;
        var tokenCostB = bPresses;
        Console.WriteLine($"A presses: {aPresses}, B presses: {bPresses}");
        Console.WriteLine($"A press costs: {tokenCostA}, B press costs: {tokenCostB}");
        cumulativeTokenCost += tokenCostA + tokenCostB;
    }
    else
    {
        Console.WriteLine("Not possible to reach prize with any button combinatons");
    }
    Console.WriteLine();
}
Console.WriteLine($"Cumulative cost: {cumulativeTokenCost}");