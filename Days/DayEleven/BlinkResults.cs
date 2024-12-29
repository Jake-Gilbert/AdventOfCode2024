namespace DayEleven
{
    public class BlinkResults
    {
        public long StoneCount { get; set; }
        public Dictionary<long, List<long>> Cached { get; set; } = new Dictionary<long, List<long>>();
    }
}
