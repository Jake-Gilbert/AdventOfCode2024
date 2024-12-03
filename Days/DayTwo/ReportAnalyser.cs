namespace DayTwo
{
    public class ReportAnalyser
    {
        public static int CalculateSafeReports(Dictionary<int, List<int>> reports)
        {
            var safeReports = 0;
            foreach (var report in reports)
            {
                if (AllAreAscending(report.Value) || AllAreDescending(report.Value))
                {
                    var figuresEvaluation = CheckFiguresAreSafe(report.Value);
                    if (figuresEvaluation <= 0)
                    {
                        safeReports += RemoveEachRowAndTryAgain(report.Value);
                    }
                    else
                    {
                        safeReports += figuresEvaluation;
                    }
                }
                else
                {
                    safeReports += RemoveEachRowAndTryAgain(report.Value);
                }
            }                     
            return safeReports;
        }

        private static int RemoveEachRowAndTryAgain(List<int> report)
        {
            var successesAfterRemoval = 0;
            for (int i = 0; i < report.Count; i++)
            {
                var tempReport = new List<int>(report);
                tempReport.RemoveAt(i);
                if (AllAreAscending(tempReport) || AllAreDescending(tempReport))
                {
                    successesAfterRemoval += CheckFiguresAreSafe(tempReport);
                }
            }

            return (successesAfterRemoval >= 1) ? 1 : 0;
        }
        
        private static bool AllAreAscending(List<int> report)
        {
            if (!report.Any())
            {
                return false;
            }
            var currentValue = 0;
            var previousValue = 0;
            for(int i = 0; i < report.Count; i++)
            {
                if (currentValue == 0)
                {
                    previousValue = report[i];
                    currentValue = report[i];
                    continue;
                }
                else
                {
                    previousValue = report[i - 1];
                }
                currentValue = report[i];
                if (currentValue - previousValue <= 0)
                {
                    return false;
                }
               
            }
            return true;
        }

        private static bool AllAreDescending(List<int> report)
        {
            if (!report.Any())
            {
                return false;
            }
            var currentValue = 0;
            var previousValue = 0;
            for (int i = 0; i < report.Count; i++)
            {
                if (currentValue == 0)
                {
                    previousValue = report[i];
                    currentValue = report[i];
                    continue;
                }
                else
                {
                    previousValue = report[i - 1];
                }
                currentValue = report[i];
                if (currentValue - previousValue >= 0)
                {
                    return false;
                }
            }
            return true;
        }
        
        private static int CheckFiguresAreSafe(List<int> report)
        {
            var currentValue = 0;
            var previousValue = 0;
            for (int i = 0; i < report.Count; i++)
            {
                if (currentValue == 0)
                {
                    previousValue = report[i];
                    currentValue = report[i];
                    continue;
                }
                else
                {
                    previousValue = report[i - 1];
                }
                currentValue = report[i];
                var difference = Math.Abs(currentValue - previousValue);
                if (difference < 1 || difference > 3)
                {
                    return 0;
                }
            }
            return 1;
        }
    }
}
