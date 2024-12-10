using DaySeven;

var mapper = new FileMapper("TestData\\ChallengeData.TXT");
var valuesArray = mapper.LeftSide;
var equationsArray = mapper.RightSide;
var operators = new List<string> {"+", "*", "||" };
var solvedLines = 0L;
for (int i = 0; i < valuesArray.Length; i++)
{
    var value = valuesArray[i];
    solvedLines += Cogitator.PossibleToMatchTarget(value, equationsArray[i], operators) ? value : 0;
}

Console.WriteLine($"{solvedLines} can achieve goal by inserting given operators");
