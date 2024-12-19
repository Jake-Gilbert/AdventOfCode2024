using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace DayNine
{
    public static class Sorter
    {
        private static readonly char _spaceChar = '￨';
        //PartOne Sort
        private static List<char> processedCharList = new List<char>();
        public static string Sort(List<char> memRep)
        {
            var mem = memRep.ToArray();
            var rightIndex = memRep.Count - 1;
            while (AnyIntAfterDot(mem))
            {
                if (mem[rightIndex] != _spaceChar)
                {
                    var temp = mem[rightIndex];
                    mem[rightIndex] = _spaceChar;
                    var nextSpaceIndex = Array.FindIndex(mem, x => x == _spaceChar);
                    mem[nextSpaceIndex] = temp;
                    rightIndex--;
                }
                else
                {
                    rightIndex--;
                }
            }
            return string.Join("", mem);
        }

        public static string BlockSort(List<char> memRep, Dictionary<int, int> freeBlocks)
        {
            var mem = memRep.ToArray();
            var rightIndex = memRep.Count - 1;

            while (rightIndex > 0)
            {
                var currentChar = memRep[rightIndex];
                if (currentChar != _spaceChar)
                {
                    var count = GetCharCount(currentChar, mem);
                    var matchingSpaceIndex = FindIndexOfFreeSpaceBlock(count, mem, rightIndex);
                    if (matchingSpaceIndex != -1)
                    {
                        InsertBlockCharToFreeSpace(currentChar, matchingSpaceIndex, mem, count);
                        MoveFreeSpace(rightIndex, count, mem);
                    }
                    rightIndex -= count;
                }
                else
                {
                    rightIndex--;
                }

            }
            return string.Join("", mem);
        }

        private static int FindIndexOfFreeSpaceBlock(int count, char[] ce, int rightIndex)
        {
            var matches = Regex.Matches(string.Join("", ce), $"{_spaceChar}+");
            if (matches.Any(m => m.Length >= count && m.Index < rightIndex))
            {
                return matches.First(m => m.Length >= count && m.Index < rightIndex).Index;
            }
            return -1;
        }

        private static void InsertBlockCharToFreeSpace(char c, int index, char[] ce, int length)
        {
            while (length > 0)
            {
                ce[index] = c;
                length--;
                index++;
            }
        }
        private static void MoveFreeSpace(int rightIndex, int  count, char[] ce)
        {
            while (count > 0)
            {
                ce[rightIndex] = _spaceChar;
                count--;
                rightIndex--;
            }
        }

        private static int GetCharCount(char c, char[] arr)
        {
            return Array.FindAll(arr, x => x == c).Length;
        }

        private static bool AnyIntAfterDot(char[] text)
        {
            var firstDotIndex = Array.FindIndex(text, x => x == '￨');
            var rightMostDigitIndex = 0;
            for (int i = text.Length - 1; i > 0;  i--)
            {
                if (text[i] != _spaceChar)
                {
                    rightMostDigitIndex = i;
                    break;
                }
            }
            return rightMostDigitIndex > firstDotIndex;
        }
    }
}
