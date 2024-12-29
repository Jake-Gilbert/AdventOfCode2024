namespace DayTwelve
{
    public class Plotter
    {
        public Dictionary<(int, int), char> _farmLand = new Dictionary<(int, int), char>();
        public int _height = 0;
        public int _width = 0;
        private Dictionary<(int, int), bool> _visited = new Dictionary<(int, int), bool>();
        private Dictionary<char, List<HashSet<(int row, int col)>>> _regions = new Dictionary<char, List<HashSet<(int row, int col)>>>();
        public Plotter(string[] grid)
        {
            InitialiseRegion(grid);
            _height = grid.Length;
            _width = grid.First().Length;
            Console.WriteLine();
        }

        public void InitialiseRegion(string[] grid)
        {
            var farmLand = new Dictionary<(int, int), char>();
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    farmLand.Add((i, j), grid[i][j]);
                    _visited.Add((i, j), false);
                    Console.Write(farmLand[(i, j)]);
                }
                Console.WriteLine();
            }
            _farmLand = farmLand;
        }

        public Dictionary<char, List<HashSet<(int row, int col)>>> CalculateRegions()
        {
            while (AnyAvailableCoordinate(out var nullableIndex))
            {
                if (!nullableIndex.HasValue)
                {
                    break;
                }
                var index = nullableIndex.Value;
                if (index.row > _width)
                {
                    index = (index.row + 1, 0);
                }
                if (!_visited[index])
                {
                    var seed = _farmLand[index];
                    var resList = BreadthFirstSearch(index);
                    if (_regions.ContainsKey(seed))
                    {
                        _regions[seed].Add(resList);
                    }
                    else
                    {
                        _regions.Add(seed, new List<HashSet<(int row, int col)>> { resList });
                    }
                }
            }
            return _regions;
        }

        private bool AnyAvailableCoordinate(out (int row, int col)? coordinate)
        {
            foreach (var coord in _visited)
            {
                if (!coord.Value) 
                {
                    coordinate = coord.Key;
                    return true; 
                }
            }

            coordinate = null;
            return false;
        }

        private HashSet<(int, int)> BreadthFirstSearch((int row, int col) start)
        {
            var queue = new Queue<(int row, int col)>();
            var region = new HashSet<(int row, int col)>();
            var visited = new HashSet<(int row, int col)>();
            region.Add(start);
            _visited[start] = true;
            var seed = _farmLand[start];
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                var plot = queue.Dequeue();
                _visited[plot] = true;
                var neighbours = new List<(int, int)>
                {
                        (plot.row + 1, plot.col), (plot.row - 1, plot.col), (plot.row, plot.col + 1), (plot.row, plot.col - 1)
                };
                foreach (var neighbour in neighbours)
                {
                    if (OutOfBounds(neighbour.Item1, neighbour.Item2))
                    {
                        continue;
                    }
                    if (_visited[neighbour])
                    {
                        continue;
                    }
                    var currSeed = _farmLand[neighbour];
                    if (seed == currSeed)
                    {
                        region.Add(neighbour);
                        queue.Enqueue(neighbour);
                    }
                }
            }
            return region;
        }

        private bool OutOfBounds(int row, int col)
        {
            return (row < 0 || row >= _height || col < 0 || col >= _width);
        }
    }
}
