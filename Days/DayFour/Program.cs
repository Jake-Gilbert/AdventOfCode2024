using System.Text;
using System.Text.RegularExpressions;

var grid = File.ReadAllLines("TestData/ChallengeData.TXT");
var xmasCount = 0;
var crossCount = 0;
var width = grid.First().Length;
var wordToMatch = "AMX";
var PartTwoMatch = "MAS";
var targetIndex = 0;


//PartOne
/*for (int i = 0; i < grid.Length; i++)
{
    targetIndex = grid[i].IndexOf("S");
    while (targetIndex >= 0 && targetIndex < width)
    {
            xmasCount += ConfirmMatch(i, targetIndex, 1, 0, new List<string> { wordToMatch });

            xmasCount += ConfirmMatch(i, targetIndex, -1, 0, new List<string> { wordToMatch });

            xmasCount += ConfirmMatch(i, targetIndex, 0, -1, new List<string> { wordToMatch });

            xmasCount += ConfirmMatch(i, targetIndex, -1, -1, new List<string> { wordToMatch });

            xmasCount += ConfirmMatch(i, targetIndex, 1, -1, new List<string> { wordToMatch });

            xmasCount += ConfirmMatch(i, targetIndex, 0, 1, new List<string> { wordToMatch });

            xmasCount += ConfirmMatch(i, targetIndex, -1, 1, new List<string> { wordToMatch });

            xmasCount += ConfirmMatch(i, targetIndex, 1, 1, new List<string> { wordToMatch });

            targetIndex = grid[i].IndexOf("A", targetIndex + 1);
    }
    Console.WriteLine($"Christmas count - {xmasCount}");

}*/

for (int i = 0; i < grid.Length; i++)
{
    targetIndex = grid[i].IndexOf("A");
    while (targetIndex >= 0 && targetIndex < width)
    {
        if (i - 1 < 0 || i + 1 >= grid.Length)
        {
            break;
        }
        else
        {
            crossCount += ConfirmCross(i, targetIndex) ? 1 : 0;
        }
        targetIndex = grid[i].IndexOf("A", targetIndex + 1);
    }
}

    Console.WriteLine($"Cross count - {crossCount}");

    //PartOne
    /*int ConfirmMatch(int row, int col, int offsetX, int offsetY, List<string> words)
    {
        foreach (var letter in words.First())
        {
            row += offsetY;
            col += offsetX;
            if (row < 0 || row >= grid.Length || col < 0 || col >= grid.Length)
            {
                return 0;
            }

            if (grid[row][col] != letter)
            {
                return 0;
            }
        }
        return 1;
    }*/

    bool ConfirmCross(int row, int col)
    {
        var oppositeCount = 0;
        if (col + 1 >= width || col - 1 < 0)
        {
            return false;
        }
        if (grid[row - 1][col + 1] == 'M')
        {
            oppositeCount += CheckForOpposite(row + 1, col - 1, 'M') ? 1 : 0;
        }
        else if (grid[row - 1][col + 1] == 'S')
        {
            oppositeCount += CheckForOpposite(row + 1, col - 1, 'S') ? 1 : 0;
        }
        if (grid[row - 1][col -1] == 'M')
        {
            oppositeCount += CheckForOpposite(row + 1, col + 1, 'M') ? 1 : 0;
        }
        else if (grid[row - 1][col - 1] == 'S')
        {
            oppositeCount += CheckForOpposite(row + 1, col + 1, 'S') ? 1 : 0;
        }
        return oppositeCount == 2;
    }

    bool CheckForOpposite(int targetRow, int targetCol, char currentChar)
    {
        if (grid[targetRow][targetCol] == 'S' && currentChar == 'M')
        {
            return true;
        }
        if (grid[targetRow][targetCol] == 'M' && currentChar == 'S')
        {
            return true;
        }
        return false;
    }
