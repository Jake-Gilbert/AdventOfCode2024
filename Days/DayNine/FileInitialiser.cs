namespace DayNine
{
    public class FileInitialiser
    {
        public Dictionary<int, int> memoryMap { get; } = new Dictionary<int, int>();
        public List<char> memRep = new List<char>();
        public Dictionary<int, int> freeSpaceIndexes = new Dictionary<int, int>();

        public FileInitialiser(string diskMap)
        {
            InitialiseFile(diskMap);
        }

        private void InitialiseFile(string diskMap)
        {
            for (int i = 0; i < diskMap.Length; i++)
            {
                memoryMap.Add(i, int.Parse(diskMap[i].ToString()));
            }
            Draw();
        }

        private void Draw()
        {
            var currentId = 0;
            foreach (var address in memoryMap.Keys)
            {
                if (address % 2 == 1 && address > 0)
                {
                    var s = string.Concat(Enumerable.Repeat('￨', memoryMap[address]));
                    freeSpaceIndexes.Add(memRep.Count, s.Length);
                    memRep.AddRange(s.ToCharArray());
                }
                else
                {
                    var c = Convert.ToChar(currentId);
                    memRep.AddRange(Enumerable.Repeat(c, memoryMap[address]));
                    currentId++;
                }
            }
        }
    }
}
