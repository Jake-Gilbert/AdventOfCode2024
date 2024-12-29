namespace DayTwelve
{
    public class Searcher
    {
        private Dictionary<(int, int), char> _grid =  new Dictionary<(int, int), char>();
        private int _height = 0;
        private int _width = 0;
        private Dictionary<(int, int), char> _visited = new Dictionary<(int, int), char>();

        public Searcher(Dictionary<(int, int), char> farmland, int height, int width)
        {
            _grid = farmland;
            _height = height;
            _width = width;
        }

        public int CalculatePerimiter(HashSet<(int row, int col)> coordinates, char current)
        {
            if (coordinates.Count == 1)
            {
                return 4;
            }

            if (AllOnSameLine(coordinates))
            {
                return coordinates.Count * 4;
            }
            var perimiter = 0;
            var configurations = new List<(int, int)>() { (0, 1), (0, -1), (1, 0), (-1, 0) };
            foreach (var coordinate in coordinates)
            {
                perimiter += Search(coordinate, current, configurations);
            }
            return perimiter;
        } 

        public int CalculateSides(HashSet<(int row, int col)> coordinates, char current)
        {
            if (coordinates.Count == 1 || AllOnSameLine(coordinates))
            {
                return 4;
            }
            var configurations = new List<(int, int)>() { (0, 1), (0, -1), (1, 0), (-1, 0) };
            var sides = PartTwoSearch(coordinates, current);
            return sides;
        }

        private bool AllOnSameLine(HashSet<(int row, int col)> coordinates)
        {
            var first = coordinates.First();
            return !coordinates.Any(x => x.row != first.row);
        }

        private int PartTwoSearch(HashSet<(int row, int col)> coordinates, char target)
        {
            var sides = 0;
            foreach (var coordinate in coordinates)
            {
                sides += AnyCorners(coordinate, target, new List<(int, int)> { (0, -1), (-1, 0) }, (-1, -1)); //northwest
                sides += AnyCorners(coordinate, target, new List<(int, int)> { (0, 1), (-1, 0) }, (-1, 1)); //northeast
                sides += AnyCorners(coordinate, target, new List<(int, int)> { (0, -1), (1, 0) }, (1, -1)); //southwest
                sides += AnyCorners(coordinate, target, new List<(int, int)> { (0, 1), (1, 0) }, (1, 1)); //southeast
            }
            return sides;
        }

        private int Search((int row, int col) coordinate, char target, List<(int row, int col)> configurations)
        {
            var localPerimiter = 0;
            foreach (var config in configurations)
            {
                var currRow = coordinate.row + config.row;
                var currCol = coordinate.col + config.col;
                localPerimiter += Search((currRow, currCol), target);
            }
            return localPerimiter;
        }

        private int AnyCorners ((int row, int col) coordinate, char target, List<(int row, int col)> offsets, (int row, int col) corner)
        {
            var cornersToAdd = new HashSet<(int, int)>();
            var currCorner = (coordinate.row + corner.row, coordinate.col + corner.col);
            var coordinates = offsets.Select(x => (coordinate.row + x.row, coordinate.col + x.col)).ToList();
            var matching = coordinates.Where(x => _grid.ContainsKey(x) && _grid[x] == target).ToHashSet();
            var notMatching = coordinates.Where(x => !_grid.ContainsKey(x) || _grid[x] != target).ToHashSet();
            if (matching.Count == 2 && (!_grid.ContainsKey(currCorner) || _grid[currCorner] != target))
            {
                return 1;
            }
            if (notMatching.Count == 2)
            {
                return 1;
            }
            return 0;
        }

        private int Search((int row, int col) coordinate, char target)
        {
            if (OutOfBounds(coordinate))
            {
                return 1;
            }
            if (_grid[(coordinate.row, coordinate.col)] != target)
            {
                return 1;
            }

            return 0;
        }

        private bool OutOfBounds((int row, int col) x)
        {
            return (x.row < 0 || x.row >= _height || x.col < 0 || x.col >= _width);
        }
    }
}
