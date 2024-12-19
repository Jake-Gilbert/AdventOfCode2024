using DayTen;

var f = File.ReadAllLines("ChallengeData.txt");
var trailheadStartLocations = new List<(int, int)>();
var grid = new Dictionary<(int, int), int>();
for (int i = 0; i < f.Length; i++)
{
    for (int j = 0; j < f.First().Length; j++)
    {
        if (f[i][j] == '0')
        {
            trailheadStartLocations.Add((i, j));
        }
        if (int.TryParse(f[i][j].ToString(), out var c))
        {
            grid.Add((i, j), c);
        }
        else
        {
            grid.Add((i, j), -1);
        }
    }
    Console.WriteLine(string.Join("", f[i]));
}

var score = 0;
var testScore = 0;
var pathFinder = new Pathfinder(grid, f.Length, f.First().Length);
foreach (var startCo in trailheadStartLocations)
{
    var bfs = pathFinder.BreadthFirstSearch(startCo);
    var partTwo = pathFinder.BreadthFirstSearchDistinct(startCo, out var distinctScore);
    score += bfs.Values.Count(x => x == 9);
    testScore += distinctScore;
}

Console.WriteLine($"Score {score}");
Console.WriteLine($"TestScore {testScore}");

