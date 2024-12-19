using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayNine
{
    public static class Calculator
    {
        private static readonly char _spaceChar = '￨';
        // Part One Checksum calculation
        public static long CalculateCheckSum(string checkSum)
        {
            var currentId = 0L;
            var currentIndex = 0;
            var sum = 0L;
            var checks = checkSum.ToCharArray();
            while (checks[currentIndex] != '￨')
            {
                currentId = checks[currentIndex];
                var res = currentId * currentIndex;
                sum += res;
                currentIndex++;
            }
            return sum;
        }

        public static long CalculatePartTwoChecksum(string checkSum)
        {
            var currentId = 0L;
            var currentIndex = 0;
            var sum = 0L;
            var checks = checkSum.ToCharArray();
            while (currentIndex < checks.Length - 1)
            {
                if (checks[currentIndex] != _spaceChar)
                {
                    currentId = checks[currentIndex];
                    var res = currentId * currentIndex;
                    sum += res;
                }
                currentIndex++;
            }
            return sum;
        }
    }
}
