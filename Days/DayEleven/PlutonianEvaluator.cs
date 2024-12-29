using System.Text;
using static System.Formats.Asn1.AsnWriter;

namespace DayEleven
{
    public static class PlutonianEvaluator
    {
        private static Dictionary<long, List<long>> _cachedEvaluations = new Dictionary<long, List<long>>();

        public static Dictionary<long, long> Evaluate(Dictionary<long, long> stones, out long count)
        {
            var stoneCount = 0L;
            var dict = new Dictionary<long, long>();
            foreach (var stone in stones.Keys)
            {
                if (!_cachedEvaluations.ContainsKey(stone))
                {
                    ProcessStone(stone);
                }                    
                var cached = _cachedEvaluations[stone];
                var times = stones[stone];
                AddCachedToDict(stone, dict, out var c, times);
                stoneCount += (c * times);               
            }
            count = stoneCount;
            return dict;
        }

        public static BlinkResults Evaluate(Dictionary<long, long> stones, int blinks)
        {
            var blinkCount = 0;
            var stoneCount = 0L;
            var dict = stones;
            while (blinks > 0)
            {
                blinkCount++;
                dict = Evaluate(dict, out var count);
                stoneCount = count;
                blinks--;
            }
            return new BlinkResults
            {
                StoneCount = stoneCount,
                Cached = _cachedEvaluations
            };
        }

        private static void ProcessStone(long stone)
        {
            if (stone == 0)
            {               
                _cachedEvaluations.Add(0L, new List<long> { 1L });
            }
            else if (stone.ToString().Length % 2 == 0)
            {
                var stoneStr = stone.ToString();
                var halfSize = stoneStr.Length / 2;
                var leftNum = long.Parse(stoneStr.Substring(0, halfSize));
                var rightNum = long.Parse(stoneStr.Substring(halfSize));
                _cachedEvaluations.Add(stone, new List<long> { leftNum, rightNum });
            }
            else
            {
                var calc = stone * 2024;
                _cachedEvaluations.Add(stone, new List<long> { calc });
            }
        }

        private static void AddCachedToDict(long stone, Dictionary<long, long> dict, out long count, long times)
        {
            var cached = _cachedEvaluations[stone];
            var stoneCount = 0;
            foreach (var cache in cached)
            {
                if (!dict.ContainsKey(cache))
                {
                    dict.Add(cache, times);
                }
                else
                {
                    dict[cache] += times;
                }
                stoneCount++;
            }
            count = stoneCount;
        }
    }
}
