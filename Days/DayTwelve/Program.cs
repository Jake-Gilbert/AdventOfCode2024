using DayTwelve;

var f = File.ReadAllLines("Data\\ChallengeData.TXT");
var plotter = new Plotter(f);
var regions = plotter.CalculateRegions();
var regionSum = 0;
var cumulativeCost = 0;
var cumulativeP2Cost = 0;
foreach (var reg in regions)
{
    Console.WriteLine();
    Console.WriteLine(reg.Key);
    foreach (var v in reg.Value)
    {
        Console.WriteLine(string.Join(",", v));
        Console.WriteLine($"Count: {v.Count}");
        var searcher = new Searcher(plotter._farmLand, plotter._height, plotter._width);
        var perimiter = searcher.CalculatePerimiter(v, reg.Key);
        Console.WriteLine($"Perimiter: {perimiter}");
        var sides = searcher.CalculateSides(v, reg.Key);
        Console.WriteLine($"Sides: {sides}");
        var cost = perimiter * v.Count;
        Console.WriteLine($"Cost: {cost}");
        var part2cost = sides * v.Count;
        Console.WriteLine($"Cost p2: {part2cost}");
        regionSum++;
        cumulativeCost += cost;
        cumulativeP2Cost += part2cost;
    }
}

Console.WriteLine();
Console.WriteLine($"Regions: {regionSum}");
Console.WriteLine($"Cost: {cumulativeCost}");
Console.WriteLine($"P2 Cost: {cumulativeP2Cost}");
