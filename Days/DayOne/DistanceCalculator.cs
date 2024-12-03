using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOne
{
    public class DistanceCalculator
    {
        public static string CalculateDistance(List<List<int>> lists)
        {
            var leftListOrderByAscending = lists.First().OrderBy(x => x);
            var rightListOrderByAscending = lists.Last().OrderBy(x => x);
            var distanceDifference = 0;
            var similarityScore = 0;
            for(int i = 0; i < leftListOrderByAscending.Count(); i++)
            {
                var leftVal = leftListOrderByAscending.ElementAt(i);
                var rightVal = rightListOrderByAscending.ElementAt(i);
                distanceDifference += Math.Abs(rightVal - leftVal);
                similarityScore += rightListOrderByAscending.Count(x => x == leftVal) * leftVal;
            }
            return $"Distance Score - {distanceDifference}\nSimilarity Score - {similarityScore}";
        }

    }
}
