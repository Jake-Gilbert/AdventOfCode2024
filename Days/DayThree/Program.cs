using System;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using static System.Net.Mime.MediaTypeNames;

var file = File.ReadAllText("TestData\\ChallengeData.TXT");
var indexOne = 0;
var indexTwo = 0;
var count = 0;

while (true)
{
    indexTwo = file.IndexOf("don't()", indexOne);
    indexOne = file.IndexOf("mul(", indexOne);

    if (indexOne == -1)
    {
        break;
    }
    else if (indexTwo < indexOne && indexTwo > 0)
    {
        indexOne = file.IndexOf("do()", indexTwo);
        if (indexOne < 1)
        {
            break;
        }
        continue;
    }
    else
    {
        indexOne += 4;
        if (AllNumbers(ref indexOne, out var leftNum))
        {
            if (file[indexOne] == ',')
            {
                indexOne++;
                if (AllNumbers(ref indexOne, out var rightNum))
                {
                    if (file[indexOne] == ')')
                    {
                        count += leftNum * rightNum;
                    }
                }
            }
        }
    }
}
   

Console.WriteLine($"Count - {count}");

bool AllNumbers(ref int idx, out int number)
{
    var result = false;
    var numStr = "";
    while (Char.IsNumber(file[idx]))
    {
        numStr += file[idx];
        idx++;
        result = true;
    }

    number = int.Parse(numStr);
    return result;
}