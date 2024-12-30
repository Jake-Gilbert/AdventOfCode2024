using System.IO;
using System.Text;
using System.Text.RegularExpressions;

var lines = File.ReadAllLines("TestData\\ChallengeData.txt");
var grid = new Dictionary<(int y, int x), char>();
var height = 103;
var width = 101;
var ascii = 48;

for (int i = 0; i < height; i++)
{
    for (int j = 0; j < width; j++)
    {
        grid.Add((i, j), '.');
    }
}
var positions = new List<(int x, int y)>();
var instructions = new Dictionary<int, (int x, int y)>();
var index = 0;
foreach (var line in lines)
{
    var positionRegx = new Regex(@"p=(\d+),(\d+)");
    var positionMatch = positionRegx.Match(line);
    var px = int.Parse(positionMatch.Groups[1].Value);
    var py = int.Parse(positionMatch.Groups[2].Value);

    var velocityRegx = new Regex(@"v=(-?\d+),(-?\d+)");
    var velocityMatch = velocityRegx.Match(line);
    var vx = int.Parse(velocityMatch.Groups[1].Value);
    var vy = int.Parse(velocityMatch.Groups[2].Value);

    positions.Add((px, py));
    instructions.Add(index, (vx, vy));
    index++;
}
var sb = new StringBuilder();
RenderRobotsAtStartPositions(positions);
var secs = 0;
while (secs < 10000)
{
    for (int i = 0; i < positions.Count; i++)
    {
        var position = positions[i];
        var instruction = instructions[i];
        ExecuteOneMove(position, instruction, i);
    }
    Console.WriteLine();
    Console.WriteLine($"secs: {secs}\n");
    DrawGrid();
    sb.Append($"Secs: {secs}");
    secs++;
}
CountRobotsInEachQuadrant();
File.WriteAllText("TestData\\T.txt", sb.ToString());

void CountRobotsInEachQuadrant()
{
    var halfWidth = width / 2;
    var halfHeight = height / 2;
    Console.WriteLine();
    var quadrants = new List<int>();
    var quadrantSum = 0;
    for (int i = 0; i < halfHeight; i++)
    {
        for (int j = 0; j < halfWidth; j++)
        {
            if (int.TryParse(grid[(i, j)].ToString(), out var topleft))
            {
                quadrantSum += topleft;
            }
            Console.Write(grid[(i, j)]);
        }
        Console.WriteLine();
    }
    quadrants.Add(quadrantSum);
    quadrantSum = 0;
    Console.WriteLine();
    for (int i = halfHeight + 1; i < height; i++)
    {
        for (int j = 0; j < halfWidth; j++)
        {
            if (int.TryParse(grid[(i, j)].ToString(), out var topleft))
            {
                quadrantSum += topleft;
            }
            Console.Write(grid[(i, j)]);
        }
        Console.WriteLine();
    }
    quadrants.Add(quadrantSum);
    quadrantSum = 0;
    Console.WriteLine();
    for (int i = 0; i < halfHeight; i++)
    {
        for (int j = halfWidth + 1; j < width; j++)
        {
            if (int.TryParse(grid[(i, j)].ToString(), out var topleft))
            {
                quadrantSum += topleft;
            }
            Console.Write(grid[(i, j)]);
        }
        Console.WriteLine();
    }
    quadrants.Add(quadrantSum);
    quadrantSum = 0;
    Console.WriteLine();
    for (int i = halfHeight + 1; i < height; i++)
    {
        for (int j = halfWidth + 1; j < width; j++)
        {
            if (int.TryParse(grid[(i, j)].ToString(), out var topleft))
            {
                quadrantSum += topleft;
            }
            Console.Write(grid[(i, j)]);
        }
        Console.WriteLine();
    }
    quadrants.Add(quadrantSum);
    quadrantSum = 0;
    Console.WriteLine(string.Join(", ", quadrants));
    var current = 0;
    foreach (var quadrant in quadrants)
    {
        if (current == 0)
        {
            current = quadrant;
        }
        else
        {
            current *= quadrant;
        }
    }
    Console.WriteLine(current);
}

void ExecuteOneMove((int x, int y) coordinate, (int x, int y) velocity, int index)
{
    UpdateCell((coordinate.y, coordinate.x), '.');
    var updatedX = coordinate.x + velocity.x;
    var updatedY = coordinate.y + velocity.y;
    if (updatedX < 0)
    {
        updatedX = width + updatedX;
    }
    if (updatedX >= width)
    {
        updatedX = updatedX - width;
    }
    if (updatedY < 0)
    {
        updatedY = height + updatedY;
    }
    if (updatedY >= height)
    {
        updatedY = updatedY - height;
    }
    UpdateCell((updatedY, updatedX), '1');
    positions[index] = (updatedX, updatedY);    
}

void RenderRobotsAtStartPositions(List<(int x, int y)> positions)
{
    foreach (var position in positions)
    {
        UpdateCell((position.y, position.x), '1');
    }
    DrawGrid();
}

void UpdateCell((int y , int x) cell, char change)
{
    if (int.TryParse(grid[(cell.y, cell.x)].ToString(), out int val))
    {
        if (change == '.' && val <= 1)
        {
            grid[(cell.y, cell.x)] = '.';
        }
        else if (change == '.' && val > 1)
        {
            grid[(cell.y, cell.x)] = (char) (ascii + (val - 1));
        }
        if (change != '.')
        {
            grid[(cell.y, cell.x)] = (char)(grid[(cell.y, cell.x)] + (change - ascii));
        }
    }
    else if (grid[(cell.y, cell.x)] == '.' && change != '.')
    {
        grid[(cell.y, cell.x)] = change;
    }
}

void DrawGrid()
{
    foreach (var coordinate in grid.Keys)
    {
        Console.Write(grid[coordinate]);
        sb.Append(grid[coordinate]);
        if (coordinate.x == 10)
        {
            Console.WriteLine();
            sb.AppendLine();
        }
    }
}