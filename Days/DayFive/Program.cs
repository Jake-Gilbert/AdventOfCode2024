using DayFive;
using System.Reflection;

var rules = TextMapper.ConstructRules();
var instructionSets = rules.InstructionSets;
var partOneMiddleCount = 0;
var partTwoMiddleCount = 0;
foreach (var rule in rules.Instructions)
{
    if (IsValid(rule, out var middle))
    {
        partOneMiddleCount += middle;
    }
    else
    {
        partTwoMiddleCount += OrganiseAndRetrieveMiddle(rule);
    }
}

Console.WriteLine($"Middle total - {partOneMiddleCount}");
Console.WriteLine($"Corrected Middle total - {partTwoMiddleCount}");

bool IsValid(string instruction, out int middle)
{
    var instructionArray = instruction.Split(",").Select(x => int.Parse(x)).ToArray();
    for (int i = 0; i < instructionArray.Length; i++)
    {
        if (instructionSets.ContainsKey(instructionArray[i]))
        {
           var localList = instructionSets[instructionArray[i]];

           var valuesIndexList = instructionArray.Where(x => localList.Contains(x)).Select(x => Array.IndexOf(instructionArray, x)).ToList();
           var anyRulesViolated = valuesIndexList.Any(x => x > i);
           if (anyRulesViolated)
            {
                middle = -1;
                return false;
            }
        }
    }
    middle = instructionArray[instructionArray.Length / 2];
    return true;
}

int OrganiseAndRetrieveMiddle(string incorrectRule)
{
    var instructionArray = incorrectRule.Split(",").Select(x => int.Parse(x)).ToArray();
    var orderedList = new List<int>();
    var problematicNumbers = RetrieveAllViolators(instructionArray);
    while (problematicNumbers.Count > 0)
    {
        foreach (var problem in problematicNumbers.Keys)
        {
            var arrayIndex = Array.IndexOf(instructionArray, problem);
            var indexList = problematicNumbers[problem].Select(x => Array.IndexOf(instructionArray, x)).ToList();
                 
            instructionArray[arrayIndex] = instructionArray[indexList.Min()];
            instructionArray[indexList.Min()] = problem;               
        }
        problematicNumbers = RetrieveAllViolators(instructionArray);
    }
    return instructionArray[instructionArray.Length / 2];
}

Dictionary<int, List<int>> RetrieveAllViolators(int[] instructionArray)
{
    var problematicNumbers = new Dictionary<int, List<int>>();
    for (int i = 0; i < instructionArray.Length; i++)
    {
        if (instructionSets.ContainsKey(instructionArray[i]))
        {
            var localList = instructionSets[instructionArray[i]];

            var valuesIndexList = instructionArray.Where(x => localList.Contains(x)).Select(x => Array.IndexOf(instructionArray, x)).ToList();
            var ruleViolators = valuesIndexList.Where(x => x > i).Select(x => instructionArray[x]).ToList();
            foreach (var violator in ruleViolators)
            {
                if (problematicNumbers.ContainsKey(violator))
                {
                    var tempList = problematicNumbers[violator];
                    tempList.Add(instructionArray[i]);
                }
                else
                {
                    problematicNumbers.Add(violator, new List<int>() { instructionArray[i] });
                }
            }
        }
    }
    return problematicNumbers;
}
