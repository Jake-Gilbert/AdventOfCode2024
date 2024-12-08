using DaySix;

var map = File.ReadAllLines("TestData\\ChallengeData.TXT");
var mapWidth = map.FirstOrDefault()?.Length ?? 0;
var loopPermutations = 0;
var searchedObstacleLocations = new Dictionary<string, bool>();
var currentObstacleIndex = "0-0";
var averageSteps = 0;
var runs = 0;
PopulatePossibleObstacleLocations();

while (searchedObstacleLocations.Values.Any(x => !x))
{
    if (searchedObstacleLocations[currentObstacleIndex])
    {
        currentObstacleIndex = CalculateNextIndex();
    }
    else
    {
        var simulator = new PatrolSimulator(map, true, currentObstacleIndex);
        var patrolResults = simulator.SimulatePatrol();

        searchedObstacleLocations[currentObstacleIndex] = true;
        currentObstacleIndex = CalculateNextIndex();
    
        if (patrolResults.TraversedSteps > 0)
        {
            averageSteps += patrolResults.TraversedSteps;
            runs += 1;
        }
        if (patrolResults.WasStuckInLoop)
        {
            loopPermutations++;
        }
    }
}

Console.WriteLine($"The guard has walked an average of {averageSteps / runs } steps");
Console.WriteLine($"The guard has ended up in an infinite loop {loopPermutations} times");

//Any coordinates which have value of false are a possible location to place an obstacle
void PopulatePossibleObstacleLocations()
{
    for (int i = 0; i < map.Length; i++)
    {
        for (int j = 0; j < mapWidth; j++)
        {
            if (map[i][j] == '^' || map[i][j] == '#')
            {
                searchedObstacleLocations.Add($"{i}-{j}", true);
            }
            else
            {
                searchedObstacleLocations.Add($"{i}-{j}", false);
            }
        }
    }
}

string CalculateNextIndex()
{
    var currentIndexArray = currentObstacleIndex.Split("-");
    var rowVal = int.Parse(currentIndexArray[0]);
    var colVal = int.Parse(currentIndexArray[1]);
    if (colVal + 1 >= mapWidth)
    {
        rowVal += 1;
        colVal = 0;
    }
    else
    {
        colVal++;
    }

    return $"{rowVal}-{colVal}";
}
