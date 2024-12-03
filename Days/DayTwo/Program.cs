using DayTwo;

var textMapper = new TextMapper("TestData\\ChallengeData.TXT");
Console.WriteLine($"Safe Reports - { ReportAnalyser.CalculateSafeReports(textMapper.Reports)}"); 
